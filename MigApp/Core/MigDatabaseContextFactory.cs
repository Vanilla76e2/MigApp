using Microsoft.EntityFrameworkCore;
using MigApp.Demo.Data;

namespace MigApp.Core
{
    public class MigDatabaseContextFactory : IDbContextFactory<DbContext>
    {
        private readonly ISecurityService _securityService;
        private readonly IAppLogger _logger;

        public MigDatabaseContextFactory(ISecurityService securityService, IAppLogger logger)
        {
            _securityService = securityService;
            _logger = logger;
        }

        public DbContext CreateDbContext()
        {
            if (Properties.Settings.Default.IsDemoMode)
            {
                _logger.LogInformation("Создание контекста базы данных в демонстрационном режиме");
                return new DemoDatabaseContext(new DbContextOptions<DemoDatabaseContext>());
            }

            _logger.LogInformation("Создание контекста базы данных");
            var connectionString = _securityService.LoadDatabaseSettingsFromVault().ToConnectionString();
            var optionsBuilder = new DbContextOptionsBuilder<MigDatabaseContext>();
            optionsBuilder.UseNpgsql(connectionString);
            _logger.LogInformation("Контекст базы данных создан");
            return new MigDatabaseContext(optionsBuilder.Options);
        }
    }
}
