using MigApp.Core.Session;

namespace MigApp.Core.Models
{
    /// <summary>
    /// Результат операции аутентификации пользователя.
    /// Включает в себя <see cref="IsAuthenticated"/> как признак успешности аутентификации,
    /// <see cref="Message"/> содержит сообщение о статусе авторизации,
    /// <see cref="Session"/> содержит сессию текущего пользователя.
    /// </summary>
    /// <remarks>
    /// Содержит информацию об успешности аутентификации, возможное сообщение об ошибке
    /// и данные сессии пользователя в случае успешной аутентификации.
    /// </remarks>
    public record AuthResult
    {
        /// <summary>
        /// Признак успешной аутентификации.
        /// </summary>
        public bool IsAuthenticated { get; init; }

        /// <summary>
        /// Сообщение о статусе авторизации.
        /// </summary>
        public string? Message { get; init; }

        /// <summary>
        /// Данные о сессии пользователя.
        /// </summary>
        public UserSession? Session { get; init; }

        public AuthResult(bool isAuthenticated, string? message, UserSession? session)
        {
            IsAuthenticated = isAuthenticated;
            Message = message;
            Session = session;
        }
    }
}
