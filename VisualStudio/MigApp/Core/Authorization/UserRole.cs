using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Core.Authorization
{
    public sealed class UserRole
    {
        public string Id { get; }
        public string UserName { get; }

        public bool IsAdmin { get; }
        public RolePermission EmployeeAccessLevel { get; }
        public RolePermission TechnicsAccessLevel { get; }
        public RolePermission FurnitureAccessLevel { get; }

        public UserRole(string? id, string? userName, bool isAdmin, RolePermission employeeAccessLevel, RolePermission technicsAccessLevel, RolePermission furnitureAccessLevel)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            IsAdmin = isAdmin;
            EmployeeAccessLevel = employeeAccessLevel;
            TechnicsAccessLevel = technicsAccessLevel;
            FurnitureAccessLevel = furnitureAccessLevel;
        }

        /// <summary>
        /// Возвращает true если пользователь имеет доступ на чтение данных о технике.
        /// </summary>
        public bool CanReadTechnics() => CheckAccess(TechnicsAccessLevel, RolePermission.Read);
        /// <summary>
        /// Возвращает true если пользователь имеет доступ на редактирование данных о технике.
        /// </summary>
        public bool CanWriteTechnics() => CheckAccess(TechnicsAccessLevel, RolePermission.ReadWrite);
        /// <summary>
        /// Возвращает true если пользователь имеет доступ на чтение данных о сотрудниках.
        /// </summary>
        public bool CanReadEmployees() => CheckAccess(EmployeeAccessLevel, RolePermission.Read);
        /// <summary>
        /// Возвращает true если пользователь имеет доступ на редактирование данных о сотрудниках.
        /// </summary>
        public bool CanWriteEmployees() => CheckAccess(EmployeeAccessLevel, RolePermission.ReadWrite);
        /// <summary>
        /// Возвращает true если пользователь имеет доступ на чтение данных о мебели.
        /// </summary>
        public bool CanReadFurniture() => CheckAccess(FurnitureAccessLevel, RolePermission.Read);
        /// <summary>
        /// Возвращает true если пользователь имеет доступ на редактирование данных о мебели.
        /// </summary>
        public bool CanWriteFurniture() => CheckAccess(FurnitureAccessLevel, RolePermission.ReadWrite);

        private bool CheckAccess(RolePermission currentPermission, RolePermission requiredPermission)
        {
            if (IsAdmin) return true;
            return currentPermission >= requiredPermission;
        }
    }
}
