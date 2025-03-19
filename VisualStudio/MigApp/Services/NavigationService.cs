using Microsoft.Extensions.DependencyInjection;
using MigApp.Core;
using MigApp.Interfaces;
using MigApp.MVVM.View;
using MigApp.MVVM.ViewModel;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace MigApp.Services
{
    internal class NavigationService : ObservableObject, INavigationService
    {
        private readonly Func<Type, ViewModel> _viewModelFactory;
        private readonly IServiceProvider _serviceProvider;
        private ViewModel? _currentView = null!;
        public ViewModel? CurrentView
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

        /// <summary>
        /// Асинхронно переключает текущее отображение на указанную ViewModel.
        /// Если ViewModel реализует интерфейс <see cref="ILoadableViewModel"/>,
        /// то также вызывает метод <see cref="ILoadableViewModel.LoadTableAsync"/>
        /// для асинхронной загрузки данных.
        /// </summary>
        /// <typeparam name="TViewModel">Тип ViewModel, на которую необходимо переключиться.
        ///  ViewModel должна быть наследником класса <see cref="ViewModel"/>.</typeparam>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        /// <exception cref="System.Exception">Возникает, если не удалось создать экземпляр ViewModel.</exception>
        public async Task NavigateTo<TViewModel>() where TViewModel : ViewModel
        {
            ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            if (viewModel != CurrentView)
            {
                CurrentView = viewModel;

                if (viewModel is ILoadbleViewModel loadbleViewModel)
                {
                    await loadbleViewModel.LoadTableAsync();
                }
            }
        }

        /// <summary>
        /// Асинхронно отображает указанное окно, используя фабрику для его создания.
        /// </summary>
        /// <typeparam name="TWindow">Тип окна, которое необходимо отобразить. Должен быть наследником класса <see cref="Window"/>.</typeparam>
        /// <param name="windowFactory">Функция, принимающая <see cref="IServiceProvider"/> и возвращающая экземпляр окна типа <typeparamref name="TWindow"/>.</param>
        /// <param name="postShowAction">Опциональное действие, которое необходимо выполнить после отображения окна. Должно быть асинхронным.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        /// <remarks>
        /// Этот метод использует фабрику для создания экземпляра окна, устанавливает его как главное окно приложения,
        /// отображает окно и, при необходимости, выполняет дополнительное асинхронное действие после отображения.
        /// </remarks>
        /// <exception cref="System.Exception">Возникает, если не удалось создать экземпляр окна.</exception>
        private async Task ShowWindow<TWindow>(Func<IServiceProvider, TWindow> windowFactory, Func<Task>? postShowAction = null) where TWindow : Window
        {
            var window = windowFactory(_serviceProvider);

            if (window != null)
            {
                Application.Current.MainWindow = window;
                window.Show();
                if (postShowAction != null)
                {
                    await postShowAction();
                }
            }
            else
            {
                Debug.WriteLine($"NavigationService.ShowWindow: Не удалось создать {typeof(TWindow).Name}.");
            }
        }

        /// <summary>
        /// Асинхронно отображает главное окно.
        /// </summary>
        public async Task NavigateToMainWindow()
        {
            await ShowWindow(sp => new MainView(sp), async () => await NavigateTo<FavouriteViewModel>());
        }

        /// <summary>
        /// Асинхронно отображает окно авторизации.
        /// </summary>
        public async Task NavigateToLoginWindow()
        {
            await ShowWindow(sp => new LoginWindow(sp));
        }
    }
}
