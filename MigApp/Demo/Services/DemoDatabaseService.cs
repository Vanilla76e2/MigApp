using MigApp.Core.Models;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.Infrastructure.Services.DatabaseService;

namespace MigApp.Demo.Services
{
    internal class DemoDatabaseService : IDatabaseConnectionTester
    {
        private IAppLogger _logger;

        public DemoDatabaseService(IAppLogger logger)
        {
            _logger = logger;
        }

        public Task<bool> TestConnectionAsync(DatabaseConnectionParameters _params)
        {
            _logger.LogDemo("Пропуск проверки подключения к БД");

            Task.Delay(1000); // Иммитация проверки подключения к БД

            return Task.FromResult(true);
        }
    }
}
