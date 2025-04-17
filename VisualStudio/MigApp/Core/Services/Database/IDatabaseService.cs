using MigApp.MVVM.Model;

namespace MigApp.Core.Services
{
    public interface IDatabaseService
    {
        Task<bool> TestConnectionAsync(DatabaseConnectionParameters _params);
    }
}
