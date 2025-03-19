using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Interfaces
{
    /// <summary>
    /// Интерфейс для логирования сообщений различных уровней важности.
    /// </summary>
    interface IAppLogger
    {
        void LogDebug(string message);
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogError(Exception ex, string message);
        void LogCritical(string message);
        void LogCritical(Exception ex, string message);

        void CreateCrashLog(Exception? ex, string message);
    }
}
