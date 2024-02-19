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
    /// Логика взаимодействия для SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        SQLConnectionClass sqlcc = new SQLConnectionClass();
        int Mode;

        public SearchWindow(int mode)
        {
            InitializeComponent();
            Mode = mode;
            Setup(mode);
        }

        // Преднастройка окна поиска
        private void Setup(int mode)
        {
            // Сотрудники
            if (mode == 0) 
            { 
                SearchPanel_0.Visibility = Visibility.Visible;
                this.Height = 300;
            }
        }

        // Применение фильтров
        private void AcceptClick(object sender, RoutedEventArgs e)
        {
            string command = "Where ";

            // Сотрудники
            if (Mode == 0)
            {
                if (ID_Emp.Text.Length > 0)
                    command += $"ID Like '{ID_Emp.Text}' AND ";
                if (FIO_Emp.Text.Length > 0)
                    command += $"ФИО Like '%{FIO_Emp.Text}%' AND ";
                if (Group_Emp.Text.Length > 0)
                    command += $"Отдел Like '%{Group_Emp.Text}%' AND ";
                if (Birthdate_Emp.Text.Length > 0)
                    command += $"[Дата рождения] Like '%{Birthdate_Emp.Text}%' AND ";
                command += "[Дата рождения] NOT Like '0001-01-01'";
                if (command != "Where [Дата рождения] NOT Like '0001-01-01'")
                {
                    MigApp.Properties.Settings.Default.Params0 = $"{ID_Emp.Text}|{FIO_Emp.Text}|{Group_Emp.Text}|{Birthdate_Emp.Text}";
                    MigApp.Properties.Settings.Default.com0 = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }
                
        }

        // Проверка на цифры
        private void NumOnly(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
