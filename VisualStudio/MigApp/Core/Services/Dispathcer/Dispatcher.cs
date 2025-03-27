using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MigApp.Core.Services.Dispathcer
{
    /// <summary>
    /// Реализация <see cref="IDispatcher"/> для WPF (Windows Presentation Foundation).
    /// Обеспечивает выполнение операций в потоке пользовательского интерфейса WPF.
    /// </summary>
    internal class WpfDispatcher : IDispatcher
    {
        private readonly Dispatcher _dispatcher;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="WpfDispatcher"/>.
        /// </summary>
        /// <param name="dispatcher">Экземпляр WPF Dispatcher, используемый для выполнения операций.</param>
        /// <exception cref="ArgumentNullException">Вызывается, если <paramref name="dispatcher"/> равен null.</exception>
        public WpfDispatcher(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <inheritdoc/>
        public void Invoke(Action action)
        {
            _dispatcher.Invoke(action);
        }

        /// <inheritdoc/>
        public Task InvokeAsync(Action action)
        {
            return _dispatcher.InvokeAsync(action).Task;
        }

        /// <inheritdoc/>
        public Task<T> InvokeAsync<T>(Func<T> func)
        {
            return _dispatcher.InvokeAsync(func).Task;
        }

        /// <inheritdoc/>
        public bool IsOnUiThread()
        {
            return _dispatcher.CheckAccess();
        }
    }
}
