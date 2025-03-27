using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;

namespace MigApp.Core.Services
{
    /// <summary>
    /// Сервис для проверки наличия интернет-соединения.
    /// </summary>
    internal class InternetService : IInternetService
    {
        private readonly IDnsResolver _dnsResolver;
        private readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="InternetService"/>.
        /// </summary>
        /// <param name="dnsResolver">Объект, реализующий интерфейс <see cref="IDnsResolver"/> для разрешения DNS-имен.</param>
        /// <exception cref="ArgumentNullException">Возникает, если <paramref name="dnsResolver"/> равен null.</exception>
        public InternetService (IDnsResolver dnsResolver)
        {
            _dnsResolver = dnsResolver;
        }

        /// <summary>
        /// Асинхронно проверяет наличие интернет-соединения путем разрешения IP-адреса 8.8.8.8 (Google Public DNS).
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию. Возвращает true, если есть интернет-соединение, иначе false.</returns>
        /// <exception cref="WebException">Возникает, если не удалось разрешить DNS-имя (например, при отсутствии интернет-соединения).</exception>
        public async Task<bool> HasInternetConnectionAsync()
        {
            try
            {
                await _dnsResolver.GetHostEntryAsync("8.8.8.8");
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Выполняет HTTP GET-запрос к указанному URI с заданными заголовками.
        /// </summary>
        /// <param name="uri">Базовый URI для запроса</param>
        /// <param name="headerName">Имя добавляемого заголовка</param>
        /// <param name="values">Значения заголовка</param>
        /// <returns>Объект HttpResponseMessage с ответом сервера</returns>
        /// <exception cref="ArgumentNullException">Выбрасывается, если uri, headerName или values равны null</exception>
        /// <exception cref="HttpRequestException">Выбрасывается при неудачном запросе (Status Code не 2XX)</exception>
        public async Task<HttpResponseMessage> GetHttpResponseAsync(string uri, string headerName, string? value)
        {
            ArgumentNullException.ThrowIfNull(uri, nameof(uri));
            ArgumentNullException.ThrowIfNull(headerName, nameof(uri));

            _httpClient.BaseAddress = new Uri(uri);
            _httpClient.DefaultRequestHeaders.Add(headerName, value);

            HttpResponseMessage response = await _httpClient.GetAsync("");
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
