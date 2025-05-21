using MigApp.Infrastructure.Data.Entities;
using MigApp.Infrastructure.Repository.Common;

namespace MigApp.Infrastructure.Repository.UsersProfiles
{
    public interface IUsersProfilesRepository : IDatabaseRepository<UsersProfile>
    {
        Task<UsersProfile?> GetByUsernameAsync(string username);

        // Сюда добавлять методы, специфичные для UsersProfile
    }
}
