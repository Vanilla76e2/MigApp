using MigApp.Core.Enums;
using MigApp.Infrastructure.Data.Entities;

namespace MigApp.Application.Services.PasswodMange
{
    public interface IPasswordChanger
    {
        Task<PasswordStatusEnum.PasswordStatus> ChangePasswordAsync(UsersProfile userProfile, string password);
    }
}
