using MigApp.Core;
using MigApp.CRWindows;
using MigApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Data;

namespace MigApp.MVVM.ViewModel
{
    internal class DepartmentViewModel : Core.ViewModel
    {
        PostgreSQLClass pgsql = PostgreSQLClass.getinstance();

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
            try
            {
                DataTable temp = await pgsql.GetTable("*", "\"Employees\".departments", Filter);
                temp.Columns["id"].ColumnName = "ID";
                temp.Columns["department_name"].ColumnName = "Наименование";
                Table = temp;
            }
            catch
            {
                MessageBox.Show("Не удалось получить данные из базы данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
        // Фильтры
        public ICommand ClearFilterCommand { get; }
        public ICommand ApplyFilterCommand { get; }

        // Добавить запись
        public ICommand AddCommand { get; }

        // Контекстное меню
        public ICommand RedactCommand { get; }
        public ICommand MyCopyCommand { get; }
        public ICommand MyDeleteCommand { get; }


        private void AddDepartment()
        {
            EmployeesWindow win = new EmployeesWindow(false, null, false, true);
            win.Show();
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

        public DepartmentViewModel()
        {
            AddCommand = new RelayCommand(o => AddDepartment(), o => true);

            ClearFilterCommand = new RelayCommand(o => ClearFilter(), o => { return Filter != "WHERE deleted = False"; });
            ApplyFilterCommand = new RelayCommand(o => ApplyFilter(), o => true);

            MyCopyCommand = new RelayCommand(o => MyCopy(), o => true);
            RedactCommand = new RelayCommand(o => Redact(), o => true);
            MyDeleteCommand = new RelayCommand(o => MyDelete(), o => true);

        }
    }
}
