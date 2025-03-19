using MigApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Interfaces
{
    interface IVersionService
    {
        /// <summary>
        /// Сервис для проверки подключения к интернету и версии приложения.
        /// </summary>
        public interface IVersionService
        {
            /// <summary>
            /// Асинхронно проверяет наличие обновления.
            /// </summary>
            Task CheckVersionAsync();
        }
    }
}
