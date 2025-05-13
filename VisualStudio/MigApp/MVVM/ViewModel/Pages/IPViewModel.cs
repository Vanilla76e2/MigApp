using MigApp.Core;
using MigApp.Helpers;
using System.Data;
using System.Net;
using System.Windows.Input;

namespace MigApp.MVVM.ViewModel
{
    internal class IPViewModel : Core.ViewModel
    {
        private int _subnet;

        public int Subnet
        {
            get => _subnet;
            set
            {
                if (value >= 0 && value <= 255)
                {
                    _subnet = value;
                    OnPropertyChanged();
                }
                else if (value < 0)
                {
                    _subnet = 0;
                    OnPropertyChanged();
                }
                else
                {
                    _subnet = 255;
                    OnPropertyChanged();
                }
            }
        }

        #region Таблица

        private DataTable table;
        public DataTable Table
        {
            get => table;
            set
            {
                table = value;
                OnPropertyChanged();
            }
        }

        //public async Task LoadTableAsync()
        //{
        //    //try
        //    //{
        //    //    Table = mc.SortTableByIP("ASC", await pgsql.GetIPsAsync($"{_subnet}"));
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Debug.WriteLine($"IPViewModel: {ex.Message}");
        //    //    MessageBox.Show("Не удалось получить данные из базы данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //    //}
        //}

        #endregion

        #region Фильтр

        private string filter = "Where deleted = False";
        public string Filter
        {
            get => filter;
            set
            {
                filter = value;
                //#pragma warning disable CS4014
                //                LoadTableAsync();
                //#pragma warning restore CS4014
            }
        }

        public void ClearFilter()
        {
            Filter = "Where deleted = False";
        }

        public void ApplyFilter()
        {

        }

        #endregion

        #region Команды
        // Сортировка
        public ICommand SortCommand { get; private set; }

        // Очистить филтр
        public ICommand ClearFilterCommand { get; }

        // Очистить филтр
        public ICommand ApplyFilterCommand { get; }

        // Добавить запись
        public ICommand AddCommand { get; }

        // Редактирование
        public ICommand RedactCommand { get; }

        // Копирование
        public ICommand MyCopyCommand { get; }

        // Удаление
        public ICommand MyDeleteCommand { get; }

        // Отчёт
        public ICommand ReportCommand { get; }

        // Кнопки изменения подсети
        public ICommand AddSubnetCommand { get; }
        public ICommand SubstrSubnetCommand { get; }


        private void AddEmployee()
        {

        }

        private void Redact()
        {

        }

        private void MyCopy()
        {

        }

        private void MyDelete()
        {

        }

        private void Report()
        {

        }
        #endregion

        public IPViewModel()
        {
            _subnet = 0;

            ClearFilterCommand = new RelayCommand(o => ClearFilter(), o => { return Filter != "WHERE deleted = False"; });
            ApplyFilterCommand = new RelayCommand(o => ApplyFilter(), o => true);

            MyCopyCommand = new RelayCommand(o => MyCopy(), o => true);
            RedactCommand = new RelayCommand(o => Redact(), o => true);

            AddSubnetCommand = new RelayCommand(o => { Subnet += 1; }, o => true);
            SubstrSubnetCommand = new RelayCommand(o => { Subnet -= 1; }, o => true);
        }

        /// <summary>
        /// Сортирует таблицу по IP адресу в указанном направлении.
        /// </summary>
        /// <param name="sortDirection"></param>
        /// <param name="table"></param>
        /// <returns>Возвращает отсортированный DataTable.</returns>
        public DataTable SortTableByIP(string sortDirection, DataTable table)
        {
            // 1.  Преобразовать строки IP в IPAddress
            var ipAddressesWithRows = table.AsEnumerable()
                .Select(row => new
                {
                    IpAddr = IPAddress.TryParse(row.Field<string>("IP"), out var ip) ? ip : null,
                    Row = row
                })
                .Where(x => x.IpAddr != null); // Отфильтровываем некорректные IP равные Null

            // 2.  Сортировка  с  помощью  LINQ  и  CompareTo
            IEnumerable<dynamic> sortedRows;
            if (sortDirection == "ASC")
            {
                sortedRows = ipAddressesWithRows.OrderBy(x => x.IpAddr!.GetAddressBytes(), new ByteArrayComparer());
            }
            else // sortDirection == "DESC"
            {
                sortedRows = ipAddressesWithRows.OrderByDescending(x => x.IpAddr!.GetAddressBytes(), new ByteArrayComparer());
            }

            // 3.  Создать новый DataTable с отсортированными данными
            DataTable sortedTable = table.Clone(); // Создаем пустую копию структуры Table
            foreach (var item in sortedRows)
            {
                sortedTable.ImportRow(item.Row);
            }

            // 4.  Заменить старый DataTable новым
            return sortedTable;
        }


    }
}
