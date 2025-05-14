using MigApp.MVVM.Model;
using System.Printing;

namespace MigApp.Demo.Services
{
    internal class DemoDatabaseService : IDatabaseService
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
