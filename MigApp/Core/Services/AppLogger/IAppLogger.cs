
using System.Runtime.CompilerServices;

namespace MigApp.Core.Services
{
    /// <summary>
    /// Интерфейс для логирования сообщений различных уровней важности.
    /// </summary>
    public interface IAppLogger
    {
        void LogDebug(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "");
        void LogInformation(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "");
        void LogWarning(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "");
        void LogError(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "");
        void LogError(Exception ex, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "");
        void LogCritical(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "");
        void LogCritical(Exception ex, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "");
        void LogDemo(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "");

        void CreateCrashLog(Exception? ex, string message);
    }
}
