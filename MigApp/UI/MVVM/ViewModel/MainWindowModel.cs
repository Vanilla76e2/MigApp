using MigApp.Core.Enums;
using MigApp.Core.Session;
using MigApp.MVVM.View;
using MigApp.UI.Base;
using MigApp.UI.MVVM.ViewModel.Pages;
using MigApp.UI.Services.Navigation;
using System.Windows.Input;
using Windows.Graphics.Printing;

namespace MigApp.UI.MVVM.ViewModel
{
    public class MainWindowModel : Base.ViewModel
    {
        public string WindowTitle { get; set; } = $"MigApp v{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version!.ToString(3)}";

        private string _subtitleText { get; set; } = "DebugUser";
        public string SubtitleText
        {
            get => _subtitleText;
            set
            {
                _subtitleText = value;
                OnPropertyChanged();
            }
        }

        private bool _isNavigationOpen { get; set; }
        public bool IsNavigationOpen
        {
            get => _isNavigationOpen;
            set
            {
                _isNavigationOpen = value;
                OnPropertyChanged();
            }
        }

        private NavigationPanelType _currentPanel = NavigationPanelType.None;
        public NavigationPanelType CurrentPanel
        {
            get => _currentPanel;
            set
            {
                if (_currentPanel != value)
                {
                    _currentPanel = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsMenuOpen));
                    OnPropertyChanged(nameof(IsAdminMenuOpen));
                    OnPropertyChanged(nameof(IsArchiveOpen));
                }
            }
        }
        public bool IsMenuOpen => CurrentPanel == NavigationPanelType.Menu;
        public bool IsAdminMenuOpen => CurrentPanel == NavigationPanelType.AdminMenu;
        public bool IsArchiveOpen => CurrentPanel == NavigationPanelType.Archive;


        private readonly IUserSession _session;
        private INavigationService _navigation;

        #region Команды

        public ICommand LogOutCommand => new RelayCommand(o => LogOut(), o => true);
        public ICommand SetNavigationPanelCommand => new RelayCommand<NavigationPanelType>(SetNavigationPanel);
        #endregion

        public MainWindowModel(IUserSession session, INavigationService navigation)
        {
            _navigation = navigation;
            _session = session;
        }

        public void LogOut()
        {
            _navigation.NavigateToLoginWindow();

            var mainWindow = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null) { mainWindow.Close(); }
        }

        private void SetNavigationPanel(NavigationPanelType panelType)
        {
            if (panelType == CurrentPanel)
            {
                CurrentPanel = NavigationPanelType.None;
                IsNavigationOpen = false;
            }
            else
            {
                CurrentPanel = panelType;
                IsNavigationOpen = true;
            }
        }

    }
}
