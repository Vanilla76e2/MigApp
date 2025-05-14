using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MigApp.Core.Services.DemoModeManager
{
    public interface IDemoModeService
    {
        event EventHandler<bool> DemoModeChanged;
        bool IsDemoModeEnabled { get; }
        ICommand ToggleDemoModeCommand { get; }

        string GetUserDisplayName();
    }
}
