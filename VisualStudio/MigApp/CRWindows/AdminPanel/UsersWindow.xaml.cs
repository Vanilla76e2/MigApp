using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MigApp.CRWindows.AdminPanel
{
    /// <summary>
    /// Логика взаимодействия для UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        SQLConnectionClass sqlcc = new SQLConnectionClass();
        MiscClass mc = new MiscClass();
        DataTable table = new DataTable();
        string CurrentUser = MigApp.Properties.Settings.Default.UserLogin;
        string ID;

        // true - Создание
        // false - Редактирование
        bool Mode;

        public UsersWindow(bool mode, string id)
        {
            InitializeComponent();
            Mode = mode;
            ID = id;
            Start(id);
        }

        // Нажатие кнопки "Сохранить"
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (Login.Text.Length > 0 && Employee.Text.Length > 0 && Role.Text.Length > 0)
            {
                if (Convert.ToInt32(sqlcc.ReqRef($"SELECT COUNT(*) FROM Users Where Login LIKE '{Login.Text}'")) < 1)
                {
                    if (Mode)
                    {
                        sqlcc.ReqNonRef($"INSERT INTO Users (Login, Employee, Role) Values ('{Login.Text}', (SELECT ID FROM Employees WHERE FIO LIKE '{Employee.Text}'), (SELECT ID FROM Roles WHERE Name LIKE '{Role.Text}'))");
                        sqlcc.Loging(CurrentUser, "Создание", "Пользователи", Login.Text, "");
                    }
                    else
                    {
                        sqlcc.ReqNonRef($"UPDATE Users SET Login = '{Login.Text}', Employee = (SELECT ID FROM Employees WHERE FIO LIKE '{Employee.Text}'), Role = (SELECT ID FROM Roles WHERE Name LIKE '{Role.Text}')");
                        sqlcc.Loging(CurrentUser, "Редактирование", "Пользователи", Login.Text, "");
                    }
                    DialogResult = true; Close();
                }
                else
                {
                    Login.Focus();
                    MessageBox.Show("Такой пользователь уже существует.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Нажатие кнопки "Удалить"
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы уверены что хотите удалить пользователя '{Login.Text}'?\nПользователь будет безвозвратно удалён!", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                sqlcc.ReqDel($"DELETE FROM Users WHERE Login LIKE '{Login.Text}'");
                DialogResult = true; Close();
            }
        }

        // Кнопка сброса пароля
        private void ResetPassword(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы уверены что хотите сбросить пароль пользователя '{Login.Text}'?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                sqlcc.ReqNonRef($"UPDATE Useres SET Password = NULL WHERE Login LIKE '{Login.Text}'");
                sqlcc.Loging(CurrentUser, "Сброс пароля", "Пользователи", Login.Text, "");
            }
        }

        #region User ComboBox

        private void EmployeeListFill()
        {
            DataTable table = new DataTable();
            table = sqlcc.DataGridUpdate("FIO", "Employees", "WHERE Deleted = 0");
            foreach (DataRow row in table.Rows) 
            {
                userList.Add(row["FIO"].ToString());
            }
            Employee.ItemsSource = userList;
        }

        private void RoleListFill()
        {
            DataTable table = new DataTable();
            table = sqlcc.DataGridUpdate("Name", "Roles", "");
            foreach(DataRow row in table.Rows)
            {
                roleList.Add(row["Name"].ToString());
            }
            Role.ItemsSource = roleList;
        }

        private List<string> userList = new List<string>();
        private List<string> roleList = new List<string>();

        private void UserChanged(object sender, TextChangedEventArgs e)
        {
            Employee.IsDropDownOpen = true;
            Employee.SelectedIndex = -1;
            string temp = ((ComboBox)sender).Text;
            var newList = userList.Where(x => x.Contains(temp));
            Employee.ItemsSource = newList.ToList();
        }

        #endregion

        // Заполнение полей и изменение названия окна
        private void Start(string ID)
        {
            EmployeeListFill();
            RoleListFill();
            if (Mode)
            {
                Title = "Пользователи (Создание)";
                DeleteButton.Visibility = Visibility.Collapsed;
                ResetPasswordButton.IsEnabled = false;
            }
            else
            {
                Login.IsReadOnly = true;
                Title = "Пользователи (Редактирование)";
                table = sqlcc.DataGridUpdate("*", "Users_View", $"Where ID Like '{ID}'");
                Login.Text = table.Rows[0].Field<string>("Логин");
                Employee.Text = table.Rows[0].Field<string>("Сотрудник");
                Role.Text = table.Rows[0].Field<string>("Роль");
                if (sqlcc.ReqRef($"SELECT Password FROM Users WHERE Login LIKE '{Login.Text}'") == "")
                    ResetPasswordButton.IsEnabled = false;
            }
        }

        
    }
}
