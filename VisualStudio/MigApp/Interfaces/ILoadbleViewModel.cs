using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Interfaces
{
    interface ILoadbleViewModel
    {
        Task LoadTableAsync();
    }
}
