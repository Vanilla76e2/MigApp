using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    /// Логика взаимодействия для SettingsWin.xaml
    /// </summary>
    public partial class SettingsWin : Window
    {
        SQLConnectionClass sqlcc = SQLConnectionClass.getinstance();
        public SettingsWin()
        {
            InitializeComponent();
            ServerName.Text = MigApp.Properties.Settings.Default.Server;
            DBName.Text = MigApp.Properties.Settings.Default.Database;
            DBPassword.Password = MigApp.Properties.Settings.Default.DBPassword;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MigApp.Properties.Settings.Default.Server = ServerName.Text;
            MigApp.Properties.Settings.Default.Database = DBName.Text;
            MigApp.Properties.Settings.Default.DBPassword = DBPassword.Password;
            MigApp.Properties.Settings.Default.Save();
            //MessageBox.Show(DBPassword.Password);
            //MessageBox.Show(MigApp.Properties.Settings.Default.DBPassword);
            if (sqlcc.SQLtest())
            {
                DialogResult = true; Close();
            }
            else
            {
                MessageBox.Show("Не удалось подключиться к серверу,\nпроверьте корректность введённых данных.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void User_Manual(object sender, RoutedEventArgs e)
        {
            Process.Start(@"https://vanilla76e2.github.io/MigApp_Manual/");
        }
    }
}
