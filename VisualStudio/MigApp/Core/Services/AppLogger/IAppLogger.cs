
namespace MigApp.Core.Services
{
    /// <summary>
    /// Интерфейс для логирования сообщений различных уровней важности.
    /// </summary>
    interface IAppLogger
    {
        void LogDebug(string message);
        void LogDebug(string message, string context);
        void LogInformation(string message);
        void LogInformation(string message, string context);
        void LogWarning(string message);
        void LogWarning(string message, string context);
        void LogError(string message);
        void LogError(string message, string context);
        void LogError(Exception ex, string message);
        void LogError(Exception ex, string message, string context);
        void LogCritical(string message);
        void LogCritical(string message, string context);
        void LogCritical(Exception ex, string message);
        void LogCritical(Exception ex, string message, string context);

        void CreateCrashLog(Exception? ex, string message);
    }
}
