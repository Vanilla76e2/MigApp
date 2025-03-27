using MigApp.Helpers;
using MigApp.MVVM.Model;

namespace MigApp.Core.Services
{
    internal class SecurityService : ISecurityService
    {
        public void SaveDataToVault(DatabaseConnectionParameters databaseConnectionParameters, UserAuthData userAuthData)
        {
            DatabaseConnectionParameters encryptedDatabaseConnectionParameters = new DatabaseConnectionParameters
            {
                server = EncryptionHelper.Encrypt(databaseConnectionParameters.server ?? string.Empty),
                port = EncryptionHelper.Encrypt(databaseConnectionParameters.port ?? string.Empty),
                database = EncryptionHelper.Encrypt(databaseConnectionParameters.database ?? string.Empty),
                user = EncryptionHelper.Encrypt(databaseConnectionParameters.user ?? string.Empty),
                password = EncryptionHelper.Encrypt(databaseConnectionParameters.password ?? string.Empty)
            };

            UserAuthData encryptedUserAuthData = new UserAuthData
            {
                username = EncryptionHelper.Encrypt(userAuthData.username ?? string.Empty),
                password = EncryptionHelper.Encrypt(userAuthData.password ?? string.Empty)
            };

            RegistrySecureHelper.SaveToRegistry(encryptedDatabaseConnectionParameters, encryptedUserAuthData);
        }

        public (DatabaseConnectionParameters, UserAuthData) LoadDataFromVault() 
        {
            var (encryptedDatabaseParams, encryptedAuthData) = RegistrySecureHelper.LoadFromRegistry();

            DatabaseConnectionParameters DatabaseConnectionParameters = new DatabaseConnectionParameters
            {
                server = EncryptionHelper.Decrypt(encryptedDatabaseParams.server),
                port = EncryptionHelper.Decrypt(encryptedDatabaseParams.port),
                database = EncryptionHelper.Decrypt(encryptedDatabaseParams.database),
                user = EncryptionHelper.Decrypt(encryptedDatabaseParams.user),
                password = EncryptionHelper.Decrypt(encryptedDatabaseParams.password)
            };

            UserAuthData userAuthData = new UserAuthData
            {
                username = EncryptionHelper.Decrypt(encryptedAuthData.username),
                password = EncryptionHelper.Decrypt(encryptedAuthData.password)
            };

            return (DatabaseConnectionParameters, userAuthData);
        }

        public string HashText(string text)
        {
            return BCrypt.Net.BCrypt.HashPassword(text);
        }
    }
}
