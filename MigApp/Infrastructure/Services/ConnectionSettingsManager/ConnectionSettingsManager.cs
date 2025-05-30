using System;
using System.Collections.Generic;
using MigApp.Core.Models;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.Infrastructure.Services.DatabaseService;
using MigApp.Infrastructure.Services.Security;

namespace MigApp.Infrastructure.Services.ConnectionSettingsManager
{
    public class ConnectionSettingsManager : IConnectionSettingsManager
    {
        private readonly IAppLogger _logger;
        private readonly ISecurityService _securityService;
        private readonly IDatabaseConnectionTester _connectionTester;

        public ConnectionSettingsManager(IAppLogger logger, ISecurityService securityService, IDatabaseConnectionTester connectionTester)
        {
            _logger = logger;
            _securityService = securityService;
            _connectionTester = connectionTester;
        }

        public async Task<DatabaseConnectionParameters> LoadParametersAsync()
        {
            return await _securityService.LoadDatabaseSettingsFromVaultAsync();
        }

        public async Task SaveParametersAsync(DatabaseConnectionParameters parameters)
        {
            await _securityService.SaveDatabaseSettingsToVaultAsync(parameters);
        }

        public async Task<bool> TestAndSaveNewConnectionAsync(DatabaseConnectionParameters parameters)
        {
            _logger.LogInformation("Проверка подключения к базе данных");
            bool test = await _connectionTester.TestConnectionAsync(parameters);
            _logger.LogInformation($"Результат проверки подключения: {test}");
            if (!test)
            {
                return false;
            }
            _logger.LogInformation("Сохранение настроек подключения к базе данных");
            await _securityService.SaveDatabaseSettingsToVaultAsync(parameters);
            return true;
        }

        public async Task<bool> TestNewConnectionAsync(DatabaseConnectionParameters parameters)
        {
            _logger.LogInformation("Проверка подключения к базе данных");
            bool test = await _connectionTester.TestConnectionAsync(parameters);
            _logger.LogInformation($"Результат проверки подключения: {test}");
            return test;
        }
    }
}
