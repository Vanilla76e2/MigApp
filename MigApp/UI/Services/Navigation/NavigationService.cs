using Microsoft.Extensions.DependencyInjection;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.MVVM.View;
using MigApp.UI.Base;
using MigApp.UI.MVVM.ViewModel;
using MigApp.UI.MVVM.ViewModel.Pages;
using System.Windows;

namespace MigApp.UI.Services.Navigation
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private readonly Func<Type, ViewModel> _viewModelFactory;
        private readonly IServiceProvider _serviceProvider;
        private ViewModel? _currentView = null;
        private readonly IAppLogger _logger;
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
        public NavigationService(Func<Type, ViewModel> viewModelFactory, IServiceProvider serviceProvider, IAppLogger logger)
        {
            _viewModelFactory = viewModelFactory;
            _serviceProvider = serviceProvider;
            _logger = logger;

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
        /// <exception cref="Exception">Возникает, если не удалось создать экземпляр ViewModel.</exception>
        public async Task NavigateTo<TViewModel>() where TViewModel : ViewModel
        {
            _logger.LogDebug($"Начало навигации к {typeof(TViewModel).Name}");
            try
            {
                ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
                if (viewModel != CurrentView)
                {
                    CurrentView = viewModel;
                    _logger.LogInformation($"Текущая ViewModel изменена на {viewModel.GetType().Name}");

                    if (viewModel is ILoadbleViewModel loadbleViewModel)
                    {
                        _logger.LogDebug($"Обнаружен интерфейс ILoadableViewModel. Начинается загрузка данных");
                        await loadbleViewModel.LoadTableAsync();
                        _logger.LogInformation($"Данные {typeof(TViewModel).Name} успешно загружены");
                    }
                }
                else _logger.LogInformation($"Модель {typeof(TViewModel).Name} уже активна");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при навигации к ViewModel {typeof(TViewModel).Name}");
                throw;
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
        /// <exception cref="Exception">Возникает, если не удалось создать экземпляр окна.</exception>
        private async Task ShowWindow<TWindow>(Func<IServiceProvider, TWindow> windowFactory, Func<Task>? postShowAction = null) where TWindow : Window
        {
            _logger.LogDebug($"Начало отображения окна {typeof(TWindow).Name}");
            try
            {
                var window = windowFactory(_serviceProvider);

                if (window == null)
                {
                    throw new InvalidOperationException($"Фабрика вернула null для окна {typeof(TWindow).Name}");
                }

                _logger.LogInformation($"Окно {typeof(TWindow).Name} успешно создано");

                System.Windows.Application.Current.MainWindow = window;
                window.Show();
                _logger.LogInformation($"Окно {typeof(TWindow).Name} установлено как главное");
                Debug.WriteLine($"LoginWindowModel создан: {window.GetHashCode()}");

                if (postShowAction != null)
                {
                    _logger.LogDebug($"Выполнение post-действия для окна.");
                    await postShowAction();
                    _logger.LogInformation($"Post-действие для окна {typeof(TWindow).Name} выполнено");
                }
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"Не удалось создать экземпляр окна {typeof(TWindow).Name}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при отображении окна {typeof(TWindow).Name}");
                throw;
            }
        }

        /// <summary>
        /// Асинхронно отображает главное окно.
        /// </summary>
        public async Task NavigateToMainWindow()
        {
            await ShowWindow(sp => new MainWindow(sp), async () => await NavigateTo<FavouriteViewModel>());
        }

        /// <summary>
        /// Асинхронно отображает окно авторизации.
        /// </summary>
        public async Task NavigateToLoginWindow()
        {
            await ShowWindow(sp => new LoginWindow(sp.GetRequiredService<LoginWindowModel>()));
        }
    }
}
