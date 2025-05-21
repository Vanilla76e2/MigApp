using MigApp.Infrastructure.Data;

namespace MigApp.Infrastructure.Services.DatabaseContextProvider
{
    /// <summary>
    /// Интерфейс для предоставления контекста базы данных.
    /// </summary>
    public interface IDbContextProvider : IDisposable
    {
        /// <summary>
        ///  Метод для получения контекста базы данных.
        /// </summary>
        /// <returns><see cref="MigDatabaseContext"/> в качестве контекста базы данных.</returns>
        MigDatabaseContext GetContext();

        /// <summary>
        /// Сбрасывает контекст базы данных.
        /// </summary>
        void ResetContext();
    }
}
