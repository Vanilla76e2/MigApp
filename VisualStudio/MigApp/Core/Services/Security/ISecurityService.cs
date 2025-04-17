using MigApp.MVVM.Model;

namespace MigApp.Core.Services
{
    /// <summary>
    /// Сервис для работы с безопасным хранением и обработкой конфиденциальных данных.
    /// </summary>
    public interface ISecurityService
    {
        /// <summary>
        /// Сохраняет параметры подключения к базе данных в защищенное хранилище.
        /// </summary>
        /// <param name="DatabaseConnectionParameters"> Параметры подключения к базе данных.</param>
        /// <exception cref="ArgumentNullException">
        /// Возникает, если <paramref name="DatabaseConnectionParameters"/> 
        /// равен <see langword="null"/>.
        /// </exception>
        /// <exception cref="SecurityException">
        /// Возникает при ошибке сохранения данных в защищенное хранилище.
        /// </exception>
        void SaveDatabaseSettingsToVault(DatabaseConnectionParameters DatabaseConnectionParameters);

        /// <summary>
        /// Сохраняет параметры учётные данные пользователя в защищенное хранилище.
        /// </summary>
        /// <param name="userCredentials">Учётные данные пользователя.</param>
        /// <exception cref="ArgumentNullException">
        /// Возникает, если <paramref name="userCredentials"/> 
        /// равен <see langword="null"/>.
        /// </exception>
        /// <exception cref="SecurityException">
        /// Возникает при ошибке сохранения данных в защищенное хранилище.
        /// </exception>
        void SaveUserCredentialsToVault(UserCredentials userCredentials);

        /// <summary>
        /// Загружает сохраненные параметры подключения из защищенного хранилища.
        /// </summary>
        /// <returns>
        /// <see cref="DatabaseConnectionParameters"/>.
        /// </returns>
        DatabaseConnectionParameters LoadDatabaseSettingsFromVault();

        /// <summary>
        /// Загружает сохраненные учётные данные из защищенного хранилища.
        /// </summary>
        /// <returns>
        /// <see cref="UserCredentials"/>.
        /// </returns>
        UserCredentials LoadUserCredentialsFromVault();

        /// <summary>
        /// Вычисляет хеш указанного текста с использованием криптографического алгоритма.
        /// </summary>
        /// <param name="text">Текст для хеширования.</param>
        /// <returns>
        /// Хеш-строка в шестнадцатеричном формате или <see langword="null"/>, 
        /// если входная строка была <see langword="null"/>.
        /// </returns>
        /// <remarks>
        /// Реализация должна использовать криптографически стойкий алгоритм хеширования,
        /// такой как SHA-256 или более современный.
        /// </remarks>
        string HashText(string text);

        /// <summary>
        /// Проверяет, соответствует ли указанный текст указанному хешу.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="hashedText"></param>
        /// <returns></returns>
        bool VerifyHash(string text, string hashedText);

    }
}
