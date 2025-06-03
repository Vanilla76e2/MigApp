using MigApp.Core.Models;

namespace MigApp.Infrastructure.Services.ConnectionTester;

public interface IDatabaseConnectionTester
{
    Task<bool> TestConnectionAsync(DatabaseConnectionParameters _params);
}
