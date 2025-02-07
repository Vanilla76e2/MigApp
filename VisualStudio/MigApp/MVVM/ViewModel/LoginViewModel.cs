using Microsoft.Extensions.DependencyInjection;
using MigApp.Core;
using MigApp.MVVM.View;
using MigApp.Services;
using MigApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using System.Windows.Navigation;
using System.Security;

namespace MigApp.MVVM.ViewModel
{
    internal class LoginViewModel : Core.ViewModel
    {
        PostgreSQLClass pgsql = PostgreSQLClass.GetInstance(); // Объявление класса для подключения к БД
        ApplicationContext appContext = ApplicationContext.GetInstance(); // Объявление класса для использования контекста данных приложения
        MiscClass mc = new MiscClass(); // Объявление класса для использования различных методов
        Encrypter enc = new Encrypter(); // Объявление класса для использования шифрования
        private readonly IServiceProvider _serviceProvider; // Объявление сервис-провайдера для разрешения зависимостей
        private readonly INavigationService _navigationService; // Объявление сервиса навигатора для переключения между представлениями


        #region Переменные
        private bool isSettingsOn { get; set; } = false;  // Параметр хранящий статус видимости настроек
        // Установка и получение параметра статуса отображения настроек
        public bool IsSettingsOn
        {
            get => isSettingsOn;
            set
            {
                isSettingsOn = value;
                OnPropertyChanged();
            }
        }

        // Авторизация
        private bool isConnectionCorrect { get; set; } // Параметр хранящий статус подключения к БД
        public bool IsConnectionCorrect
        {
            get => isConnectionCorrect;
            set
            {
                isConnectionCorrect = value;
                OnPropertyChanged();
            }
        } // Установка и получение параметра статуса подключения к БД

        private bool isLoading { get; set; } = true; // Параметр хранящий статус отображения загрзуки
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged();
            }
        } // Установка и получение параметра статуса отображения загрузки

        private string userLogin { get; set; } = string.Empty; // Параметр хранящий логин пользователя
        public string UserLogin
        {
            get => userLogin;
            set
            {
                userLogin = value;
            }
        } // Установка и получение логина пользователя

        private string userPassword { get; set; } = string.Empty; // Параметр хранящий пароль пользователя
        public string UserPassword
        {
            get => userPassword;
            set
            {
                userPassword = enc.HashPassword(value.ToString());
            }
        } // Установка и получение пароля пользователя

        public bool IsPasswordRemembered { get; set; } // Параметр хранящий статус сохранения данных входа

        // Настройки
        private string dbServer { get; set; } // Параметр хранящий сервер для подключения к БД
        public string DBServer
        {
            get => dbServer;
            set
            {
                dbServer = value;
                OnPropertyChanged();
            }
        } // Установка и получение сервера для подключения к БД

        private string dbPort { get; set; } // Параметр хранящий порт для подключения к БД
        public string DBPort
        {
            get => dbPort;
            set
            {
                dbPort = value;
                OnPropertyChanged();
            }
        } // Установка и получение порта для подключения к БД

        private string dbName { get; set; } // Параметр хранящий имя БД
        public string DBName
        {
            get => dbName;
            set
            {
                dbName = value;
                OnPropertyChanged();
            }
        } // Установка и получение имени ЬД

        private string dbUser { get; set; } // Параметр хранящий имя пользователя от БД
        public string DBUser
        {
            get => dbUser;
            set
            {
                dbUser = value;
                OnPropertyChanged();
            }
        } // Установка и получение имени пользователя от БД

        private string dbPassword { get; set; } // Параметр хранящий пароль от БД
        public string DBPassword
        {
            get => dbPassword;
            set
            {
                dbPassword = value;
                OnPropertyChanged();
            }
        } // Установка и получение пароля от БД
        #endregion

        #region Команды

        public RelayCommand LoginCommand { get; set; } // Объявление команды на авторизацию
        public RelayCommand SettingsCommitCommand { get; set; } // Объявление команды на сохранение настроек

        #endregion

        // Основной конструктор класса LoginViewModel
        public LoginViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider; // Установка сервис-провайдера для разрешения зависимостей
            _navigationService = serviceProvider.GetRequiredService<INavigationService>(); // Установка сервиса навигатора для переключения между представлениями

            // Восстановление данных пользователя
            #region Данные пользователя
            IsPasswordRemembered = MigApp.Properties.Settings.Default.userRemember;
            userLogin = MigApp.Properties.Settings.Default.userLogin;
            userPassword = MigApp.Properties.Settings.Default.userPassword;
            #endregion

            // Восстановление данных для подключения
            #region Данные подключения
            DBServer = MigApp.Properties.Settings.Default.pgServer;
            DBPort = MigApp.Properties.Settings.Default.pgPort;
            DBName = MigApp.Properties.Settings.Default.pgDatabase;
            DBUser = MigApp.Properties.Settings.Default.pgUser;
            #endregion

            LoginCommand = new RelayCommand(async o => await ExecuteLoginCommand(), o => true); // Установка команды авторизации
            SettingsCommitCommand = new RelayCommand(async o => await OnSettingsCommit(), o => true); // Установка команды сохранения настроек

        }

        // Проверка подключения
        public async Task InitializeAsync()
        {
            IsLoading = true; // Включить отображение загрузки
            try
            {
                bool result = await pgsql.ConnectionTest(); // Проверка подключения к БД
                IsConnectionCorrect = result; // Установка статуса подключения к БД
                if (!result)
                {
                    MessageBox.Show("Подключение к базе данных отсутствует.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    IsSettingsOn = true; // Включить отображение настройки, если не удалось установить соединение
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LoginViewModel: Ошибка при InitializeAsync: {ex.Message}");
            }
            finally
            {
                IsLoading = false; // Отключить отображение загрузки
            }
        }

        // Команда на авторизацию в систему
        public async Task ExecuteLoginCommand()
        {
            #if DEBUG
            await _navigationService.NavigateToMainWindow();
            var debug = Application.Current.Windows.OfType<LoginView>().FirstOrDefault();
            if (debug != null) { debug.Close(); }
            return;
            #endif

            IsLoading = true; // Включение отображения загрузки
            try
            {
                bool isValid = await pgsql.CheckLogin(userLogin, userPassword); // Проверка данных пользователя
                if (isValid)
                {
                    if (IsPasswordRemembered) // Если установлен параметр запоминания данных, то данные сохраняются
                    {
                        MigApp.Properties.Settings.Default.userRemember = true;
                        MigApp.Properties.Settings.Default.userLogin = userLogin;
                        MigApp.Properties.Settings.Default.userPassword = userPassword;
                        MigApp.Properties.Settings.Default.Save();
                    }

                    appContext.SetCurrentUser(userLogin); // Отправить данные пользователя в ApplicationContext

                    await _navigationService.NavigateToMainWindow(); // Открыть MainView при помощи сервиса навигации

                    var loginWindow = Application.Current.Windows.OfType<LoginView>().FirstOrDefault(); // Получить LoginView как экземпляр объекта
                    if (loginWindow != null) { loginWindow.Close(); } // Закрыть LoginView
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ExecuteLoginCommand: {ex}");
                MessageBox.Show("Произошла непредвиденная ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false; // Отключение отображения загрузки
            }
        }

        // Вход по памяти
        public async Task OnLoginRemembered()
        {
            if (IsConnectionCorrect) // Проверка статуса соединения
            {
                IsLoading = true; // Включить отображение загрузки
                try
                {
                    if (IsPasswordRemembered) // Проверка параметра запоминания данных
                    {
                        bool isValid = await pgsql.CheckRememberedLogin(userLogin, userPassword); // Проверка данных пользователя
                        if (isValid) // Если данные корректны, то открывается главное окно
                        {
                            await _navigationService.NavigateToMainWindow(); // Открыть MainView при помощи сервиса навигации
                            var loginWindow = Application.Current.Windows.OfType<LoginView>().FirstOrDefault(); // Получить LoginView как экземпляр объекта
                            if (loginWindow != null) { loginWindow.Close(); } // Закрыть LoginView 
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"OnLoginRemembered: {ex}");
                    MessageBox.Show("Произошла непредвиденная ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally 
                { 
                    IsLoading = false; //Отключение отображения загрузки  
                }
            }
        }

        // Изменение параметров подключения
        public async Task OnSettingsCommit()
        {
            IsLoading = true; // Включение отображения загрузки
            try
            {
                SaveConnectionParametrs(); // Сохранение параметров подключения к БД
                bool result = await pgsql.ConnectionTest(); // Проверка подключения к БД
                IsConnectionCorrect = result; // Установка статуса подключения к БД
                if (result)
                {
                    MessageBox.Show("Подключение к базе данных установлено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    IsSettingsOn = false; // Отключить отображение настроек
                }
                else
                {
                    MessageBox.Show("Подключение к базе данных отсутствует.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"OnSettingsCommit: {ex}");
                MessageBox.Show("Произошла непредвиденная ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false; // Отключить отображение загрузки
            }
        }

        // Сохранение параметров подключения
        private void SaveConnectionParametrs()
        {
            MigApp.Properties.Settings.Default.pgServer = DBServer;
            MigApp.Properties.Settings.Default.pgPort = DBPort;
            MigApp.Properties.Settings.Default.pgDatabase = DBName;
            MigApp.Properties.Settings.Default.pgUser = DBUser;
            MigApp.Properties.Settings.Default.pgPassword = DBPassword;
            MigApp.Properties.Settings.Default.Save();
        }
    }
}
