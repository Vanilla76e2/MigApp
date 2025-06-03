using Microsoft.EntityFrameworkCore;
using MigApp.Infrastructure.Data;
using MigApp.Infrastructure.Data.Entities;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.Infrastructure.Services.DatabaseContextProvider;
namespace MigApp.Infrastructure.Repository.Common
{
    public class EfUnitOfWork : IUnitOfWork
    {
        public EfUnitOfWork(IDatabaseContextProvider contextProvider, IAppLogger logger)
        {
            UsersProfiles = new EfRepository<UsersProfile>(contextProvider, logger);
            Computers = new EfRepository<Computer>(contextProvider, logger);
        }

        public IDatabaseRepository<UsersProfile> UsersProfiles { get; }

        public IDatabaseRepository<Computer> Computers { get;}
    }
}
