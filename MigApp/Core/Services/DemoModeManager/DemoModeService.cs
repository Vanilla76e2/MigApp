using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MigApp.Core.Services.DemoModeManager
{
    public class DemoModeService : IDemoModeService
    {
        public event EventHandler<bool>? DemoModeChanged;
        public bool IsDemoModeEnabled => Properties.Settings.Default.IsDemoMode;
        public ICommand ToggleDemoModeCommand { get; }
        private readonly IAppLogger _logger;

        public DemoModeService(IAppLogger logger)
        {
            ToggleDemoModeCommand = new RelayCommand(_ => ToggleDemoMode(), _ => true);
            _logger = logger;
        }

        private void ToggleDemoMode()
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
