using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Windows;

namespace MigApp.Core.Services
{
    internal class VersionService : IVersionService
    {
        private readonly string _currentVersion;
        private readonly IInternetService _internetService;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="VersionService"/>.
        /// </summary>
        /// <param name="internetService">Сервис для проверки интернет-соединения.</param>
        /// <param name="httpClient">HttpClient для выполнения HTTP-запросов.</param>
        /// <exception cref="ArgumentNullException">Возникает, если <paramref name="internetService"/> равен null.</exception>
        public VersionService(IInternetService internetService, HttpClient httpClient)
        {
            _internetService = internetService;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _currentVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "0.0.0";
        }

        /// <summary>
        /// Асинхронно проверяет наличие подключения к интернету.
        /// </summary>
        /// <returns>
        /// Задача, представляющая асинхронную операцию. Возвращает true, если есть подключение к интернету, иначе false.
        /// </returns>
        public async Task<bool> HasInternetConnectionAsync()
        {
            return await _internetService.HasInternetConnectionAsync();
        }

        /// <summary>
        /// Асинхронно получает информацию о последнем релизе приложения из API GitHub.
        /// </summary>
        /// <returns>
        /// Задача, представляющая асинхронную операцию. Возвращает объект <see cref="ReleaseInfo"/> с информацией о релизе,
        /// или null, если не удалось получить информацию.
        /// </returns>
        public async Task<ReleaseInfo?> GetLatestReleaseInfoAsync()
        {
            try
            {
                // Настраиваем HttpClient
                _httpClient.BaseAddress = new Uri("https://api.github.com/repos/Vanilla76e2/MigApp/releases");
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "MigApp");

                // Выполняем GET-запрос к API GitHub
                HttpResponseMessage response = await _httpClient.GetAsync("");
                response.EnsureSuccessStatusCode(); // Проверяем, что запрос выполнен успешно

                // Читаем JSON-ответ
                string json = await response.Content.ReadAsStringAsync();

                // Парсим JSON-ответ
                using (JsonDocument document = JsonDocument.Parse(json))
                {
                    JsonElement root = document.RootElement;

                    if (root.ValueKind == JsonValueKind.Array)
                    {
                        ReleaseInfo? latestReleaseInfo = null;

                        foreach (JsonElement release in root.EnumerateArray())
                        {
                            // Получаем значения свойств, обрабатывая возможность null
                            if (!release.TryGetProperty("prerelease", out JsonElement preReleaseElement) || preReleaseElement.ValueKind != JsonValueKind.False)
                            {
                                // Пропускаем pre-release версии
                                continue;
                            }

                            string? version = release.GetProperty("tag_name").GetString();
                            bool isPreRelease = release.GetProperty("prerelease").GetBoolean();
                            string? downloadUrl = null;

                            if (release.TryGetProperty("assets", out JsonElement assets) && assets.ValueKind == JsonValueKind.Array)
                            {
                                foreach (JsonElement asset in assets.EnumerateArray())
                                {
                                    string? assetName = asset.GetProperty("name").GetString();
                                    if (assetName != null && assetName.EndsWith(".msi", StringComparison.OrdinalIgnoreCase))
                                    {
                                        string? url = asset.GetProperty("browser_download_url").GetString();
                                        if (url != null)
                                        {
                                            downloadUrl = url;
                                            break; // Выходим из цикла по assets, если нашли MSI
                                        }
                                    }
                                }
                            }

                            // Если это первый найденный не пререлиз
                            if (latestReleaseInfo == null)
                            {
                                latestReleaseInfo = new ReleaseInfo
                                {
                                    Version = version,
                                    IsPreRelease = isPreRelease,
                                    DownloadUrl = downloadUrl
                                };
                            }
                        }

                        return latestReleaseInfo; // Вернуть найденную не пререлиз версию
                    }
                    else
                    {
                        // Если API вернул что-то неожиданное, возвращаем null
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении информации о релизе: {ex.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// Метод для сравнения версий
        /// </summary>
        /// <param name="releaseInfo"></param>
        /// <returns>Возвращает true, если доступная версия новее текущей, иначе false</returns>
        public bool IsNewerVersionAvailable(ReleaseInfo releaseInfo)
        {
            // Сначала преобразуем строки версий в объекты Version
            if (releaseInfo.Version != null && Version.TryParse(_currentVersion, out Version? currentVersion) && Version.TryParse(releaseInfo.Version, out Version? newVersion))
            {
                // Сравниваем версии
                return newVersion > currentVersion;
            }
            else
            {
                // Если преобразование не удалось, считаем, что новая версия недоступна.
                return false;
            }
        }

        /// <summary>
        /// Асинхронно проверяет наличие новой версии приложения и, если она доступна, запускает установщик.
        /// </summary>
        /// <param name="releaseInfo"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateApplicationAsync(ReleaseInfo releaseInfo)
        {
            ArgumentNullException.ThrowIfNull(releaseInfo, "Информация о релизе оказалась null.");
            string? dir = null;
            dir = await DownloadInstallerAsync(releaseInfo.DownloadUrl);
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

        /// <summary>
        /// Вспомогательный класс для хранения параметров подключения.
        /// </summary>
        private class ConnectionSettings
        {
            public string? pgServer { get; set; }
            public string? pgDatabase { get; set; }
            public string? pgPassword { get; set; }
            public string? pgUser { get; set; }
            public string? pgPort { get; set; }
        }
    }

    /// <summary>
    /// Класс для хранения информации о релизе приложения.
    /// </summary>
    public class ReleaseInfo
    {
        /// <summary>
        /// Версия релиза.
        /// </summary>
        public string? Version { get; set; }

        /// <summary>
        /// Признак предварительной версии (pre-release).
        /// </summary>
        public bool? IsPreRelease { get; set; }

        /// <summary>
        /// URL для скачивания MSI-пакета.
        /// </summary>
        public string? DownloadUrl { get; set; }
    }
}
