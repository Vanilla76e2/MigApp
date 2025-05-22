using MigApp.Core.Models;
using MigApp.Core.Security;
using MigApp.Infrastructure.Services.AppLogger;

namespace MigApp.Infrastructure.Services.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly IAppLogger _logger;

        public SecurityService(IAppLogger logger)
        {
            _logger = logger;
        }

        public void SaveDatabaseSettingsToVault(DatabaseConnectionParameters DatabaseConnectionParameters)
        {
            _logger.LogInformation("Сохранение параметров подключения к БД в хранилище");
            try
            {
                DatabaseConnectionParameters encryptedDatabaseConnectionParameters = new DatabaseConnectionParameters
                (
                    EncryptionHelper.Encrypt(DatabaseConnectionParameters.Host ?? string.Empty),
                    EncryptionHelper.Encrypt(DatabaseConnectionParameters.Port ?? string.Empty),
                    EncryptionHelper.Encrypt(DatabaseConnectionParameters.Database ?? string.Empty),
                    EncryptionHelper.Encrypt(DatabaseConnectionParameters.Username ?? string.Empty),
                    EncryptionHelper.Encrypt(DatabaseConnectionParameters.Password ?? string.Empty)
                );

                _logger.LogInformation("Параметры подключения успешно зашифрованы");
                RegistrySecureHelper.SaveDatabaseSettingsToRegistry(encryptedDatabaseConnectionParameters);
                _logger.LogInformation("Параметры подключения сохранены");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при сохранении параметров подключения");
            }
        }

        public void SaveUserCredentialsToVault(UserCredentials userCredentials)
        {
            _logger.LogInformation("Сохранение учётных данных в хранилище");
            try
            {
                UserCredentials encryptedUserAuthData = new UserCredentials
                {
                    Username = EncryptionHelper.Encrypt(userCredentials.Username ?? string.Empty),
                    Password = EncryptionHelper.Encrypt(userCredentials.Password ?? string.Empty)
                };

                _logger.LogInformation("Учётные данные успешно зашифрованы");
                RegistrySecureHelper.SaveUserCredentialsToVault(encryptedUserAuthData);
                _logger.LogInformation("Учётные данные сохранены");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при сохранении параметров подключения");
            }
        }

        public DatabaseConnectionParameters LoadDatabaseSettingsFromVault()
        {
            _logger.LogInformation("Загрузка параметров подключения из хранилища");
            try
            {
                var encryptedDatabaseParams = RegistrySecureHelper.LoadDatabaseSettingsFromRegistry();
                _logger.LogInformation("Зашифрованные данные подключения получены");

                DatabaseConnectionParameters decryptedDatabaseParams = new DatabaseConnectionParameters
                (
                    EncryptionHelper.Decrypt(encryptedDatabaseParams.Host),
                    EncryptionHelper.Decrypt(encryptedDatabaseParams.Port),
                    EncryptionHelper.Decrypt(encryptedDatabaseParams.Database),
                    EncryptionHelper.Decrypt(encryptedDatabaseParams.Username),
                    EncryptionHelper.Decrypt(encryptedDatabaseParams.Password)
                );
                _logger.LogInformation("Данные подключения успешно расшифрованы");

                return decryptedDatabaseParams;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении параметров подключения");
                throw;
            }
        }

        public UserCredentials LoadUserCredentialsFromVault()
        {
            _logger.LogInformation("Загрузка учётных данных из хранилища");

            try
            {
                var encryptedCredentials = RegistrySecureHelper.LoadUserCredentialsFromRegistry();
                _logger.LogInformation("Зашифрованные учётные данные получены");

                UserCredentials decryptedCredentials = new UserCredentials
                {
                    Username = EncryptionHelper.Decrypt(encryptedCredentials.Username),
                    Password = EncryptionHelper.Decrypt(encryptedCredentials.Password)
                };
                _logger.LogInformation("Учётные данные успешно расшифрованы");

                return decryptedCredentials;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении учётных данных");
                throw;
            }
        }

        public string HashText(string text)
        {
            try
            {
                string _hash = BCrypt.Net.BCrypt.HashPassword(text);
                _logger.LogInformation("Строка успешно преобразована в хэш");
                return _hash;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при попытке получить хэш");
                throw;
            }

        }

        public bool VerifyHash(string text, string hashedText)
        {
            _logger.LogDebug($"Проверка хэша");
            try
            {
                bool _res = BCrypt.Net.BCrypt.Verify(text, hashedText);
                _logger.LogInformation($"Резльтат проверки хэша: {_res}");
                return _res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при проверке хеша");
                throw;
            }
        }
    }
}
