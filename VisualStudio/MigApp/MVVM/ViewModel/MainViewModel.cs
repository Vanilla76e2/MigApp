using MigApp.Core;
using MigApp.MVVM.View;
using MigApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MigApp.MVVM.ViewModel
{
    internal class MainViewModel : Core.ViewModel
    {
        public string WindowTitle { get; set;} = "MigApp v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
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

        private readonly IServiceProvider _serviceProvider;
        private INavigationService _navigation;
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
        public RelayCommand NavigateToFavouriteCommand { get; set; }

        public RelayCommand NavigateToEmployeesCommand { get; set; }
        public RelayCommand NavigateToDepartmentCommand { get; set; }
        public RelayCommand NavigateToComputersCommand { get; set; }
        public RelayCommand NavigateToLaptopsCommand { get; set; }
        public RelayCommand NavigateToTabletsCommand { get; set; }
        public RelayCommand NavigateToOrgtechCommand { get; set; }
        public RelayCommand NavigateToMonitorsCommand { get; set; }
        public RelayCommand NavigateToRoutersCommand { get; set; }
        public RelayCommand NavigateToSwitchesCommand { get; set; }
        public RelayCommand NavigateToCCTVCommand { get; set; }

        public RelayCommand NavigateToFurnitureCommand { get; set; }
        public RelayCommand NavigateToFurnitureTypeCommand { get; set; }

        public RelayCommand NavigateToUsersCommand { get; set; }
        public RelayCommand NavigateToRolesCommand { get; set; }
        public RelayCommand NavigateToLogsCommand { get; set; }
        public RelayCommand NavigateToIPCommand { get; set; }

        public RelayCommand LogOutCommand { get; }
        #endregion

        // Конструктор класса с командами
        public MainViewModel(IServiceProvider serviceProvider, INavigationService navService)
        {
            #if DEBUG
            CurrentUser = "Debug";
            #endif

            _serviceProvider = serviceProvider;
            Navigation = navService;

            NavigateToFavouriteCommand = new RelayCommand(o => { Navigation.NavigateTo<FavouriteViewModel>(); }, o => true);

            NavigateToEmployeesCommand = new RelayCommand(o => { Navigation.NavigateTo<EmployeesViewModel>(); }, o => true);
            NavigateToDepartmentCommand = new RelayCommand(o => { Navigation.NavigateTo<DepartmentViewModel>(); }, o => true);
            NavigateToComputersCommand = new RelayCommand(o => { Navigation.NavigateTo<ComputersViewModel>(); }, o => true);
            NavigateToLaptopsCommand = new RelayCommand(o => { Navigation.NavigateTo<LaptopsViewModel>(); }, o => true);
            NavigateToTabletsCommand = new RelayCommand(o => { Navigation.NavigateTo<TabletsViewModel>(); }, o => true);
            NavigateToOrgtechCommand = new RelayCommand(o => { Navigation.NavigateTo<OrgtechViewModel>(); }, o => true);
            NavigateToMonitorsCommand = new RelayCommand(o => { Navigation.NavigateTo<MonitorsViewModel>(); }, o => true);
            NavigateToRoutersCommand = new RelayCommand(o => { Navigation.NavigateTo<RoutersViewModel>(); }, o => true);
            NavigateToSwitchesCommand = new RelayCommand(o => { Navigation.NavigateTo<SwitchesViewModel>(); }, o => true);
            NavigateToCCTVCommand = new RelayCommand(o => { Navigation.NavigateTo<CCTVViewModel>(); }, o => true);

            NavigateToFurnitureCommand = new RelayCommand(o => { Navigation.NavigateTo<FurnitureViewModel>(); }, o => true);
            NavigateToFurnitureTypeCommand = new RelayCommand(o => { Navigation.NavigateTo<FurnitureTypeViewModel>(); }, o => true);


            NavigateToUsersCommand = new RelayCommand(o => { Navigation.NavigateTo<UsersViewModel>(); }, o => true);
            NavigateToRolesCommand = new RelayCommand(o => { Navigation.NavigateTo<RolesViewModel>(); }, o => true);
            NavigateToLogsCommand = new RelayCommand(o => { Navigation.NavigateTo<LogsViewModel>(); }, o => true);
            NavigateToIPCommand = new RelayCommand(o => { Navigation.NavigateTo<IPViewModel>(); }, o => true);

            LogOutCommand = new RelayCommand(o => LogOut(), o => true);
        }

        // Команда выхода из профиля
        public void LogOut()
        {
            MigApp.Properties.Settings.Default.userLogin = string.Empty;
            MigApp.Properties.Settings.Default.userPassword = string.Empty;
            MigApp.Properties.Settings.Default.userRemember = false;
            MigApp.Properties.Settings.Default.Save();

            // Открыть LoginView
            Navigation.NavigateToLoginWindow();

            // Закрыть MainWindow
            var mainWindow = Application.Current.Windows.OfType<MainView>().FirstOrDefault();
            if (mainWindow != null) { mainWindow.Close(); }
        }

        private async Task UpdateTable()
        {
            var currentView = Navigation.CurrentView;
            if(currentView is EmployeesViewModel employeesViewModel)
            {
                await employeesViewModel.LoadTableAsync();
            }
            else
            {
                Console.WriteLine("UpdateTable: CurrentView не соответствует ни одному из предусмотренных параметров");
            }
        }
    }
}
