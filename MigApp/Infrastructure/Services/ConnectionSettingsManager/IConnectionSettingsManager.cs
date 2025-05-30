using MigApp.Core.Models;

namespace MigApp.Infrastructure.Services.ConnectionSettingsManager
{
    public interface IConnectionSettingsManager
    {
        Task<bool> TestAndSaveNewConnectionAsync(DatabaseConnectionParameters parameters);
        Task<bool> TestNewConnectionAsync(DatabaseConnectionParameters parameters);

        Task SaveParametersAsync(DatabaseConnectionParameters parameters);
        Task<DatabaseConnectionParameters> LoadParametersAsync();
    }
}
