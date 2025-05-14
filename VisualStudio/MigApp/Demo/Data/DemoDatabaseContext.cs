using Microsoft.EntityFrameworkCore;
using MigApp.Data;

namespace MigApp.Demo.Data
{
    internal class DemoDatabaseContext : MigDatabaseContext
    {
        public DemoDatabaseContext(DbContextOptions<MigDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cctv> Cctvs { get; set; }

        public virtual DbSet<Computer> Computers { get; set; }

        public virtual DbSet<ComputersComponent> ComputersComponents { get; set; }

        public virtual DbSet<ComputersDevice> ComputersDevices { get; set; }

        public virtual DbSet<ComputersServiceHistory> ComputersServiceHistories { get; set; }

        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Favourite> Favourites { get; set; }

        public virtual DbSet<FavouriteView> FavouriteViews { get; set; }

        public virtual DbSet<GenericDevice> GenericDevices { get; set; }

        public virtual DbSet<IpAddressInfo> IpAddressInfos { get; set; }

        public virtual DbSet<Laptop> Laptops { get; set; }

        public virtual DbSet<LogEntry> Logs { get; set; }

        public virtual DbSet<MigApp.Data.Monitor> Monitors { get; set; }

        public virtual DbSet<Orgtechnic> Orgtechnics { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<RolesView> RolesViews { get; set; }

        public virtual DbSet<Router> Routers { get; set; }

        public virtual DbSet<MigApp.Data.Switch> Switches { get; set; }

        public virtual DbSet<Tablet> Tablets { get; set; }

        public virtual DbSet<UserProfileView> UserProfileViews { get; set; }

        public virtual DbSet<UsersProfile> UsersProfiles { get; set; }

        public override int SaveChanges() => 0;
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult(0);

        private DbSet<T> CreateFakeDbSet<T>(IEnumerable<T> data) where T : class
        {
            return new FakeDbSet<T>(data);
        }
    }
}
