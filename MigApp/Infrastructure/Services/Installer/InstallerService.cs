using MigApp.Infrastructure.Services.AppLogger;
using System.ComponentModel;
using System.IO;
using System.Text.Json;

namespace MigApp.Infrastructure.Services.Installer
{
    /// <summary>
    /// Реализация сервиса <see cref="IInstallerService"/>.
    /// </summary>
    public class InstallerService : IInstallerService
    {
        private readonly IAppLogger _logger;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="InstallerService"/>.
        /// </summary>
        /// <param name="logger">Сервис логирования.</param>
        public InstallerService(IAppLogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Запускает процесс установки из указанного файла.
        /// </summary>
        /// <param name="filePath">Путь к файлу установщика.</param>
        /// <exception cref="ArgumentNullException">Возникает, если filePath равен null.</exception>
        /// <exception cref="FileNotFoundException">Возникает, если файл не существует.</exception>
        /// <exception cref="Win32Exception">Возникает при ошибках запуска процесса.</exception>
        /// <exception cref="Exception">Другие непредвиденные ошибки.</exception>
        public void Install(string filePath)
        {
            _logger.LogInformation($"Начало установки из файла: {filePath}");

            try
            {
                ArgumentNullException.ThrowIfNull(filePath, nameof(filePath));

                if (!File.Exists(filePath))
                {
                    _logger.LogError($"Файл установщика не найден: {filePath}");
                    throw new FileNotFoundException($"Файл не найден: {filePath}", filePath);
                }

                var processStartInfo = new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true,
                    Verb = "runas"
                };

                _logger.LogDebug($"Параметры запуска: {JsonSerializer.Serialize(processStartInfo)}");

                var process = Process.Start(processStartInfo);
                if (process == null)
                {
                    _logger.LogError($"Не удалось запустить процесс для файла: {filePath}");
                    throw new Win32Exception("Не удалось запустить процесс установки");
                }

                _logger.LogInformation($"Процесс установки успешно запущен (PID: {process.Id})");
                Application.Current.Shutdown();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Путь к файлу установщика не указан");
                throw;
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogError(ex, $"Файл установщика не найден: {filePath}");
                throw;
            }
            catch (Win32Exception ex)
            {
                _logger.LogCritical(ex, $"Ошибка запуска установщика (требуются права администратора?): {filePath}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Критическая ошибка при установке: {filePath}");
                throw;
            }
        }
    }
}
