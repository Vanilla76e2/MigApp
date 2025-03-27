using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Core.Services
{
    /// <summary>
    /// Класс, перехватывающий необработанные исключения и записывающий их в лог.
    /// </summary>
    internal class CrashLogger : ICrashLogger
    {
        private readonly IAppLogger _logger;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CrashLogger"/>.
        /// </summary>
        /// <param name="logger">Экземпляр <see cref="IAppLogger"/> для записи логов.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="logger"/> равен null.</exception>
        public CrashLogger(IAppLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Подписывается на события необработанных исключений, чтобы перехватывать и логировать их.
        /// </summary>
        public void Initialize()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException!;
        }

        /// <summary>
        /// Обработчик события <see cref="AppDomain.UnhandledException"/>.
        /// Логирует информацию о необработанном исключении.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Объект <see cref="UnhandledExceptionEventArgs"/>, содержащий данные события.</param>
        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception? ex = e.ExceptionObject as Exception;
            LogException(ex, "Необработанное исключение.");
        }

        /// <summary>
        /// Обработчик события <see cref="TaskScheduler.UnobservedTaskException"/>.
        /// Логирует информацию о необработанном исключении в задаче.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Объект <see cref="UnobservedTaskExceptionEventArgs"/>, содержащий данные события.</param>
        private void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            Exception? ex = e.Exception.InnerException;
            LogException(ex, "Необработанное исключение в задаче.");
        }

        /// <summary>
        /// Логирует информацию об исключении с указанным контекстом.
        /// </summary>
        /// <param name="ex">Объект <see cref="Exception"/>, представляющий исключение.</param>
        /// <param name="context">Контекст, в котором произошло исключение.</param>
        private void LogException(Exception? ex, string context)
        {
            try
            {
                _logger.CreateCrashLog(ex, $"{context}: Необработанное исключение");
            }
            catch
            {
                // Не удалось записать лог. Ничего не остается, кроме как вывести сообщение в консоль.
                Debug.WriteLine($"{context}: Необработанное исключение");
            }
        }

        /// <summary>
        /// Закрывает и очищает ресурсы, связанные с логированием.
        /// </summary>
        public void Close()
        {
            if (Log.Logger != null)
                Log.CloseAndFlush();

            AppDomain.CurrentDomain.UnhandledException -= OnUnhandledException;
            TaskScheduler.UnobservedTaskException -= OnUnobservedTaskException;
        }
    }
}
