using System;
using System.Collections.Generic;
using MigApp.Core.Models;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.Infrastructure.Services.ConnectionTester;
using MigApp.Infrastructure.Services.RegistryService;
using MigApp.Infrastructure.Services.Security;

namespace MigApp.Infrastructure.Services.ConnectionSettingsManager
{
    public class ConnectionSettingsManager : IConnectionSettingsManager
    {
        private readonly IAppLogger _logger;
        private readonly IRegistryService _regService;
        private readonly IDatabaseConnectionTester _connectionTester;

        public ConnectionSettingsManager(IAppLogger logger, IRegistryService regService, IDatabaseConnectionTester connectionTester)
        {
            _logger = logger;
            _regService = regService;
            _connectionTester = connectionTester;
        }

        public DatabaseConnectionParameters LoadParametersAsync()
        {
            return _regService.LoadDatabaseSettingsFromRegistry();
        }

        public void SaveParametersAsync(DatabaseConnectionParameters parameters)
        {
            _regService.SaveDatabaseSettingsToRegistry(parameters);
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
            _regService.SaveDatabaseSettingsToRegistry(parameters);
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
