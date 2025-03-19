using MigApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Converters;

namespace MigApp.Services
{
    /// <summary>
    /// Сервис для проверки наличия интернет-соединения.
    /// </summary>
    public class InternetService
    {
        private readonly IDnsResolver _dnsResolver;

        public InternetService()
        {
            _dnsResolver = new DnsResolver();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="InternetService"/>.
        /// </summary>
        /// <param name="dnsResolver">Объект, реализующий интерфейс <see cref="IDnsResolver"/> для разрешения DNS-имен.</param>
        /// <exception cref="ArgumentNullException">Возникает, если <paramref name="dnsResolver"/> равен null.</exception>
        public InternetService (IDnsResolver dnsResolver)
        {
            _dnsResolver = dnsResolver ?? throw new ArgumentNullException(nameof(dnsResolver));
        }

        /// <summary>
        /// Асинхронно проверяет наличие интернет-соединения путем разрешения IP-адреса 8.8.8.8 (Google Public DNS).
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию. Возвращает true, если есть интернет-соединение, иначе false.</returns>
        /// <exception cref="WebException">Возникает, если не удалось разрешить DNS-имя (например, при отсутствии интернет-соединения).</exception>
        public async Task<bool> InternetCheckerAsync()
        {
            try
            {
                await _dnsResolver.GetHostEntryAsync("8.8.8.8");
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
