using Microsoft.EntityFrameworkCore;
using MigApp.Demo;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.Infrastructure.Services.Security;
using System.Threading.Tasks;

namespace MigApp.Infrastructure.Data;

public class MigDatabaseContextFactory : IDbContextFactory<DbContext>
{
    private readonly ISecurityService _securityService;
    private readonly IAppLogger _logger;

    public MigDatabaseContextFactory(ISecurityService securityService, IAppLogger logger)
    {
        _securityService = securityService;
        _logger = logger;
    }

    //public async Task<DbContext> CreateDbContext()
    //{
    //    if (Properties.Settings.Default.IsDemoMode)
    //    {
    //        _logger.LogInformation("Создание контекста базы данных в демонстрационном режиме");
    //        return new DemoDatabaseContext(new DbContextOptions<MigDatabaseContext>());
    //    }

    //    _logger.LogInformation("Создание контекста базы данных");
    //    var connectionString = await _securityService.LoadDatabaseSettingsFromVaultAsync().ToConnectionString();
    //    var optionsBuilder = new DbContextOptionsBuilder<MigDatabaseContext>();
    //    optionsBuilder.UseNpgsql(connectionString);
    //    _logger.LogInformation("Контекст базы данных создан");
    //    return new MigDatabaseContext(optionsBuilder.Options);
    //}

    DbContext IDbContextFactory<DbContext>.CreateDbContext()
    {
        if (Properties.Settings.Default.IsDemoMode)
        {
            _logger.LogInformation("Создание контекста базы данных в демонстрационном режиме");
            return new DemoDatabaseContext(new DbContextOptions<MigDatabaseContext>());
        }

        _logger.LogInformation("Создание контекста базы данных");

        // Безопасно для вызова: данные из реестра, без долгих ожиданий
        var connectionString = _securityService.LoadDatabaseSettingsFromVaultAsync().GetAwaiter().GetResult().ToConnectionString();

        var optionsBuilder = new DbContextOptionsBuilder<MigDatabaseContext>();
        optionsBuilder.UseNpgsql(connectionString);

        _logger.LogInformation("Контекст базы данных создан");
        return new MigDatabaseContext(optionsBuilder.Options);
    }
}
