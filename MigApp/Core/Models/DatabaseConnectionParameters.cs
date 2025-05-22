using Npgsql;
using System.Text.Json.Serialization;

namespace MigApp.Core.Models
{
    /// <summary>
    /// Запись, содержащая параметры подключения к БД.
    /// </summary>
    public class DatabaseConnectionParameters
    {
        public string Host { get; }
        public string Port { get; }
        public string Database { get; }
        public string Username { get; }
        public string Password { get; }

        [JsonConstructor]
        public DatabaseConnectionParameters(string host = "", string port = "", string database = "", string username = "", string password = "")
        {
            Host = host;
            Port = port;
            Database = database;
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Метод собирающий параметры в строку подключения к pgSQL.
        /// </summary>
        /// <returns>Возвращает строку подключения к pgSQL.</returns>
        public string ToConnectionString()
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = Host,
                Port = int.TryParse(Port, out var port) ? port : 5432,
                Database = Database,
                Username = Username,
                Password = Password
            };
            return builder.ToString();
        }

        /// <summary>
        /// Метод собирающий параметры в строку подключения к pgSQL со скрытым паролем.
        /// </summary>
        /// <returns>Возвращает строку подключения к pgSQL со скрытым паролем.</returns>
        /// <remarks>
        /// Используется для отображения в логах.
        /// <c>Не использовать</c> для реального подключения к базе данных.
        /// </remarks>
        public string ToSecureConnectionString()
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = Host,
                Port = int.TryParse(Port, out var port) ? port : 5432,
                Database = Database,
                Username = Username,
                Password = "*****"
            };
            return builder.ToString();
        }
    }
}
