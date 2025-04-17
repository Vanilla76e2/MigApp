using System.Windows.Input;

namespace MigApp.Core
{
    public class RelayCommand : ICommand
    {
        private readonly Predicate<object?> _canExecute;
        private readonly Action<object?> _execute;

        // Конструктор класса
        public RelayCommand(Action<object?> execute, Predicate<object?> canExecute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        // Обработчик событий
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
            return;
        }

    }
}
