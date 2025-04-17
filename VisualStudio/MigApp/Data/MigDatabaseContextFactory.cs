using Microsoft.EntityFrameworkCore;

namespace MigApp.Data
{
    public class MigDatabaseContextFactory : IDbContextFactory<MigDatabaseContext>
    {
        private readonly ISecurityService _securityService;
        private readonly IAppLogger _logger;

        public MigDatabaseContextFactory(ISecurityService securityService, IAppLogger logger)
        {
            _securityService = securityService;
            _logger = logger;
        }

        public MigDatabaseContext CreateDbContext()
        {
            _logger.LogInformation("Создание контекста базы данных");
            var connectionString = _securityService.LoadDatabaseSettingsFromVault().ToConnectionString();
            var optionsBuilder = new DbContextOptionsBuilder<MigDatabaseContext>();
            optionsBuilder.UseNpgsql(connectionString);
            _logger.LogInformation("Контекст базы данных создан");
            return new MigDatabaseContext(optionsBuilder.Options);
        }
    }
}
