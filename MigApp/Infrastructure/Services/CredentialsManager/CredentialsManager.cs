using MigApp.Core.Models;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.Infrastructure.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Infrastructure.Services.CredentialsManager
{
    public class CredentialsManager : ICredentialsManager
    {
        private readonly ISecurityService _securityService;

        public CredentialsManager(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        public async Task<UserCredentials> LoadUserCredentialsAsync()
        {
            return await _securityService.LoadUserCredentialsFromVaultAsync();
        }

        public async Task SaveUserCredentialsAsync(string username, string password)
        {
            await _securityService.SaveUserCredentialsToVaultAsync(new UserCredentials
            {
                Username = username,
                Password = password
            });
        }

        public async Task SaveUserCredentialsAsync(UserCredentials credentials)
        {
            await _securityService.SaveUserCredentialsToVaultAsync(credentials);
        }
    }
}
