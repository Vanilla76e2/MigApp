﻿using System.Diagnostics;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace MigApp
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        SQLConnectionClass sqlcc = SQLConnectionClass.getinstance();
        MiscClass mc = new MiscClass();
        Encrypter encrypt = new Encrypter();
        string curver = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);

        public LoginWindow()
        {
            InitializeComponent();
            CustomTitle.Text = "   MigApp v" + curver;
            Title = "MigApp v" + curver;
            UserLogin.Focus();
            //Проверка на запомнить пароль
            if (MigApp.Properties.Settings.Default.RemPass)
            {
                UserLogin.Text = MigApp.Properties.Settings.Default.UserLogin;
                UserPassword2.Password = MigApp.Properties.Settings.Default.UserPassword;
                UserPassword2.Visibility = Visibility.Visible;
                Enter2.Visibility = Visibility.Visible;
                RemPass.IsChecked = true;
            }
            else
            {
                UserPassword1.Visibility = Visibility.Visible;
                Enter1.Visibility = Visibility.Visible;
            }
        }

        //Если пароль не был сохранён
        private void LoginClick_1(object sender, RoutedEventArgs e)
        {
            if (UserLogin.Text.Length > 0 && UserPassword1.Password.Length > 0)
                if(sqlcc.ReqRef($"Select Login From Users Where Login = '{UserLogin.Text}'") != "")
                {
                    if (sqlcc.ReqRef($"Select Password From Users Where Login = '{UserLogin.Text}'") == encrypt.HashPassword(UserPassword1.Password))
                    {
                        if (RemPass.IsChecked == true)
                        {
                            MigApp.Properties.Settings.Default.RemPass = true;
                            MigApp.Properties.Settings.Default.UserPassword = UserPassword1.Password;
                            MigApp.Properties.Settings.Default.Save();
                        }
                        else
                        {
                            MigApp.Properties.Settings.Default.RemPass = false;
                            MigApp.Properties.Settings.Default.Save();
                        }
                        MigApp.Properties.Settings.Default.UserLogin = UserLogin.Text;
                        MigApp.Properties.Settings.Default.UserRole = sqlcc.ReqRef($"SELECT Role FROM Users WHERE Login = '{UserLogin.Text}'");
                        MigApp.Properties.Settings.Default.Save();
                        MainWindow win = new MainWindow();
                        win.Show(); Close();
                    }
                    else if (sqlcc.ReqRef($"Select Password From Users Where Login = '{UserLogin.Text}'") == "")
                    {
                        Registration();
                    }
                    else MessageBox.Show("Логин или Пароль введены не верно", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                else
                {
                    MessageBox.Show($"Пользователя {UserLogin.Text} не существует в базе данных!","Внимание",MessageBoxButton.OK,MessageBoxImage.Stop);
                }
        }

        //Если пароль был сохранён
        private void LoginClick_2(object sender, RoutedEventArgs e)
        {
            if (UserLogin.Text.Length > 0 && UserPassword2.Password.Length > 0)
            {
                if (sqlcc.ReqRef($"Select Login From Users Where Login = '{UserLogin.Text}'") != "")
                {
                    if (sqlcc.ReqRef($"Select Password From Users Where Login = '{UserLogin.Text}'") == encrypt.HashPassword(UserPassword2.Password))
                    {
                        if (RemPass.IsChecked == false)
                        {
                            MigApp.Properties.Settings.Default.RemPass = false;
                            MigApp.Properties.Settings.Default.Save();
                        }

                        MigApp.Properties.Settings.Default.UserLogin = UserLogin.Text;
                        MigApp.Properties.Settings.Default.UserRole = sqlcc.ReqRef($"SELECT Role FROM Users WHERE Login = '{UserLogin.Text}'");
                        MigApp.Properties.Settings.Default.Save();
                        MainWindow win = new MainWindow();
                        win.Show();
                        Close();
                    }
                    else if (sqlcc.ReqRef($"Select Password From Users Where Login = '{UserLogin.Text}'") == "")
                    {
                        MessageBox.Show("Ваш пароль был сброшен\nВведите новый пароль", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                        PasswordFlip();
                    }
                    else MessageBox.Show("Логин или Пароль введены не верно", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                else
                {
                    MessageBox.Show($"Пользователя {UserLogin.Text} не существует в базе данных!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
            else
            {
                MessageBox.Show("Введите логин и пароль","Внимание",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

        //Открытие окна настроек
        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            SettingsWin win = new SettingsWin();
            win.ShowDialog();
        }

        #region Безопасность

        //При редактировании пароля
        private void PasswordRedact(object sender, RoutedEventArgs e)
        {
            PasswordFlip();
        }

        //Смена поля для пароля
        private void PasswordFlip()
        {
            UserPassword2.Password = "";
            UserPassword2.Visibility = Visibility.Collapsed;
            UserPassword1.Visibility = Visibility.Visible;
            Enter2.Visibility = Visibility.Collapsed;
            Enter1.Visibility = Visibility.Visible;
            RemPass.IsChecked = false;
            UserPassword1.Focus();
        }

        #endregion

        //Кнопка обновления подключения БД
        private void RefreshDBConnection(object sender, RoutedEventArgs e)
        {
            if (sqlcc.SQLtest() == true)
            {
                if (Enter1.IsVisible)
                Enter1.IsEnabled = true;
                else if (Enter2.IsVisible)
                Enter2.IsEnabled = true;
                Refresh.Visibility = Visibility.Collapsed;
                MessageBox.Show("База данных найдена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("База данных не найдена!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        //Если пароль отсутствует в бд
        private void Registration()
        {
            if(MessageBox.Show("Ваш пароль был сброшен\nХотите сохранить как новый пароль?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (UserPassword1.Password.Length >= 8)
                {
                    sqlcc.ReqNonRef($"UPDATE Users SET Password = '{encrypt.HashPassword(UserPassword1.Password)}' WHERE Login LIKE '{UserLogin.Text}'");
                }
                else
                    MessageBox.Show("Пароль должен содержать минимум 8 символов", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #region ControlPanel

        //Кнопка закрыть
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //Перетаскивание окна
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        #endregion

        private void Content_Rendered(object sender, System.EventArgs e)
        {
            mc.CheckVersion();
            // Подключение к бд после обновления
            try
            {
                string tmp = Path.GetTempPath() + "MigAppConnectionParametrs.txt";
                string constr = File.ReadAllText(tmp, Encoding.UTF8);
                string[] conparams = constr.Split('|');
                MigApp.Properties.Settings.Default.Server = conparams[0];
                MigApp.Properties.Settings.Default.Database = conparams[1];
                MigApp.Properties.Settings.Default.DBPassword = conparams[2];
                MigApp.Properties.Settings.Default.Save();
                File.Delete(tmp);
            }
            catch { }
            // Проверка на подключение к БД
            if (!sqlcc.SQLtest())
            {

                MessageBox.Show("База данных не найдена!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                SettingsWin win = new SettingsWin();
                win.ShowDialog();
            }
        }
    }
}
