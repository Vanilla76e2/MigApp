using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MigApp
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        SQLConnectionClass sqlcc = new SQLConnectionClass();
        Encrypter encrypt = new Encrypter();

        public LoginWindow()
        {
            InitializeComponent();

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

            // Проверка на подключение к БД
            //if (!sqlcc.SQLtest())
            //{
            //    Enter1.IsEnabled = false;
            //    Enter2.IsEnabled = false;
            //    Refresh.Visibility = Visibility.Visible;
            //}
        }

        //Если пароль не был сохранён
        private void LoginClick_1(object sender, RoutedEventArgs e)
        {

            //if (sqlcc.ReqRef($"Select Password From Users Where Login Like {UserLogin.Text}") == encrypt.HashPassword(UserPassword1.Password))
            //{
                if (RemPass.IsChecked == true)
                {
                    MigApp.Properties.Settings.Default.RemPass = true;
                    MigApp.Properties.Settings.Default.UserLogin = UserLogin.Text;
                    MigApp.Properties.Settings.Default.UserPassword = UserPassword1.Password;
                    MigApp.Properties.Settings.Default.Save();
                }
                else
                {
                    MigApp.Properties.Settings.Default.RemPass = false;
                }
                MainWindow win = new MainWindow();
                win.Show(); Close();
            //}





            //Для отладки
            //MainWindow win1 = new MainWindow();
            //win1.Show();
            //Close();
        }

        //Если пароль был сохранён
        private void LoginClick_2(object sender, RoutedEventArgs e)
        {
            //if (sqlcc.ReqRef($"Select Password From Users Where Login Like {UserLogin.Text}") == encrypt.HashPassword(UserPassword1.Password))
            //{
                if (RemPass.IsChecked == false)
                {
                    MigApp.Properties.Settings.Default.RemPass = false;
                    MigApp.Properties.Settings.Default.Save();
                }

                MainWindow win = new MainWindow();
                win.Show();
                Close();
            //}
        }

        //Открытие окна настроек
        private void SettingsClick(object sender, RoutedEventArgs e)
        {
                SettingsWin win = new SettingsWin();
                win.ShowDialog();
                if (win.DialogResult == true)
                {
                    //Если применены настройки
                    bool test = sqlcc.SQLtest();
                    if (test)
                    {
                        Enter1.IsEnabled = true;
                        Enter2.IsEnabled = true;
                    }
                }
        }

        //При редактировании пароля
        private void PasswordFlip(object sender, RoutedEventArgs e)
        {
            if (UserPassword2.Password != MigApp.Properties.Settings.Default.UserPassword)
            {
                UserPassword2.Password = "";
                UserPassword2.Visibility = Visibility.Collapsed;
                UserPassword1.Visibility = Visibility.Visible;
                Enter2.Visibility = Visibility.Collapsed;
                Enter1.Visibility = Visibility.Visible;
                RemPass.IsChecked = false;
                UserPassword1.Focus();
            }
        }

        //Кнопка обновления подключения БД
        private void RefreshDBConnection(object sender, RoutedEventArgs e)
        {
            if (sqlcc.SQLtest())
            {
                Enter1.IsEnabled = true;
                Enter2.IsEnabled = true;
                MessageBox.Show("База данных найдена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("База данных не найдена!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
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

    }
}
