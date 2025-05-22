using MigApp.Core.Models;

namespace MigApp.Application.Services.StartupInitialize
{
    public interface IStartupInitializer
    {
        Task<StartupResult> InitializeAsync();
    }
}
