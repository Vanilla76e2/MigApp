using MigApp.Core;
using MigApp.CRWindows;
using MigApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MigApp.MVVM.ViewModel
{
    public class EmployeesViewModel : Core.ViewModel
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
                DataTable temp = await pgsql.GetTable("id, fio, department_name, room, comment", "\"Employees\".employees_view", Filter);
                temp.Columns["id"].ColumnName = "ID";
                temp.Columns["fio"].ColumnName = "ФИО";
                temp.Columns["department_name"].ColumnName = "Отдел";
                temp.Columns["room"].ColumnName = "Кабинет";
                temp.Columns["comment"].ColumnName = "Комментарий";
                Table = temp;
            }
            catch
            {
                MessageBox.Show("Не удалось получить данные из базы данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

            MyCopyCommand = new RelayCommand(o =>  MyCopy(), o => true);
            RedactCommand = new RelayCommand(o =>  Redact(), o => true);
            MyDeleteCommand = new RelayCommand(o =>  MyDelete(), o => true);
            ReportCommand = new RelayCommand(o =>  Report(), o => true);

        }
    }
}
