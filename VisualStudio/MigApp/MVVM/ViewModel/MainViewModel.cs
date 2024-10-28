using MigApp.Core;
using MigApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.MVVM.ViewModel
{
    internal class MainViewModel : Core.ViewModel
    {
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

        public RelayCommand NavigateToFavouriteCommand { get; set; }
        public RelayCommand NavigateToEmployeesCommand { get; set; }

        public MainViewModel(INavigationService navService)
        {
            Navigation = navService;
            NavigateToFavouriteCommand = new RelayCommand(o => { Navigation.NavigateTo<FavouriteViewModel>(); }, o => true);
            NavigateToEmployeesCommand = new RelayCommand(o => { Navigation.NavigateTo<EmployeesViewModel>(); }, o => true);
        }
    }
}
