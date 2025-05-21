using MigApp.UI.Base;
using System.Data;
using System.Windows.Input;

namespace MigApp.UI.MVVM.ViewModel.Pages
{
    internal class LogsViewModel : Base.ViewModel
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
            //    DataTable temp = await pgsql.GetTable("id, action_date, Username, action, table_name, row, specifies", "\"Misc\".logs", Filter);
            //    temp.Columns["id"].ColumnName = "ID";
            //    temp.Columns["action_date"].ColumnName = "Дата";
            //    temp.Columns["Username"].ColumnName = "Пользователь";
            //    temp.Columns["action"].ColumnName = "Действие";
            //    temp.Columns["table_name"].ColumnName = "Таблица";
            //    temp.Columns["row"].ColumnName = "Запись";
            //    temp.Columns["specifies"].ColumnName = "Подробности";
            //    Table = temp;
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine($"RolesViewModel: {ex.Message}");
            //    MessageBox.Show("Не удалось получить данные из базы данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        #endregion

        #region Фильтр

        private string filter = "";
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
            Filter = "";
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

        public LogsViewModel()
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
