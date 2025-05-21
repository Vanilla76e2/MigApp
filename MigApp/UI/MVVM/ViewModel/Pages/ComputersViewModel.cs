using MigApp.MVVM.View.CRWindows;
using MigApp.UI.Base;
using MigApp.UI.MVVM.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MigApp.UI.MVVM.ViewModel.Pages
{
    internal class ComputersViewModel : Base.ViewModel
    {
        #region Таблица
        private ObservableCollection<ComputersModel> _computers;
        public ObservableCollection<ComputersModel> Computers
        {
            get => _computers;
            set
            {
                _computers = value;
                OnPropertyChanged();
            }
        }

        public async void LoadTableAsync()
        {
            //try
            //{
            //    Computers = new ObservableCollection<ComputersModel>(await getModel.Computers(Filter));
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Не удалось получить данные из базы данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    Debug.WriteLine($"ComputersViewModel: Ошибка загрузки данных\n{ex.Message}");
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
                LoadTableAsync();
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

        public ComputersViewModel()
        {


            AddCommand = new RelayCommand(o => { ComputersWindow rcwin = new ComputersWindow(); rcwin.Show(); return; }, o => true);

            ClearFilterCommand = new RelayCommand(o => ClearFilter(), o => { return Filter != "WHERE deleted = False"; });
            ApplyFilterCommand = new RelayCommand(o => ApplyFilter(), o => true);

            MyCopyCommand = new RelayCommand(o => MyCopy(), o => true);
            RedactCommand = new RelayCommand(o => Redact(), o => true);
            MyDeleteCommand = new RelayCommand(o => MyDelete(), o => true);
            ReportCommand = new RelayCommand(o => Report(), o => true);

        }
    }
}
