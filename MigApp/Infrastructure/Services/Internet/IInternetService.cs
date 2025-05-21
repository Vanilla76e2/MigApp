using System.Net.Http;

namespace MigApp.Infrastructure.Services.Internet
{
    /// <summary>
    /// Сервис для работы с интернет-соединением и выполнения HTTP-запросов.
    /// </summary>
    public interface IInternetService
    {
        /// <summary>
        /// Проверяет наличие активного интернет-соединения.
        /// </summary>
        /// <returns>
        /// Задача, возвращающая <see langword="true"/>, если интернет-соединение активно;
        /// <see langword="false"/> в противном случае.
        /// </returns>
        Task<bool> HasInternetConnectionAsync();

        /// <summary>
        /// Выполняет HTTP-запрос к указанному URI с заданным заголовком.
        /// </summary>
        /// <param name="uri">URI для запроса.</param>
        /// <param name="headerName">Имя HTTP-заголовка.</param>
        /// <param name="value">Значение HTTP-заголовка. Может быть <see langword="null"/>.</param>
        /// <returns>Ответ сервера в виде <see cref="HttpResponseMessage"/>.</returns>
        Task<HttpResponseMessage> GetHttpResponseAsync(string uri, string headerName, string? value);

        /// <summary>
        /// Скачивает файл установщика приложения по указанному URL.
        /// </summary>
        /// <param name="downloadUrl">URL для скачивания файла.</param>
        /// <returns>
        /// Путь к скачанному файлу или <see langword="null"/>, если загрузка не удалась.
        /// </returns>
        Task<string?> DownloadAppInstallerAsync(string downloadUrl);
    }
}
