
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Data;
using MigApp.Core;

namespace MigApp.MVVM.ViewModel
{
    internal class RolesViewModel : Core.ViewModel
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
            //    DataTable temp = await pgsql.GetTable("id, role_name, employees_permission, technics_permission, furniture_permission, administrator_permission", "\"Misc\".roles_view", Filter);
            //    temp.Columns["id"].ColumnName = "ID";
            //    temp.Columns["role_name"].ColumnName = "Наименование";
            //    temp.Columns["employees_permission"].ColumnName = "Сотрудники";
            //    temp.Columns["technics_permission"].ColumnName = "Техника";
            //    temp.Columns["furniture_permission"].ColumnName = "Мебель";
            //    temp.Columns["administrator_permission"].ColumnName = "Администратор";
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

        public RolesViewModel()
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
