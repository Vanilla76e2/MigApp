using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MigApp.Services
{
    class AuthenticationService : IAuthenticationService
    {
        private readonly MigDataBaseContext dbc;

        public AuthenticationService(MigDataBaseContext context)
        {
            dbc = context;
        }

        /// <summary>
        /// Асинхронно проверяет возможность подключения к базе данных, используя Entity Framework Core.
        /// </summary>
        /// <returns>
        ///   <c>true</c>, если подключение к базе данных успешно установлено в течение заданного времени ожидания;
        ///   в противном случае - <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Этот метод пытается установить соединение с базой данных, используя текущие настройки DbContext.
        /// Если подключение не может быть установлено в течение 10 секунд, метод возвращает <c>false</c>.
        /// В случае возникновения исключения при попытке подключения, метод также возвращает <c>false</c> и регистрирует информацию об ошибке.
        /// </remarks>
        /// <exception cref="System.Exception">Возникает в случае непредвиденной ошибки при попытке подключения к базе данных.</exception>
        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                var ConTask = dbc.Database.CanConnectAsync();
                if (await Task.WhenAny(ConTask, Task.Delay(TimeSpan.FromSeconds(10))) == ConTask)
                {
                    await ConTask;
                    return true;
                }
                else
                {
                    Debug.WriteLine("AuthenticationService.TestConnectionAsync: Не удалось установить соединение с базой данных: Превышено время ожидания.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"AuthenticationService.TestConnectionAsync: Не удалось установить соединение с базой данных: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AuthenticateAsync()
        {
            return await TestConnectionAsync();
        }
    }
}
