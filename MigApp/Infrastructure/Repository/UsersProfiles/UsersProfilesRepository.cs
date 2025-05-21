using Microsoft.EntityFrameworkCore;
using MigApp.Infrastructure.Data;
using MigApp.Infrastructure.Data.Entities;
using MigApp.Infrastructure.Repository.Common;
using MigApp.Infrastructure.Services.AppLogger;

namespace MigApp.Infrastructure.Repository.UsersProfiles
{
    public class UsersProfilesRepository : EfRepository<UsersProfile>, IUsersProfilesRepository
    {
        private readonly IDbContextFactory<MigDatabaseContext> _contextFactory;

        public UsersProfilesRepository(IDbContextFactory<MigDatabaseContext> contextFactory, IAppLogger logger) : base(contextFactory, logger)
        {
            _contextFactory = contextFactory;
        }

        public async Task<UsersProfile?> GetByUsernameAsync(string username)
        {
            return await FindAsync(x => x.Username == username);
        }

        // Сюда добавлять методы, специфичные для UsersProfile
    }
}
