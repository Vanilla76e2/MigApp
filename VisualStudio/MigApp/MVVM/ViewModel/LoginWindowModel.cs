using Microsoft.Extensions.DependencyInjection;
using MigApp.MVVM.View;
using MigApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using System.Windows.Navigation;
using System.Security;
using MigApp.Core;
using MigApp.Interfaces;

namespace MigApp.MVVM.ViewModel
{
    /// <summary>
    /// ViewModel для окна авторизации. Обрабатывает логику авторизации пользователя, управления настройками подключения к базе данных и навигации между представлениями.
    /// </summary>
    internal class LoginWindowModel : Core.ViewModel
    {
        private readonly IAppLogger _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly INavigationService _navigationService;
        private readonly IVersionService _versionService;
        private readonly IAuthenticationService _authenticationService;
        ApplicationContext appContext = ApplicationContext.GetInstance();


        #region Переменные
        private bool isSettingsOn { get; set; } = false;  // Параметр хранящий статус видимости настроек
        public bool IsSettingsOn
        {
            get => isSettingsOn;
            set
            {
                isSettingsOn = value;
                OnPropertyChanged();
            }
        }

        // Авторизация
        private bool isConnectionCorrect { get; set; } = false; // Параметр хранящий статус подключения к БД
        public bool IsConnectionCorrect
        {
            get => isConnectionCorrect;
            set
            {
                isConnectionCorrect = value;
                OnPropertyChanged();
            }
        }

        private bool isLoading { get; set; } = true; // Параметр хранящий статус отображения загрзуки
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged();
            }
        }

        private string userLogin { get; set; } = string.Empty; // Параметр хранящий логин пользователя
        public string UserLogin
        {
            get => userLogin;
            set
            {
                userLogin = value;
            }
        }

        private string userPassword { get; set; } = string.Empty; // Параметр хранящий пароль пользователя
        public string UserPassword
        {
            get => userPassword;
            set
            {
                userPassword = value;
            }
        }

        public bool IsPasswordRemembered { get; set; } = false; // Параметр хранящий статус сохранения данных входа

        // Настройки подключения
        private string dbServer { get; set; } = string.Empty; // Параметр хранящий сервер для подключения к БД
        public string DBServer
        {
            get => dbServer;
            set
            {
                dbServer = value;
                OnPropertyChanged();
            }
        }

        private string dbPort { get; set; } = string.Empty; // Параметр хранящий порт для подключения к БД
        public string DBPort
        {
            get => dbPort;
            set
            {
                dbPort = value;
                OnPropertyChanged();
            }
        } 

        private string dbName { get; set; } = string.Empty; // Параметр хранящий имя БД
        public string DBName
        {
            get => dbName;
            set
            {
                dbName = value;
                OnPropertyChanged();
            }
        }

        private string dbUser { get; set; } = string.Empty; // Параметр хранящий имя пользователя от БД
        public string DBUser
        {
            get => dbUser;
            set
            {
                dbUser = value;
                OnPropertyChanged();
            }
        }

        private string dbPassword { get; set; } = string.Empty; // Параметр хранящий пароль от БД
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

        public RelayCommand LoginCommand { get; set; } // Объявление команды на авторизацию
        public RelayCommand SettingsCommitCommand { get; set; } // Объявление команды на сохранение настроек

        #endregion

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LoginWindowModel"/>.
        /// </summary>
        /// <param name="authenticationService">Сервис аутентификации.</param>
        /// <param name="logger">Сервис логирования.</param>
        /// <param name="versionService">Сервис проверки версии приложения.</param>
        public LoginWindowModel(IAppLogger logger, IServiceProvider serviceProvider, IVersionService versionService, INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _versionService = versionService ?? throw new ArgumentNullException(nameof(versionService));

            GetUserParametrs();

            GetDatabaseParametrs();

            LoginCommand = new RelayCommand(async o => await ExecuteLoginCommand(), o => true); // Установка команды авторизации
            SettingsCommitCommand = new RelayCommand(async o => await OnSettingsCommit(), o => true); // Установка команды сохранения настроек

            Task.Run(async () => await InitializeAsync());
        }

        /// <summary>
        /// Асинхронно инициализирует ViewModel, проверяет подключение к базе данных, наличие интернета и новой версии приложения.
        /// </summary>
        public async Task InitializeAsync()
        {
            IsLoading = true;

            await CheckDatabaseConnectionAsync();


            IsLoading = false;
        }

        private void GetUserParametrs()
        {
            IsPasswordRemembered = Properties.Settings.Default.userRemember;
            userLogin = Properties.Settings.Default.userLogin;
            userPassword = Properties.Settings.Default.userPassword;
        }

        /// <summary>
        /// Устанавливает значения параметров подключения к базе данных из настроек приложения.
        /// </summary>
        private void GetDatabaseParametrs()
        {
            DBServer = Properties.Settings.Default.pgServer ?? string.Empty;
            DBPort = Properties.Settings.Default.pgPort ?? string.Empty;
            DBName = Properties.Settings.Default.pgDatabase ?? string.Empty;
            DBUser = Properties.Settings.Default.pgUser ?? string.Empty;
            DBPassword = Properties.Settings.Default.pgPassword ?? string.Empty;
        }

        private async Task CheckInternetAndVersionAsync()
        {
            try
            {
                
            }
            catch
            {

            }
            throw new NotImplementedException();
        }

        private async Task CheckDatabaseConnectionAsync()
        {
           
            try
            {
                IsConnectionCorrect = await _authenticationService.AuthenticateAsync();
                if (!IsConnectionCorrect)
                {
                    _logger.LogWarning("Подключение к базе данных отсутствует.");
                    MessageBox.Show("Подключение к базе данных отсутствует.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    IsSettingsOn = true;
                }
                else
                {
                    _logger.LogInformation("Подключение к базе данных установлено.");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при подключении к базе данных.");
                MessageBox.Show("Произошла ошибка при подключении к базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Команда на авторизацию в систему
        public async Task ExecuteLoginCommand()
        {
#if DEBUG
            await _navigationService.NavigateToMainWindow();
            var debug = Application.Current.Windows.OfType<LoginWindow>().FirstOrDefault();
            if (debug != null) { debug.Close(); }
            return;
#endif

            IsLoading = true; // Включение отображения загрузки
            //try
            //{
            //    bool isValid = await pgsql.CheckLogin(userLogin, userPassword); // Проверка данных пользователя
            //    if (isValid)
            //    {
            //        if (IsPasswordRemembered) // Если установлен параметр запоминания данных, то данные сохраняются
            //        {
            //            MigApp.Properties.Settings.Default.userRemember = true;
            //            MigApp.Properties.Settings.Default.userLogin = userLogin;
            //            MigApp.Properties.Settings.Default.userPassword = userPassword;
            //            MigApp.Properties.Settings.Default.Save();
            //        }

            //        appContext.SetCurrentUser(userLogin); // Отправить данные пользователя в ApplicationContext

            //        await _navigationService.NavigateToMainWindow(); // Открыть MainView при помощи сервиса навигации

            //        var loginWindow = Application.Current.Windows.OfType<LoginWindow>().FirstOrDefault(); // Получить LoginWindow как экземпляр объекта
            //        if (loginWindow != null) { loginWindow.Close(); } // Закрыть LoginWindow
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine($"ExecuteLoginCommand: {ex}");
            //    MessageBox.Show("Произошла непредвиденная ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            //finally
            //{
            //    IsLoading = false; // Отключение отображения загрузки
            //}
        }

        // Вход по памяти
        public async Task OnLoginRemembered()
        {
            //if (IsConnectionCorrect) // Проверка статуса соединения
            //{
            //    IsLoading = true; // Включить отображение загрузки
            //    try
            //    {
            //        if (IsPasswordRemembered) // Проверка параметра запоминания данных
            //        {
            //            bool isValid = await pgsql.CheckRememberedLogin(userLogin, userPassword); // Проверка данных пользователя
            //            if (isValid) // Если данные корректны, то открывается главное окно
            //            {
            //                await _navigationService.NavigateToMainWindow(); // Открыть MainView при помощи сервиса навигации
            //                var loginWindow = Application.Current.Windows.OfType<LoginWindow>().FirstOrDefault(); // Получить LoginWindow как экземпляр объекта
            //                if (loginWindow != null) { loginWindow.Close(); } // Закрыть LoginWindow 
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Debug.WriteLine($"OnLoginRemembered: {ex}");
            //        MessageBox.Show("Произошла непредвиденная ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //    finally
            //    {
            //        IsLoading = false; //Отключение отображения загрузки  
            //    }
            //}
        }

        // Изменение параметров подключения
        public async Task OnSettingsCommit()
        {
            //IsLoading = true; // Включение отображения загрузки
            //try
            //{
            //    SaveConnectionParametrs(); // Сохранение параметров подключения к БД
            //    bool result = await pgsql.ConnectionTest(); // Проверка подключения к БД
            //    IsConnectionCorrect = result; // Установка статуса подключения к БД
            //    if (result)
            //    {
            //        MessageBox.Show("Подключение к базе данных установлено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            //        IsSettingsOn = false; // Отключить отображение настроек
            //    }
            //    else
            //    {
            //        MessageBox.Show("Подключение к базе данных отсутствует.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine($"OnSettingsCommit: {ex}");
            //    MessageBox.Show("Произошла непредвиденная ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            //finally
            //{
            //    IsLoading = false; // Отключить отображение загрузки
            //}
        }

        // Сохранение параметров подключения
        private void SaveConnectionParametrs()
        {
            Properties.Settings.Default.pgServer = DBServer;
            Properties.Settings.Default.pgPort = DBPort;
            Properties.Settings.Default.pgDatabase = DBName;
            Properties.Settings.Default.pgUser = DBUser;
            Properties.Settings.Default.pgPassword = DBPassword;
            Properties.Settings.Default.Save();
        }
    }
}
