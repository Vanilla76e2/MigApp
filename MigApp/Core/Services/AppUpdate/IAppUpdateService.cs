
namespace MigApp.Core.Services.AppUpdate
{
    /// <summary>
    /// Сервис для управления процессом обновления приложения.
    /// </summary>
    public interface IAppUpdateService
    {
        /// <summary>
        /// Асинхронно выполняет полный процесс обновления приложения.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию обновления.</returns>
        Task UpdateApplicationAsync();
    }
}
