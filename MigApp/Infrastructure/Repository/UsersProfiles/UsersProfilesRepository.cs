using MigApp.Infrastructure.Data.Entities;
using MigApp.Infrastructure.Repository.Common;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.Infrastructure.Services.DatabaseContextProvider;

namespace MigApp.Infrastructure.Repository.UsersProfiles
{
    public class UsersProfilesRepository : EfRepository<UsersProfile>, IUsersProfilesRepository
    {
        private readonly IDatabaseContextProvider _contextProvider;

        public UsersProfilesRepository(IDatabaseContextProvider contextProvider, IAppLogger logger) : base(contextProvider, logger)
        {
            _contextProvider = contextProvider;
        }

        public async Task<UsersProfile?> GetByUsernameAsync(string username)
        {
            return await FindAsync(x => x.Username == username);
        }

        // Сюда добавлять методы, специфичные для UsersProfile
    }
}
