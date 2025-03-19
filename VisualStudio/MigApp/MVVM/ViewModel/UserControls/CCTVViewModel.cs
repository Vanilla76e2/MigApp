using MigApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MigApp.Data;
using System.Windows;
using System.Data;
using MigApp.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using MigApp.Interfaces;

namespace MigApp.MVVM.ViewModel
{
    internal class CCTVViewModel : Core.ViewModel, ILoadbleViewModel
    {
        private readonly MigDataBaseContext dbc;

        public CCTVViewModel(MigDataBaseContext context)
        {
            dbc = context;
            AddCommand = new RelayCommand(o => AddEmployee(), o => true);

            ClearFilterCommand = new RelayCommand(o => ClearFilter(), o => { return Filter != "WHERE deleted = False"; });
            ApplyFilterCommand = new RelayCommand(o => ApplyFilter(), o => true);

            MyCopyCommand = new RelayCommand(o => MyCopy(), o => true);
            RedactCommand = new RelayCommand(o => Redact(), o => true);
            MyDeleteCommand = new RelayCommand(o => MyDelete(), o => true);
            ReportCommand = new RelayCommand(o => Report(), o => true);

        }

        #region Таблица

        private ObservableCollection<Cctv> _table = new ObservableCollection<Cctv>();
        public ObservableCollection<Cctv> Table
        {
            get => _table;
            set
            {
                _table = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadTableAsync()
        {
            try
            {
                IQueryable<Cctv> query = dbc.Cctvs;

                query = query.Where(c => c.Deleted == false);

                Table = new ObservableCollection<Cctv>(await query.ToListAsync());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Source}: Ошибка при загрузке данных: {ex.Message}");
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
    }
}
