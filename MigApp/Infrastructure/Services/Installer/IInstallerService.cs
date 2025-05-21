namespace MigApp.Infrastructure.Services.Installer
{
    /// <summary>
    /// Интерфейс сервиса для установки приложений.
    /// </summary>
    public interface IInstallerService
    {
        /// <summary>
        /// Асинхронно запускает процесс установки из указанного файла.
        /// </summary>
        /// <param name="filePath">Путь к файлу установщика.</param>
        void Install(string filePath);
    }
}
