using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Core.Services
{
    interface IInternetService
    {
        Task<bool> HasInternetConnectionAsync();

        Task<HttpResponseMessage> GetHttpResponseAsync(string uri, string headerName, string? value);
    }
}
