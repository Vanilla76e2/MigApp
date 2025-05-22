
namespace MigApp.Core.Models
{
    public class StartupResult
    {
        public bool IsConnectionSuccessful { get; init; }
        public DatabaseConnectionParameters Connection { get; set; } = new();
        public UserCredentials Credentials { get; set; } = new();
    }
}
