using Microsoft.EntityFrameworkCore;
using MigApp.Core.Models;
using MigApp.Infrastructure.Data;
using MigApp.Infrastructure.Data.Entities;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.Infrastructure.Services.Security;
using MigApp.UI.Services.UINotification;
using static MigApp.Core.Enums.PasswordStatusEnum;

namespace MigApp.Application.Services.PasswodManger
{
    internal class PasswordManager
    {
        protected readonly IAppLogger _logger;
        protected readonly IDbContextFactory<MigDatabaseContext> _contextFactory;
        protected readonly ISecurityService _securityService;
        protected readonly IUINotificationService _ui;

        public PasswordManager(IAppLogger logger, IDbContextFactory<MigDatabaseContext> contextFactory, ISecurityService securityService, IUINotificationService ui)
        {
            _logger = logger;
            _contextFactory = contextFactory;
            _securityService = securityService;
            _ui = ui;
        }

        /// <summary>
        /// Проверяет корректность введённого пользователем пароля.
        /// Если у пользователя пароль отсутствует — запускает процесс смены пароля.
        /// </summary>
        /// <param name="userProfile">Профиль пользователя, содержащий логин и хэш пароля.</param>
        /// <param name="password">Введённый пользователем пароль.</param>
        /// <returns>
        /// Возвращает результат аутентификации: успешно или нет, а также сообщение об ошибке.
        /// </returns>
        protected async Task<AuthResult> VerifyPasswordAsync(UsersProfile userProfile, string password)
        {
            _logger.LogInformation($"Начата проверка пароля для пользователя: {userProfile.Username}");
            if (string.IsNullOrEmpty(userProfile.UserPassword))
            {
                _logger.LogDebug($"Пароль пользователя {userProfile.Username} не установлен");

                PasswordStatus passwordChanged = await ChangePasswordAsync(userProfile, password);

                var result = ChangePasswordStatusHandler(passwordChanged, userProfile.Username);

                if (!result.IsAuthenticated)
                    return result;
            }

            if (!_securityService.VerifyHash(password, userProfile.UserPassword))
            {
                _logger.LogWarning($"Неверный пароль пользователя {userProfile.Username}");
                return new AuthResult(false, "Неверное имя пользователя или пароль.", null);
            }

            _logger.LogInformation($"Пароль для пользователя {userProfile.Username} верный");
            return new AuthResult(true, null, null);
        }

        /// <summary>
        /// Выполняет попытку смены пароля пользователя.
        /// Предварительно запрашивает подтверждение на смену, проверяет силу пароля,
        /// и сохраняет его при успешном подтверждении.
        /// </summary>
        /// <param name="userProfile">Профиль пользователя, для которого меняется пароль.</param>
        /// <param name="password">Новый пароль, который вводит пользователь.</param>
        /// <returns>
        /// Возвращает один из возможных статусов смены пароля:
        /// <list type="bullet">
        /// <item><description><see cref="PasswordStatus.Saved"/> — если пароль успешно сохранён.</description></item>
        /// <item><description><see cref="PasswordStatus.WeakPassword"/> — если пароль слишком слабый.</description></item>
        /// <item><description><see cref="PasswordStatus.Cancelled"/> — если пользователь отказался от сохранения.</description></item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Бросается, если <paramref name="userProfile"/>, его имя пользователя или сам <paramref name="password"/> равны null.
        /// </exception>
        protected async Task<PasswordStatus> ChangePasswordAsync(UsersProfile userProfile, string password)
        {
            ArgumentNullException.ThrowIfNull(userProfile);

            _logger.LogDebug($"Начата попытка смены пароля для пользователя: {userProfile.Username}");
            ArgumentNullException.ThrowIfNull(userProfile.Username, nameof(userProfile.Username));
            ArgumentNullException.ThrowIfNull(password, nameof(password));

            _logger.LogInformation($"Отправлено предложение на смену пароля для пользователя: {userProfile.Username}");
            // Если пользователь согласился
            if (await ConfirmPasswordChangeAsync(userProfile.Username))
            {
                _logger.LogInformation($"Пользователь {userProfile.Username} решил сохранить новый пароль");
                if (ValidatePasswordStrength(password) == PasswordStatus.WeakPassword)
                {
                    await _ui.ShowWarningAsync("Пароль должен быть длиной 8 символов или больше!");
                    return PasswordStatus.WeakPassword;
                }

                _logger.LogDebug($"Получение записи пользователя: {userProfile.Username}");

                return await SaveNewPasswordAsync(userProfile.Username, password);
            }
            else
            {
                _logger.LogInformation($"Пользователь {userProfile.Username} отказался сохранять новый пароль");
                return PasswordStatus.Cancelled;
            }
        }

        /// <summary>
        /// Запрашивает у пользователя подтверждение на сохранение нового пароля,
        /// если текущий пароль был сброшен.
        /// </summary>
        /// <param name="username">Имя пользователя, для которого выполняется запрос подтверждения.</param>
        /// <returns>
        /// Возвращает <c>true</c>, если пользователь согласился сохранить новый пароль, иначе <c>false</c>.
        /// </returns>
        protected async Task<bool> ConfirmPasswordChangeAsync(string username)
        {
            _logger.LogInformation($"Отправлено предложение на смену пароля для пользователя: {username}");
            bool confirmed = await _ui.ShowConfirmation("Ваш пароль был сброшен.\nЖелаете сохранить введённый пароль, как новый?");

            if (!confirmed)
                _logger.LogInformation($"Пользователь {username} отказался менять пароль.");

            return confirmed;
        }

        /// <summary>
        /// Проверяет силу пароля на соответствие минимальным требованиям.
        /// В текущей реализации проверяется только длина пароля.
        /// </summary>
        /// <param name="password">Пароль, который необходимо проверить.</param>
        /// <returns>
        /// Возвращает <see cref="PasswordStatus.Success"/>, если пароль допустим.
        /// Возвращает <see cref="PasswordStatus.WeakPassword"/>, если пароль слишком короткий.
        /// </returns>
        protected PasswordStatus ValidatePasswordStrength(string password)
        {
            if (password.Length < 8)
            {
                _logger.LogWarning("Пароль оказался короче 8 символов");
                return PasswordStatus.WeakPassword;
            }
            return PasswordStatus.Success;
        }

        /// <summary>
        /// Сохраняет новый пароль для указанного пользователя в базе данных.
        /// Выполняется поиск пользователя, хеширование пароля и сохранение изменений.
        /// </summary>
        /// <param name="username">Имя пользователя, для которого сохраняется новый пароль.</param>
        /// <param name="password">Новый пароль, который необходимо сохранить.</param>
        /// <returns>
        /// Возвращает <see cref="PasswordStatus.Success"/>, если пароль успешно обновлён.<br/>
        /// Возвращает <see cref="PasswordStatus.MissedUser"/>, если пользователь не найден в базе данных.
        /// </returns>
        /// <remarks>
        /// TODO: Его здесь быть не должно. Нужно вынести в отдельный класс, который будет отвечать за работу с БД.
        /// </remarks>
        protected async Task<PasswordStatus> SaveNewPasswordAsync(string username, string password)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            _logger.LogDebug($"Запрашиваем профил пользователя: {username}");
            var userProfile = await context.UsersProfiles.FirstOrDefaultAsync(u => u.Username == username);

            if (userProfile != null)
            {
                _logger.LogDebug($"Обновление пароля для пользователя: {userProfile.Username}");
                userProfile.UserPassword = _securityService.HashText(password);
                await context.SaveChangesAsync();
                _logger.LogInformation($"Новый пароль успешно сохранён для пользователя: {userProfile.Username}");
                return PasswordStatus.Success;
            }
            else
            {
                _logger.LogError($"Не удалось найти пользователя: {userProfile.Username}");
                return PasswordStatus.MissedUser;
            }
        }

        /// <summary>
        /// Обрабатывает результат смены пароля и формирует соответствующий ответ авторизации.
        /// </summary>
        /// <param name="status">Статус смены пароля, полученный после попытки сохранить новый пароль.</param>
        /// <param name="username">Имя пользователя, участвовавшего в процессе смены пароля.</param>
        /// <returns>
        /// Возвращает <see cref="AuthResult"/> с флагом успешности и сообщением для UI.
        /// </returns>
        /// <remarks>
        /// TODO: Метод следует перенести в отдельный сервис (например, <c>PasswordStatusHandler</c> или <c>AuthService</c>) 
        /// для соблюдения принципа SRP.
        /// </remarks>
        protected AuthResult ChangePasswordStatusHandler(PasswordStatus status, string username)
        {
            switch (status)
            {
                case PasswordStatus.Success:
                    _logger.LogInformation($"Пароль для пользователя {username} успешно сохранён. Требуется повторная авторизация.");
                    return new AuthResult(false, "Требуется повторная авторизация.", null);

                case PasswordStatus.MissedUser:
                    _logger.LogError($"Пользователь {username} Не найден");
                    return new AuthResult(false, "Произошла ошибка при попытке сохранить пароль.", null);

                case PasswordStatus.WeakPassword:
                    return new AuthResult(false, "Пароль должен содержать не менее 8 символов!", null);

                case PasswordStatus.Cancelled:
                    _logger.LogInformation($"Пользователь {username} отказался устанавливать новый пароль.");
                    return new AuthResult(false, null, null);

                default:
                    _logger.LogCritical(new ArgumentException(), $"Статус события смены пароля за пределами возможных значений");
                    return new AuthResult(false, "Произошла критическая ошибка при выполнении операции.", null);

            }
        }
    }
}
