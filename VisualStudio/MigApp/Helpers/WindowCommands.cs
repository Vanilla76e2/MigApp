using MigApp.Core;
using System.Windows;
using System.Windows.Input;

namespace MigApp.Helpers
{
    static class WindowCommands
    {
        public static ICommand CloseCommand => CreateCommand(window => window.Close());

        public static ICommand MinimizeCommand => CreateCommand(window =>
            window.WindowState = WindowState.Minimized);

        public static ICommand ToggleMaximizeCommand => CreateCommand(window =>
            window.WindowState = window.WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized);

        private static ICommand CreateCommand(Action<Window> action) =>
            new RelayCommand(param =>
            {
                if (param is Window window)
                {
                    action(window);
                }
            }, _ => true);
    }
}
