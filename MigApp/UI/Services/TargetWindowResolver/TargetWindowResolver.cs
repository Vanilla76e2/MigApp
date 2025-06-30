
using MigApp.Infrastructure.Services.AppLogger;

namespace MigApp.UI.Services.TargetWindowResolver
{
    internal class TargetWindowResolver : ITargetWindowResolver
    {
        private readonly IAppLogger _logger;

        public TargetWindowResolver(IAppLogger logger)
        {
            _logger = logger;
        }

        public Type? ResolveCurrentWindow()
        {
            var activeWindow = System.Windows.Application.Current.Windows
                .OfType<System.Windows.Window>()
                .FirstOrDefault(w => w.IsActive);

            if (activeWindow == null)
                _logger.LogWarning("Не найдено ни одного активного окна для отображения сообщения.");

            return activeWindow?.GetType();
        }
    }
}
