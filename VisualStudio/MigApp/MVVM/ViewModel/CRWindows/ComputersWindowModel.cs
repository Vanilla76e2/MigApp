using MigApp.MVVM.Model.CRWindows;
using MigApp.Services;
using System;
using System.Collections.ObjectModel;

namespace MigApp.MVVM.ViewModel.CRWindows
{
    internal class ComputersWindowModel : Core.ViewModel
    {
        GetModel getModel = new GetModel();

        public ComputersWindowModel() 
        {
            LoadUsersData();
        }
         
        #region Привязка
        private int id { get; set; }

        private string _computerName { get; set; }
        public string ComputerName
        {
            get => _computerName;
            set
            {
                _computerName = value;
                OnPropertyChanged();
            }
        }

        private string _inventoryNumber { get; set; }
        public string InventoryNumber
        {
            get => _inventoryNumber;
            set
            {
                _inventoryNumber = value;
                OnPropertyChanged();
            }
        }

        public string _ip { get; set; }
        public string IP
        {
            get => _ip;
            set
            {
                if(_ip != value)
                {
                    _ip = value;
                    OnPropertyChanged(nameof(IP));
                }
            }
        }

        private ObservableCollection<PersonComboBoxModel> _userList { get; set; }
        public ObservableCollection<PersonComboBoxModel> UserList
        {
            get => _userList;
            set
            {
                _userList = value;
                OnPropertyChanged();
            }
        }

        private int _selectedUser { get; set; }
        public int SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }

        private string _os { get; set; }
        public string OS
        {
            get => _os;
            set
            {
                _os = value;
                OnPropertyChanged();
            }
        }

        private string _comment { get; set; }
        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ComputerComponentsModel> _components {  get; set; }
        public ObservableCollection<ComputerComponentsModel> Components
        {
            get => _components;
            set
            {
                _components = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ComputerConnectedDevicesModel> _devices { get; set; }
        public ObservableCollection <ComputerConnectedDevicesModel> Devices
        {
            get => _devices;
            set
            {
                _devices = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ComputerServiceHistory> _serviceHistory {  get; set; }
        public ObservableCollection<ComputerServiceHistory> ServiceHistory
        {
            get => _serviceHistory;
            set
            {
                _serviceHistory = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Загрузка данных

        // Загрузка списка пользователей
        private async void LoadUsersData()
        {
            try
            {
                UserList = new ObservableCollection<PersonComboBoxModel>(await getModel.UsersFIO());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ComputersWindowModel: Ошибка загрузки пользователей.\n {ex.Message}");
            }
        }
        #endregion

        public void test ()
        {
            Console.WriteLine("VieModel "+IP);
        }
    }
}
