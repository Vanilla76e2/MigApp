using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Core.Services
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
