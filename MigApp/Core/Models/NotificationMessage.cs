using MigApp.Core.Enums;

namespace MigApp.Core.Models
{
    public class NotificationMessage
    {
        public NotificationKind Kind { get; init; } = NotificationKind.Snackbar;

        public string Message { get; init; } = string.Empty;
        public string? Title {  get; init; }
        
        public SnackbarType SnackbarType { get; init; } = SnackbarType.Info;
        public ModalDialogType ModalDialogType { get; init; } = ModalDialogType.Info;

        public int Duration { get; init; } = 3000;

        public Action? OnConfirmed { get; init; }
        public Action? OnCancelled { get; init; }
    }
}
