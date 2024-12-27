using Microsoft.Extensions.DependencyInjection;
using MigApp.Core;
using MigApp.CRWindows;
using MigApp.MVVM.View;
using MigApp.MVVM.ViewModel;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace MigApp.Services
{
    public interface INavigationService
    {
        ViewModel CurrentView { get; }
        Task NavigateTo<T>() where T : ViewModel;

        Task NavigateToMainWindow();
        void NavigateToLoginWindow();
    }

    internal class NavigationService : ObservableObject, INavigationService
    {
        private readonly Func<Type, ViewModel> _viewModelFactory;
        private readonly IServiceProvider _serviceProvider;
        private ViewModel _currentView;
        public ViewModel CurrentView
        {
            get => _currentView;
            private set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        // Конструктор класса
        public NavigationService(Func<Type, ViewModel> viewModelFactory, IServiceProvider serviceProvider)
        {
            _viewModelFactory = viewModelFactory;
            _serviceProvider = serviceProvider;

            // По умолчанию открывается Избранное
            CurrentView = _viewModelFactory.Invoke(typeof(FavouriteViewModel));
        }

        // Навигация внутри MainWindow
        public async Task NavigateTo<TViewModel>() where TViewModel : ViewModel
        {
            ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            if (viewModel != CurrentView)
            {
                CurrentView = viewModel;

                if (viewModel is EmployeesViewModel employeesViewModel)
                {
                    await employeesViewModel.LoadTableAsync();
                }
                else if (viewModel is FavouriteViewModel favouriteViewModel)
                {
                    await favouriteViewModel.LoadTableAsync();
                }
                else if (viewModel is DepartmentViewModel departmentViewModel)
                {
                    await departmentViewModel.LoadTableAsync();
                }
                else if (viewModel is ComputersViewModel computersViewModel)
                {
                    computersViewModel.LoadTableAsync();
                }
                else if (viewModel is LaptopsViewModel laptopsViewModel)
                {
                    await laptopsViewModel.LoadTableAsync();
                }
                else if (viewModel is TabletsViewModel tabletsViewModel)
                {
                    await tabletsViewModel.LoadTableAsync();
                }
                else if (viewModel is OrgtechViewModel orgtechViewModel)
                {
                    await orgtechViewModel.LoadTableAsync();
                }
                else if (viewModel is MonitorsViewModel monitorsViewModel)
                {
                    await monitorsViewModel.LoadTableAsync();
                }
                else if (viewModel is RoutersViewModel routersViewModel)
                {
                    await routersViewModel.LoadTableAsync();
                }
                else if (viewModel is SwitchesViewModel switchesViewModel)
                {
                    await switchesViewModel.LoadTableAsync();
                }
                else if (viewModel is CCTVViewModel cctvViewModel)
                {
                    await cctvViewModel.LoadTableAsync();
                }
                else if (viewModel is FurnitureTypeViewModel furnitureTypeViewModel)
                {
                    await furnitureTypeViewModel.LoadTableAsync();
                }
                else if (viewModel is FurnitureViewModel furnitureViewModel)
                {
                    await furnitureViewModel.LoadTableAsync();
                }
                else if (viewModel is UsersViewModel usersViewModel)
                {
                    await usersViewModel.LoadTableAsync();
                }
                else if (viewModel is RolesViewModel rolesViewModel)
                {
                    await rolesViewModel.LoadTableAsync();
                }
                else if (viewModel is LogsViewModel logsViewModel)
                {
                    await logsViewModel.LoadTableAsync();
                }
                else if (viewModel is IPViewModel ipViewModel)
                {
                    await ipViewModel.LoadTableAsync();
                }
                // Сюда добавлять обновления таблиц
            }
        }

        public async Task NavigateToMainWindow()
        {
            var window = new MainView(_serviceProvider);

            if (window != null)
            {
                Application.Current.MainWindow = window;
                window.Show();
            }
            await NavigateTo<FavouriteViewModel>();
        }

        public void NavigateToLoginWindow()
        {
            var window = new LoginView(_serviceProvider);

            if (window != null)
            {
                Application.Current.MainWindow = window;
                window.Show();
            }
        }
    }
}
