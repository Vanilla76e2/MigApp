using Microsoft.EntityFrameworkCore;
using MigApp.Core.Models;
using MigApp.Core.Session;
using MigApp.Infrastructure.Data.Entities;
using MigApp.Infrastructure.Repository.Common;
using MigApp.Infrastructure.Services.AppLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Application.Services.SessionBuilder
{
    internal class SessionBuilder : ISessionBuilder
    {
        private readonly IDatabaseRepository<Role> _roleRepository;
        private readonly IUserSession _userSession;
        private readonly IAppLogger _logger;

        public SessionBuilder(IDatabaseRepository<Role> roleRepository,
                              IUserSession userSession,
                              IAppLogger logger)
        {
            _roleRepository = roleRepository;
            _userSession = userSession;
            _logger = logger;
        }

        public async Task<UserSession> BuildAsync(UsersProfile userProfile)
        {
            _logger.LogInformation($"Начало сборки сессии для пользователя {userProfile.Username}");

            var roleEntity = await _roleRepository.GetByIdAsync(userProfile.Role);
            if (roleEntity == null)
            {
                _logger.LogError($"Роль с ID={userProfile.Role} не найдена в базе");
                throw new NullReferenceException("Роль пользователя не найдена");
            }

            var role = new UserRole(
                roleEntity.Id.ToString(),
                roleEntity.RoleName,
                roleEntity.IsAdministrator,
                roleEntity.EmployeesAccesslevel,
                roleEntity.TechnicsAccesslevel,
                roleEntity.FurnitureAccesslevel
            );

            var session = _userSession.StartSession(
                userProfile.Id.ToString(),
                userProfile.Username,
                role
            );

            _logger.LogInformation($"Сессия пользователя {userProfile.Username} успешно создана");
            return session;
        }
    }
}
