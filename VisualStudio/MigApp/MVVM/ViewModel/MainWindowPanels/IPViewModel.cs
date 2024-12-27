using MigApp.Core;
using MigApp.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Data;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Net;
using System.Linq;

namespace MigApp.MVVM.ViewModel
{
    internal class IPViewModel : Core.ViewModel
    {
        PostgreSQLClass pgsql = PostgreSQLClass.getinstance();
        MiscClass mc = new MiscClass();


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
#pragma warning disable CS4014
                LoadTableAsync();
#pragma warning restore CS4014
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

        public async Task LoadTableAsync()
        {
            try
            {
                Table = mc.SortTableByIP("ASC", await pgsql.GetIPsAsync($"{_subnet}"));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"IPViewModel: {ex.Message}");
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
    }
}
