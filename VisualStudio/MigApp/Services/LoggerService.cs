using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Sinks.Debug;

namespace MigApp.Services
{
    /// <summary>
    /// Класс, реализующий интерфейс IAppLogger и использующий Serilog для записи логов.
    /// </summary>
    class LoggerService : IAppLogger
    {
        private readonly ILogger _logger;
        string _currentLogFilePath;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LoggerService"/>.
        /// </summary>
        public LoggerService()
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

        /// <summary>
        ///  Получает путь к файлу лога, формируя имя файла на основе текущей даты.
        /// </summary>
        /// <returns>Полный путь к файлу лога.</returns>
        private string GetLogFilePath(string logType)
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string logDirectory = System.IO.Path.Combine(appDataFolder, "MigApp", logType);

            if (!System.IO.Directory.Exists(logDirectory))
            {
                System.IO.Directory.CreateDirectory(logDirectory);
            }

            string logFileName = logType == "Logs" ? $"log.txt" : $"crash_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

            return System.IO.Path.Combine(logDirectory, logFileName);
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
            catch
            {

            }
        }


        /// <summary>
        /// Записывает отладочное сообщение в лог.
        /// </summary>
        /// <param name="message">Сообщение для записи.</param>
        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        /// <summary>
        /// Записывает информационное сообщение в лог.
        /// </summary>
        /// <param name="message">Сообщение для записи.</param>
        public void LogInformation(string message)
        {
            _logger.Information(message);
        }

        /// <summary>
        /// Записывает предупреждающее сообщение в лог.
        /// </summary>
        /// <param name="message">Сообщение для записи.</param>
        public void LogWarning(string message)
        {
            _logger.Warning(message);
        }

        /// <summary>
        /// Записывает сообщение об ошибке в лог.
        /// </summary>
        /// <param name="message">Сообщение для записи.</param>
        public void LogError(string message)
        {
            _logger.Error(message);
        }

        /// <summary>
        /// Записывает сообщение об ошибке в лог с информацией об исключении.
        /// </summary>
        /// <param name="ex">Исключение для записи.</param>
        /// <param name="message">Сообщение для записи.</param>
        public void LogError(Exception ex, string message)
        {
            _logger.Error(ex, message);
        }

        /// <summary>
        /// Записывает критическое сообщение в лог.
        /// </summary>
        /// <param name="message">Сообщение для записи.</param>
        public void LogCritical(string message)
        {
            _logger.Fatal(message);
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
    }
}
