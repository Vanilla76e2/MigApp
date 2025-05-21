using Microsoft.EntityFrameworkCore;
using MigApp.Application.Services.PasswodManger;
using MigApp.Core.Models;
using MigApp.Core.Session;
using MigApp.Infrastructure.Data;
using MigApp.Infrastructure.Data.Entities;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.Infrastructure.Services.Security;
using MigApp.UI.Services.UINotification;

namespace MigApp.Application.Services.Authorization
{
    internal class RealAuthorizationStrategy : PasswordManager, IAuthorizationStrategy
    {
        private readonly IUserSession _userSession;

        public RealAuthorizationStrategy(IUserSession userSession, IAppLogger logger, IDbContextFactory<MigDatabaseContext> contextFactory, ISecurityService securityService, IUINotificationService ui)
        : base(logger, contextFactory, securityService, ui)
        {
            _userSession = userSession;
        }

        /// <summary>
        /// Выполняет процесс авторизации пользователя по логину и паролю.
        /// Включает загрузку профиля, проверку пароля и создание пользовательской сессии.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        /// <param name="password">Введённый пользователем пароль.</param>
        /// <returns>
        /// Объект <see cref="AuthResult"/>, содержащий результат авторизации,
        /// сообщение для пользователя и данные сессии (если успешно).
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Бросается, если <paramref name="username"/> или <paramref name="password"/> равны <c>null</c>.
        /// </exception>
        /// <remarks>
        /// TODO: Рассмотреть возможность декомпозиции метода на несколько меньших (SRP),
        /// например: <c>AuthorizeUserAsync()</c>, <c>CheckCredentialsAsync()</c>, <c>BuildSessionAsync()</c>.
        /// </remarks>
        public async Task<AuthResult> AuthorizationAsync(string username, string password)
        {
            _logger.LogDebug($"Проверка входных параметров: username={username}, password={(string.IsNullOrEmpty(password) ? "" : "********")}");
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

        /// <summary>
        /// Выполняет выход пользователя из системы.
        /// Очищает текущую пользовательскую сессию.
        /// </summary>
        public void Logout()
        {
            _logger.LogInformation("Очищаем сессию.");
            _userSession.DisposeSession();
        }

        /// <summary>
        /// Загружает профиль пользователя из базы данных по имени пользователя.
        /// </summary>
        /// <param name="username">Имя пользователя для поиска в таблице <see langword="UsersProfiles"/>.</param>
        /// <returns>
        /// Объект <see cref="UsersProfile"/>, если найден, иначе <see langword="null"/>.
        /// </returns>
        private async Task<UsersProfile?> LoadUserProfileAsync(string username)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.UsersProfiles.FirstOrDefaultAsync(u => u.Username == username);
        }

        /// <summary>
        /// Создаёт пользовательскую сессию на основе переданного профиля пользователя.
        /// Загружает роль пользователя из базы данных и формирует объект <see cref="UserSession"/>.
        /// </summary>
        /// <param name="userProfile">Профиль пользователя, для которого создаётся сессия.</param>
        /// <returns>
        /// Объект <see cref="UserSession"/> с актуальными правами и идентификатором.
        /// </returns>
        /// <exception cref="NullReferenceException">
        /// Бросается, если роль пользователя не найдена в базе данных.
        /// </exception>
        /// <remarks>
        /// TODO: Перенести в <see langword="UserSessionBuilder"/> или <see langword="SessionService"/> для соблюдения SRP.
        /// </remarks>
        protected async Task<UserSession> CreateUserSessionAsync(UsersProfile userProfile)
        {
            _logger.LogInformation($"Сбор данных пользователя {userProfile.Username} в сессию");
            await using var context = await _contextFactory.CreateDbContextAsync();

            var roleEntity = await context.Roles.FindAsync(userProfile.Role);
            if (roleEntity == null)
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

        /// <summary>
        /// Обрабатывает результат авторизации: отображает предупреждение при неуспехе и возвращает флаг успешности.
        /// </summary>
        /// <param name="result">Результат авторизации.</param>
        /// <returns>
        /// <see langword="true"/>, если авторизация успешна; иначе <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// TODO: Метод зависит от UI-сервиса. При необходимости можно выделить интерфейс <see langword="IAuthNotifier"/>
        /// для инверсии зависимостей (чтобы не тянуть UI в бизнес-логику).
        /// </remarks>
        protected async Task<bool> HandleAuthResultAsync(AuthResult result)
        {
            if (!result.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(result.Message))
                    await _ui.ShowWarningAsync(result.Message);

                return false;
            }

            return true;
        }
    }
}
