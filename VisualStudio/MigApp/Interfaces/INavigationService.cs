using MigApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Interfaces
{
    interface INavigationService
    {
        ViewModel? CurrentView { get; }
        Task NavigateTo<T>() where T : ViewModel;

        Task NavigateToMainWindow();
        Task NavigateToLoginWindow();
    }
}
