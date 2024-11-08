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

        public bool IsConnectionCorrect { get; set; }
        private bool isLoading { get; set; }
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

        #endregion

        #region Команды

        public RelayCommand LoginCommand { get; set; }

        #endregion

        public LoginViewModel(IServiceProvider serviceProvider)
        {
            if (IsPasswordRemembered)
            {
                userLogin = MigApp.Properties.Settings.Default.userLogin;
                userPassword = MigApp.Properties.Settings.Default.userPassword;
            }

            LoginCommand = new RelayCommand(async o => await ExecuteLoginCommand(), o => true);
            _serviceProvider = serviceProvider;
            _navigationService = serviceProvider.GetRequiredService<INavigationService>();
        }

        // Проверка подключения
        public async Task InitializeAsync()
        {
            IsLoading = true;
            try
            {
                bool result = await pgsql.ConnectionTest();
                IsConnectionCorrect = result;
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
            //IsLoading = true;
            //try
            //{
            //    bool isValid = await pgsql.CheckLogin(userLogin, userPassword);
            //    if (true)
            //    {
            //        // Открыть MainView
            //        _navigationService.NavigateToMainWindow();

            //        // Закрыть LoginView
            //        var loginWindow = Application.Current.Windows.OfType<LoginView>().FirstOrDefault();
            //        if (loginWindow != null) { loginWindow.Close(); }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Неверный логин или пароль", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"ExecuteLoginCommand: {ex}");
            //    MessageBox.Show("Произошла непредвиденная ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            //finally
            //{
            //    IsLoading = false;
            //}
            MessageBox.Show($"Login: {userLogin}\nPassword: {userPassword}");
        }
    }
}
