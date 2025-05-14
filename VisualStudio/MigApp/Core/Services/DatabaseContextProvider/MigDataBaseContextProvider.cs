using Microsoft.EntityFrameworkCore;

namespace MigApp.Core.Services
{
    /// <summary>
    /// Класс реализующий интерфейс <see cref="IDbContextProvider"/>.
    /// </summary>
    public class MigDatabaseContextProvider : IDbContextProvider
    {
        private readonly IDbContextFactory<MigDatabaseContext> _factory;
        private readonly IAppLogger _logger;
        private MigDatabaseContext? _context;

        public MigDatabaseContextProvider(IDbContextFactory<MigDatabaseContext> factory, IAppLogger logger)
        {
            _factory = factory;
            _logger = logger;
        }

        /// <inheritdoc/>
        public MigDatabaseContext GetContext()
        {
            try
            {
                _logger.LogInformation("Был запрошен контекст базы данных");
                if (_context == null || !_context.Database.CanConnect())
                {
                    _logger.LogDebug("Создание нового контекста базы данных");
                    _context = _factory.CreateDbContext();
                }
                _logger.LogInformation("Возврат контекста базы данных");
                return _context;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Произошла критическая ошибка при передаче контекста базы данных");
                throw;
            }
        }

        /// <inheritdoc/>
        public void ResetContext()
        {

            _logger.LogInformation("Сброс контекста базы данных");
            _context?.Dispose();
            _context = _factory.CreateDbContext();
            _logger.LogInformation("Контекст базы данных был сброшен");
        }

        /// <summary>
        /// Освобождает ресурсы, используемые контекстом базы данных.
        /// </summary>
        public void Dispose()
        {

            _context?.Dispose();
        }
    }
}
