using MigApp.Core.Models;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.Infrastructure.Services.Internet;
using MigApp.Properties;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;

namespace MigApp.Infrastructure.Services.Version
{
    public class VersionService : IVersionService
    {
        private readonly IInternetService _internetService;
        private readonly IAppLogger _logger;
        private string _currentVersion;
        private string releasesUrl = AppConstants.Urls.ReleasesLink;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="VersionService"/>.
        /// </summary>
        /// <param name="internetService">Сервис для проверки интернет-соединения.</param>
        /// <param name="httpClient">HttpClient для выполнения HTTP-запросов.</param>
        /// <exception cref="ArgumentNullException">Возникает, если <paramref name="internetService"/> равен null.</exception>
        public VersionService(IInternetService internetService, IAppLogger logger)
        {
            _internetService = internetService;
            _currentVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "0.0.0";
            _logger = logger;
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
                _logger.LogInformation($"Отправка запроса к GitHub API: {releasesUrl}");
                HttpResponseMessage response = await _internetService.GetHttpResponseAsync(releasesUrl, "User-Agent", "MigApp");

                _logger.LogInformation("Получен ответ от GitHub API. Чтение содержимого...");
                string json = await response.Content.ReadAsStringAsync();
                _logger.LogDebug($"Полученные JSON-данные (первые 100 символов): {json[..Math.Min(100, json.Length)]}...");

                using (JsonDocument document = JsonDocument.Parse(json))
                {
                    JsonElement root = document.RootElement;

                    if (root.ValueKind != JsonValueKind.Array)
                    {
                        _logger.LogWarning("Получен неожиданный формат ответа от GitHub API");
                        return null;
                    }

                    foreach (JsonElement release in root.EnumerateArray())
                    {
                        if (release.TryGetProperty("prerelease", out JsonElement preReleaseElement) && preReleaseElement.GetBoolean())
                        {
                            _logger.LogDebug($"Пропуск pre-release версии: {release.GetProperty("tag_name").GetString()}");
                            continue;
                        }

                        string? version = release.GetProperty("tag_name").GetString();
                        string? downloadUrl = null;

                        _logger.LogDebug($"Обработка релиза {version}");

                        if (release.TryGetProperty("assets", out JsonElement assets) && assets.ValueKind == JsonValueKind.Array)
                        {
                            foreach (JsonElement asset in assets.EnumerateArray())
                            {
                                string? assetName = asset.GetProperty("name").GetString();
                                if (assetName != null && assetName.EndsWith(".msi", StringComparison.OrdinalIgnoreCase))
                                {
                                    downloadUrl = asset.GetProperty("browser_download_url").GetString();
                                    _logger.LogDebug($"Найден MSI-установщик: {downloadUrl}");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            _logger.LogDebug($"MSI-установщик не найден для релиза {version}");
                        }

                        if (version != null)
                        {
                            _logger.LogInformation($"Получена информация о релизе: {version}");
                            return new ReleaseInfo
                            {
                                Version = version,
                                IsPreRelease = false,
                                DownloadUrl = downloadUrl
                            };
                        }
                    }

                    _logger.LogWarning("Не найдено подходящих релизов");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Ошибка HTTP при запросе к GitHub API. StatusCode: {ex.StatusCode}");
                return null;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Ошибка парсинга JSON-ответа от GitHub API");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Критическая ошибка при получении информации о релизе");
                return null;
            }
        }
    }
}
