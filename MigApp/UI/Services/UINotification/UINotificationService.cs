using MigApp.Infrastructure.Services.AppLogger;
using MigApp.UI.Services.Dispathcer;
using System.Windows;

namespace MigApp.UI.Services.UINotification
{
    internal class UINotificationService : IUINotificationService
    {
        private readonly IDispatcher _dispatcher;
        private readonly IAppLogger _logger;

        /// <summary>
        /// Инициализирует новый экземпляр сервиса уведомлений.
        /// </summary>
        /// <param name="dispatcher">Диспетчер для выполнения в UI-потоке.</param>
        /// <param name="logger">Логгер для записи событий.</param>
        public UINotificationService(IDispatcher dispathcer, IAppLogger logger)
        {
            _dispatcher = dispathcer;
            _logger = logger;
        }

        /// <summary>
        /// Отображает диалоговое окно с подтверждением.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <returns><see langword="true"/>, если нажата кнопка "Да". <see langword="false"/>, если нажата кнопка "Нет".</returns>
        public async Task<bool> ShowConfirmation(string message)
        {
            _logger.LogInformation($"Отображение диалога подтверждения: {message}");
            try
            {
                var result = await _dispatcher.InvokeAsync(() =>
                    MessageBox.Show(message, "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes);

                _logger.LogDebug($"Пользователь выбрал: {(result ? "Да" : "Нет")}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при отображении диалога подтверждения: {message}");
                throw;
            }
        }

        /// <summary>
        /// Выводит уведомление об ошибке.
        /// </summary>
        /// <param name="message"></param>
        public async Task ShowErrorAsync(string message)
        {
            _logger.LogDebug($"Отображение ошибки: {message}");
            try
            {
                await _dispatcher.InvokeAsync(() =>
                {
                    MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                });
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Не удалось отобразить ошибку: {message}");
                throw;
            }
        }

        /// <summary>
        /// Выводит информационное уведомление.
        /// </summary>
        /// <param name="message"></param>
        public async Task ShowInfoAsync(string message)
        {
            await _dispatcher.InvokeAsync(() => { MessageBox.Show(message, "Информация", MessageBoxButton.OK, MessageBoxImage.Information); });
        }

        /// <summary>
        /// Выводит предупреждение.
        /// </summary>
        /// <param name="message"></param>
        public async Task ShowWarningAsync(string message)
        {
            _logger.LogWarning($"Отображение предупреждения: {message}");
            try
            {
                await _dispatcher.InvokeAsync(() =>
                {
                    MessageBox.Show(message, "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Не удалось отобразить предупреждение: {message}");
                throw;
            }
        }
    }
}
