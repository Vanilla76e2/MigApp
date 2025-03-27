using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Core.Services
{
    interface ICrashLogger
    {
        void Initialize();

        void Close();
    }
}
