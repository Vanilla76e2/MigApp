using Microsoft.Extensions.DependencyInjection;
using MigApp.Core;
using MigApp.MVVM.View;
using MigApp.Services;
using MigApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using System.Windows.Navigation;
using System.Security;

namespace MigApp.MVVM.ViewModel
{
    internal class LoginViewModel : Core.ViewModel
    {
        PostgreSQLClass pgsql = PostgreSQLClass.getinstance();
        MiscClass mc = new MiscClass();
        Encrypter enc = new Encrypter();
        private readonly IServiceProvider _serviceProvider;
        private readonly INavigationService _navigationService;


        #region Переменные
        private bool isSettingsOn { get; set; } = false;
        public bool IsSettingsOn
        {
            get => isSettingsOn;
            set
            {
                isSettingsOn = value;
                OnPropertyChanged();
            }
        }
        public bool IsSettingsOff
        {
            get
            {
                return !isSettingsOn;
            }
        }

        // Авторизация
        private bool isConnectionCorrect { get; set; }
        public bool IsConnectionCorrect
        {
            get => isConnectionCorrect;
            set
            {
                isConnectionCorrect = value;
                OnPropertyChanged();
            }
        }

        private bool isLoading { get; set; } = true;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged();
            }
        }

        private string userLogin { get; set; } = string.Empty;
        public string UserLogin
        {
            get => userLogin;
            set
            {
                userLogin = value;
            }
        }
        private string userPassword { get; set; } = string.Empty;
        public string UserPassword
        {
            get => userPassword;
            set
            {
                userPassword = enc.HashPassword(value.ToString());
            }
        }
        public bool IsPasswordRemembered { get; set; }

        // Настройки
        private string dbServer { get; set; }
        public string DBServer
        {
            get => dbServer;
            set
            {
                dbServer = value;
                OnPropertyChanged();
            }
        }

        private string dbPort { get; set; }
        public string DBPort
        {
            get => dbPort;
            set
            {
                dbPort = value;
                OnPropertyChanged();
            }
        }

        private string dbName { get; set; }
        public string DBName
        {
            get => dbName;
            set
            {
                dbName = value;
                OnPropertyChanged();
            }
        }

        private string dbUser { get; set; }
        public string DBUser
        {
            get => dbUser;
            set
            {
                dbUser = value;
                OnPropertyChanged();
            }
        }

        private string dbPassword { get; set; }
        public string DBPassword
        {
            get => dbPassword;
            set
            {
                dbPassword = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Команды

        public RelayCommand LoginCommand { get; set; }
        public RelayCommand SettingsCommitCommand { get; set; }

        #endregion

        public LoginViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _navigationService = serviceProvider.GetRequiredService<INavigationService>();

            #region Данные пользователя
            IsPasswordRemembered = MigApp.Properties.Settings.Default.userRemember;
            userLogin = MigApp.Properties.Settings.Default.userLogin;
            userPassword = MigApp.Properties.Settings.Default.userPassword;
            #endregion

            #region Данные подключения
            DBServer = MigApp.Properties.Settings.Default.pgServer;
            DBPort = MigApp.Properties.Settings.Default.pgPort;
            DBName = MigApp.Properties.Settings.Default.pgDatabase;
            DBUser = MigApp.Properties.Settings.Default.pgUser;
            #endregion

            LoginCommand = new RelayCommand(async o => await ExecuteLoginCommand(), o => true);
            SettingsCommitCommand = new RelayCommand(async o => await OnSettingsCommit(), o => true);

        }

        // Проверка подключения
        public async Task InitializeAsync()
        {
            IsLoading = true;
            try
            {
                bool result = await pgsql.ConnectionTest();
                IsConnectionCorrect = result;
                if (!result)
                {
                    MessageBox.Show("Подключение к базе данных отсутствует.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    IsSettingsOn = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LoginViewModel: Ошибка при InitializeAsync: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        // Команда на авторизацию в систему
        public async Task ExecuteLoginCommand()
        {
            #if DEBUG
            _navigationService.NavigateToMainWindow();
            var debug = Application.Current.Windows.OfType<LoginView>().FirstOrDefault();
            if (debug != null) { debug.Close(); }
            return;
            #endif

            IsLoading = true;
            try
            {
                bool isValid = await pgsql.CheckLogin(userLogin, userPassword);
                if (isValid)
                {
                    if (IsPasswordRemembered)
                    {
                        MigApp.Properties.Settings.Default.userRemember = true;
                        MigApp.Properties.Settings.Default.userLogin = userLogin;
                        MigApp.Properties.Settings.Default.userPassword = userPassword;
                    }
                    MigApp.Properties.Settings.Default.Save();

                    // Открыть MainView
                    _navigationService.NavigateToMainWindow();

                    // Закрыть LoginView
                    var loginWindow = Application.Current.Windows.OfType<LoginView>().FirstOrDefault();
                    if (loginWindow != null) { loginWindow.Close(); }
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ExecuteLoginCommand: {ex}");
                MessageBox.Show("Произошла непредвиденная ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        // Вход по памяти
        public async Task OnLoginRemembered()
        {
            if (IsConnectionCorrect)
            {
                IsLoading = true;
                if (IsPasswordRemembered)
                {
                    bool isValid = await pgsql.CheckLogin(userLogin, userPassword);
                    if (isValid)
                    {
                        _navigationService.NavigateToMainWindow();
                        var loginWindow = Application.Current.Windows.OfType<LoginView>().FirstOrDefault();
                        if (loginWindow != null) { loginWindow.Close(); }
                    }
                    else
                    {
                        IsLoading = false;
                        MessageBox.Show("Неверный логин или пароль.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    IsLoading = false;
                }
            }
        }

        // Изменение параметров подключения
        public async Task OnSettingsCommit()
        {
            IsLoading = true;
            try
            {
                SaveConnectionParametrs();
                bool result = await pgsql.ConnectionTest();
                IsConnectionCorrect = result;
                if (result)
                {
                    MessageBox.Show("Подключение к базе данных установлено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    IsSettingsOn = false;
                }
                else
                {
                    MessageBox.Show("Подключение к базе данных отсутствует.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"OnSettingsCommit: {ex}");
                MessageBox.Show("Произошла непредвиденная ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        // Сохранение параметров подключения
        private void SaveConnectionParametrs()
        {
            MigApp.Properties.Settings.Default.pgServer = DBServer;
            MigApp.Properties.Settings.Default.pgPort = DBPort;
            MigApp.Properties.Settings.Default.pgDatabase = DBName;
            MigApp.Properties.Settings.Default.pgUser = DBUser;
            MigApp.Properties.Settings.Default.pgPassword = DBPassword;
            MigApp.Properties.Settings.Default.Save();
        }
    }
}
