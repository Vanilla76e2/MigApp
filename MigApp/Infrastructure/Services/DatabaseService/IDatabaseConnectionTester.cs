using MigApp.UI.MVVM.Model;

namespace MigApp.Infrastructure.Services.DatabaseService;

public interface IDatabaseConnectionTester
{
    Task<bool> TestConnectionAsync(DatabaseConnectionParameters _params);
}
