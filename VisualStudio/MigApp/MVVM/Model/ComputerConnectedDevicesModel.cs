using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.MVVM.Model
{
    public class ComputerConnectedDevicesModel
    {
        public string? id {  get; set; }
        public string? name { get; set; }
        public string? inventory_number { get; set; }
        public string? model {  get; set; }
        public string? specification {  get; set; }
        public string? comment { get; set; }
    }
}
