
namespace MigApp.Core.Services
{
    internal interface IAuthorizationStrategy
    {
        Task<AuthResult> AuthorizationAsync(string username, string password);
    }
}
