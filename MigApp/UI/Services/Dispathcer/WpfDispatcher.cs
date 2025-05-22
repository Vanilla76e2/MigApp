using MigApp.Infrastructure.Services.AppLogger;
using System.Windows.Threading;

namespace MigApp.UI.Services.Dispathcer
{
    /// <summary>
    /// Реализация <see cref="IDispatcher"/> для WPF (Windows Presentation Foundation).
    /// Обеспечивает выполнение операций в потоке пользовательского интерфейса WPF.
    /// </summary>
    public class WpfDispatcher : IDispatcher
    {
        private readonly Dispatcher _dispatcher;
        private readonly IAppLogger _logger;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="WpfDispatcher"/>.
        /// </summary>
        /// <param name="dispatcher">Экземпляр WPF Dispatcher, используемый для выполнения операций.</param>
        /// <exception cref="ArgumentNullException">Вызывается, если <paramref name="dispatcher"/> равен null.</exception>
        public WpfDispatcher(Dispatcher dispatcher, IAppLogger logger)
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            _logger = logger;
        }

        /// <inheritdoc/>
        public void Invoke(Action action)
        {
            _logger.LogDebug($"Вызов синхронного действия в UI-потоке (IsOnUiThread: {IsOnUiThread()})");
            try
            {
                _dispatcher.Invoke(action);
                _logger.LogDebug("Синхронное действие успешно выполнено в UI-потоке");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении синхронного действия в UI-потоке");
                throw;
            }
        }

        /// <inheritdoc/>
        public Task InvokeAsync(Action action)
        {
            _logger.LogDebug($"Вызов асинхронного действия в UI-потоке (IsOnUiThread: {IsOnUiThread()})");
            try
            {
                Task _d = _dispatcher.InvokeAsync(action).Task;
                _logger.LogDebug("Асинхронное действие отправлено в UI-поток");
                return _d;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при отправке асинхронного действия в UI-поток");
                throw;
            }
        }

        /// <inheritdoc/>
        public Task<T> InvokeAsync<T>(Func<T> func)
        {
            _logger.LogDebug($"Асинхронный вызов функции в UI-потоке (IsOnUiThread: {IsOnUiThread()})");
            try
            {
                var task = _dispatcher.InvokeAsync(func).Task;
                _logger.LogDebug("Асинхронная функция отправлена в UI-поток");
                return task;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при постановке асинхронной функции в очередь UI-потока");
                throw;
            }
        }

        /// <inheritdoc/>
        public bool IsOnUiThread()
        {
            return _dispatcher.CheckAccess();
        }
    }
}
