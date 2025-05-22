using Microsoft.EntityFrameworkCore;
using MigApp.Infrastructure.Data.Entities;
using MigApp.UI.Base;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace MigApp.UI.MVVM.ViewModel.Pages
{
    internal class CCTVViewModel : Base.ViewModel, ILoadbleViewModel
    {

        public CCTVViewModel()
        {
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

        public Task LoadTableAsync()
        {
            //try
            //{
            //    IQueryable<Cctv> query = dbc.Cctvs;

            //    query = query.Where(c => c.Deleted == false);

            //    Table = new ObservableCollection<Cctv>(await query.ToListAsync());
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine($"{ex.Source}: Ошибка при загрузке данных: {ex.Message}");
            //    MessageBox.Show("Не удалось получить данные из базы данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            throw new NotImplementedException("Метод LoadTableAsync не реализован.");
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
