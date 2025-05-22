using MigApp.Core.Models;
using MigApp.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Application.Services.PasswodMange
{
    public interface IPasswordVerifier
    {
        /// <summary>
        /// Проверяет пароль пользователя и возвращает результат авторизации.
        /// </summary>
        /// <param name="userProfile">Профиль пользователя, полученный из БД.</param>
        /// <param name="plainPassword">Введённый пользователем пароль в открытом виде.</param>
        /// <returns>Результат авторизации в виде <see cref="AuthResult"/>.</returns>
        Task<AuthResult> VerifyPasswordAsync(UsersProfile userProfile, string plainPassword);
    }
}
