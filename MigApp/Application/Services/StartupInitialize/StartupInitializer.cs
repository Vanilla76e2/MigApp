using MigApp.Core.Models;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.Infrastructure.Services.AppUpdate;
using MigApp.Infrastructure.Services.ConnectionSettingsManager;
using MigApp.Infrastructure.Services.CredentialsManager;

namespace MigApp.Application.Services.StartupInitialize
{
    internal class StartupInitializer : IStartupInitializer
    {
        private readonly IAppUpdateService _appUpdateService;
        private readonly IConnectionSettingsManager _connectionManager;
        private readonly ICredentialsManager _userManager;
        private readonly IAppLogger _logger;
        private bool remembered = Properties.Settings.Default.userRemembered;

        public StartupInitializer(IAppUpdateService appUpdateService,
                                  IConnectionSettingsManager connectionManager,
                                  ICredentialsManager userManager,
                                  IAppLogger logger)
        {
            _appUpdateService = appUpdateService;
            _connectionManager = connectionManager;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<StartupResult> InitializeAsync()
        {
            try
            {
                _logger.LogInformation("Запуск инициализации приложения...");

                await _appUpdateService.UpdateApplicationAsync();

                var dbSettings = await _connectionManager.LoadParametersAsync();
                _logger.LogDebug($"Загружены настройки БД");

                UserCredentials userCreds = new();

                if (remembered)
                {
                    userCreds = await _userManager.LoadUserCredentialsAsync();
                    _logger.LogDebug($"Загружены учетные данные пользователя");
                }
                else
                {
                    userCreds = new UserCredentials
                    {
                        Username = string.Empty,
                        Password = string.Empty
                    };
                    _logger.LogDebug($"Учетные данные пользователя не загружены");
                }

                _logger.LogInformation($"Проверка подключения к БД");
                var isConnected = await _connectionManager.TestAndSaveNewConnectionAsync(dbSettings);
                _logger.LogInformation($"Проверка подключения к БД завершена. Результат: {isConnected}");

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
