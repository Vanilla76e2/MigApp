using System.Net;

namespace MigApp.Core.Services
{
    /// <summary>
    /// Интерфейс для разрешения DNS-имен в IP-адреса.
    /// </summary>
    public interface IDnsResolver
    {
        /// <summary>
        /// Асинхронно разрешает DNS-имя в IP-адрес.
        /// </summary>
        /// <param name="hostNameOrAddress">DNS-имя или IP-адрес для разрешения.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Возвращает IPHostEntry с информацией об IP-адресе.</returns>
        /// <exception cref="ArgumentNullException">Возникает, если <paramref name="hostNameOrAddress"/> равен null или пустой строке.</exception>
        /// <exception cref="SocketException">Возникает, если не удалось разрешить DNS-имя.</exception>
        Task<IPHostEntry> GetHostEntryAsync(string hostNameOrAddress);
    }
}
