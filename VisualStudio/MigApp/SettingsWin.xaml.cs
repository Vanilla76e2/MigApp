using System;
using System.Collections.Generic;
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
        public SettingsWin()
        {
            InitializeComponent();
            ServerName.Text = MigApp.Properties.Settings.Default.Server;
            DBName.Text = MigApp.Properties.Settings.Default.Database;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MigApp.Properties.Settings.Default.Server = ServerName.Text;
            MigApp.Properties.Settings.Default.Database = DBName.Text;
            MigApp.Properties.Settings.Default.Save();
            DialogResult = true;
            Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}
