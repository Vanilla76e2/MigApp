using MigApp.Core.Models;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.Infrastructure.Services.Installer;
using MigApp.Infrastructure.Services.Internet;
using MigApp.Infrastructure.Services.Version;
using MigApp.UI.Services.UINotification;

namespace MigApp.Infrastructure.Services.AppUpdate
{
    public class AppUpdateService : IAppUpdateService
    {
        private readonly IVersionService _versionService;
        private readonly IUINotificationService _niotificationService;
        private readonly IInternetService _internetService;
        private readonly IInstallerService _installerService;
        private readonly IAppLogger _logger;
        private ReleaseInfo? _latestReleaseInfo;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AppUpdateService"/>.
        /// </summary>
        /// <param name="versionService">Сервис для работы с версиями приложения.</param>
        /// <param name="notificationService">Сервис для взаимодействия с пользователем.</param>
        /// <param name="logger">Сервис логирования.</param>
        /// <param name="installer">Сервис для установки приложений.</param>
        /// <param name="internet">Сервис для работы с интернет-соединением.</param>
        public AppUpdateService(IVersionService versionService, IUINotificationService notificationService, IAppLogger logger, IInstallerService installer, IInternetService internet)
        {
            _versionService = versionService;
            _niotificationService = notificationService;
            _installerService = installer;
            _internetService = internet;
            _logger = logger;
        }

        /// <summary>
        /// Получает информацию о последнем релизе из сервиса версий.
        /// </summary>
        private async Task GetLatestReleaseInfoAsync()
        {
            try
            {
                _logger.LogDebug($"Запрос информации о последнем релизе");
                _latestReleaseInfo = await _versionService.GetLatestReleaseInfoAsync();
                _logger.LogInformation($"Получена информация о релизе: {_latestReleaseInfo?.Version ?? "null"}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении информации о релизе");
                throw;
            }
        }


        /// <summary>
        /// Проверяет, доступна ли новая версия приложения.
        /// </summary>
        /// <param name="releaseInfo">Информация о релизе для проверки.</param>
        /// <returns>
        /// <see langword="true"/> если доступна новая версия; иначе <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Возникает, если <paramref name="releaseInfo"/> равен null.
        /// </exception>
        private bool IsNewerVersionAvailable(ReleaseInfo releaseInfo)
        {
            ArgumentNullException.ThrowIfNull(releaseInfo, nameof(releaseInfo));
            _logger.LogDebug($"Текущая версия={_versionService.GetCurrentVersion()}, новая версия={releaseInfo.Version}");

            if (!Version.TryParse(_versionService.GetCurrentVersion(), out var currentVersion))
            {
                _logger.LogWarning($"Не удалось разобрать текущую версию: {_versionService.GetCurrentVersion()}");
                return false;
            }

            if (!Version.TryParse(releaseInfo.Version, out var newVersion))
            {
                _logger.LogWarning($"Не удалось разобрать новую версию: {releaseInfo.Version}");
                return false;
            }

            bool isNewVersionAvailable = newVersion > currentVersion;
            _logger.LogInformation($"Новая версия {(isNewVersionAvailable ? "доступна" : "не требуется")} ({currentVersion} → {newVersion})");
            return isNewVersionAvailable;
        }

        ///<inheritdoc/>
        public async Task UpdateApplicationAsync()
        {
            try
            {
                _logger.LogDebug($"Запуск процесса обновления приложения");
                await GetLatestReleaseInfoAsync();

                _logger.LogDebug($"Проверка наличия версии: {_latestReleaseInfo != null}");
                if (IsNewerVersionAvailable(_latestReleaseInfo ?? throw new NullReferenceException(nameof(_latestReleaseInfo))))
                {
                    _logger.LogInformation($"Обнаружена версия: {_latestReleaseInfo.Version}");
                    if (await _niotificationService.ShowConfirmation($"Была обнаружена новая версия {_latestReleaseInfo.Version}.\nЖелаете обновить приложение?"))
                    {
                        _logger.LogDebug($"Пользователь подтвердил обновление до версии {_latestReleaseInfo.Version}");
                        string? installerPath = await _internetService.DownloadAppInstallerAsync(_latestReleaseInfo.DownloadUrl!);
                        _logger.LogInformation($"Установщик загружен по пути: {installerPath}");
                        _installerService.Install(installerPath!);
                        _logger.LogInformation($"Запущена установка новой версии приложения");
                    }
                    else
                    {
                        _logger.LogInformation($"Пользователь отказался от обновления до версии {_latestReleaseInfo.Version}");
                    }
                }
                else
                {
                    _logger.LogInformation($"Обновление не требуется, текущая версия актуальна");
                }
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex, $"Информация о релизе оказалась null");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Критическая ошибка при обновлении приложения");
                throw;
            }
        }
    }
}
