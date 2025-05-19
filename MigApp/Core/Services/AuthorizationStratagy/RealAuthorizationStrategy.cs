
using Microsoft.EntityFrameworkCore;
using MigApp.Core.Session;
using MigApp.Data;
using Windows.UI;

namespace MigApp.Core.Services.AuthorizationStratagy
{
    internal class RealAuthorizationStrategy : IAuthorizationStrategy
    {
        private readonly IDbContextFactory<MigDatabaseContext> _contextFactory;
        private readonly ISecurityService _securityService;
        private readonly IAuthorizationStrategy _authorizationStrategy;
        private readonly IUserSession _userSession;
        private readonly IUINotificationService _ui;
        private readonly IAppLogger _logger;

        public RealAuthorizationStrategy(IDbContextFactory<MigDatabaseContext> contextFactory, IAppLogger logger, ISecurityService securityService, IAuthorizationStrategy authorizationStrategy, IUserSession userSession, IUINotificationService ui)
        {
            _contextFactory = contextFactory;
            _securityService = securityService;
            _authorizationStrategy = authorizationStrategy;
            _userSession = userSession;
            _ui = ui;
            _logger = logger;
        }


        public async Task<AuthResult> AuthorizationAsync(string username, string password)
        {
            _logger.LogDebug($"Проверка входных параметров: username={username}, password={(string.IsNullOrEmpty(password) ? "пустой" : "задан")}");
            ArgumentNullException.ThrowIfNull(username, nameof(username));
            ArgumentNullException.ThrowIfNull(password, nameof(password));

            try
            {
                _logger.LogInformation($"Начата попытка авторизации пользователя: {username}");

                var userProfile = await LoadUserProfileAsync(username);

                // Проверяем наличие пользователя в базе данных
                if (userProfile == null)
                {
                    _logger.LogWarning($"Пользователь {username} не найден в базе данных");
                    return new AuthResult(false, "Неверное имя пользователя или пароль.", null);
                }
                _logger.LogInformation($"Пользователь {username} найден в базе данных");

                // Проверяем пароль
                var verifyResult = await VerifyPasswordAsync(userProfile, password);
                if (!await HandleAuthResultAsync(verifyResult))
                    return verifyResult;
                
                // Собираем данные пользователя
                var session = await CreateUserSessionAsync(userProfile);

                // Возвращаем успешный вход
                _logger.LogInformation($"Выполнен успешный вход пользователя: {username}");
                return new AuthResult(true, "Успешный вход", session);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex, $"Данные для пользователя {username} неполные");
                return new AuthResult(false, "Ошибка при попытке входа.", null);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Критическая ошибка при авторизации пользователя: {username}");
                return new AuthResult(false, "Критическая ошибка при попытке входа.", null);
            }
        }

        public async Task<PasswordChangeStatus> ChangePasswordAsync(UsersProfile userProfile, string password)
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
                if (ValidatePasswordStrength(password) == PasswordChangeStatus.WeakPassword)
                {
                    await _ui.ShowWarningAsync("Пароль должен быть длиной 8 символов или больше!");
                    return PasswordChangeStatus.WeakPassword;
                }

                _logger.LogDebug($"Получение записи пользователя: {userProfile.Username}");

                return await SaveNewPasswordAsync(userProfile.Username, password);
            }
            else
            {
                _logger.LogInformation($"Пользователь {userProfile.Username} отказался сохранять новый пароль");
                return PasswordChangeStatus.Cancelled;
            }
        }

        public void Logout()
        {
            _logger.LogInformation("Очищаем сессию.");
            _userSession.DisposeSession();
        }

        private async Task<UsersProfile?> LoadUserProfileAsync(string username)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.UsersProfiles.FirstOrDefaultAsync(u => u.Username == username);
        }

        private async Task<AuthResult> VerifyPasswordAsync(UsersProfile userProfile, string password)
        {
            _logger.LogInformation($"Начата проверка пароля для пользователя: {userProfile.Username}");
            if (string.IsNullOrEmpty(userProfile.UserPassword))
            {
                _logger.LogDebug($"Пароль пользователя {userProfile.Username} не установлен");

                PasswordChangeStatus passwordChanged = await ChangePasswordAsync(userProfile, password);

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

        private async Task<bool> HandleAuthResultAsync(AuthResult result)
        {
            if (!result.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(result.Message))
                    await _ui.ShowWarningAsync(result.Message);

                return false;
            }

            return true;
        }

        private async Task<UserSession> CreateUserSessionAsync(UsersProfile userProfile)
        {
            _logger.LogInformation($"Сбор данных пользователя {userProfile.Username} в сессию");
            await using var context = await _contextFactory.CreateDbContextAsync();

            var roleEntity = await context.Roles.FindAsync(userProfile.Role);
            if(roleEntity == null)
            {
                _logger.LogError($"Роль для пользователя {userProfile.Username} не найдена");
                throw new NullReferenceException("Роль не найдена");
            }

            var role = new UserRole(
                roleEntity.Id.ToString(),
                roleEntity.RoleName,
                roleEntity.IsAdministrator,
                roleEntity.EmployeesAccesslevel,
                roleEntity.TechnicsAccesslevel,
                roleEntity.FurnitureAccesslevel);

            var session = _userSession.StartSession(userProfile.Id.ToString(), userProfile.Username, role);
            _logger.LogInformation($"Сессия для пользователя {userProfile.Username} успешно собрана");

            return session;
        }

        private async Task<bool> ConfirmPasswordChangeAsync(string username)
        {
            _logger.LogInformation($"Отправлено предложение на смену пароля для пользователя: {username}");
            bool confirmed = await _ui.ShowConfirmation("Ваш пароль был сброшен.\nЖелаете сохранить введённый пароль, как новый?");

            if (!confirmed)
                _logger.LogInformation($"Пользователь {username} отказался менять пароль.");

            return confirmed;
        }

        private PasswordChangeStatus ValidatePasswordStrength(string password)
        {
            if (password.Length < 8)
            {
                _logger.LogWarning("Пароль оказался короче 8 символов");
                return PasswordChangeStatus.WeakPassword;
            }

            return PasswordChangeStatus.Success;
        }

        private async Task<PasswordChangeStatus> SaveNewPasswordAsync(string username, string password)
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
                return PasswordChangeStatus.Success;
            }
            else
            {
                _logger.LogError($"Не удалось найти пользователя: {userProfile.Username}");
                return PasswordChangeStatus.MissedUser;
            }
        }

        private AuthResult ChangePasswordStatusHandler(PasswordChangeStatus status, string username)
        {
            switch (status)
            {
                case PasswordChangeStatus.Success:
                _logger.LogInformation($"Пароль для пользователя {username} успешно сохранён. Требуется повторная авторизация.");
                return new AuthResult(false, "Требуется повторная авторизация.", null);

                case PasswordChangeStatus.MissedUser:
                    _logger.LogError($"Пользователь {username} Не найден");
                    return new AuthResult(false, "Произошла ошибка при попытке сохранить пароль.", null);

                case PasswordChangeStatus.WeakPassword:
                    return new AuthResult(false, "Пароль должен содержать не менее 8 символов!", null);

                case PasswordChangeStatus.Cancelled:
                    _logger.LogInformation($"Пользователь {username} отказался устанавливать новый пароль.");
                    return new AuthResult(false, null, null);

                default:
                    _logger.LogCritical(new ArgumentException(), $"Статус события смены пароля за пределами возможных значений");
                    return new AuthResult(false, "Произошла критическая ошибка при выполнении операции.", null);

            }
        }

        public enum PasswordChangeStatus
        {
            Cancelled,
            WeakPassword,
            Success,
            MissedUser
        }
    }
}
