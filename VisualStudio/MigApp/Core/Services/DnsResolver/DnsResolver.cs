using System.Net;

namespace MigApp.Core.Services
{
    /// <summary>
    /// Реализация интерфейса <see cref="IDnsResolver"/>, использующая стандартный класс <see cref="Dns"/> для разрешения DNS-имен.
    /// </summary>
    internal class DnsResolver : IDnsResolver
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
