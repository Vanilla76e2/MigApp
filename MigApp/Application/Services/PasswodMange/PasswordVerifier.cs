using Microsoft.EntityFrameworkCore;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.Infrastructure.Services.Security;
using MigApp.Infrastructure.Data;
using MigApp.Infrastructure.Data.Entities;
using MigApp.Core.Models;
using MigApp.Core.Enums;
using MigApp.UI.Services.AuthNotifier;

namespace MigApp.Application.Services.PasswodMange
{
    public class PasswordVerifier : IPasswordVerifier
    {
        private readonly IAppLogger _logger;
        private readonly ISecurityService _securityService;
        private readonly IPasswordChanger _passwodChanger;
        private readonly IAuthNotifier _notifier;

        public PasswordVerifier(IAuthNotifier notifier,
                                IAppLogger logger,
                                IDbContextFactory<MigDatabaseContext> dbContextFactory, 
                                ISecurityService securityService,
                                IPasswordChanger passwordChanger)
        {
            _notifier = notifier;
            _logger = logger;
            _passwodChanger = passwordChanger;
            _securityService = securityService;
        }

        /// <summary>
        /// Проверяет пароль, введённый пользователем, и возвращает результат авторизации.
        /// </summary>
        /// <param name="userProfile">Профиль пользователя из базы данных.</param>
        /// <param name="plainPassword">Введённый пользователем пароль в открытом виде.</param>
        /// <returns>
        /// Объект <see cref="AuthResult"/>:
        /// <see langword="true"/> в <see cref="AuthResult.IsAuthenticated"/> — если пароль подтверждён;
        /// <see langword="false"/> — если пароль не задан, введён неверно или отклонена смена.
        /// </returns>
        /// <remarks>
        /// В случае отсутствия пароля пользователю предлагается установить новый.
        /// </remarks>
        public async Task<AuthResult> VerifyPasswordAsync(UsersProfile userProfile, string plainPassword)
        {
            _logger.LogInformation($"Проверка пароля для {userProfile.Username}");

            if (string.IsNullOrEmpty(userProfile.UserPassword))
            {
                _logger.LogDebug($"Пароль для пользователя {userProfile.Username} не установлен.");
                
                bool confirmed = await _notifier.ConfirmPasswordChangeAsync();
                if(!confirmed)
                {
                    return new AuthResult(false, null, null);
                }

                if (ValidatePasswordStrength(plainPassword) == PasswordStatusEnum.PasswordStatus.WeakPassword)
                {
                    return new AuthResult(false, "Пароль должен содержать не менее 8 символов.", null);
                }

                var saveStatus = await _passwodChanger.ChangePasswordAsync(userProfile, plainPassword);
                if (saveStatus != PasswordStatusEnum.PasswordStatus.Success)
                {
                    return HandleChangePasswordFailure(saveStatus);
                }

                return new AuthResult(false, "Пароль сохранён.\nПовторите вход.", null);
            }

            if (!_securityService.VerifyHash(plainPassword, userProfile.UserPassword))
            {
                _logger.LogWarning($"Пароль для пользователя {userProfile.Username} неверный.");
                return new AuthResult(false, "Неверный логин или пароль.", null);
            }

            _logger.LogInformation($"Пароль пользователя {userProfile.Username} подтверждён.");
            return new AuthResult(true, null, null);
        }

        private PasswordStatusEnum.PasswordStatus ValidatePasswordStrength(string password)
        {
            if (password.Length < 8)
            {
                _logger.LogWarning("Пароль короче 8 символов");
                return PasswordStatusEnum.PasswordStatus.WeakPassword;
            }

            return PasswordStatusEnum.PasswordStatus.Success;
        }

        private AuthResult HandleChangePasswordFailure(PasswordStatusEnum.PasswordStatus status)
        {
            return status switch
            {
                PasswordStatusEnum.PasswordStatus.MissedUser =>
                    new AuthResult(false, "Пользователь не найден при попытке смены пароля.", null),

                PasswordStatusEnum.PasswordStatus.Cancelled =>
                    new AuthResult(false, null, null),

                PasswordStatusEnum.PasswordStatus.Failed =>
                    new AuthResult(false, "Ошибка при смене пароля.", null),

                _ => new AuthResult(false, "Неизвестная ошибка смены пароля.", null),
            };
        }
    }
}
