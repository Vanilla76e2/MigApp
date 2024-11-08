using Microsoft.Extensions.DependencyInjection;
using MigApp.Core;
using MigApp.MVVM.View;
using MigApp.MVVM.ViewModel;
using System;
using System.ComponentModel;
using System.Windows;

namespace MigApp.Services
{
    public interface INavigationService
    {
        ViewModel CurrentView { get; }
        void NavigateTo<T>() where T : ViewModel;

        void NavigateToMainWindow();
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
        public void NavigateTo<TViewModel>() where TViewModel : ViewModel
        {
            ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;

            if (viewModel is EmployeesViewModel employeesViewModel)
            {
                employeesViewModel.OnNavigatedTo();
            } 
            // Сюда добавлять для обновления таблиц
        }

        public void NavigateToMainWindow()
        {
            var window = new MainView(_serviceProvider);

            if (window != null)
            {
                Application.Current.MainWindow = window;
                window.Show();
            }
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
