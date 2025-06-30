using MigApp.UI.Models.NotificationEvent;

namespace MigApp.Application.Services.EventAgregator
{
    public interface INotificationEventBus
    {
        void Publish(INotificationEvent e);
        void Subscribe<T>(Action<T> handler) where T : INotificationEvent;
        void Unsubscribe<T>(Action<T> handler) where T : INotificationEvent;
    }
}
