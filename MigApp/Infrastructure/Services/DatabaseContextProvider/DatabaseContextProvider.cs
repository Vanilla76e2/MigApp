using Microsoft.EntityFrameworkCore;
using MigApp.Demo;
using MigApp.Infrastructure.Data;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.Infrastructure.Services.Security;

namespace MigApp.Infrastructure.Services.DatabaseContextProvider
{
    internal class DatabaseContextProvider : IDatabaseContextProvider
    {
        private readonly ISecurityService _securityService;
        private readonly IAppLogger _logger;

        public DatabaseContextProvider(ISecurityService securityService, IAppLogger logger)
        {
            _securityService = securityService;
            _logger = logger;
        }

        public async Task<DbContext> GetDbContextAsync()
        {
            if (Properties.Settings.Default.IsDemoMode)
            {
                _logger.LogDemo("Создаётся демонстрационный контекст данных");
                return new DemoDatabaseContext(new DbContextOptions<MigDatabaseContext>());
            }

            _logger.LogInformation("Создаётся реальный контекст данных");
            var connectionString = (await _securityService.LoadDatabaseSettingsFromVaultAsync()).ToConnectionString();
            var optionsBuilder = new DbContextOptionsBuilder<MigDatabaseContext>();
            optionsBuilder.UseNpgsql(connectionString);
            return new MigDatabaseContext(optionsBuilder.Options);
        }

        public DbContext GetDbContext(string connectionString)
        {
            if (Properties.Settings.Default.IsDemoMode)
            {
                _logger.LogDemo("Создаётся демонстрационный контекст данных");
                return new DemoDatabaseContext(new DbContextOptions<MigDatabaseContext>());
            }

            _logger.LogInformation("Создаётся реальный контекст данных с заданной строкой подключения");
            var optionsBuilder = new DbContextOptionsBuilder<MigDatabaseContext>();
            optionsBuilder.UseNpgsql(connectionString);
            return new MigDatabaseContext(optionsBuilder.Options);
        }
    }
}
