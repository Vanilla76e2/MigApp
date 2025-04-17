namespace MigApp.Core.Services
{
    public sealed class UserRole
    {
        public string Id { get; }
        public string Username { get; }

        public bool IsAdmin { get; }
        public RolePermission EmployeeAccessLevel { get; }
        public RolePermission TechnicsAccessLevel { get; }
        public RolePermission FurnitureAccessLevel { get; }

        public UserRole(string? id, string? Username, bool isAdmin, RolePermission employeeAccessLevel, RolePermission technicsAccessLevel, RolePermission furnitureAccessLevel)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Username = Username ?? throw new ArgumentNullException(nameof(Username));
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
