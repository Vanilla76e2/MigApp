using MigApp.Interfaces;
using MigApp.Properties;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Windows;

namespace MigApp.Services
{
    public class VersionService : IVersionService
    {
        private readonly string _currentVersion;
        private readonly InternetService _internetService;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="VersionService"/>.
        /// </summary>
        /// <param name="internetService">Сервис для проверки интернет-соединения.</param>
        /// <param name="httpClient">HttpClient для выполнения HTTP-запросов.</param>
        /// <exception cref="ArgumentNullException">Возникает, если <paramref name="internetService"/> равен null.</exception>
        public VersionService(InternetService internetService, HttpClient httpClient)
        {
            _internetService = internetService ?? throw new ArgumentNullException(nameof(internetService)); // Получаем сервис для проверки интернет-соединения
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient)); // Получаем HttpClient для выполнения HTTP-запросов
            _currentVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "0.0.0"; // Получаем версию сборки
        }

        /// <summary>
        /// Асинхронно проверяет наличие подключения к интернету.
        /// </summary>
        /// <returns>
        /// Задача, представляющая асинхронную операцию. Возвращает true, если есть подключение к интернету, иначе false.
        /// </returns>
        private async Task<bool> HasInternetConnectionAsync()
        {
            return await _internetService.InternetCheckerAsync();
        }

        /// <summary>
        /// Асинхронно проверяет наличие новой версии приложения и предлагает пользователю обновиться, если она доступна.
        /// </summary>
        public async Task CheckVersionAsync()
        {
            if (await HasInternetConnectionAsync())
            {
                // Выполняем миграцию настроек только один раз после обновления
                if (!Settings.Default.IsMigrationCompleted)
                {
                    // Читаем старые настройки
                    ConnectionSettings? oldSettings = ReadOldConnectionSettings();

                    // Если удалось прочитать старые настройки, переносим их
                    if (oldSettings != null)
                    {
                        MigrateConnectionSettings(oldSettings);

                        // Устанавливаем флаг, что миграция выполнена
                        Settings.Default.IsMigrationCompleted = true;
                        Settings.Default.Save();
                    }
                }

                ReleaseInfo? releaseInfo = await GetLatestReleaseInfoAsync();

                if (releaseInfo != null && releaseInfo.Version != null)
                {
                    // Сравниваем версии
                    if (IsNewerVersionAvailable(releaseInfo.Version))
                    {
                        if (MessageBox.Show("Доступна новая версия. Обновить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            if (releaseInfo.DownloadUrl == null)
                            {
                                Debug.WriteLine("VersionService.CheckVersionAsync: Ссылка на скачивание оказалась null.");
                                MessageBox.Show("Не удалось получить ссылку на скачивание установщика.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            // Скачиваем MSI-файл
                            string? installerPath = await DownloadInstallerAsync(releaseInfo.DownloadUrl);

                            if (!string.IsNullOrEmpty(installerPath))
                            {
                                // Запускаем установщик
                                InstallInstaller(installerPath);
                            }
                            else
                            {
                                Debug.WriteLine("VersionService.CheckVersionAsync: Путь к установщику оказался null.");
                                MessageBox.Show("Не удалось скачать установщик.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("VersionService.CheckVersionAsync: Информация о версии оказалась null.");
                    MessageBox.Show("Не удалось получить информацию о версии.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Debug.WriteLine("VersionService.CheckVersionAsync: Отсутствует подключение к инетренету.");
                MessageBox.Show("Нет доступа к сети.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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
        /// <param name="latestVersion"></param>
        /// <returns>Возвращает true, если доступная версия новее текущей, иначе false</returns>
        private bool IsNewerVersionAvailable(string latestVersion)
        {
            // Сначала преобразуем строки версий в объекты Version
            if (latestVersion != null && Version.TryParse(_currentVersion, out Version? currentVersion) && Version.TryParse(latestVersion, out Version? newVersion))
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
        /// Читает параметры подключения из App.config.
        /// </summary>
        /// <returns>
        /// Объект, содержащий параметры подключения, или null, если не удалось прочитать настройки.
        /// </returns>
        private static ConnectionSettings? ReadOldConnectionSettings()
        {
            try
            {
                // Получаем значения из App.config
                string? pgServer = ConfigurationManager.AppSettings["pgServer"];
                string? pgDataBase = ConfigurationManager.AppSettings["pgDatabase"];
                string? pgPassword = ConfigurationManager.AppSettings["pgPassword"];
                string? pgUser = ConfigurationManager.AppSettings["pgUser"];
                string? pgPort = ConfigurationManager.AppSettings["pgPort"];

                // Проверяем, что все значения существуют
                if (string.IsNullOrEmpty(pgServer) || string.IsNullOrEmpty(pgDataBase) || string.IsNullOrEmpty(pgPassword) || string.IsNullOrEmpty(pgUser) || string.IsNullOrEmpty(pgPort))
                {
                    Console.WriteLine("Не удалось прочитать параметры подключения из App.config");
                    return null;
                }

                // Создаем объект ConnectionSettings
                return new ConnectionSettings
                {
                    pgServer = pgServer,
                    pgDatabase = pgDataBase,
                    pgPassword = pgPassword,
                    pgUser = pgUser,
                    pgPort = pgPort
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении параметров подключения из App.config: {ex.Message}");
                return null;
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

        /// <summary>
        /// Переносит параметры подключения в настройки новой версии приложения.
        /// </summary>
        /// <param name="connectionSettings">Объект, содержащий параметры подключения.</param>
        private static void MigrateConnectionSettings(ConnectionSettings connectionSettings)
        {
            try
            {
                // Проверяем, что объект connectionSettings не null
                if (connectionSettings == null)
                {
                    Console.WriteLine("Невозможно перенести настройки, так как объект connectionSettings равен null");
                    return;
                }

                // Переносим настройки в Settings.Default
                Settings.Default.pgServer = connectionSettings.pgServer;
                Settings.Default.pgDatabase = connectionSettings.pgDatabase;
                Settings.Default.pgPassword = connectionSettings.pgPassword;
                Settings.Default.pgUser = connectionSettings.pgUser;
                Settings.Default.pgPort = connectionSettings.pgPort;

                // Сохраняем изменения
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при переносе параметров подключения: {ex.Message}");
            }
        }

        /// <summary>
        /// Асинхронно скачивает MSI-файл по указанному URL в папку TEMP.
        /// </summary>
        /// <param name="downloadUrl"></param>
        /// <returns>Ссылка на скачанный файл.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        private async Task<string?> DownloadInstallerAsync(string downloadUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(downloadUrl))
                {
                    throw new ArgumentNullException(nameof(downloadUrl), "Ссылка на файл оказалась пустой или равна null.");
                }

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
                Process.Start(installerPath);
            }
            catch (Exception ex)
            {
                // Обработка ошибок при запуске установщика
                Console.WriteLine($"Ошибка при запуске установщика: {ex.Message}");
            }
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
