using MigApp.Core;
using MigApp.CRWindows;
using MigApp.MVVM.Model;
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

        private ObservableCollection<EmployeesModel> _employeesTable;
        public ObservableCollection<EmployeesModel> EmployeesTable
        {
            get => _employeesTable;
            set
            {
                _employeesTable = value;
                OnPropertyChanged();
            }
        }

        public async void LoadTable()
        {
            try
            {
                DataTable employeesData = await pgsql.GetTable("*", "\"Employees\".employees_view", EmployeesFilter);

                EmployeesTable = new ObservableCollection<EmployeesModel>(employeesData.Rows.OfType<DataRow>().Select(row =>
                {
                    return new EmployeesModel
                    {
                        ID = Convert.ToInt32(row["id"]),
                        ФИО = row["fio"].ToString(),
                        Отдел = row["group_name"].ToString(),
                        Кабинет = row["room"].ToString()
                    };
                }));
            }
            catch
            {
                MessageBox.Show("Не удалось получить данные из базы данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Фильтр

        private string _employeesFilter = "Where deleted = False";
        public string EmployeesFilter
        {
            get => _employeesFilter;
            set
            {
                _employeesFilter = value;
                LoadTable();
            }
        }

        public void ClearFilter()
        {
            EmployeesFilter = "Where deleted = False";
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

        #endregion

        public EmployeesViewModel()
        {
            AddCommand = new RelayCommand(o => AddEmployee(), o => true);

            ClearFilterCommand = new RelayCommand(o => ClearFilter(), o => { return EmployeesFilter != "WHERE deleted = False"; });
            ApplyFilterCommand = new RelayCommand(o => ApplyFilter(), o => true);

            MyCopyCommand = new RelayCommand(o =>  MyCopy(), o => true);
            RedactCommand = new RelayCommand(o =>  Redact(), o => true);
            MyDeleteCommand = new RelayCommand(o =>  MyDelete(), o => true);
            ReportCommand = new RelayCommand(o =>  Report(), o => true);

        }

        public void OnNavigatedTo()
        {
            LoadTable();
        }

        private void AddEmployee()
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
    }
}
