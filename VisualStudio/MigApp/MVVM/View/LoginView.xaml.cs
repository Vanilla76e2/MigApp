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
    public partial class LoginView : Window
    {
        PostgreSQLClass pgsql = PostgreSQLClass.getinstance();

        public LoginView()
        {
            InitializeComponent();
            FillUserInfo();
        }

        public LoginView(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            DataContext = new LoginViewModel(serviceProvider);
            FillUserInfo();
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as LoginViewModel;
            if (viewModel != null)
            {
                await viewModel.InitializeAsync();
            }
            else
            {
                Console.WriteLine("LoginView: viewModel = null");
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                var viewModel = DataContext as LoginViewModel;
                if (viewModel != null)
                {
                    viewModel.UserPassword = passwordBox.Password;
                }
            }
        }

        private void FillUserInfo()
        {
            if(MigApp.Properties.Settings.Default.userRemember)
            loginBox.Text = MigApp.Properties.Settings.Default.userLogin;
            int i = MigApp.Properties.Settings.Default.userPasswordL;
            for (int j = 0; j < i; j++)
            {
                passwordBox.Password += "1";
            }
        }
    }
}
