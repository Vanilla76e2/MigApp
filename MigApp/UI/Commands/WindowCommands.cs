using MigApp.UI.Base;
using System.Windows;
using System.Windows.Input;

namespace MigApp.UI.Commands
{
    static public class WindowCommands
    {
        public static ICommand CloseCommand => CreateCommand<Window>(window => window.Close());

        public static ICommand MinimizeCommand => CreateCommand<Window>(window =>
            window.WindowState = WindowState.Minimized);

        public static ICommand ToggleMaximizeCommand => CreateCommand<Window>(window =>
            window.WindowState = window.WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized);

        public static ICommand CreateCommand<T>(Action<T> action) where T : class => new RelayCommand
            (param =>
            {
                if (param is T target)
                {
                    action(target);
                }
            },
            _ => true);
    }
}
