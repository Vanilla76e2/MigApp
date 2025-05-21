using MigApp.Core.Models;

namespace MigApp.Application.Services.Authorization
{
    internal interface IAuthorizationStrategy
    {
        Task<AuthResult> AuthorizationAsync(string username, string password);
    }
}
