using MigApp.Core.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Core.Services
{
    internal class DemoAuthorizationStrategy : IAuthorizationStrategy
    {
        private readonly IAppLogger _logger;
        UserSession _userSession = new UserSession();
        private readonly string _username = "DemoUser";

        public DemoAuthorizationStrategy(IAppLogger logger)
        {
            _logger = logger;
        }

        public Task<AuthResult> AuthorizationAsync(string username, string password)
        {
            _logger.LogInformation($"Авторизация в демонстрационном режиме");
            return Task.FromResult(new AuthResult(true, "Успешный вход (демо-режим)", _userSession.StartSession("0", _username, new UserRole("0", _username, true, RolePermission.ReadWrite, RolePermission.ReadWrite, RolePermission.ReadWrite))));
        }
    }
}
