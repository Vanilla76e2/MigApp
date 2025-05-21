using System.ComponentModel.DataAnnotations;

namespace MigApp.UI.MVVM.Model
{
    public class ComputersModel
    {
        [Key]
        public string? id { get; set; }
        public string? inventory_number { get; set; }
        public string? name { get; set; }
        public string? ip { get; set; }
        public string? fio { get; set; }
        public string? room { get; set; }
        public string? operating_system { get; set; }
        public string? components { get; set; }
        public string? comment { get; set; }
    }
}
