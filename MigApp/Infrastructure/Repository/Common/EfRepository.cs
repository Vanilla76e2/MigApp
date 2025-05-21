using Microsoft.EntityFrameworkCore;
using MigApp.Infrastructure.Data;
using MigApp.Infrastructure.Services.AppLogger;
using System.Linq.Expressions;

namespace MigApp.Infrastructure.Repository.Common
{
    public class EfRepository<T> : IDatabaseRepository<T> where T : class
    {
        private readonly IDbContextFactory<MigDatabaseContext> _contextFactory;
        private readonly IAppLogger _logger;

        public EfRepository(IDbContextFactory<MigDatabaseContext> contextFactory, IAppLogger logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();
                await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
                _logger.LogDebug($"Добавлена сущность типа {typeof(T).Name}");
            }
            catch
            {
                _logger.LogError($"Ошибка при добавлении сущности типа {typeof(T).Name}");
                throw;
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
                _logger.LogDebug($"Удалена сущность типа {typeof(T).Name}"); 
            }
            catch
            {
                _logger.LogError($"Ошибка при удалении сущности типа {typeof(T).Name}");
                throw;
            }
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
                _logger.LogDebug($"Изменена сущность типа {typeof(T).Name}");
            }
            catch
            {
                _logger.LogError($"Ошибка при изменении сущности типа {typeof(T).Name}");
                throw;
            }
        }
    }
}
