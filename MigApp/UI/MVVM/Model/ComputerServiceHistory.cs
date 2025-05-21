namespace MigApp.UI.MVVM.Model
{
    public class ComputerServiceHistory
    {
        public string? id { get; set; }
        private string? _date { get; set; }
        public string? date
        {
            get => _date;
            set
            {
                if (value.Length > 10)
                    _date = value.Substring(0, 10);
                else
                    _date = value;
            }
        }
        public string? employee { get; set; }
        public string? description { get; set; }
    }
}
