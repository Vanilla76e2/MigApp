using MaterialDesignThemes.Wpf;
using MigApp.Application.Services.Authorization;
using MigApp.Application.Services.StartupInitialize;
using MigApp.Core.Models;
using MigApp.Demo.Services.DemoModeManager;
using MigApp.Infrastructure.Services.AppLogger;
using MigApp.Infrastructure.Services.ConnectionSettingsManager;
using MigApp.Infrastructure.Services.CredentialsManager;
using MigApp.Properties;
using MigApp.UI.Base;
using MigApp.UI.Services.Navigation;
using MigApp.UI.Services.UINotification;
using System.Security;
using System.Windows.Input;

namespace MigApp.UI.MVVM.ViewModel
{
    /// <summary>
    /// ViewModel для окна авторизации. Обрабатывает логику авторизации пользователя, управления настройками подключения к базе данных и навигации между представлениями.
    /// </summary>
    public class LoginWindowModel : Base.ViewModel
    {
        // Сервисы
        private readonly IAppLogger _logger;
        private readonly INavigationService _navigationService;
        private readonly IStartupInitializer _initalizer;
        private readonly ICredentialsManager _userManager;
        private readonly IConnectionSettingsManager _connectionManager;
        private readonly IDemoModeService _demoModeService;
        private readonly IAuthorizationStrategy _auth;
        private readonly IUINotificationService _ui;

        // Команды
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand CommitSettingsCommand { get; set; }
        public RelayCommand ToggleSettingsCommand { get; set; }
        public RelayCommand ShowGuideCommand { get; set; }
        public ICommand ToggleDemoModeCommand { get; }


        #region Свойства

        public bool IsDemoModeEnabled => _demoModeService.IsDemoModeEnabled;

        public string WindowTitle { get; } = $"MigApp {System.Reflection.Assembly.GetExecutingAssembly().GetName()?.Version?.ToString(3) ?? "Unknown Version"}";

        public ISnackbarMessageQueue SnackbarMessageQueue { get; }

        public void ShowMessage(string message)
        {
            _logger.LogDebug($"Принят текст сообщения: {message}");
            SnackbarMessageQueue.Enqueue(message);
        }

        private bool _isSettingsVisible { get; set; } = false;
        public bool IsSettingsOn
        {
            get => _isSettingsVisible;
            set
            {
                _isSettingsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isConnectionCorrect { get; set; } = true;
        public bool IsConnectionCorrect
        {
            get => _isConnectionCorrect;
            set
            {
                _isConnectionCorrect = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoading { get; set; } = true;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private string _username { get; set; } = string.Empty;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        private SecureString _userPassword = new SecureString();
        public SecureString UserPassword
        {
            get => _userPassword.Copy();
            set
            {
                _userPassword?.Dispose();
                _userPassword = value?.Copy() ?? new SecureString();
                _userPassword.MakeReadOnly();
                OnPropertyChanged();
            }
        }


        private bool _isPasswordRemembered { get; set; } = false;
        public bool IsPasswordRemembered
        {
            get => _isPasswordRemembered;
            set
            {
                _isPasswordRemembered = value;
                OnPropertyChanged();
            }
        }

        private string _dbServer { get; set; } = string.Empty;
        public string DBServer
        {
            get => _dbServer;
            set
            {
                _dbServer = value;
                OnPropertyChanged();
            }
        }

        private string _dbPort { get; set; } = string.Empty;
        public string DBPort
        {
            get => _dbPort;
            set
            {
                _dbPort = value;
                OnPropertyChanged();
            }
        }

        private string _dbName { get; set; } = string.Empty;
        public string DBName
        {
            get => _dbName;
            set
            {
                _dbName = value;
                OnPropertyChanged();
            }
        }

        private string _dbUser { get; set; } = string.Empty;
        public string DBUser
        {
            get => _dbUser;
            set
            {
                _dbUser = value;
                OnPropertyChanged();
            }
        }

        private SecureString _dbPassword { get; set; } = new SecureString();
        public SecureString DBPassword
        {
            get => _dbPassword.Copy();
            set
            {
                _dbPassword?.Dispose();
                _dbPassword = value?.Copy() ?? new SecureString();
                _dbPassword.MakeReadOnly();
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
        public LoginWindowModel(IAppLogger logger,  
                                IConnectionSettingsManager connectionManager, 
                                ICredentialsManager userManager,
                                IStartupInitializer initializer, 
                                INavigationService navigation, 
                                IUINotificationService ui,
                                IAuthorizationStrategy auth,
                                IDemoModeService demoMode)
        {
            _logger = logger;
            _auth = auth;
            _userManager = userManager;
            _connectionManager = connectionManager;
            _navigationService = navigation;
            _initalizer = initializer;
            _ui = ui;
            _demoModeService = demoMode;

            _demoModeService.DemoModeChanged += OnDemoModeChanged;

            LoginCommand = new RelayCommand(async o => await AuthorizeUserAsync(), o => IsConnectionCorrect);
            CommitSettingsCommand = new RelayCommand(async o => await CommitSettingsChangings(), o => true);
            ToggleSettingsCommand = new RelayCommand(o => ToggleSettings(), o => true);
            ShowGuideCommand = new RelayCommand(o => ShowGuide(), o => true);
            ToggleDemoModeCommand = new RelayCommand(o => _demoModeService.ToggleDemoMode(), o => true);
            SnackbarMessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(1));
        }

        private void OnDemoModeChanged(object? sender, bool isDemoModeEnabled)
        {
            if (isDemoModeEnabled)
            {
                Username = AppConstants.DemoMode.DefaultUsername;
                UserPassword = PasswordHelper.ConvertPasswordToSecureString(AppConstants.DemoMode.DefaultPassword);
                IsPasswordRemembered = true;
            }
            else
            {
                Username = string.Empty;
                UserPassword = new SecureString();
                IsPasswordRemembered = false;
            }
        }

        /// <summary>
        /// Инициализирует асинхронные процессы, такие как обновление приложения и установка параметров подключения.
        /// </summary>
        public async Task InitializeAsync()
        {
            IsLoading = true;
            try
            {
                var initData = await _initalizer.InitializeAsync();
                SetDatabaseConnectionParameters(initData.Connection);
                SetUserCredentials(initData.Credentials);
                IsConnectionCorrect = initData.IsConnectionSuccessful;
                IsSettingsOn = !IsConnectionCorrect;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Произошла критическая ошибка");
                await _ui.ShowErrorAsync($"Произошла ошибка инициализации: \n{ex.Message}");
            }
            finally
            { IsLoading = false; }
        }

        /// <summary>
        /// Устанавливает параметры подключения к базе данных.
        /// </summary>
        private void SetDatabaseConnectionParameters(DatabaseConnectionParameters parameters)
        {
            DBServer = parameters.Host ?? string.Empty;
            DBPort = parameters.Port ?? string.Empty;
            DBName = parameters.Database ?? string.Empty;
            DBUser = parameters.Username ?? string.Empty;
            DBPassword = PasswordHelper.ConvertPasswordToSecureString(parameters.Password) ?? new SecureString();
        }

        /// <summary>
        /// Передаёт параметры подключения к базе данных в виде объекта <see cref="DatabaseConnectionParameters"/>.
        /// </summary>
        private DatabaseConnectionParameters GetDatabaseConnectionParameters()
        {
            return new DatabaseConnectionParameters
            (
                DBServer.Trim(),
                DBPort.Trim(),
                DBName.Trim(),
                DBUser.Trim(),
                PasswordHelper.ConvertPasswordToString(DBPassword)
            );
        }

        /// <summary>
        /// Устанавливает учетные данные пользователя.
        /// </summary>
        private void SetUserCredentials(UserCredentials credentials)
        {
            Username = credentials.Username ?? string.Empty;
            UserPassword = PasswordHelper.ConvertPasswordToSecureString(credentials.Password) ?? new SecureString();
        }

        /// <summary>
        /// Передаёт учетные данные пользователя в виде объекта <see cref="UserCredentials"/>.
        /// </summary>
        private UserCredentials GetUserCredetials()
        {
            return new UserCredentials
            {
                Username = Username,
                Password = PasswordHelper.ConvertPasswordToString(UserPassword)
            };
        }

        /// <summary>
        /// Асинхронно авторизует пользователя с использованием введенных учетных данных.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию авторизации.</returns>
        private async Task AuthorizeUserAsync()
        {
            try
            {
                AuthResult authResult = await _auth.AuthorizationAsync(Username, PasswordHelper.ConvertPasswordToString(UserPassword));

                if (authResult.Message != null && !authResult.IsAuthenticated)
                {
                    await _ui.ShowErrorAsync(authResult.Message);
                    _logger.LogError(authResult.Message);
                }
                else if (authResult.IsAuthenticated)
                {
                    _logger.LogInformation($"Выполнен успешный вход пользователя: {Username}");
                    if (IsPasswordRemembered)
                    {
                        await _userManager.SaveUserCredentialsAsync(GetUserCredetials());
                    }

                    Settings.Default.userRemembered = IsPasswordRemembered;
                    Settings.Default.Save();

                    await _navigationService.NavigateToMainWindow();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при авторизации");
            }
        }

        /// <summary>
        /// Асинхронно применяет изменения настроек, проверяя подключение к базе данных и сохраняя параметры при успешном подключении.
        /// </summary>
        /// <remarks>
        /// Привязка к комманде <see cref="CommitSettingsCommand"/>.
        /// </remarks>
        private async Task CommitSettingsChangings()
        {
            IsLoading = true;
            try
            {
                _isConnectionCorrect = await _connectionManager.TestAndSaveNewConnectionAsync(GetDatabaseConnectionParameters());
                IsSettingsOn = !_isConnectionCorrect;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при применении настроек подключения к базе данных");
            }
            finally
            { IsLoading = false; }
        }

        /// <summary>
        /// Открывает руководство пользователя в браузере по умолчанию.
        /// </summary>
        /// <remarks>
        /// Привязка к комманде <see cref="ShowGuideCommand"/>.
        /// </remarks>
        private void ShowGuide()
        {
            try
            {
                SnackbarMessageQueue.Enqueue("Тестовое сообщение", "Открыть", () => Process.Start(new ProcessStartInfo
                {
                    FileName = "https://example.com",
                    UseShellExecute = true
                }));
            }
            catch (Exception ex)
            {
                _ui.ShowErrorAsync("Не удалось открыть руководство пользователя.\nПроверьте подключение к интернету или наличие браузера.");
                _logger.LogError(ex, "Ошибка при открытии руководства пользователя");
            }
        }

        /// <summary>
        /// Переключает видимость настроек подключения к базе данных.
        /// </summary>
        /// <remarks>
        /// Привязка к комманде <see cref="ToggleSettingsCommand"/>.
        /// </remarks> 
        private void ToggleSettings()
        {
            IsSettingsOn = !IsSettingsOn;
        }

        /// <summary>
        /// Освобождает ресурсы, связанные с паролями пользователя и базы данных.
        /// </summary>
        public void DisposePasswords()
        {
            _userPassword?.Dispose();
            _userPassword = new SecureString();

            _dbPassword?.Dispose();
            _dbPassword = new SecureString();
        }
    }
}
