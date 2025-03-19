using MigApp.Properties;

namespace MigApp.Helpers
{
    public class ConnectionStringAssembler
    {
        /// <summary>
        /// Собирает строку подключения к базе данных из настроек приложения.
        /// </summary>
        /// <returns>Строка подключения к базе данных.</returns>
        /// <exception cref="Exception">Выбрасывается, если один из параметров подключения пустой.</exception>
        public string BuildConnectionString(ISettings settings)
        {
            string server = settings.pgServer;
            string port = settings.pgPort;
            string user = settings.pgUser;
            string password = settings.pgPassword;
            string database = settings.pgDatabase;

            // Проверка на пустые значения
            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(port) || string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(database))
            {
                throw new Exception("Не все параметры подключения к базе данных заполнены");
            }

            // Собираем строку подключения
            string connectionString = $"Host={server};Port={port};Database={database};Username={user};Password={password}";

            return connectionString;
        }
    }
}
