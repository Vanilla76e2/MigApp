using System.IO;
using Serilog;

namespace MigApp.Core.Services
{
    /// <summary>
    /// Класс, реализующий интерфейс IAppLogger и использующий Serilog для записи логов.
    /// </summary>
    internal class AppLogger : IAppLogger
    {
        private readonly ILogger _logger;
        private string? _currentLogFilePath;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AppLogger"/>.
        /// </summary>
        public AppLogger(ILogger logger)
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
                Log.Logger = configuration.CreateLogger();
                _logger = Log.Logger;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(AppLogger)}: Не удалось инициализировать логгер. {ex.Message}");
            }
            finally
            {
                if (_logger == null) throw new NullReferenceException(nameof(_logger));
                Debug.WriteLine($"{nameof(AppLogger)}: Не удалось инициализировать логгер.");
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
        /// Записывает отладочное сообщение в лог.
        /// </summary>
        /// <param name="message">Сообщение для записи.</param>
        public void LogDebug(string message)
        {
            _logger.Debug($"[DEBUG] {message}");
        }

        /// <summary>
        /// Записывает отладочное сообщение в лог.
        /// </summary>
        /// <param name="message">Сообщение для записи.</param>
        public void LogDebug(string message, string context)
        {
            _logger.Debug($"[DEBUG] {context}: {message}");
        }

        /// <summary>
        /// Записывает информационное сообщение в лог.
        /// </summary>
        /// <param name="message">Сообщение для записи.</param>
        public void LogInformation(string message)
        {
            _logger.Information($"[INFO] {message}");
        }

        /// <summary>
        /// Записывает информационное сообщение в лог.
        /// </summary>
        /// <param name="message">Сообщение для записи.</param>
        public void LogInformation(string message, string context)
        {
            _logger.Information($"[INFO] {context}: {message}");
        }

        /// <summary>
        /// Записывает предупреждающее сообщение в лог.
        /// </summary>
        /// <param name="message">Сообщение для записи.</param>
        public void LogWarning(string message)
        {
            _logger.Warning($"[WARN] {message}");
        }

        /// <summary>
        /// Записывает предупреждающее сообщение в лог.
        /// </summary>
        /// <param name="message">Сообщение для записи.</param>
        public void LogWarning(string message, string context)
        {
            _logger.Warning($"[WARN] {context}: {message}");
        }

        /// <summary>
        /// Записывает сообщение об ошибке в лог.
        /// </summary>
        /// <param name="message">Сообщение для записи.</param>
        public void LogError(string message)
        {
            _logger.Error($"[ERROR] {message}");
        }

        /// <summary>
        /// Записывает сообщение об ошибке в лог.
        /// </summary>
        /// <param name="message">Сообщение для записи.</param>
        public void LogError(string message, string context)
        {
            _logger.Error($"[ERROR] {context}: {message}");
        }

        /// <summary>
        /// Записывает сообщение об ошибке в лог с информацией об исключении.
        /// </summary>
        /// <param name="ex">Исключение для записи.</param>
        /// <param name="message">Сообщение для записи.</param>
        public void LogError(Exception ex, string message)
        {
            _logger.Error($"[ERROR] {message}\n\t{ex}");
        }

        /// <summary>
        /// Записывает сообщение об ошибке в лог с информацией об исключении.
        /// </summary>
        /// <param name="ex">Исключение для записи.</param>
        /// <param name="message">Сообщение для записи.</param>
        public void LogError(Exception ex, string message, string context)
        {
            _logger.Error($"[ERROR] {context}: {message}\n\t{ex}");
        }

        /// <summary>
        /// Записывает критическое сообщение в лог.
        /// </summary>
        /// <param name="message">Сообщение для записи.</param>
        public void LogCritical(string message)
        {
            _logger.Fatal($"[CRIT] {message}");
        }

        /// <summary>
        /// Записывает критическое сообщение в лог.
        /// </summary>
        /// <param name="message">Сообщение для записи.</param>
        public void LogCritical(string message, string context)
        {
            _logger.Error($"[CRIT] {context}: {message}");
        }

        /// <summary>
        /// Записывает критическое сообщение в лог с информацией об исключении.
        /// </summary>
        /// <param name="ex">Исключение для записи.</param>
        /// <param name="message">Сообщение для записи.</param>
        public void LogCritical(Exception ex, string message)
        {
            _logger.Fatal(ex, message);
        }

        /// <summary>
        /// Записывает критическое сообщение в лог с информацией об исключении.
        /// </summary>
        /// <param name="ex">Исключение для записи.</param>
        /// <param name="message">Сообщение для записи.</param>
        public void LogCritical(Exception ex, string message, string context)
        {
            _logger.Error($"[ERROR] {context}: {message}\n\t{ex}");
        }
    }
}
