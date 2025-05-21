using MigApp.MVVM.View;
using MigApp.UI.Base;
using MigApp.UI.MVVM.ViewModel.Pages;
using MigApp.UI.Services.Navigation;

namespace MigApp.UI.MVVM.ViewModel
{
    class MainWindowModel : Base.ViewModel
    {
        public string WindowTitle { get; set; } = "MigApp v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
        private string currentUser { get; set; }
        public string CurrentUser
        {
            get => currentUser;
            set
            {
                currentUser = value;
                OnPropertyChanged();
            }
        }

        private readonly IServiceProvider _serviceProvider; // Объявление сервис-провайдера для разрешения зависимостей
        private INavigationService _navigation; // Сервис навигации для перемещения между представлениями.

        // Получает или устанавливает сервис навигации.
        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        #region Команды
        public RelayCommand NavigateToFavouriteCommand { get; set; } // Команда для перемещения на "Избранное"

        public RelayCommand NavigateToEmployeesCommand { get; set; } // Команда для перемещения на "Сотрудников"
        public RelayCommand NavigateToDepartmentCommand { get; set; } // Команда для перемещения на "Отделы"
        public RelayCommand NavigateToComputersCommand { get; set; } // Команда для перемещения на "Компьютеры"
        public RelayCommand NavigateToLaptopsCommand { get; set; } // Команда для перемещения на "Ноутбуки"
        public RelayCommand NavigateToTabletsCommand { get; set; } // Команда для перемещения на "Плашеты"
        public RelayCommand NavigateToOrgtechCommand { get; set; } // Команда для перемещения на "Оргтехнику"
        public RelayCommand NavigateToMonitorsCommand { get; set; } // Команда для перемещения на "Мониторы"
        public RelayCommand NavigateToRoutersCommand { get; set; } // Команда для перемещения на "Роутеры"
        public RelayCommand NavigateToSwitchesCommand { get; set; } // Команда для перемещения на "Коммутаторы"
        public RelayCommand NavigateToCCTVCommand { get; set; } // Команда для перемещения на "Видеонаблюдение"

        public RelayCommand NavigateToFurnitureCommand { get; set; } // Команда для перемещения на "Мебель"
        public RelayCommand NavigateToFurnitureTypeCommand { get; set; } // Команда для перемещения на "Типы мебели"

        public RelayCommand NavigateToUsersCommand { get; set; } // Команда для перемещения на "Пользователей"
        public RelayCommand NavigateToRolesCommand { get; set; } // Команда для перемещения на "Роли"
        public RelayCommand NavigateToLogsCommand { get; set; } // Команда для перемещения на "Журнал собыитий"
        public RelayCommand NavigateToIPCommand { get; set; } // Команда для перемещения на "Список IP-адрессов"

        public RelayCommand LogOutCommand { get; } // Команда для выхода из профиля
        #endregion

        // Конструктор класса с установкой команд командами
        public MainWindowModel(IServiceProvider serviceProvider, INavigationService navService)
        {
#if DEBUG
            CurrentUser = "Debug";
#endif

            _serviceProvider = serviceProvider; // Установка сервис-провайдера для разрешения зависимостей
            Navigation = navService; // Установка сервиса навигации

            NavigateToFavouriteCommand = new RelayCommand(o => { Navigation.NavigateTo<FavouriteViewModel>(); }, o => true); // Установка команды для перемещения на "Избранное"
            NavigateToEmployeesCommand = new RelayCommand(o => { Navigation.NavigateTo<EmployeesViewModel>(); }, o => true); // Установка команды для перемещения на "Сотрудников"
            NavigateToDepartmentCommand = new RelayCommand(o => { Navigation.NavigateTo<DepartmentViewModel>(); }, o => true); // Установка команды для перемещения на "Отделы"
            NavigateToComputersCommand = new RelayCommand(o => { Navigation.NavigateTo<ComputersViewModel>(); }, o => true); // Установка команды для перемещения на "Компьютеры"
            NavigateToLaptopsCommand = new RelayCommand(o => { Navigation.NavigateTo<LaptopsViewModel>(); }, o => true); // Установка команды для перемещения на "Ноутбуки"
            NavigateToTabletsCommand = new RelayCommand(o => { Navigation.NavigateTo<TabletsViewModel>(); }, o => true); // Установка команды для перемещения на "Плашеты"
            NavigateToOrgtechCommand = new RelayCommand(o => { Navigation.NavigateTo<OrgtechViewModel>(); }, o => true); // Установка команды для перемещения на "Оргтехнику"
            NavigateToMonitorsCommand = new RelayCommand(o => { Navigation.NavigateTo<MonitorsViewModel>(); }, o => true); // Установка команды для перемещения на "Мониторы"
            NavigateToRoutersCommand = new RelayCommand(o => { Navigation.NavigateTo<RoutersViewModel>(); }, o => true); // Установка команды для перемещения на "Роутеры"
            NavigateToSwitchesCommand = new RelayCommand(o => { Navigation.NavigateTo<SwitchesViewModel>(); }, o => true); // Установка команды для перемещения на "Коммутаторы"
            NavigateToCCTVCommand = new RelayCommand(o => { Navigation.NavigateTo<CCTVViewModel>(); }, o => true); // Установка команды для перемещения на "Видеонаблюдение"

            NavigateToFurnitureCommand = new RelayCommand(o => { Navigation.NavigateTo<FurnitureViewModel>(); }, o => true); // Установка команды для перемещения на "Мебель"
            NavigateToFurnitureTypeCommand = new RelayCommand(o => { Navigation.NavigateTo<FurnitureTypeViewModel>(); }, o => true); // Установка команды для перемещения на "Типы мебели"


            NavigateToUsersCommand = new RelayCommand(o => { Navigation.NavigateTo<UsersViewModel>(); }, o => true); // Установка команды для перемещения на "Пользователей"
            NavigateToRolesCommand = new RelayCommand(o => { Navigation.NavigateTo<RolesViewModel>(); }, o => true); // Установка команды для перемещения на "Роли"
            NavigateToLogsCommand = new RelayCommand(o => { Navigation.NavigateTo<LogsViewModel>(); }, o => true); // Установка команды для перемещения на "Журнал собыитий"
            NavigateToIPCommand = new RelayCommand(o => { Navigation.NavigateTo<IPViewModel>(); }, o => true); // Установка команды для перемещения на "Список IP-адрессов"

            LogOutCommand = new RelayCommand(o => LogOut(), o => true); // Установка команды для выхода из профиля
        }

        // Команда выхода из профиля
        public void LogOut()
        {


            // Открыть окно LoginView
            Navigation.NavigateToLoginWindow();

            // Закрыть окно MainWindow
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null) { mainWindow.Close(); }
        }

        // Команда для принудительного обновления таблиц
        private async Task UpdateTable()
        {
            var currentView = Navigation.CurrentView; // Получение текущего представления
            if (currentView is EmployeesViewModel employeesViewModel) // Обновление таблицы "Сотрудники"
            {
                await employeesViewModel.LoadTableAsync();
            }
            // Добавить обновление для остальных таблиц
            else
            {
                Debug.WriteLine("UpdateTable: CurrentView не соответствует ни одному из предусмотренных параметров");
            }
        }
    }
}
