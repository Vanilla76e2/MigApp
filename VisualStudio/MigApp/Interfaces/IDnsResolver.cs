using System.Net;

namespace MigApp.Interfaces
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

    /// <summary>
    /// Реализация интерфейса <see cref="IDnsResolver"/>, использующая стандартный класс <see cref="Dns"/> для разрешения DNS-имен.
    /// </summary>
    public class DnsResolver : IDnsResolver
    {
        /// <inheritdoc />
        public async Task<IPHostEntry> GetHostEntryAsync(string hostNameOrAddress)
        {
            // Проверка аргумента на null
            if (string.IsNullOrEmpty(hostNameOrAddress))
            {
                throw new ArgumentNullException(nameof(hostNameOrAddress));
            }

            // Вызов асинхронного метода Dns.GetHostEntryAsync
            return await Dns.GetHostEntryAsync(hostNameOrAddress);
        }
    }
}
