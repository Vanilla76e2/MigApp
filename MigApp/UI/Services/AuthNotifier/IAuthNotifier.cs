using MigApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.UI.Services.AuthNotifier
{
    public interface IAuthNotifier
    {
        Task NotifyAsync(AuthResult result);
        Task<bool> ConfirmPasswordChangeAsync();
    }
}
