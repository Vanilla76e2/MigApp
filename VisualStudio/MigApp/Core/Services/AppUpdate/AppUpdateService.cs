

using System.IO;
using System.Net.Http;
using System.Windows;

namespace MigApp.Core.Services.AppUpdate
{
    internal class AppUpdateService : IAppUpdateService
    {
        private readonly IVersionService _versionService;
        private readonly IUINotificationService _niotificationService;
        private readonly IAppLogger _logger;
        private ReleaseInfo? _latestReleaseInfo;


        public AppUpdateService(IVersionService versionService, IUINotificationService notificationService, IAppLogger logger)
        {
            _versionService = versionService;
            _niotificationService = notificationService;
            _logger = logger;
        }

        public Task<bool> CheckForUpdatesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ReleaseInfo?> GetLatestReleaseInfoAsync()
        {
            throw new NotImplementedException();
        }

        public bool IsNewerVersionAvailable(ReleaseInfo releaseInfo)
        {
            if (releaseInfo.Version != null && Version.TryParse(_versionService.GetCurrentVersion(), out Version? currentVersion) && Version.TryParse(releaseInfo.Version, out Version? newVersion))
            {
                return newVersion > currentVersion;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Асинхронно проверяет наличие новой версии приложения и, если она доступна, запускает установщик.
        /// </summary>
        /// <param name="releaseInfo"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateApplicationAsync()
        {
            ArgumentNullException.ThrowIfNull(_latestReleaseInfo, "Информация о релизе оказалась null.");
            string? dir = null;
            dir = await DownloadInstallerAsync(_latestReleaseInfo.DownloadUrl);
            if (dir != null)
                InstallInstaller(dir);
            else throw new FileNotFoundException($"Ссылка на директорию установщика оказалась null.");
        }

        /// <summary>
        /// Асинхронно скачивает MSI-файл по указанному URL в папку TEMP.
        /// </summary>
        /// <param name="downloadUrl"></param>
        /// <returns>Ссылка на скачанный файл.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        private async Task<string?> DownloadInstallerAsync(string? downloadUrl)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(downloadUrl, "Ссылка на скачивание оказалась null.");

                string tmpmsi = Path.GetTempPath() + "MigAppInstaller.msi";

                // Скачиваем файл
                using (var response = await _httpClient.GetAsync(downloadUrl, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode(); // Проверяем, что запрос прошел успешно

                    using (var streamToReadFrom = await response.Content.ReadAsStreamAsync())
                    using (var streamToWriteTo = File.Open(tmpmsi, FileMode.Create))
                    {
                        await streamToReadFrom.CopyToAsync(streamToWriteTo);
                    }
                }

                return tmpmsi;
            }
            catch (HttpRequestException ex)
            {
                // Обрабатываем ошибки HTTP
                Console.WriteLine($"Ошибка при скачивании файла: {ex.Message}");
                return null; // Или бросаем исключение, если нужно
            }
            catch (Exception ex)
            {
                // Обрабатываем другие ошибки
                Console.WriteLine($"Ошибка при скачивании файла: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Запускает установщик по указанному пути.
        /// </summary>
        /// <param name="installerPath"></param>
        /// <exception cref="ArgumentException"></exception>
        private void InstallInstaller(string installerPath)
        {
            try
            {
                if (string.IsNullOrEmpty(installerPath) || !File.Exists(installerPath))
                {
                    throw new ArgumentException("Ссылка оказалась пустой или файл не существует.", nameof(installerPath));
                }
                // Запускаем установщик
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = installerPath,
                    UseShellExecute = true,
                    Verb = "runas"
                };
                Process.Start(processStartInfo);

                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                // Обработка ошибок при запуске установщика
                Console.WriteLine($"Ошибка при запуске установщика: {ex.Message}");
            }
        }
    }
}
