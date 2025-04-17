namespace MigApp.Core.Services
{
    public interface ICrashLogger
    {
        void Initialize();

        void Close();
    }
}
