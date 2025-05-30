using MigApp.Core.Models;

namespace MigApp.Application.Services.Authorization
{
    public interface IAuthorizationStrategy
    {
        Task<AuthResult> AuthorizationAsync(string username, string password);
    }
}
