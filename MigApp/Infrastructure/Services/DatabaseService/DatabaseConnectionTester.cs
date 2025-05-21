using Microsoft.EntityFrameworkCore;
using MigApp.Infrastructure.Data;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.Infrastructure.Services.DatabaseContextProvider;
using MigApp.Infrastructure.Services.Security;
using MigApp.UI.MVVM.Model;
using MigApp.UI.Services.UINotification;
using Npgsql;

namespace MigApp.Infrastructure.Services.DatabaseService;

public class DatabaseConnectionTester : IDatabaseConnectionTester
{
    private readonly IDbContextProvider _provider;
    private readonly ISecurityService _securityService;
    private IAppLogger _logger;
    private IUINotificationService _ui;

    public DatabaseConnectionTester(IAppLogger logger, IUINotificationService ui, IDbContextProvider provider, ISecurityService securityService)
    {
        _logger = logger;
        _ui = ui;
        _provider = provider;
        _securityService = securityService;
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

        _logger.LogInformation("Начало проверки подключения к БД");
        if (!ValidateParameters(databaseConnectionParameters))
            return false;

        try
        {
            var connectionString = databaseConnectionParameters.ToConnectionString();
            _logger.LogDebug($"Используется строка подключения: {databaseConnectionParameters.ToSecureConnectionString()}");

            var context = CreateDbContext(connectionString);

            bool result = await CheckConnectionStep(context, "Базовое подключение", () => GetConnection(context)) &&
                    await CheckConnectionStep(context, "Существование БД", () => IsDatabaseExist(context, databaseConnectionParameters.Database)) &&
                    await CheckConnectionStep(context, "Проверка прав", () => HasPermission(context)) &&
                    await CheckConnectionStep(context, "Тестовый запрос", () => TestQuery(context));
            if (result)
            {
                _securityService.SaveDatabaseSettingsToVault(databaseConnectionParameters);
                _provider.ResetContext();
                _logger.LogInformation("Подключение к базе данных успешно установлено");
            }
            else await _ui.ShowWarningAsync("Не удалось установить соединение с базой данных.");
            return result;
        }
        catch (NpgsqlException npgEx)
        {
            LogAndShowError(npgEx, $"Ошибка PostgreSQL (код: {npgEx.SqlState})");
            return false;
        }
        catch (Exception ex)
        {
            LogAndShowError(ex, "Критическая ошибка подключения");
            return false;
        }
    }

    private async Task<bool> CheckConnectionStep(MigDatabaseContext context, string stepName, Func<Task<bool>> checkAction)
    {
        _logger.LogDebug($"Проверка: {stepName}");

        try
        {
            var result = await checkAction();
            _logger.LogInformation($"{stepName} = {result}");
            if (result)
            {
                return true;
            }

            _logger.LogWarning($"{stepName}: проверка не пройдена");
            return false;
        }
        catch (PostgresException pgEx)
        {
            await LogAndShowError(pgEx, $"Ошибка PostgreSQL при проверке {stepName} (код: {pgEx.SqlState})");
            return false;
        }
        catch (Exception ex)
        {
            await LogAndShowError(ex, $"Ошибка при проверке {stepName}");
            return false;
        }
    }

    private async Task LogAndShowError(Exception ex, string message)
    {
        _logger.LogError(ex, message);
        await _ui.ShowErrorAsync($"{message}: {ex.Message}");
    }

    private async Task<bool> GetConnection(MigDatabaseContext context)
    {
        return await context.Database.CanConnectAsync();
    }

    private async Task<bool> IsDatabaseExist(MigDatabaseContext context, string DatabaseName)
    {
        return await context.Database.SqlQueryRaw<int>(
            "SELECT 1 FROM pg_Database WHERE datname = current_Database()").AnyAsync();
    }

    private async Task<bool> HasPermission(MigDatabaseContext context)
    {
        return await context.Database.SqlQueryRaw<int>(
            "SELECT 1 FROM information_schema.table_privileges WHERE grantee = current_user LIMIT 1").AnyAsync();
    }

    private async Task<bool> TestQuery(MigDatabaseContext context)
    {
        var result = await context.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == 0);

        return result?.Id == 0 || result == null;
    }

    private bool ValidateParameters(DatabaseConnectionParameters parameters)
    {
        if (parameters == null)
        {
            _logger.LogWarning("Параметры подключения не указаны");
            _ui.ShowWarningAsync("Не указаны параметры подключения").Wait();
            return false;
        }

        if (string.IsNullOrWhiteSpace(parameters.Host))
        {
            _logger.LogWarning("Не указан сервер базы данных");
            _ui.ShowWarningAsync("Не указан сервер базы данных").Wait();
            return false;
        }
        else if (string.IsNullOrEmpty(parameters.Database))
        {
            _logger.LogWarning("Не указана база данных");
            _ui.ShowWarningAsync("Не указана база данных").Wait();
            return false;
        }
        else if (string.IsNullOrEmpty(parameters.Username))
        {
            _logger.LogWarning("Не указан пользователь базы данных");
            _ui.ShowWarningAsync("Не указан пользователь базы данных").Wait();
            return false;
        }
        else if (string.IsNullOrEmpty(parameters.Password))
        {
            _logger.LogWarning("Не указан пароль базы данных");
            _ui.ShowWarningAsync("Не указан пароль базы данных").Wait();
            return false;
        }
        return true;
    }

    private MigDatabaseContext CreateDbContext(string connectionString)
    {
        var options = new DbContextOptionsBuilder<MigDatabaseContext>()
            .UseNpgsql(connectionString)
            .Options;
        return new MigDatabaseContext(options);
    }
}
