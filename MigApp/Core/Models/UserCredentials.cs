namespace MigApp.Core.Models
{
    /// <summary>
    /// Запись, содержащая данные для аутентификации пользователя.
    /// </summary>
    public record UserCredentials
    {
        public string? Username { get; init; } = string.Empty;
        public string? Password { get; init; } = string.Empty;

        public UserCredentials() { } // Пустой конструктор для сериализации

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password);
        }
    }
}
