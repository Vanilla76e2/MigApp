
namespace MigApp.Core.Services.AppUpdate
{
    interface IAppUpdateService
    {
        /// <summary>
        /// Начинает процесс обновления приложения.
        /// </summary>
        Task UpdateApplication();

        /// <summary>
        /// Сравнивает полученную версию с текущей.
        /// </summary>
        /// <returns>Возвращает ture если полученная версия новее, false если установлена последняя версия.</returns>
        bool IsNewerVersionAvailable(ReleaseInfo releaseInfo);

        Task<ReleaseInfo?> GetLatestReleaseInfoAsync();
    }
}
