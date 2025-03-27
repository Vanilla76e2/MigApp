using System.Net.Http;
using System.Reflection;
using System.Text.Json;

namespace MigApp.Core.Services
{
    internal class VersionService : IVersionService
    {
        private readonly IInternetService _internetService;
        private string _currentVersion;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="VersionService"/>.
        /// </summary>
        /// <param name="internetService">Сервис для проверки интернет-соединения.</param>
        /// <param name="httpClient">HttpClient для выполнения HTTP-запросов.</param>
        /// <exception cref="ArgumentNullException">Возникает, если <paramref name="internetService"/> равен null.</exception>
        public VersionService(IInternetService internetService)
        {
            _internetService = internetService;
            _currentVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "0.0.0";
        }

        public string GetCurrentVersion()
        {
            return _currentVersion;
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
                HttpResponseMessage response = await _internetService.GetHttpResponseAsync("https://api.github.com/repos/Vanilla76e2/MigApp/releases", "User-Agent", "MigApp");

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
                            if (!release.TryGetProperty("prerelease", out JsonElement preReleaseElement) || preReleaseElement.ValueKind != JsonValueKind.False)
                            {
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
                                            break;
                                        }
                                    }
                                }
                            }

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

                        return latestReleaseInfo;
                    }
                    else
                    {
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
    }
}
