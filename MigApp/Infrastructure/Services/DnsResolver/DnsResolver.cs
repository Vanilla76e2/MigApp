using MigApp.Infrastructure.Services.AppLogger;
using System.Net;
using System.Net.Sockets;

namespace MigApp.Infrastructure.Services.DnsResolver
{
    /// <summary>
    /// Реализация интерфейса <see cref="IDnsResolver"/>, использующая стандартный класс <see cref="Dns"/> для разрешения DNS-имен.
    /// </summary>
    public class DnsResolver : IDnsResolver
    {
        public readonly IAppLogger _logger;

        public DnsResolver(IAppLogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<IPHostEntry> GetHostEntryAsync(string hostNameOrAddress)
        {
            _logger.LogDebug($"Начало разрешения DNS для хоста или адреса: {hostNameOrAddress}");
            // Проверка аргумента на null
            if (string.IsNullOrWhiteSpace(hostNameOrAddress))
            {
                _logger.LogError($"Передан пустой или null аргумент {nameof(hostNameOrAddress)}");
                throw new ArgumentNullException(nameof(hostNameOrAddress));
            }

            try
            {
                var result = await Dns.GetHostEntryAsync(hostNameOrAddress);
                _logger.LogInformation($"Успешно разрешён DNS для {hostNameOrAddress}. Получено: {result.HostName} ({string.Join(", ", result.AddressList.Select(a => a.ToString()))})");
                return result;
            }
            catch (SocketException ex)
            {
                _logger.LogError(ex, $"Ошибка разрешения DNS для {hostNameOrAddress}. Код ошибки: {ex.SocketErrorCode}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Критическая ошибка при обработке DNS-запроса для {hostNameOrAddress}");
                throw;
            }
        }
    }
}
