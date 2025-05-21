using Microsoft.EntityFrameworkCore;
using MigApp.Infrastructure.Data;
using MigApp.Infrastructure.Data.Entities;
using MigApp.Infrastructure.Services.AppLogger;
namespace MigApp.Infrastructure.Repository.Common
{
    public class EfUnitOfWork : IUnitOfWork
    {
        public EfUnitOfWork(IDbContextFactory<MigDatabaseContext> contextFactory, IAppLogger logger)
        {
            UsersProfiles = new EfRepository<UsersProfile>(contextFactory, logger);
            Computers = new EfRepository<Computer>(contextFactory, logger);
        }

        public IDatabaseRepository<UsersProfile> UsersProfiles { get; }

        public IDatabaseRepository<Computer> Computers { get;}
    }
}
