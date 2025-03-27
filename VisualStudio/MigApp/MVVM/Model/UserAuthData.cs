using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.MVVM.Model
{
    /// <summary>
    /// Запись, содержащая данные для аутентификации пользователя.
    /// </summary>
    public record UserAuthData
    {
        public string? username { get; init; } = string.Empty;
        public string? password { get; init; } = string.Empty;

        public UserAuthData() { } // Пустой конструктор для сериализации

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password);
        }
    }
}
