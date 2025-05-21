namespace MigApp.Infrastructure.Services.AppLogger
{
    public interface ICrashLogger
    {
        void Initialize();

        void Close();
    }
}
