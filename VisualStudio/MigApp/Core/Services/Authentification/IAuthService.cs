
namespace MigApp.Core.Services
{
	public interface IAuthService
    {
        Task<AuthResult> AuthorizeUserAsync(string Username, string password);

        Task<bool> ChangePasswordAsync(string Username, string password);

        void Logout();
    }
}
