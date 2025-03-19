using Microsoft.Extensions.DependencyInjection;
using MigApp.MVVM.ViewModel;
using MigApp.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MigApp.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        public LoginWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            DataContext = new LoginWindowModel(serviceProvider);
            FillConnectionParametrs();
        }

        // Закрыть приложение
        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // При загрузке окна
        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as LoginWindowModel;
            if (viewModel != null)
            {
                await viewModel.InitializeAsync();
                await viewModel.OnLoginRemembered();
            }
            else
            {
                Debug.WriteLine("LoginView: viewModel = null");
            }
        }

        // Отслеживание изменения пароля пользователя
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                var viewModel = DataContext as LoginWindowModel;
                if (viewModel != null)
                {
                    viewModel.UserPassword = passwordBox.Password;
                }
            }
        }

        // Отслеживание изменения пароля БД
        private void DataBase_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                var viewModel = DataContext as LoginWindowModel;
                if (viewModel != null)
                {
                    viewModel.DBPassword = passwordBox.Password;
                }
            }
        }

        // Заполенение параметров подключения
        private void FillConnectionParametrs()
        {
            DBPassword_Passwordbox.Password = MigApp.Properties.Settings.Default.pgPassword;
        }
    }
}
