using MigApp.Infrastructure.Services.AppLogger;
using MigApp.Infrastructure.Services.DnsResolver;
using System.IO;
using System.Net.Http;

namespace MigApp.Infrastructure.Services.Internet
{
    /// <summary>
    /// Реализация сервиса <see cref="IInternetService"/>
    /// </summary>
    public class InternetService : IInternetService
    {
        private readonly IDnsResolver _dnsResolver;
        private readonly IAppLogger _logger;
        private readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="InternetService"/>.
        /// </summary>
        public InternetService(IDnsResolver dnsResolver, IAppLogger logger)
        {
            _dnsResolver = dnsResolver;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<bool> HasInternetConnectionAsync()
        {
            try
            {
                _logger.LogInformation("Проверка подключения к интернету...");
                await _dnsResolver.GetHostEntryAsync("8.8.8.8");
                _logger.LogInformation("Подключение к интернету доступно");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Отсутствует подключение к интернету");
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> GetHttpResponseAsync(string uri, string headerName, string? value)
        {
            _logger.LogInformation($"Начало HTTP-запроса к {uri} с заголовком {headerName}");

            ArgumentNullException.ThrowIfNull(uri, nameof(uri));
            ArgumentNullException.ThrowIfNull(headerName, nameof(headerName));

            try
            {
                _httpClient.BaseAddress = new Uri(uri);
                _httpClient.DefaultRequestHeaders.Add(headerName, value);

                var response = await _httpClient.GetAsync("");
                _logger.LogInformation($"HTTP-запрос к {uri} завершен со статусом {(int)response.StatusCode}");

                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Ошибка HTTP-запроса к {uri}");
                throw;
            }
            catch (UriFormatException ex)
            {
                _logger.LogError(ex, $"Некорректный URI: {uri}");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<string?> DownloadAppInstallerAsync(string downloadUrl)
        {
            _logger.LogInformation($"Начало загрузки установщика из {downloadUrl}");

            try
            {
                ArgumentNullException.ThrowIfNull(downloadUrl, nameof(downloadUrl));

                string tmpmsi = Path.GetTempPath() + "MigAppInstaller.msi";
                _logger.LogDebug($"Временный путь для установщика: {tmpmsi}");

                using (var response = await _httpClient.GetAsync(downloadUrl, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();
                    _logger.LogInformation($"Файл успешно загружен с {downloadUrl}");

                    using (var streamToReadFrom = await response.Content.ReadAsStreamAsync())
                    using (var streamToWriteTo = File.Open(tmpmsi, FileMode.Create))
                    {
                        await streamToReadFrom.CopyToAsync(streamToWriteTo);
                        _logger.LogInformation($"Файл сохранен по пути: {tmpmsi}");
                    }
                }

                return tmpmsi;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Ошибка загрузки файла с {downloadUrl}");
                return null;
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, $"Ошибка сохранения файла из {downloadUrl}");
                return null;
            }
        }
    }
}
