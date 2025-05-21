
using Microsoft.Extensions.DependencyInjection;
using MigApp.UI.Services.Navigation;
using System.Windows;
using System.Windows.Input;

namespace MigApp.MVVM.View
{
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            MaxWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
        }

        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            MaxWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            DataContext = new MainWindowModel(serviceProvider, serviceProvider.GetRequiredService<INavigationService>());
        }

        private void CustomUI_CloseMenu(object sender, MouseButtonEventArgs e)
        {

        }

        //#region CastomUI

        //// Close navigation menu
        //private void CustomUI_CloseMenu(object sender, MouseButtonEventArgs e)
        //{
        //    AdminButton.IsChecked = false;
        //    MenuButton.IsChecked = false;
        //    ArchiveButton.IsChecked = false;
        //    e.Handled = true;
        //}

        //// Header control buttons
        //private void CustomUI_WindowControl(object sender, RoutedEventArgs e)
        //{
        //    if (sender.Equals(Custom_MinimizeButton))
        //        this.WindowState = WindowState.Minimized;

        //    else if (sender.Equals(Custom_MaximizeButton))
        //        if (this.WindowState == WindowState.Maximized)
        //            this.WindowState = WindowState.Normal;
        //        else
        //            this.WindowState = WindowState.Maximized;

        //    else if (sender.Equals(Custom_CloseButton))
        //        Application.Current.Shutdown();
        //}

        //// Switch navigation menus
        //private void CustomUI_MenuSwitch(object sender, RoutedEventArgs e)
        //{
        //    if (sender.Equals(MenuButton))
        //    {
        //        AdminButton.IsChecked = false;
        //        ArchiveButton.IsChecked = false;
        //        ArchiveMenu.Visibility = Visibility.Collapsed;
        //        AdminMenu.Visibility = Visibility.Collapsed;
        //        MainMenu.Visibility = Visibility.Visible;
        //    }
        //    else if (sender.Equals(AdminButton))
        //    {
        //        MenuButton.IsChecked = false;
        //        ArchiveButton.IsChecked = false;
        //        MainMenu.Visibility = Visibility.Collapsed;
        //        ArchiveMenu.Visibility = Visibility.Collapsed;
        //        AdminMenu.Visibility = Visibility.Visible;
        //    }
        //    else if (sender.Equals(ArchiveButton))
        //    {
        //        MenuButton.IsChecked = false;
        //        AdminButton.IsChecked = false;
        //        MainMenu.Visibility = Visibility.Collapsed;
        //        AdminMenu.Visibility = Visibility.Collapsed;
        //        ArchiveMenu.Visibility = Visibility.Visible;
        //    }
        //}
        //#endregion
    }
}