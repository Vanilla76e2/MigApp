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

namespace MigApp.CRWindows
{
    /// <summary>
    /// Логика взаимодействия для EmpGroupWindow.xaml
    /// </summary>
    public partial class EmpGroupWindow : Window
    {
        SQLConnectionClass sqlcc = SQLConnectionClass.getinstance();
        string CurrentUser = MigApp.Properties.Settings.Default.UserLogin;
        public EmpGroupWindow()
        {
            InitializeComponent();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Length > 0)
            {
                if (Convert.ToInt32(sqlcc.ReqRef($"SELECT COUNT(*) FROM [Group] Where Name LIKE '{Name.Text}'")) < 1)
                {                       
                    sqlcc.ReqNonRef($"INSERT INTO [Group] (Name) Values ('{Name.Text}')");
                    sqlcc.Loging(CurrentUser, "Создание", "Отделы", Name.Text, "");
                    Close();
                }
                else
                {
                    Name.Focus();
                    MessageBox.Show("Такой отдел уже существует.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RecoveryClick(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
