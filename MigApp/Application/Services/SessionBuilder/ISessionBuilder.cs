using MigApp.Core.Session;
using MigApp.Infrastructure.Data.Entities;

namespace MigApp.Application.Services.SessionBuilder
{
    public interface ISessionBuilder
    {
        Task<UserSession> BuildAsync(UsersProfile userProfile);
    }
}
