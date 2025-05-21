using MigApp.Infrastructure.Data.Entities;

namespace MigApp.Infrastructure.Repository.Common
{
    internal interface IUnitOfWork
    {
        IDatabaseRepository<UsersProfile> UsersProfiles { get; }
        IDatabaseRepository<Computer> Computers { get; }
    }
}
