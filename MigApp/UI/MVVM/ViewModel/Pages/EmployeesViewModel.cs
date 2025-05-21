using MigApp.UI.Base;
using System.Data;
using System.Windows.Input;

namespace MigApp.UI.MVVM.ViewModel.Pages
{
    public class EmployeesViewModel : Base.ViewModel
    {

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

        public async Task LoadTableAsync()
        {
            //try
            //{
            //    DataTable temp = await pgsql.GetTable("id, fio, department_name, room, phone_number, comment", "\"Employees\".employees_view", Filter);
            //    if (temp != null)
            //    {
            //        if (temp.Columns.Contains("id")) temp.Columns["id"].ColumnName = "ID";
            //        if (temp.Columns.Contains("fio")) temp.Columns["fio"].ColumnName = "ФИО";
            //        if (temp.Columns.Contains("department_name")) temp.Columns["department_name"].ColumnName = "Отдел";
            //        if (temp.Columns.Contains("room")) temp.Columns["room"].ColumnName = "Кабинет";
            //        if (temp.Columns.Contains("phone_number")) temp.Columns["phone_number"].ColumnName = "Телефон";
            //        if (temp.Columns.Contains("comment")) temp.Columns["comment"].ColumnName = "Комментарий";
            //    }
            //    Table = temp;
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine($"EmployeesViewModel: {ex.Message}");
            //    MessageBox.Show("Не удалось получить данные из базы данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        #endregion

        #region Фильтр

        private string filter = "Where deleted = False";
        public string Filter
        {
            get => filter;
            set
            {
                filter = value;
#pragma warning disable CS4014
                LoadTableAsync();
#pragma warning restore CS4014
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

        public EmployeesViewModel()
        {
            AddCommand = new RelayCommand(o => AddEmployee(), o => true);

            ClearFilterCommand = new RelayCommand(o => ClearFilter(), o => { return Filter != "WHERE deleted = False"; });
            ApplyFilterCommand = new RelayCommand(o => ApplyFilter(), o => true);

            MyCopyCommand = new RelayCommand(o => MyCopy(), o => true);
            RedactCommand = new RelayCommand(o => Redact(), o => true);
            MyDeleteCommand = new RelayCommand(o => MyDelete(), o => true);
            ReportCommand = new RelayCommand(o => Report(), o => true);

        }
    }
}
