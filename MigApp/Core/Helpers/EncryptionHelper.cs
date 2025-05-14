using System.Security.Cryptography;
using System.Text;

namespace MigApp.Core.Helpers
{
    /// <summary>
    /// Вспомогательный класс для шифрования данных.
    /// </summary>
    public static class EncryptionHelper
    {
        private const string KeyContainerName = "MigAppKeyConteiner";
        private const CspProviderFlags KeyFlags = CspProviderFlags.UseMachineKeyStore;

        /// <summary>
        /// Генерирует новый ключ (если его нет) и возвращает публичную часть.
        /// </summary>
        /// <returns>Публичный ключ для шифрования.</returns>
        private static string GetOrCreatePublicKey()
        {
            using var rsa =
                new RSACryptoServiceProvider
                (
                    new CspParameters
                    {
                        KeyContainerName = KeyContainerName,
                        Flags = KeyFlags
                    }
                )
                {
                    PersistKeyInCsp = true // Сохранить ключ в контейнере
                };

            return rsa.ToXmlString(includePrivateParameters: false);
        }

        /// <summary>
        /// Шифрует данные с помощью публичного ключа.
        /// </summary>
        /// <returns>Зашифрованная строка.</returns>
        public static string Encrypt(string plainText)
        {
            ArgumentNullException.ThrowIfNull(plainText, nameof(plainText));
            using var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(GetOrCreatePublicKey());

            byte[] dataBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes = rsa.Encrypt(dataBytes, fOAEP: true);
            return Convert.ToBase64String(encryptedBytes);
        }

        /// <summary>
        /// Дешифрует данные с помощью закрытого ключа из контейнера.
        /// </summary>
        /// <returns>Дешифрованная строка.</returns>
        public static string Decrypt(string? encryptedText)
        {

            if (string.IsNullOrWhiteSpace(encryptedText)) return string.Empty;

            using var rsa =
                new RSACryptoServiceProvider(
                new CspParameters
                {
                    KeyContainerName = KeyContainerName,
                    Flags = KeyFlags
                });

            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, fOAEP: true);
            return Encoding.UTF8.GetString(decryptedBytes);
        }

        /// <summary>
        /// Удаляет ключ из контейнера (очистка).
        /// </summary>
        public static void DeleteKeyFromContainer()
        {
            using var rsa = new RSACryptoServiceProvider(
                new CspParameters
                {
                    KeyContainerName = KeyContainerName,
                    Flags = KeyFlags
                })
            {
                PersistKeyInCsp = false
            };
            rsa.Clear();
        }
    }
}
