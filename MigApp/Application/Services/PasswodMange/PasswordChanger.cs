using MigApp.Core.Enums;
using MigApp.Infrastructure.Data.Entities;
using MigApp.Infrastructure.Repository.UsersProfiles;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.Infrastructure.Services.Security;

namespace MigApp.Application.Services.PasswodMange
{
    public class PasswordChanger : IPasswordChanger
    {
        private readonly ISecurityService _securityService;
        private readonly IUsersProfilesRepository _userRepo;
        private readonly IAppLogger _logger;

        public PasswordChanger(ISecurityService securityService,
                               IUsersProfilesRepository userRepo,
                               IAppLogger logger)
        {
            _securityService = securityService;
            _userRepo = userRepo;
            _logger = logger;
        }

        public async Task<PasswordStatusEnum.PasswordStatus> ChangePasswordAsync(UsersProfile userProfile, string password)
        {
            if (userProfile == null)
            {
                _logger.LogError("Пользователь не найден.");
                return PasswordStatusEnum.PasswordStatus.MissedUser;
            }

            userProfile.UserPassword = _securityService.HashText(password);

            try
            {
                await _userRepo.UpdateAsync(userProfile);
                _logger.LogInformation($"Пароль пользоваетеля {userProfile.Username} успешно обновлён");
                return PasswordStatusEnum.PasswordStatus.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при обновлении пароля пользователя {userProfile.Username}");
                return PasswordStatusEnum.PasswordStatus.Failed;
            }
        }
    }
}
