using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace MigApp.MVVM.Model
{
    /// <summary>
    /// Запись, содержащая параметры подключения к БД.
    /// </summary>
    public record DatabaseConnectionParameters
    {
        public string? server { get; init; } = string.Empty;
        public string? port { get; init; } = string.Empty;
        public string? database { get; init; } = string.Empty;
        public string? user { get; init; } = string.Empty;
        public string? password { get; init; } = string.Empty;

        public DatabaseConnectionParameters() { } // Пустой конструктор для сериализации
    }
}
