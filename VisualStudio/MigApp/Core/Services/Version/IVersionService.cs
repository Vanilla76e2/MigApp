namespace MigApp.Core.Services
{
    interface IVersionService
    {
        /// <summary>
        /// Сервис для проверки подключения к интернету и версии приложения.
        /// </summary>
        public interface IVersionService
        {
            /// <summary>
            /// Асинхронно проверяет наличие интернет-соединения.
            /// </summary>
            /// <returns></returns>
            Task HasInternetConnectionAsync();

            /// <summary>
            /// Асинхронно получает информацию о последнем релизе.
            /// </summary>
            /// <returns></returns>
            Task<ReleaseInfo?> GetLatestReleaseInfoAsync();

            /// <summary>
            /// Проверяет, доступно ли новое обновление.
            /// </summary>
            /// <returns></returns>
            bool IsNewerVersionAvailable();

            /// <summary>
            /// Асинхронно обновляет приложение.
            /// </summary>
            /// <returns></returns>
            Task UpdateApplicationAsync();
        }
    }
}
