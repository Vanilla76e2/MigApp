namespace MigApp.Core.Services
{
    /// <summary>
    /// Сервис для проверки подключения к интернету и версии приложения.
    /// </summary>
    public interface IVersionService
    {
        /// <summary>
        /// Асинхронно получает информацию о последнем релизе.
        /// </summary>
        /// <returns></returns>
        Task<ReleaseInfo?> GetLatestReleaseInfoAsync();

        string GetCurrentVersion();
    }
}
