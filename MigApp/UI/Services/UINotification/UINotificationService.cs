using MaterialDesignThemes.Wpf;
using MigApp.Application.Services.EventAgregator;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.UI.Base;
using MigApp.UI.Models.NotificationEvent;
using MigApp.UI.Services.Dispathcer;
using System.Threading.Tasks;

namespace MigApp.UI.Services.UINotification
{
    public class UINotificationService : IUINotificationService
    {
        private readonly IDispatcher _dispatcher;
        private readonly IAppLogger _logger;
        private readonly ISnackbarHost _snackbarHost;
        private readonly IModalDialogHost _modalHost;

        public UINotificationService(IDispatcher dispatcher, 
                                    IAppLogger logger, 
                                    INotificationEventBus eventBus,
                                    ISnackbarHost snackbarHost,
                                    IModalDialogHost modalHost)
        {
            _dispatcher = dispatcher;
            _logger = logger;
            _snackbarHost = snackbarHost;
            _modalHost = modalHost;

            eventBus.Subscribe<SnackbarNotificationEvent>(OnSnackbar);
            eventBus.Subscribe<ModalDialogNotificationEvent>(notification => _ = OnModal(notification));
        }

        private void OnSnackbar(SnackbarNotificationEvent notification)
        {
            _dispatcher.Invoke(() =>
            {
                _logger.LogDebug($"Snackbar: {notification.Message}");
                _snackbarHost.Show(notification.Message, notification.Type, notification.Duration);
            });
        }

        private async Task OnModal(ModalDialogNotificationEvent notification)
        {
            await _dispatcher.InvokeAsync(async () =>
            {
                bool result = await _modalHost.ShowAsync(
                    notification.Title,
                    notification.Message,
                    notification.Type);

                if (result)
                {
                    notification.OnConfirmed?.Invoke();
                }
                else
                {
                    notification.OnCancelled?.Invoke();
                }
            });
        }
    }
}
