using Microsoft.EntityFrameworkCore;
using MigApp.Application.Services.PasswodMange;
using MigApp.Application.Services.SessionBuilder;
using MigApp.Core.Models;
using MigApp.Core.Session;
using MigApp.Infrastructure.Data.Entities;
using MigApp.Infrastructure.Repository.UsersProfiles;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.UI.Services.AuthNotifier;

namespace MigApp.Application.Services.Authorization
{
    public class RealAuthorizationStrategy : IAuthorizationStrategy
    {
        private readonly IUsersProfilesRepository _userRepo;
        private readonly ISessionBuilder _sessionBuilder;
        private readonly IPasswordVerifier _verifier;
        private readonly IAuthNotifier _notifier;
        private readonly IUserSession _session;
        private readonly IAppLogger _logger;

        public RealAuthorizationStrategy(IUsersProfilesRepository userRepo, IPasswordVerifier verifier, ISessionBuilder sessionBuilder, IAuthNotifier notifier, IUserSession session, IAppLogger logger)
        {
            _userRepo = userRepo;
            _sessionBuilder = sessionBuilder;
            _verifier = verifier;
            _notifier = notifier;
            _session = session;
            _logger = logger;
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
            ArgumentNullException.ThrowIfNull(username, nameof(username));
            ArgumentNullException.ThrowIfNull(password, nameof(password));

            _logger.LogInformation($"Начата авторизация пользователя: {username}");

            var user = await _userRepo.GetByUsernameAsync(username);
            if (user == null)
            {
                _logger.LogWarning($"Пользлватель {username} не найден");
                return new AuthResult(false, "Неверное имя пользователя или пароль.", null);
            }

            var authResult = await _verifier.VerifyPasswordAsync(user, password);
            if (!authResult.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(authResult.Message))
                    await _notifier.NotifyAsync(authResult);
                
                return authResult;
            }

            var session = await _sessionBuilder.BuildAsync(user);
            return new AuthResult(true, "Успешный вход", session);
        }

        /// <summary>
        /// Выполняет выход пользователя из системы.
        /// Очищает текущую пользовательскую сессию.
        /// </summary>
        public void Logout()
        {
            _logger.LogInformation("Очищаем сессию.");
            _session.DisposeSession();
        }
    }
}
