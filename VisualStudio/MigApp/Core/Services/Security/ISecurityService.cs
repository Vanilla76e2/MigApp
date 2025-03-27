using MigApp.MVVM.Model;

namespace MigApp.Core.Services
{
    interface ISecurityService
    {
        void SaveDataToVault(DatabaseConnectionParameters DatabaseConnectionParameters, UserAuthData userAuthData);

        (DatabaseConnectionParameters, UserAuthData) LoadDataFromVault();

        string HashText(string text);

    }
}
