namespace MigApp.Core.Services.Dispathcer
{
    /// <summary>  
    /// Предоставляет методы для выполнения операций в потоке пользовательского интерфейса (UI).  
    /// Позволяет безопасно взаимодействовать с UI из фоновых потоков.  
    /// </summary>  
    public interface IDispatcher
    {
        /// <summary>  
        /// Синхронно выполняет указанное действие в потоке UI.  
        /// </summary>  
        /// <param name="action">Действие, которое необходимо выполнить.</param>
        void Invoke(Action action);

        /// <summary>  
        /// Асинхронно выполняет указанное действие в потоке UI.  
        /// </summary>  
        /// <param name="action">Действие, которое необходимо выполнить.</param>  
        /// <returns>Задача, представляющая асинхронную операцию.</returns>  
        Task InvokeAsync(Action action);

        /// <summary>  
        /// Асинхронно выполняет указанную функцию в потоке UI и возвращает результат.  
        /// </summary>  
        /// <typeparam name="T">Тип возвращаемого значения.</typeparam>  
        /// <param name="func">Функция, которую необходимо выполнить.</param>  
        /// <returns>Задача, содержащая результат выполнения функции.</returns>  
        Task<T> InvokeAsync<T>(Func<T> func);

        /// <summary>  
        /// Проверяет, выполняется ли текущий код в потоке UI.  
        /// </summary>  
        /// <returns>  
        /// <see langword="true"/>, если текущий поток является потоком UI;  
        /// <see langword="false"/> в противном случае.  
        /// </returns>  
        bool IsOnUiThread();
    }
}
