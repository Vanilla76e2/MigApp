using MigApp.Core.Enums;

namespace MigApp.UI.Base
{
    public interface ISnackbarHost
    {
        void Show(string message, SnackbarType type, int duration);
    }
}
