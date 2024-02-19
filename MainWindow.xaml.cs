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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MigApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SQLConnectionClass sqlcc = new SQLConnectionClass();
        MiscClass mc = new MiscClass();

        public MainWindow()
        {
            InitializeComponent();
            this.MaxWidth = System.Windows.SystemParameters.PrimaryScreenWidth + 50;
            this.MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight + 50;
            ClearFilters();
            //UpdateAllTables();
        }


        #region Context menu

        private void Delete_Emp(object sender, RoutedEventArgs e)
        {

        }
        private void Delete_PC(object sender, RoutedEventArgs e)
        {

        }
        private void Delete_Lap(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Tab(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Pri(object sender, RoutedEventArgs e)
        {

        }


        private void Delete_Mon(object sender, RoutedEventArgs e)
        {

        }

        #endregion


        // Обновление всех таблиц
        private void UpdateAllTables()
        {
            EmployeeTable.ItemsSource = sqlcc.DataGridUpdate("*", "Employees_View", $"{MigApp.Properties.Settings.Default.com0}").DefaultView;
            PCTable.ItemsSource = sqlcc.DataGridUpdate("*", "PC_View", $"{MigApp.Properties.Settings.Default.com1}").DefaultView;
            LaptopTable.ItemsSource = sqlcc.DataGridUpdate("*", "Notebooks_View", $"{MigApp.Properties.Settings.Default.com2}").DefaultView;
            TabletsTable.ItemsSource = sqlcc.DataGridUpdate("*", "Tablet_View", $"{MigApp.Properties.Settings.Default.com3}").DefaultView;
            PrintersTable.ItemsSource = sqlcc.DataGridUpdate("*", "OrgTech_View", $"{MigApp.Properties.Settings.Default.com4}").DefaultView;
            MonitorsTable.ItemsSource = sqlcc.DataGridUpdate("*", "Monitors_View", $"{MigApp.Properties.Settings.Default.com5}").DefaultView;
        }

        // Затемнить окно
        private void BlindfallSwitch()
        {
            if (Blindfall.Visibility == Visibility.Collapsed)
                Blindfall.Visibility = Visibility.Visible;
            else
                Blindfall.Visibility = Visibility.Collapsed;
        }

        // Кнопка возврата к окну авторизации
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            LoginWindow win = new LoginWindow();
            win.Show(); Close();
        }

        #region Filters

        // Фильтр по сотрудникам
        private void SEmployee(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow(0);
            BlindfallSwitch();
            win.Owner = this;
            win.ShowDialog();
            if (win.DialogResult == true)
            {
                Filter_Emp.Visibility = Visibility.Visible;
                //EmployeeTable.ItemsSource = sqlcc.DataGridUpdate("*", "Employees_View", $"{MigApp.Properties.Settings.Default.com0}").DefaultView;
                FilterEmpText.Text = mc.Splitter(MigApp.Properties.Settings.Default.Params0);
            }
            BlindfallSwitch();

        }
        private void FilterEmpClear(object sender, RoutedEventArgs e)
        {
            MigApp.Properties.Settings.Default.com0 = "";
            MigApp.Properties.Settings.Default.Params0 = "";
            MigApp.Properties.Settings.Default.Save();
            FilterEmpText.Text = "";
            Filter_Emp.Visibility = Visibility.Collapsed;
            //EmployeeTable.ItemsSource = sqlcc.DataGridUpdate("*", "Employees_View", "").DefaultView;
        }

        // Очистка фильтров при запуске
        private void ClearFilters ()
        {
            MigApp.Properties.Settings.Default.com0 = null;
            MigApp.Properties.Settings.Default.com1 = null;
            MigApp.Properties.Settings.Default.com2 = null;
            MigApp.Properties.Settings.Default.com3 = null;
            MigApp.Properties.Settings.Default.com4 = null;
            MigApp.Properties.Settings.Default.com5 = null;
            MigApp.Properties.Settings.Default.Params0 = null;
            MigApp.Properties.Settings.Default.Params1 = null;
            MigApp.Properties.Settings.Default.Params2 = null;
            MigApp.Properties.Settings.Default.Params3 = null;
            MigApp.Properties.Settings.Default.Params4 = null;
            MigApp.Properties.Settings.Default.Params5 = null;
            MigApp.Properties.Settings.Default.Save();
        }

        #endregion
    }
}
