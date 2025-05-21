
using MigApp.Core.Models;

namespace MigApp.Core.Session
{
    public interface IUserSession
    {
        string UserId { get; }
        string Username { get; }
        UserRole? Role { get; }
        DateTime? LoginTime { get; }
        bool IsAuthenticated { get; }

        UserSession StartSession(string id, string username, UserRole role);
        void DisposeSession();
        bool CanReadTechnics();
        bool CanWriteTechnics();
        bool CanReadEmployees();
        bool CanWriteEmployees();
        bool CanReadFurniture();
        bool CanWriteFurniture();
    }
}
