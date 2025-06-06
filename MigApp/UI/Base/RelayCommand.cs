using System.Windows.Input;

namespace MigApp.UI.Base
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

    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T>? _canExecute;
        private readonly Action<T> _execute;

        public RelayCommand(Action<T> execute, Predicate<T>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (parameter == null && typeof(T).IsValueType && Nullable.GetUnderlyingType(typeof(T)) == null)
                return false;

            return _canExecute == null || _canExecute((T)parameter!);
        }

        public void Execute(object? parameter)
        {
            if (parameter == null && typeof(T).IsValueType && Nullable.GetUnderlyingType(typeof(T)) == null)
                return;

            _execute((T)parameter!);
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value!;
            remove => CommandManager.RequerySuggested -= value!;
        }
    }
}
