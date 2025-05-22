using MigApp.Core.Models;

namespace MigApp.Infrastructure.Services.DatabaseService;

public interface IDatabaseConnectionTester
{
    Task<bool> TestConnectionAsync(DatabaseConnectionParameters _params);
}
