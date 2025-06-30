using MigApp.Core.Enums;

namespace MigApp.UI.Models.NotificationEvent
{
    public class SnackbarNotificationEvent : INotificationEvent
    {
        public string Message { get; set; }
        public SnackbarType Type { get; set; }
        /// <summary>
        /// Длительность отображения уведомления в миллисекундах.
        /// </summary>
        public int Duration { get; set; }

        public SnackbarNotificationEvent(string message, SnackbarType type = SnackbarType.Info, int duration = 3000)
        {
            Message = message;
            Type = type;
            Duration = duration;
        }
    }
}
