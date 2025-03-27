using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Npgsql.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MigApp.MVVM.Model;
using MigApp.Helpers;

namespace MigApp.Core.Services
{
    class DatabaseService : IDatabaseService
    {
        private IAppLogger _logger;

        public DatabaseService(IAppLogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Асинхронно проверяет возможность подключения к базе данных, используя Entity Framework Core.
        /// </summary>
        /// <returns>
        ///   <c>true</c>, если подключение к базе данных успешно установлено в течение заданного времени ожидания;
        ///   в противном случае - <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Этот метод пытается установить соединение с базой данных, используя текущие настройки DbContext.
        /// Если подключение не может быть установлено в течение 10 секунд, метод возвращает <c>false</c>.
        /// В случае возникновения исключения при попытке подключения, метод также возвращает <c>false</c> и регистрирует информацию об ошибке.
        /// </remarks>
        /// <exception cref="Exception">Возникает в случае непредвиденной ошибки при попытке подключения к базе данных.</exception>
        public async Task<bool> TestConnectionAsync(DatabaseConnectionParameters databaseConnectionParameters)
        {
            ArgumentNullException.ThrowIfNull(databaseConnectionParameters, nameof(databaseConnectionParameters));

            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<MigDataBaseContext>();

                try { optionsBuilder.UseNpgsql(ConnectionStringAssembler.BuildConnectionString(databaseConnectionParameters)); }
                catch (Exception ex)
                { _logger.LogError(ex, "Ошибка при попытке собрать строку подключения", nameof(TestConnectionAsync)); return false; }

                using (var dbc = new MigDataBaseContext(optionsBuilder.Options))
                {
                    try
                    {
                        var connectionTask = dbc.Database.CanConnectAsync();
                        if (await Task.WhenAny(connectionTask, Task.Delay(TimeSpan.FromSeconds(10))) == connectionTask)
                        {
                            await connectionTask;
                            _logger.LogInformation("Успешное подключение к базе данных", nameof(TestConnectionAsync));
                            return true;
                        }
                        else
                        {
                            _logger.LogWarning("Не удалось установить соединение с базой данных: Превышено время ожидания.", nameof(TestConnectionAsync));
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Не удалось установить соединение с базой данных.", nameof(TestConnectionAsync));
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла непредвиденная ошибка.", nameof(TestConnectionAsync));
                return false;
            }

        }
    }
}
