using MigApp.Infrastructure.Services.AppLogger;
using MigApp.UI.Base;
using System.Windows.Input;

namespace MigApp.Demo.Services.DemoModeManager
{
    public class DemoModeService : IDemoModeService
    {
        public event EventHandler<bool>? DemoModeChanged;
        public bool IsDemoModeEnabled => Properties.Settings.Default.IsDemoMode;
        private readonly IAppLogger _logger;

        public DemoModeService(IAppLogger logger)
        {
            _logger = logger;
        }

        public void ToggleDemoMode()
        {
            Properties.Settings.Default.IsDemoMode = !IsDemoModeEnabled;
            Properties.Settings.Default.Save();

            DemoModeChanged?.Invoke(this, IsDemoModeEnabled);
            _logger.LogInformation($"Демонстрационный режим был {(IsDemoModeEnabled ? "включен" : "отключен")}");
        }

        public string GetUserDisplayName()
        {
            return IsDemoModeEnabled ? "Demo Mode" : "UserName";
        }
    }
}
