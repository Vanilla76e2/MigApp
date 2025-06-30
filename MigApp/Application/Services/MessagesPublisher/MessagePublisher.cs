
using MaterialDesignThemes.Wpf;
using MigApp.Application.Services.EventAgregator;
using MigApp.Core.Enums;
using MigApp.Core.Models;
using MigApp.UI.Models.NotificationEvent;
using MigApp.UI.Services.TargetWindowResolver;

namespace MigApp.Application.Services.MessagesPublisher
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly INotificationEventBus _eventBus;

        public MessagePublisher(INotificationEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Info(string message)
        {
            Notify(new  NotificationMessage
            {
                Kind = NotificationKind.Snackbar,
                Message = message,
                SnackbarType = SnackbarType.Info
            });
        }

        public void Success(string message)
        {
            Notify(new NotificationMessage
            {
                Kind = NotificationKind.Snackbar,
                Message = message,
                SnackbarType = SnackbarType.Success
            });
        }

        public void Warning(string message)
        {
            Notify(new NotificationMessage
            {
                Kind = NotificationKind.Snackbar,
                Message = message,
                SnackbarType = SnackbarType.Warning
            });
        }

        public void Error(string message)
        {
            Notify(new NotificationMessage
            {
                Kind = NotificationKind.Snackbar,
                Message = message,
                SnackbarType = SnackbarType.Error
            });
        }

        /// <summary>
        /// Метод публикации модального сообщения подтверждения.
        /// </summary>
        /// <remarks>
        /// Для передачи метода можно использовать <see langword="OnConfirmed = () => DeleteItem()"/>.
        /// </remarks>
        /// <param name="message">Текст сообщения</param>
        /// <param name="targetWindow">Целевое окно отображения</param>
        /// <param name="onConfirmed">Действие при согласии</param>
        /// <param name="onCancelled">Действие при отказе</param>
        public void ShowConfirmation(string message, Action? onConfirmed, Action? onCancelled)
        {
            Notify(new NotificationMessage
            {
                Kind = NotificationKind.Modal,
                Title = "Подтверждение",
                Message = message,
                ModalDialogType = ModalDialogType.Confirmation,
                OnConfirmed = onConfirmed,
                OnCancelled = onCancelled
            });
        }

        public void Notify(NotificationMessage message)
        {
            INotificationEvent notificationEvent = message.Kind switch
            {
                NotificationKind.Snackbar => new SnackbarNotificationEvent(
                    message: message.Message,
                    type: message.SnackbarType,
                    duration: message.Duration),

                NotificationKind.Modal => new ModalDialogNotificationEvent(
                    title: message.Title ?? string.Empty,
                    message: message.Message,
                    type: message.ModalDialogType,
                    onConfirmed: message.OnConfirmed,
                    onCancelled: message.OnCancelled),

                _ => throw new NotSupportedException($"Неподдерживаемый тип уведомления: {message.Kind}")
            };

            _eventBus.Publish(notificationEvent);
        }
    }
}
