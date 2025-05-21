using MigApp.UI.Base;
using System.Data;
using System.Windows.Input;

namespace MigApp.UI.MVVM.ViewModel.Pages
{
    internal class FavouriteViewModel : Base.ViewModel
    {

        //ApplicationContext appContext = ApplicationContext.GetInstance();

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
            //    DataTable temp = await pgsql.GetTable("faved_date, table_name, row_id, inventory_number, naming, comment", "\"Misc\".favourite_view", Filter);
            //    temp.Columns["faved_date"].ColumnName = "Дата";
            //    temp.Columns["table_name"].ColumnName = "Категория";
            //    temp.Columns["row_id"].ColumnName = "ID";
            //    temp.Columns["inventory_number"].ColumnName = "Инвентарный номер";
            //    temp.Columns["naming"].ColumnName = "Имя";
            //    temp.Columns["comment"].ColumnName = "Комментарий";
            //    Table = temp;
            //}
            //catch
            //{
            //    MessageBox.Show("Не удалось получить данные из базы данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }
        #endregion

        #region Фильтр
        private string filter;
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

        public ICommand ClearFilterCommand;
        public ICommand ApplyFilterCommand;

        public ICommand RedactCommand;
        public ICommand MyCopyCommand;
        public ICommand RemoveFavCommand;

        public void Redact()
        {

        }

        public void MyCopy()
        {

        }

        public void RemoveFav()
        {

        }

        #endregion

        public FavouriteViewModel()
        {

            ClearFilterCommand = new RelayCommand(o => ClearFilter(), o => true);
            ApplyFilterCommand = new RelayCommand(o => ApplyFilter(), o => true);

            RedactCommand = new RelayCommand(o => Redact(), o => true);
            MyCopyCommand = new RelayCommand(o => MyCopy(), o => true);
            RemoveFavCommand = new RelayCommand(o => RemoveFav(), o => true);
        }
    }
}
