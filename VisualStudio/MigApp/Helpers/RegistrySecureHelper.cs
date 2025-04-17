using Microsoft.Win32;
using MigApp.MVVM.Model;
using System.Text.Json;

namespace MigApp.Helpers
{
    public static class RegistrySecureHelper
    {
        private const string RegPath = @"HKEY_CURRENT_USER\Software\MigApp\Credentials";

        /// <summary>
        /// Сохраняет параметры подключения к базе данных в реестре.
        /// </summary>
        /// <param name="dbParams">Набор параметров для подключения к базе данных.</param>
        public static void SaveDatabaseSettingsToRegistry(DatabaseConnectionParameters dbParams)
        {
            string json = JsonSerializer.Serialize(dbParams);
            Registry.SetValue(RegPath, "AppDatabaseSettings", json);
        }

        /// <summary>
        /// Сохраняет учётные данные пользователя в реестре.
        /// </summary>
        /// <param name="userCredetials">Учётные данные пользователя.</param>
        public static void SaveUserCredentialsToVault(UserCredentials userCredetials)
        {
            string json = JsonSerializer.Serialize(userCredetials);
            Registry.SetValue(RegPath, "UserCredentials", json);
        }

        /// <summary>
        /// Загружает настройки подключения к базе данных из реестра.
        /// </summary>
        /// <returns><see cref="DatabaseConnectionParameters"/>.</returns>
        public static DatabaseConnectionParameters LoadDatabaseSettingsFromRegistry()
        {
            string? json = Registry.GetValue(RegPath, "AppDatabaseSettings", null) as string;
            if (string.IsNullOrEmpty(json)) return new();

            try
            {
                var dbParams = JsonSerializer.Deserialize<DatabaseConnectionParameters>(json);
                return dbParams ?? new DatabaseConnectionParameters();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new DatabaseConnectionParameters();
            }
        }

        /// <summary>
        /// Загружает учётные данные из реестра.
        /// </summary>
        /// <returns><see cref="UserCredentials"/>.</returns>
        public static UserCredentials LoadUserCredentialsFromRegistry()
        {
            string? json = Registry.GetValue(RegPath, "userCredentials", null) as string;

            if (string.IsNullOrEmpty(json)) return new();

            try
            {
                var credentials = JsonSerializer.Deserialize<UserCredentials>(json);
                return credentials ?? new();
            }
            catch
            {
                return new UserCredentials();
            }
        }

        /// <summary>
        /// Безопасный метод десериализации JSON-строки.
        /// </summary>
        private static T? SafeDeserialize<T>(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(json);
            }
            catch
            {
                return default;
            }
        }
    }
}
