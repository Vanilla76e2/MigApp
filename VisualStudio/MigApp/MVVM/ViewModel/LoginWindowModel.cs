using Microsoft.Extensions.DependencyInjection;
using MigApp.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using System.Security;
using MigApp.Core;
using MigApp.MVVM.Model;
using MigApp.Core.Services.NavigationService;
using MigApp.Core.Services.Settings;
using MigApp.Core.Services.AuthService;

namespace MigApp.MVVM.ViewModel
{
    /// <summary>
    /// ViewModel для окна авторизации. Обрабатывает логику авторизации пользователя, управления настройками подключения к базе данных и навигации между представлениями.
    /// </summary>
    internal class LoginWindowModel : Core.ViewModel
    {
        // Сервисы
        private readonly IAuthService _authService;
        private readonly ISettingsService _settingsService;
        private readonly INavigationService _navigationService;
        private readonly IAppLogger _logger;

        // Команды
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand SaveSettingsCommand { get; set; }
        public RelayCommand ToggleSettingsCommand { get; set; }

        #region Свойства
        private bool isSettingsVisible { get; set; } = false;
        public bool IsSettingsOn
        {
            get => isSettingsVisible;
            set
            {
                isSettingsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool isConnectionCorrect { get; set; } = false;
        public bool IsConnectionCorrect
        {
            get => isConnectionCorrect;
            set
            {
                isConnectionCorrect = value;
                OnPropertyChanged();
            }
        }

        private bool isLoading { get; set; } = true;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged();
            }
        }

        private string userLogin { get; set; } = string.Empty;
        public string UserLogin
        {
            get => userLogin;
            set
            {
                userLogin = value;
            }
        }

        private string userPassword { get; set; } = string.Empty;
        public string UserPassword
        {
            get => userPassword;
            set
            {
                userPassword = value;
            }
        }

        public bool IsPasswordRemembered { get; set; } = false;

        
        private string dbServer { get; set; } = string.Empty;
        public string DBServer
        {
            get => dbServer;
            set
            {
                dbServer = value;
                OnPropertyChanged();
            }
        }

        private string dbPort { get; set; } = string.Empty;
        public string DBPort
        {
            get => dbPort;
            set
            {
                dbPort = value;
                OnPropertyChanged();
            }
        } 

        private string dbName { get; set; } = string.Empty;
        public string DBName
        {
            get => dbName;
            set
            {
                dbName = value;
                OnPropertyChanged();
            }
        }

        private string dbUser { get; set; } = string.Empty;
        public string DBUser
        {
            get => dbUser;
            set
            {
                dbUser = value;
                OnPropertyChanged();
            }
        }

        private string dbPassword { get; set; } = string.Empty;
        public string DBPassword
        {
            get => dbPassword;
            set
            {
                dbPassword = value;
                OnPropertyChanged();
            }
        }
        #endregion

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LoginWindowModel"/>.
        /// </summary>
        /// <param name="authenticationService">Сервис аутентификации.</param>
        /// <param name="logger">Сервис логирования.</param>
        /// <param name="versionService">Сервис проверки версии приложения.</param>
        public LoginWindowModel(IAppLogger logger, ISettingsService settings, IAuthService auth)
        {
            _authService = auth;
            _settingsService = settings;
            _logger = logger;
        }

        private async Task InitializeAsync()
        {

        }

        // Инициализация
        // Загрузка данных
        // Настройка подключения к БД
        // Проверка подключения к БД
        // Подтянуть данные пользователя
        // Авторизация пользователя

        
    }
}
