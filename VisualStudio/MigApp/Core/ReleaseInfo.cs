namespace MigApp.Core
{
    public record ReleaseInfo
    {
        /// <summary>
        /// Версия релиза.
        /// </summary>
        public string? Version { get; set; }

        /// <summary>
        /// Признак предварительной версии (pre-release).
        /// </summary>
        public bool? IsPreRelease { get; set; }

        /// <summary>
        /// URL для скачивания MSI-пакета.
        /// </summary>
        public string? DownloadUrl { get; set; }
    }
}
