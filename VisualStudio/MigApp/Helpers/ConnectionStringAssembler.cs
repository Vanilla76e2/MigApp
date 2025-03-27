using MigApp.MVVM.Model;
using MigApp.Properties;

namespace MigApp.Helpers
{
    public static class ConnectionStringAssembler
    {
        /// <summary>
        /// Собирает строку подключения к базе данных из настроек приложения.
        /// </summary>
        /// <returns>Строка подключения к базе данных.</returns>
        /// <exception cref="Exception">Выбрасывается, если один из параметров подключения пустой.</exception>
        public static string BuildConnectionString(DatabaseConnectionParameters _params)
        {
            ArgumentNullException.ThrowIfNull(_params, $"При попытке собрать строку подключения {typeof(DatabaseConnectionParameters)} оказался null.");
            string server = _params.server ?? throw new ArgumentNullException($"Параметр {nameof(_params.server)} оказался null.");
            string port = _params.port ?? throw new ArgumentNullException($"Параметр {nameof(_params.port)} оказался null.");
            string database = _params.database ?? throw new ArgumentNullException($"Параметр {nameof(_params.database)} оказался null.");
            string user = _params.user ?? throw new ArgumentNullException($"Параметр {nameof(_params.user)} оказался null.");
            string password = _params.password ?? throw new ArgumentNullException($"Параметр {nameof(_params.password)} оказался null.");

            // Собираем строку подключения
            string connectionString = $"Host={server};Port={port};Database={database};Username={user};Password={password}";

            return connectionString;
        }
    }
}
