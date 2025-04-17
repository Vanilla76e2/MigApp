using Serilog;
using System.IO;
using System.Runtime.CompilerServices;

namespace MigApp.Core.Services
{
    /// <summary>
    /// Класс, реализующий интерфейс IAppLogger и использующий Serilog для записи логов.
    /// </summary>
    public class AppLogger : IAppLogger
    {
        private readonly ILogger _logger;
        private string? _currentLogFilePath;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AppLogger"/>.
        /// </summary>
        public AppLogger()
        {
            try
            {
                _currentLogFilePath = GetLogFilePath("Logs");
                var configuration = new LoggerConfiguration();
#if DEBUG
                // При отладке логи будут выводиться в консоль и окно Output Visual Studio.
                configuration.MinimumLevel.Debug().WriteTo.Console().WriteTo.Debug();
#else
                // В релизной версии приложения логи будут записываться в файл.
                configuration.MinimumLevel.Information().WriteTo.File(
                    _currentLogFilePath, 
                    rollingInterval: RollingInterval.Infinite, 
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    retainedFileCountLimit: 1
                );
#endif
                _logger = configuration.CreateLogger();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(AppLogger)}: Не удалось инициализировать логгер. {ex.Message}");
                throw;
            }
            finally
            {
                if (_logger == null) throw new NullReferenceException(nameof(_logger));
            }
        }

        /// <summary>
        ///  Получает путь к файлу лога, формируя имя файла на основе текущей даты.
        /// </summary>
        /// <returns>Полный путь к файлу лога.</returns>
        private string GetLogFilePath(string logType)
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string logDirectory = Path.Combine(appDataFolder, "MigApp", logType);

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            string logFileName = logType == "Logs" ? $"log.txt" : $"crash_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

            return Path.Combine(logDirectory, logFileName) ?? string.Empty;
        }

        /// <summary>
        /// Создает файл с крашлогом, содержащий информацию об ошибке и логи за текущую сессию.
        /// </summary>
        /// <param name="exception">Исключение, вызвавшее краш.</param>
        /// <param name="message">Сообщение об ошибке.</param>
        public void CreateCrashLog(Exception? exception, string message)
        {
            string crasgFilePath = GetLogFilePath("CrashLogs");

            try
            {
                using (StreamWriter writer = new StreamWriter(crasgFilePath))
                {
                    writer.WriteLine($"Сообщение: {message}");
                    writer.WriteLine($"Время: {DateTime.Now}");
                    writer.WriteLine($"Тип исключения: {(exception == null ? "Не удалось получить тип исключения." : exception.GetType().FullName)}");
                    writer.WriteLine($"Сообщение исключения: {(exception == null ? "Не удалось получить исключение." : exception.Message)}");
                    writer.WriteLine($"Трассировка стека: {(exception == null ? "Не удалось получить исключение." : exception.StackTrace)}");
                    writer.WriteLine("--------------------------------------------------");
                    writer.WriteLine("Логи за сессию:");

                    if (File.Exists(_currentLogFilePath))
                    {
                        string[] logs = File.ReadAllLines(_currentLogFilePath);
                        foreach (var log in logs)
                        {
                            writer.WriteLine(log);
                        }
                    }
                    else
                    {
                        writer.WriteLine("Файл логов не найден.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(CreateCrashLog)}: Не удалось создать крашлог. {ex.Message}");
            }
        }

        /// <summary>
        /// Извлекает имя класса из пути к файлу
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private static string GetContextFromFilePath(string filepath)
        {
            try
            {
                return Path.GetFileNameWithoutExtension(filepath);
            }
            catch
            {
                return "Unknown";
            }
        }

        /// <summary>
        /// Записывает отладочное сообщение в лог. Автоматически получает контекст.
        /// </summary>
        /// <param name="message">Сообщение для записи.</param>
        public void LogDebug(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "")
        {
            var context = GetContextFromFilePath(filePath);
            _logger.Debug($"{context}.{memberName}: {message}");
        }

        /// <summary>
        /// Записывает информационное сообщение в лог. Автоматически получает контекст.
        /// </summary>
        /// <param name="message">Сообщение для записи.</param>
        public void LogInformation(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "")
        {
            var context = GetContextFromFilePath(filePath);
            _logger.Information($"{context}.{memberName}: {message}");
        }

        /// <summary>
        /// Записывает предупреждающее сообщение в лог. Автоматически получает контекст.
        /// </summary>
        /// <param name="message">Сообщение для записи.</param>
        public void LogWarning(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "")
        {
            var context = GetContextFromFilePath(filePath);
            _logger.Warning($"{context}.{memberName}: {message}");
        }

        /// <summary>
        /// Записывает сообщение об ошибке в лог. Автоматически получает контекст.
        /// </summary>
        /// <param name="message">Сообщение для записи.</param>
        public void LogError(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "")
        {
            var context = GetContextFromFilePath(filePath);
            _logger.Error($"{context}.{memberName}: {message}");
        }

        /// <summary>
        /// Записывает сообщение об ошибке в лог. Автоматически получает контекст.
        /// </summary>
        /// /// <param name="ex">Исключение для записи.</param>
        /// <param name="message">Сообщение для записи.</param>
        public void LogError(Exception ex, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "")
        {
            var context = GetContextFromFilePath(filePath);
            _logger.Error($"{context}.{memberName}: {message}\n{ex}");
        }

        /// <summary>
        /// Записывает сообщение о критической ошибке в лог. Автоматически получает контекст.
        /// </summary>
        /// <param name="message">Сообщение для записи.</param>
        public void LogCritical(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "")
        {
            var context = GetContextFromFilePath(filePath);
            _logger.Fatal($"{context}.{memberName}: {message}");
        }

        /// <summary>
        /// Записывает сообщение о критической ошибке в лог. Автоматически получает контекст.
        /// </summary>
        /// <param name="ex">Исключение для записи.</param>
        /// <param name="message">Сообщение для записи.</param>
        public void LogCritical(Exception ex, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "")
        {
            var context = GetContextFromFilePath(filePath);
            _logger.Fatal($"{context}.{memberName}: {message}\n{ex}");
        }
    }
}