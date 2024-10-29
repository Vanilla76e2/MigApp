using MigApp.Core;
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
        private INavigationService _navigation;
        public string WindowTitle { get; set;} = "MigApp v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(3);

        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand NavigateToFavouriteCommand { get; set; }

        public RelayCommand NavigateToEmployeesCommand { get; set; }
        public RelayCommand NavigateToEmployeesGroupsCommand { get; set; }
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

        public MainViewModel(INavigationService navService)
        {
            Navigation = navService;
            NavigateToFavouriteCommand = new RelayCommand(o => { Navigation.NavigateTo<FavouriteViewModel>(); }, o => true);

            NavigateToEmployeesCommand = new RelayCommand(o => { Navigation.NavigateTo<EmployeesViewModel>(); }, o => true);
            NavigateToEmployeesGroupsCommand = new RelayCommand(o => { Navigation.NavigateTo<EmployeesGroupsViewModel>(); }, o => true);
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
        }
    }
}
