using MigApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Core.Services.Settings
{
    interface ISettingsService
    {
        DatabaseConnectionParameters LoadDatabaseParameters();
        void SaveDatabaseParameters();
        UserAuthData GetUserAuthData();
        void SaveUserAuthData();
    }
}
