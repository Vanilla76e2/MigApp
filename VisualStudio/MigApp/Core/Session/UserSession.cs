using MigApp.Core.Authorization;

namespace MigApp.Core.Session
{
    /// <summary>
    /// Данные текущей сессии пользователя.
    /// Обновляется при входе/выходе.
    /// </summary>
    public sealed class UserSession: IUserSession
    {
        public string UserId { get; }
        public string UserName { get; }
        public UserRole Role { get; }
        public DateTime LoginTime { get; }
        public bool IsAuthenticated => true;

        /// <summary>
        /// Создает новую сессию пользователя.
        /// </summary>
        /// <param name="userId">ID пользователя в базе данных.</param>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="role">Роль пользователя, определяющая его права.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UserSession(string userId, string userName, UserRole role)
        {
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Role = role ?? throw new ArgumentNullException(nameof(role));
            LoginTime = DateTime.UtcNow;
        }

        /// <inheritdoc cref="UserRole.CanReadTechnics"/>
        public bool CanReadTechnics() => Role.CanReadTechnics();

        /// <inheritdoc cref="UserRole.CanWriteTechnics"/>
        public bool CanWriteTechnics() => Role.CanWriteTechnics();

        /// <inheritdoc cref="UserRole.CanReadEmployees"/>
        public bool CanReadEmployees() => Role.CanReadEmployees();

        /// <inheritdoc cref="UserRole.CanWriteEmployees"/>
        public bool CanWriteEmployees() => Role.CanWriteEmployees();

        /// <inheritdoc cref="UserRole.CanReadFurniture"/>
        public bool CanReadFurniture() => Role.CanReadFurniture();

        /// <inheritdoc cref="UserRole.CanWriteFurniture"/>
        public bool CanWriteFurniture() => Role.CanWriteFurniture();
    }
}
