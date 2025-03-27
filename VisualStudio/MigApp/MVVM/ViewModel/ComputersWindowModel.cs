using MigApp.Core;
using System.Diagnostics;

namespace MigApp.MVVM.ViewModel.CRWindows
{
    internal class ComputersWindowModel : Core.ViewModel
    {
        //ApplicationContext appContext = ApplicationContext.GetInstance(); // Объявление класса для использования контекста данных приложения



        private string _mode; // Параметр хранящий в себе режим работы окна
        private string _computerID; // Параметр хранящий в себе ID редактируемого компьютера


        public ComputersWindowModel(/*string mode, string computerID*/)
        {
            // Компоненты
            AddComponentCommand = new RelayCommand(ExecuteAddComponent, o => true);

            // Устройства
            AddMonitor = new RelayCommand(o => addMonitor(), o => true); // Установка команды на добавление МОНИТОРА в таблицу УСТРОЙСТВ
            AddUPS = new RelayCommand(o => addUPS(), o => true); // Установка команды на добавление ИБП в таблицу УСТРОЙСТВ
            AddOtherDevice = new RelayCommand(o => addOtherDevice(), o => true); // Установка команды на добавление ДРУГОГО в таблицу УСТРОЙСТВ

            // История обслуживания
            AddService = new RelayCommand(o => addService(), o => true); // Установка команды на добавление записи в таблицу ИСТОРИИ ОБСЛУЖИВАНИЯ


            saveData = new RelayCommand(o => SaveData(), o => true);
            _mode = "test"; // Установка режима работы
            _computerID = "0";

            if (_mode == "edit" && !string.IsNullOrEmpty(_computerID))
            {
                LoadData();
            }
            LoadUsersData();
        }

        #region Команды

        #region Комплектующие
        private void AddComponent(string _name)
        {
            //int newid = Components.Count + 1;
            //Components.Add(new ComputerComponentsModel { id = $"{newid}", name = _name });
        }

        public RelayCommand AddComponentCommand { get; set; }
        private void ExecuteAddComponent(object? parameter)
        {
            if (parameter is string componentType)
            {
                switch (componentType)
                {
                    case "Motherboard":
                        AddComponent("Материнская плата");
                        break;
                    case "CPU":
                        AddComponent("ЦП");
                        break;
                    case "RAM":
                        AddComponent("ОЗУ");
                        break;
                    case "PS":
                        AddComponent("БП");
                        break;
                    case "GPU":
                        AddComponent("ГП");
                        break;
                    case "Other":
                        AddComponent("");
                        break;
                    default:
                        throw new ArgumentException("Invalid component type");
                }
            }
        }
        #endregion

        #region Устройства
        private void AddDevice(string _name)
        {
            //int newid = Devices.Count + 1;
            //Devices.Add(new ComputerConnectedDevicesModel { id = $"{newid}", name = _name });
        }

        public RelayCommand AddMonitor { get; set; }
        private void addMonitor()
        {
            AddDevice("Монитор");
        }

        public RelayCommand AddUPS { get; set; }
        private void addUPS()
        {
            AddDevice("ИБП");
        }

        public RelayCommand AddOtherDevice { get; set; }
        private void addOtherDevice()
        {
            AddDevice("");
        }
        #endregion

        #region Журнал обслуживания
        public RelayCommand AddService { get; set; }
        private void addService()
        {
            //int newid = ServiceHistory.Count + 1;
            string employee = "";
            try
            {
                
            }
            catch
            {

            }
            //ServiceHistory.Add(new ComputerServiceHistory { id = $"{newid}", date = DateTime.Today.Date.ToString("dd.MM.yyyy"), employee = employee });
        }
        #endregion

        #region Сохранение
        public RelayCommand saveData { get; set; } // Объявление команды на сохранение данных
        private async void SaveData() // Сохранение изменений в базе данных
        {
            
            //if (id == null) // Создание записи
            //{
            //    //id = await pgsql.ReqRef($"INSERT INTO \"Technic\".computers (inventory_number, name, ip, employee_id, operating_system, comment) VALUES ({InventoryNumber}, '{ComputerName}', '{IP}', {SelectedUser}, '{OS}', '{Comment}') RETURNING id");
            //    PushComputersComponents();
            //    PushComputersDevices();
            //}
            //else // Редактирование записи
            //{
            //    //await pgsql.ReqDel($"UPDATE \"Technic\".computers (inventory_number = '{InventoryNumber}', name = '{ComputerName}', ip = '{IP}', employee_id = '{SelectedUser}', operating_system = '{OS}', comment = '{Comment}') WHERE id = {id}");
            //    // Перезапись комплектующих при отличии данных
            //    //DataTable table = await pgsql.GetTable("*", "\"Technic\".computers_components", $"WHERE computer_id = {id}");
            //    if (!mc.AreEqual(table, Components))
            //    {
            //        await pgsql.ReqDel($"DLETE FROM \"Technic\".computers_components WHERE computer_id = {id}");
            //        PushComputersComponents();
            //    }
            //    // Перезапись устройств при отличии данных
            //    table = await pgsql.GetTable("*", "\"Technic\".computers_devices", $"WHERE computer_id = {id}");
            //    if(!mc.AreEqual(table, Devices))
            //    {
            //        await pgsql.ReqDel($"DELETE FROM \"Technic\".computers_devices WHERE computer_id = {id}");
            //        PushComputersDevices();
            //    }
            //}
        }

        private async void PushComputersComponents() // Внести таблицу комплектующих в БД
        {
            //foreach (ComputerComponentsModel row in Components)
            //{
            //    await pgsql.ReqNonRef($"INSERT INTO \"Technic\".computers_components (computer_id, component_id, component_name, component_invnum, compoent_specifies) VALUES ({id}, {row.id}, '{row.name}', {row.inventory_number}, '{row.specifies}')");
            //}
        }
        private async void PushComputersDevices() // Внести таблицу устройств в БД
        {
            //foreach (ComputerConnectedDevicesModel row in Devices)
            //{
            //    await pgsql.ReqNonRef($"INSERT INTO \"Technic\".computers_devices (computer_id, device_id, device_invnum, device_specification, device_comment) VALUES ({id}, {row.id}, '{row.name}', {row.inventory_number}, '{row.specification}', '{row.comment}')");
            //}
        }
        private async void PushServiceHistory() // Внести таблицу истории обслуживания в БД
        {
            //foreach(ComputerServiceHistory row in ServiceHistory)
            //{
            //    await pgsql.ReqNonRef($"INSERT INTO \"Technic\".computers_service_history (computer_id, service_id, service_date, servicer, service_description) VALUES ({id}, {row.id}, {row.date}, '{row.employee}', '{row.description}')");
            //}
        }

        #endregion

        #endregion

        #region Привязка
        private string id { get; set; }

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

        public string _ip { get; set; } = "192.168";
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

        //private ObservableCollection<PersonComboBoxModel> _userList = new ObservableCollection<PersonComboBoxModel>();
        //public ObservableCollection<PersonComboBoxModel> UserList
        //{
        //    get => _userList;
        //    set
        //    {
        //        _userList = value;
        //        OnPropertyChanged();
        //    }
        //}

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

        //private ObservableCollection<ComputerComponentsModel> _components = new ObservableCollection<ComputerComponentsModel>();
        //public ObservableCollection<ComputerComponentsModel> Components
        //{
        //    get => _components;
        //    set
        //    {
        //        _components = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private ObservableCollection<ComputerConnectedDevicesModel> _devices = new ObservableCollection<ComputerConnectedDevicesModel>();
        //public ObservableCollection <ComputerConnectedDevicesModel> Devices
        //{
        //    get => _devices;
        //    set
        //    {
        //        _devices = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private ObservableCollection<ComputerServiceHistory> _serviceHistory = new ObservableCollection<ComputerServiceHistory>();
        //public ObservableCollection<ComputerServiceHistory> ServiceHistory
        //{
        //    get => _serviceHistory;
        //    set
        //    {
        //        _serviceHistory = value;
        //        OnPropertyChanged();
        //    }
        //}
        #endregion

        #region Загрузка данных

        // Установка значений при запуске окна
        private void LoadData()
        {

        }

        // Загрузка списка пользователей
        private async void LoadUsersData() // Загрузка выпадающего списка сотрудников
        {
            try
            {
                //UserList = new ObservableCollection<PersonComboBoxModel>(await getModel.UsersFIO());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ComputersWindowModel: Ошибка загрузки пользователей.\n {ex.Message}");
            }
        }

        #endregion
    }
}
