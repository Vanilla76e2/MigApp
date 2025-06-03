using System.Windows.Input;

namespace MigApp.Demo.Services.DemoModeManager
{
    public interface IDemoModeService
    {
        event EventHandler<bool> DemoModeChanged;
        bool IsDemoModeEnabled { get; }
        void ToggleDemoMode();
    }
}
