using MigApp.Core;
using System.Windows;
using System.Windows.Input;

namespace MigApp.Helpers
{
    class WindowCommands
    {
        public static ICommand CloseCommand => new RelayCommand(param =>
        {
            if (param is Window window)
                window.Close();
        }, _ => true);

        public static ICommand MinimizeCommand => new RelayCommand(param =>
        {
            if (param is Window window)
                window.WindowState = WindowState.Minimized;
        }, _ => true);

        public static ICommand ToggleMaximizeCommand => new RelayCommand(param =>
        {
            if (param is Window window)
            {
                window.WindowState = window.WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;
            }
        }, _ => true);
    }
}
