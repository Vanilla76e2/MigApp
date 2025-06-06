using MigApp.Core.Session;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MigApp.UI.Controls
{
    public partial class MainNavigationPanel : UserControl
    {
        public static readonly DependencyProperty PanelCornerRadiusProperty = DependencyProperty.Register(
            nameof(PanelCornerRadius), typeof(CornerRadius), typeof(MainNavigationPanel), new PropertyMetadata());

        public static readonly DependencyProperty PanelBackgroundProperty = DependencyProperty.Register(
            nameof(PanelBackground), typeof(Brush), typeof(MainNavigationPanel), new PropertyMetadata());

        public static readonly DependencyProperty PanelWidthProperty = DependencyProperty.Register(
            nameof(PanelWidth), typeof(double), typeof(MainNavigationPanel), new PropertyMetadata(50.0));

        public static readonly DependencyProperty NavigationButtonStyleProperty = DependencyProperty.Register(
            nameof(NavigationButtonStyle), typeof(Style), typeof(MainNavigationPanel), new PropertyMetadata(null));

        public static readonly DependencyProperty ButtonStyleProperty = DependencyProperty.Register(
            nameof(ButtonStyle), typeof(Style), typeof(MainNavigationPanel), new PropertyMetadata(null));

        public static readonly DependencyProperty IconsSizeProperty = DependencyProperty.Register(
            nameof(IconsSize), typeof(double), typeof(MainNavigationPanel), new PropertyMetadata(24.0));

        public static readonly DependencyProperty UserSessionProperty = DependencyProperty.Register(
            nameof(UserSession), typeof(IUserSession), typeof(MainNavigationPanel), new PropertyMetadata(null));

        public static readonly DependencyProperty OpenMenuCommandProperty = DependencyProperty.Register(
            nameof(OpenMenuCommand), typeof(ICommand), typeof(MainNavigationPanel));

        public static readonly DependencyProperty OpenAdminMenuCommandProperty = DependencyProperty.Register(
            nameof(OpenAdminMenuCommand), typeof(ICommand), typeof(MainNavigationPanel));

        public static readonly DependencyProperty OpenArchiveCommandProperty = DependencyProperty.Register(
            nameof(OpenArchiveCommand), typeof(ICommand), typeof(MainNavigationPanel));

        public static readonly DependencyProperty OpenSettingsCommandProperty = DependencyProperty.Register(
            nameof(OpenSettingsCommand), typeof(ICommand), typeof(MainNavigationPanel));

        public static readonly DependencyProperty LogoutCommandProperty = DependencyProperty.Register(
            nameof(LogoutCommand), typeof(ICommand), typeof(MainNavigationPanel));

        public static readonly DependencyProperty IsMenuVisibleProperty = DependencyProperty.Register(
            nameof(IsMenuVisible), typeof(bool), typeof(MainNavigationPanel), new PropertyMetadata(true));

        public static readonly DependencyProperty IsAdminMenuVisibleProperty = DependencyProperty.Register(
            nameof(IsAdminMenuVisible), typeof(bool), typeof(MainNavigationPanel), new PropertyMetadata(true));

        public static readonly DependencyProperty IsArchiveVisibleProperty = DependencyProperty.Register(
            nameof(IsArchiveVisible), typeof(bool), typeof(MainNavigationPanel), new PropertyMetadata(true));
        
        public static readonly DependencyProperty MenuCommandParameterProperty = DependencyProperty.Register(
            nameof(MenuCommandParameter), typeof(object), typeof(MainNavigationPanel), new PropertyMetadata(null));

        public static readonly DependencyProperty AdminmenuCommandParameterProperty = DependencyProperty.Register(
            nameof(AdminmenuCommandParameter), typeof(object), typeof(MainNavigationPanel), new PropertyMetadata(null));

        public static readonly DependencyProperty ArchiveCommandParameterProperty = DependencyProperty.Register(
            nameof(ArchiveCommandParameter), typeof(object), typeof(MainNavigationPanel), new PropertyMetadata(null));

        public object MenuCommandParameter
        {
            get => GetValue(MenuCommandParameterProperty);
            set => SetValue(MenuCommandParameterProperty, value);
        }

        public object AdminmenuCommandParameter
        {
            get => GetValue(AdminmenuCommandParameterProperty);
            set => SetValue(AdminmenuCommandParameterProperty, value);
        }

        public object ArchiveCommandParameter
        {
            get => GetValue(ArchiveCommandParameterProperty);
            set => SetValue(ArchiveCommandParameterProperty, value);
        }

        public CornerRadius PanelCornerRadius
        {
            get => (CornerRadius)GetValue(PanelCornerRadiusProperty);
            set => SetValue(PanelCornerRadiusProperty, value);
        }

        public Brush PanelBackground
        {
            get => (Brush)GetValue(PanelBackgroundProperty);
            set => SetValue(PanelBackgroundProperty, value);
        }

        public double PanelWidth
        {
            get => (double)GetValue(PanelWidthProperty);
            set => SetValue(PanelWidthProperty, value);
        }

        public Style NavigationButtonStyle
        {
            get => (Style)GetValue(NavigationButtonStyleProperty);
            set => SetValue(NavigationButtonStyleProperty, value);
        }

        public Style ButtonStyle
        {
            get => (Style)GetValue(ButtonStyleProperty);
            set => SetValue(ButtonStyleProperty, value);
        }

        public double IconsSize
        {
            get => (double)GetValue(IconsSizeProperty);
            set => SetValue(IconsSizeProperty, value);
        }

        public IUserSession UserSession
        {
            get => (IUserSession)GetValue(UserSessionProperty);
            set => SetValue(UserSessionProperty, value);
        }

        public ICommand OpenMenuCommand
        {
            get => (ICommand)GetValue(OpenMenuCommandProperty);
            set => SetValue(OpenMenuCommandProperty, value);
        }

        public ICommand OpenAdminMenuCommand
        {
            get => (ICommand)GetValue(OpenAdminMenuCommandProperty);
            set => SetValue(OpenAdminMenuCommandProperty, value);
        }

        public ICommand OpenArchiveCommand
        {
            get => (ICommand)GetValue(OpenArchiveCommandProperty);
            set => SetValue(OpenArchiveCommandProperty, value);
        }

        public ICommand OpenSettingsCommand
        {
            get => (ICommand)GetValue(OpenSettingsCommandProperty);
            set => SetValue(OpenSettingsCommandProperty, value);
        }

        public ICommand LogoutCommand
        {
            get => (ICommand)GetValue(LogoutCommandProperty);
            set => SetValue(LogoutCommandProperty, value);
        }

        public bool IsMenuVisible
        {
            get => (bool)GetValue(IsMenuVisibleProperty);
            set => SetValue(IsMenuVisibleProperty, value);
        }

        public bool IsAdminMenuVisible
        {
            get => (bool)GetValue(IsAdminMenuVisibleProperty);
            set => SetValue(IsAdminMenuVisibleProperty, value);
        }

        public bool IsArchiveVisible
        {
            get => (bool)GetValue(IsArchiveVisibleProperty);
            set => SetValue(IsArchiveVisibleProperty, value);
        }

        public MainNavigationPanel()
        {
            InitializeComponent();
            Loaded += MainNavigationPanel_Loaded;
        }

        private void MainNavigationPanel_Loaded(object sender, RoutedEventArgs e)
        {
            var topStackPanel = (StackPanel)FindName("TopStackPanel");
            if (topStackPanel != null)
            {
                foreach (var child in topStackPanel.Children)
                {
                    if (child is ToggleButton toggleButton)
                    {
                        toggleButton.Checked += ToggleButton_Checked;
                    }
                }
            }
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            var selectedButton = sender as ToggleButton;
            var topStackPanel = (StackPanel)FindName("TopStackPanel");

            if (topStackPanel != null)
            {
                foreach (var child in topStackPanel.Children)
                {
                    if (child is ToggleButton toggleButton && toggleButton != selectedButton)
                    {
                        toggleButton.IsChecked = false;
                    }
                }
            }
        }
    }
}