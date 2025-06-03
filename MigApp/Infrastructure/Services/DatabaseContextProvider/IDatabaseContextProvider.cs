using Microsoft.EntityFrameworkCore;

namespace MigApp.Infrastructure.Services.DatabaseContextProvider
{
    public interface IDatabaseContextProvider
    {
        Task<DbContext> GetDbContextAsync();

        DbContext GetDbContext(string connectionString);
    }
}
