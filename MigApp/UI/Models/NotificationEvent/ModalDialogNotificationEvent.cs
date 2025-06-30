using MigApp.Core.Enums;

namespace MigApp.UI.Models.NotificationEvent
{
    public class ModalDialogNotificationEvent : INotificationEvent
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public ModalDialogType Type { get; set; }

        public Action? OnConfirmed { get; set; }
        public Action? OnCancelled { get; set; }


        public ModalDialogNotificationEvent(string title, 
            string message, 
            ModalDialogType type = ModalDialogType.Info,
            Action? onConfirmed = null, 
            Action? onCancelled = null)
        {
            Title = title;
            Message = message;
            Type = type;
            OnConfirmed = onConfirmed;
            OnCancelled = onCancelled;
        }
    }
}
