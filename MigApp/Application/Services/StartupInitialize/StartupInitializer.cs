using MigApp.Core.Models;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.Infrastructure.Services.AppUpdate;
using MigApp.Infrastructure.Services.DatabaseService;
using MigApp.Infrastructure.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Application.Services.StartupInitialize
{
    internal class StartupInitializer : IStartupInitializer
    {
        private readonly IAppUpdateService _appUpdateService;
        private readonly ISecurityService _securityService;
        private readonly IDatabaseConnectionTester _databaseConnectionTester;
        private readonly IAppLogger _logger;

        public StartupInitializer(IAppUpdateService appUpdateService,
                                  ISecurityService securityService,
                                  IDatabaseConnectionTester databaseConnectionTester,
                                  IAppLogger logger)
        {
            _appUpdateService = appUpdateService;
            _securityService = securityService;
            _databaseConnectionTester = databaseConnectionTester;
            _logger = logger;
        }

        public async Task<StartupResult> InitializeAsync()
        {
            try
            {
                _logger.LogInformation("Запуск инициализации приложения...");

                await _appUpdateService.UpdateApplicationAsync();

                var dbSettings = _securityService.LoadDatabaseSettingsFromVault();
                _logger.LogDebug($"Загружены настройки БД");

                var userCreds = _securityService.LoadUserCredentialsFromVault();
                _logger.LogDebug($"Загружены учетные данные пользователя");

                _logger.LogDebug($"Проверка подключения к БД");
                var isConnected = await _databaseConnectionTester.TestConnectionAsync(dbSettings);
                _logger.LogDebug($"Проверка подключения к БД завершена. Результат: {isConnected}");

                return new StartupResult
                {
                    IsConnectionSuccessful = isConnected,
                    Connection = dbSettings,
                    Credentials = userCreds
                };
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Ошибка при инициализации приложения");
                throw;
            }
        }
    }
}
