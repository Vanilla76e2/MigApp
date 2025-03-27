
namespace MigApp.Core.Services
{
    internal interface IUINotificationService
    {
        Task ShowInfoAsync(string message);
        Task ShowErrorAsync(string message);
        Task ShowWarningAsync(string message);
        Task<bool> ShowConfirmation(string message);
    }
}
