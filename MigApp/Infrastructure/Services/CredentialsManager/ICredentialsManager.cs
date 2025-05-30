using MigApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Infrastructure.Services.CredentialsManager
{
    public interface ICredentialsManager
    {
        Task SaveUserCredentialsAsync(string username, string password);

        Task<UserCredentials> LoadUserCredentialsAsync();
        Task SaveUserCredentialsAsync(UserCredentials credentials);
    }
}
