using MigApp.Core.Services.Dispathcer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MigApp.Core.Services
{
    internal class UINotificationService : IUINotificationService
    {
        private readonly IDispatcher _dispathcer;

        public UINotificationService(IDispatcher dispathcer)
        {
            _dispathcer = dispathcer;
        }

        /// <summary>
        /// Отображает диалоговое окно с подтверждением.
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Возвращает true, если нажата кнопка Да. False, если нажата кнопка Нет.</returns>
        public async Task<bool> ShowConfirmation(string message)
        {
            return await _dispathcer.InvokeAsync(() => MessageBox.Show(message, "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes);
        }

        /// <summary>
        /// Выводит уведомление об ошибке.
        /// </summary>
        /// <param name="message"></param>
        public async Task ShowErrorAsync(string message)
        {
            await _dispathcer.InvokeAsync(() => { MessageBox.Show(message,"Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); });
        }

        /// <summary>
        /// Выводит информационное уведомление.
        /// </summary>
        /// <param name="message"></param>
        public async Task ShowInfoAsync(string message)
        {
            await _dispathcer.InvokeAsync(() => { MessageBox.Show(message, "Информация", MessageBoxButton.OK, MessageBoxImage.Information); });
        }

        /// <summary>
        /// Выводит предупреждение.
        /// </summary>
        /// <param name="message"></param>
        public async Task ShowWarningAsync(string message)
        {
            await _dispathcer.InvokeAsync(() => { MessageBox.Show(message, "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning); });
        }
    }
}
