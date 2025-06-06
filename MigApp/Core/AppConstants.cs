using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Core
{
    public static class AppConstants
    {
        public static class Application
        {
            public const string Name = "MigApp";
        }

        public static class DemoMode
        {
            public const string DefaultUsername = "DemoUser";
            public const string DefaultPassword = "password";
        }

        public static class Database
        {
            public const string DefaultPort = "5432";
        }

        public static class Urls
        {
            public const string ManualLink = "https://vanilla76e2.github.io/MigApp_Manual/";
            public const string ReleasesLink = "https://api.github.com/repos/Vanilla76e2/MigApp/releases";
        }
    }
}
