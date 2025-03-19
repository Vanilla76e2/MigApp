using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.Interfaces
{
    /// <summary>
    /// Интерфейс для взаимодействия с параметрами подключения к БД.
    /// </summary>
    public interface ISettings
    {
        string pgServer { get; set; }
        string pgPort { get; set; }
        string pgUser { get; set; }
        string pgPassword { get; set; }
        string pgDatabase { get; set; }
    }
}
