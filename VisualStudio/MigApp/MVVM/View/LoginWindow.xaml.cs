using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MigApp.Core.Services.AppUpdate;
using MigApp.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace MigApp.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow(LoginWindowModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            this.Closed += (s, e) => { if (DataContext is LoginWindowModel vm) vm.DisposePasswords(); };
            SettignsGrid.IsVisibleChanged += (s, e) => { SwitchDefaultButton(); };
        }

        // Закрыть приложение
        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SwitchDefaultButton()
        {
            if (SettignsGrid.Visibility == Visibility.Visible)
            {
                LoginButton.IsDefault = false;
                CommitSettingsButton.IsDefault = true;
            }
            else
            {
                LoginButton.IsDefault = true;
                CommitSettingsButton.IsDefault = false;
            }
        }
    }
}
