using MigApp.UI.MVVM.ViewModel;
using System.Windows;

namespace MigApp.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindowModel ViewModel { get; } 

        public LoginWindow(LoginWindowModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = ViewModel;
            this.Loaded += async (s, e) => await ViewModel.InitializeAsync();
            this.Closed += (s, e) => { if (DataContext is LoginWindowModel vm) vm.DisposePasswords(); };
            SettingsGrid.IsVisibleChanged += (s, e) => { SwitchDefaultButton(); };
        }

        private void SwitchDefaultButton()
        {
            if (SettingsGrid.Visibility == Visibility.Visible)
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
