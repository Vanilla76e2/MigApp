using MigApp.Core.Enums;

namespace MigApp.UI.Base
{
    public interface IModalDialogHost
    {
        Task<bool> ShowAsync(string title, string message, ModalDialogType type);
    }
}
