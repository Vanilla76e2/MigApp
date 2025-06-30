using MigApp.UI.Models.NotificationEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Application.Services.EventAgregator
{
    internal class NotificationEventBus : INotificationEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _subscribers = new();

        public void Publish(INotificationEvent e)
        {
            var type = e.GetType();
            if (_subscribers.TryGetValue(type, out var handlers))
            {
                foreach (var handler in handlers.Cast<Action<INotificationEvent>>())
                {
                    handler(e);
                }
            }
        }

        public void Subscribe<T>(Action<T> handler) where T : INotificationEvent
        {
            var type = typeof(T);
            if (_subscribers.ContainsKey(type))
                _subscribers[type] = new List<Delegate>();

            _subscribers[type].Add(handler);
        }

        public void Unsubscribe<T>(Action<T> handler) where T : INotificationEvent
        {
            var type = typeof(T);
            if (_subscribers.TryGetValue(type, out var handlers))
                handlers.Remove(handler);
        }
    }
}
