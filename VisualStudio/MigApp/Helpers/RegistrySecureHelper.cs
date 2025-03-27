using Microsoft.Win32;
using MigApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MigApp.Helpers
{
    public static class RegistrySecureHelper
    {
        private const string RegPath = @"HKEY_CURRENT_USER\Software\MigApp\Credentials";

        /// <summary>
        /// Сохраняет данные в реестре.
        /// </summary>
        /// <param name="dbParams"></param>
        /// <param name="userAuthData"></param>
        public static void SaveToRegistry(DatabaseConnectionParameters dbParams, UserAuthData userAuthData)
        {
            var dataToSave = new
            {
                DatabaseConnectionParameters = dbParams,
                AuthData = userAuthData
            };

            string json = JsonSerializer.Serialize(dataToSave);
            Registry.SetValue(RegPath, "AppSavedData", json);
        }

        /// <summary>
        /// Загружает данные из реестра.
        /// </summary>
        /// <returns>Возвращает <see cref="DatabaseConnectionParameters"/> и <see cref="UserAuthData"/>.</returns>
        public static (DatabaseConnectionParameters, UserAuthData) LoadFromRegistry()
        {
            string? json = Registry.GetValue(RegPath, "AppSavedData", null) as string;
            
            if (string.IsNullOrEmpty(json)) return (new(), new());

            try
            {
                var data = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
                if (data == null)
                {
                    return (new DatabaseConnectionParameters(), new UserAuthData());
                }

                DatabaseConnectionParameters? dbParams = data.TryGetValue("DatabaseConnectionParameters", out var dbElement)
                    ? SafeDeserialize<DatabaseConnectionParameters>(dbElement.ToString()!) ?? new()
                    : new();

                UserAuthData? authData = data.TryGetValue("AuthData", out var authElement)
                    ? SafeDeserialize<UserAuthData>(authElement.ToString()!) ?? new()
                    : new();

                return (dbParams, authData);
            }
            catch
            {
                return (new DatabaseConnectionParameters(), new UserAuthData());
            }
        }

        /// <summary>
        /// Безопасный метод десериализации JSON-строки.
        /// </summary>
        private static T? SafeDeserialize<T>(string json) where T : class, new()
        {
            try
            {
                return JsonSerializer.Deserialize<T>(json) ?? new T();
            }
            catch
            {
                return new T();
            }
        }
    }
}
