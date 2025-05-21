
using MigApp.Core.Models;

namespace MigApp.Core.Session
{
    /// <summary>
    /// Данные текущей сессии пользователя.
    /// Обновляется при входе/выходе.
    /// </summary>
    public sealed class UserSession : IUserSession
    {
        public string UserId { get; private set; } = string.Empty;
        public string Username { get; private set; } = string.Empty;
        public UserRole? Role { get; private set; }
        public DateTime? LoginTime { get; private set; }
        public bool IsAuthenticated { get; private set; }

        /// <summary>
        /// Создает пустую сессию пользователя.
        /// </summary>
        /// /// <param name="userId">ID пользователя в базе данных.</param>
        /// <param name="Username">Имя пользователя.</param>
        /// <param name="role">Роль пользователя, определяющая его права.</param>
        public UserSession()
        {
            UserId = string.Empty;
            Username = string.Empty;
            Role = null;
            LoginTime = null;
            IsAuthenticated = false;
        }

        public UserSession StartSession(string id, string username, UserRole role)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException(nameof(username));

            UserId = id;
            Username = username;
            Role = role ?? throw new ArgumentNullException(nameof(role));
            LoginTime = DateTime.UtcNow;
            IsAuthenticated = true;

            return this;
        }

        public void DisposeSession()
        {
            UserId = string.Empty;
            Username = string.Empty;
            Role = null;
            LoginTime = null;
            IsAuthenticated = false;
        }

        /// <inheritdoc cref="UserRole.CanReadTechnics"/>
        public bool CanReadTechnics() => IsAuthenticated == true ? Role!.CanReadTechnics() : throw new InvalidOperationException("Cannot access permission: user is not authenticated.");

        /// <inheritdoc cref="UserRole.CanWriteTechnics"/>
        public bool CanWriteTechnics() => IsAuthenticated == true ? Role!.CanWriteTechnics() : throw new InvalidOperationException("Cannot access permission: user is not authenticated.");

        /// <inheritdoc cref="UserRole.CanReadEmployees"/>
        public bool CanReadEmployees() => IsAuthenticated == true ? Role!.CanReadEmployees() : throw new InvalidOperationException("Cannot access permission: user is not authenticated.");

        /// <inheritdoc cref="UserRole.CanWriteEmployees"/>
        public bool CanWriteEmployees() => IsAuthenticated == true ? Role!.CanWriteEmployees() : throw new InvalidOperationException("Cannot access permission: user is not authenticated.");

        /// <inheritdoc cref="UserRole.CanReadFurniture"/>
        public bool CanReadFurniture() => IsAuthenticated == true ? Role!.CanReadFurniture() : throw new InvalidOperationException("Cannot access permission: user is not authenticated.");

        /// <inheritdoc cref="UserRole.CanWriteFurniture"/>
        public bool CanWriteFurniture() => IsAuthenticated == true ? Role!.CanWriteFurniture() : throw new InvalidOperationException("Cannot access permission: user is not authenticated.");
    }
}
