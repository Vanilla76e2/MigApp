using MigApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Application.Services.MessagesPublisher
{
    public interface IMessagePublisher
    {
        void Info(string message);
        void Success(string message);
        void Warning(string message);
        void Error(string message);

        void ShowConfirmation(string message, Action? onConfirmed, Action? onCancelled);

        void Notify(NotificationMessage message);
    }
}
