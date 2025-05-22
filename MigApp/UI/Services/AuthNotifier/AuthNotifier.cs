using MigApp.Core.Models;
using MigApp.UI.Services.UINotification;

namespace MigApp.UI.Services.AuthNotifier
{
    public class AuthNotifier : IAuthNotifier
    {
        private readonly IUINotificationService _ui;

        public AuthNotifier(IUINotificationService ui)
        {
            _ui = ui;
        }

        public Task<bool> ConfirmPasswordChangeAsync()
        {
            return _ui.ShowConfirmation("Ваш пароль был сброшен.\nЖелаете сохранить введённый пароль как новый?");
        }

        public async Task NotifyAsync(AuthResult result)
        {
            if (result.IsAuthenticated)
                return;

            if (!string.IsNullOrWhiteSpace(result.Message))
            {
                await _ui.ShowErrorAsync(result.Message);
            }
        }
    }
}
