using MigApp.Core.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Core.Session
{
    interface IUserSession
    {
        string UserId { get; }
        string UserName { get; }
        UserRole Role { get; }
        bool IsAuthenticated { get; }

        bool CanReadTechnics();
        bool CanWriteTechnics();
        bool CanReadEmployees();
        bool CanWriteEmployees();
        bool CanReadFurniture();
        bool CanWriteFurniture();
    }
}
