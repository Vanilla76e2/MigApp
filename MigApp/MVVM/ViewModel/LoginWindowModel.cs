using MaterialDesignThemes.Wpf;
using MigApp.Core;
using MigApp.Core.Services.AppUpdate;
using MigApp.Core.Services.DemoModeManager;
using MigApp.Helpers;
using MigApp.MVVM.Model;
using MigApp.Properties;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace MigApp.MVVM.ViewModel
{
    /// <summary>
    /// ViewModel для окна авторизации. Обрабатывает логику авторизации пользователя, управления настройками подключения к базе данных и навигации между представлениями.
    /// </summary>
    public class LoginWindowModel : Core.ViewModel
    {
        // Сервисы
        private readonly IAppLogger _logger;
        private readonly IAuthService _authService;
        private readonly ISecurityService _securityService;
        private readonly IDatabaseService _databaseService;
        private readonly INavigationService _navigationService;
        private readonly IAppUpdateService _appUpdateService;
        private readonly IUINotificationService _ui;
        private readonly IDemoModeService _demoModeService;

        // Команды
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand CommitSettingsCommand { get; set; }
        public RelayCommand ToggleSettingsCommand { get; set; }
        public RelayCommand ShowGuideCommand { get; set; }
        public ICommand ToggleDemoModeCommand => _demoModeService.ToggleDemoModeCommand;


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
            get
            {
                var secureCopy = new SecureString();
                IntPtr ptr = IntPtr.Zero;
                try
                {
                    ptr = Marshal.SecureStringToBSTR(_userPassword);
                    string plainText = Marshal.PtrToStringBSTR(ptr);
                    foreach (char c in plainText ?? "")
                    {
                        secureCopy.AppendChar(c);
                    }
                }
                finally
                {
                    if (ptr != IntPtr.Zero)
                        Marshal.ZeroFreeBSTR(ptr);
                }
                secureCopy.MakeReadOnly();
                return secureCopy;
            }
            set
            {
                _userPassword?.Dispose();
                _userPassword = new SecureString();

                if (value != null && value.Length > 0)
                {
                    IntPtr ptr = IntPtr.Zero;
                    try
                    {
                        ptr = Marshal.SecureStringToBSTR(value);
                        string plainText = Marshal.PtrToStringBSTR(ptr);
                        foreach (char c in plainText ?? "")
                        {
                            _userPassword.AppendChar(c);
                        }
                    }
                    finally
                    {
                        if (ptr != IntPtr.Zero)
                            Marshal.ZeroFreeBSTR(ptr);
                    }
                }

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
        private bool _dbPasswordInitialized;
        public SecureString DBPassword
        {
            get
            {
                if (!_dbPasswordInitialized)
                {
                    _dbPassword = new SecureString();
                    _dbPasswordInitialized = true;
                }
                return _dbPassword.Copy();
            }
            set
            {
                _dbPassword?.Dispose();
                _dbPassword = value?.Copy() ?? new SecureString();
                _dbPasswordInitialized = true;
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
        public LoginWindowModel(IAppLogger logger, ISecurityService security, IDatabaseService database, IAppUpdateService update, INavigationService navigation, IUINotificationService ui, IAuthService auth, IDemoModeService demoMode)
        {
            _logger = logger;
            _securityService = security;
            _databaseService = database;
            _navigationService = navigation;
            _appUpdateService = update;
            _ui = ui;
            _authService = auth;
            _demoModeService = demoMode;

            _demoModeService.DemoModeChanged += OnDemoModeChanged;

            LoginCommand = new RelayCommand(async o => await AuthorizeUserAsync(), o => IsConnectionCorrect);
            CommitSettingsCommand = new RelayCommand(async o => await CommitSettingsChangings(), o => true);
            ToggleSettingsCommand = new RelayCommand(o => ToggleSettings(), o => true);
            ShowGuideCommand = new RelayCommand(o => ShowGuide(), o => true);
            SnackbarMessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(1));

            Task.Run(async () => { await InitializeAsync(); });
        }

        private void OnDemoModeChanged(object? sender, bool isDemoModeEnabled)
        {
            if (isDemoModeEnabled)
            {
                Username = "DemoUser";
                UserPassword = PasswordHelper.ConvertPasswordToSecureString("DemoPassword");
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
        private async Task InitializeAsync()
        {
            IsLoading = true;
            try
            {
                await _appUpdateService.UpdateApplicationAsync();

                SetDatabaseConnectionParameters();
                SetUsercredentials();

                await GetDatabaseConnectionAsync();
                if (!IsConnectionCorrect)
                {
                    IsSettingsOn = true;
                }

                await Task.CompletedTask;
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
        /// Устанавливает параметры подключения к базе данных, загруженные из защищенного хранилища.
        /// </summary>
        private void SetDatabaseConnectionParameters()
        {
            _logger.LogDebug("Начата загрузка параметрова подключения");
            try
            {
                var loadedData = _securityService.LoadDatabaseSettingsFromVault();
                DatabaseConnectionParameters connectionParameters = loadedData;
                DBServer = connectionParameters.Host ?? string.Empty;
                DBPort = connectionParameters.Port ?? string.Empty;
                DBName = connectionParameters.Database ?? string.Empty;
                DBUser = connectionParameters.Username ?? string.Empty;
                DBPassword = PasswordHelper.ConvertPasswordToSecureString(connectionParameters.Password);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при попытке загрузить параметры подключения");
            }
        }

        /// <summary>
        /// Устанавливает учетные данные пользователя, загруженные из защищенного хранилища.
        /// </summary>
        private void SetUsercredentials()
        {
            var loadedData = _securityService.LoadUserCredentialsFromVault();
            bool userRemembered = Settings.Default.userRemembered;
            _logger.LogDebug($"Запомнить прользователя установлено на: {userRemembered}");
            if (userRemembered)
            {
                UserCredentials userAuthData = loadedData;
                Username = userAuthData.Username ?? string.Empty;
                UserPassword = PasswordHelper.ConvertPasswordToSecureString(userAuthData.Password);
                IsPasswordRemembered = true;
                _logger.LogDebug("Учётные данные пользователя восстановлены");
            }
            else
            {
                Username = string.Empty;
                UserPassword = new SecureString();
                _logger.LogDebug("Учётные данные пользователя по умолчанию");
            }
        }

        /// <summary>
        /// Асинхронно проверяет подключение к базе данных с использованием текущих параметров подключения.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию проверки подключения.</returns>
        private async Task GetDatabaseConnectionAsync()
        {
            try
            {
                var parameters = new DatabaseConnectionParameters(
                    DBServer.Trim(),
                    DBPort.Trim(),
                    DBName.Trim(),
                    DBUser.Trim(),
                    PasswordHelper.ConvertPasswordToString(DBPassword));

                IsConnectionCorrect = await _databaseService.TestConnectionAsync(parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при проверке подключения");
                IsConnectionCorrect = false;
            }
        }

        /// <summary>
        /// Асинхронно авторизует пользователя с использованием введенных учетных данных.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию авторизации.</returns>
        private async Task AuthorizeUserAsync()
        {
            try
            {
                AuthResult authResult = await _authService.AuthorizeUserAsync(Username, GetPasswordAsString());

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
                        SaveUserCredentials();
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
        private async Task CommitSettingsChangings()
        {
            IsLoading = true;
            await GetDatabaseConnectionAsync();
            if (IsConnectionCorrect)
            {
                //SaveSettings();
                IsSettingsOn = false;
            }
            IsLoading = false;
        }

        /// <summary>
        /// Сохраняет учетные данные пользователя в защищенное хранилище.
        /// </summary>
        private void SaveUserCredentials()
        {
            try
            {
                UserCredentials userCredentials = new UserCredentials
                {
                    Username = Username,
                    Password = PasswordHelper.ConvertPasswordToString(UserPassword)
                };
                _securityService.SaveUserCredentialsToVault(userCredentials);
                _logger.LogInformation("Учётные данные успешно сохранены в хранилище");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Не удалось сохранить учётные данные в хранилище");
            }

        }

        /// <summary>
        /// Сохраняет параметры подключения к базе данных в защищенное хранилище.
        /// </summary>
        private void SaveSettings()
        {
            try
            {
                DatabaseConnectionParameters connectionParameters = new DatabaseConnectionParameters
                (
                    DBServer,
                    DBPort,
                    DBName,
                    DBUser,
                    PasswordHelper.ConvertPasswordToString(DBPassword)
                );
                _securityService.SaveDatabaseSettingsToVault(connectionParameters);
                _logger.LogInformation("Параметры подключения к базе данных успешно сохранены в хранилище");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Не удалось сохнать параметры подключения к базе данных в хранилище");
            }
        }

        private void ShowGuide()
        {
            SnackbarMessageQueue.Enqueue("Тестовое сообщение", "Открыть", () => System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://example.com",
                UseShellExecute = true
            }));
        }

        private void ToggleSettings()
        {
            IsSettingsOn = !IsSettingsOn;
        }

        public void DisposePasswords()
        {
            _userPassword?.Dispose();
            _userPassword = new SecureString();

            _dbPassword?.Dispose();
            _dbPassword = new SecureString();
        }

        public bool HasPassword()
        {
            return _userPassword != null && _userPassword.Length > 0;
        }

        public string GetPasswordAsString()
        {
            if (!HasPassword()) return string.Empty;

            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.SecureStringToBSTR(_userPassword);
                return Marshal.PtrToStringBSTR(ptr) ?? string.Empty;
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.ZeroFreeBSTR(ptr);
            }
        }
    }
}
