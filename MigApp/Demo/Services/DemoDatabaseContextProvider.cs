using MigApp.Infrastructure.Data;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.Infrastructure.Services.DatabaseContextProvider;

namespace MigApp.Demo.Services
{
    class DemoDatabaseContextProvider : IDbContextProvider
    {
        private readonly IAppLogger _logger;

        public DemoDatabaseContextProvider(IAppLogger logger)
        {
            _logger = logger;
        }

        public MigDatabaseContext GetContext()
        {
            throw new NotImplementedException();
        }

        public void ResetContext() { _logger.LogDebug("Функция не доступна в демо-режиме."); }

        public void Dispose() { _logger.LogDebug("Функция не доступна в демо-режиме."); }
    }
}
