using MigApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Core.Services.AuthService
{
    interface IAuthService
    {
        Task<bool> LoginAsync(string username, string password);
        Task<bool> TestConnectionAsync(DatabaseConnectionParameters parameters);
        Task LoadUserSettingsAsync();
    }
}
