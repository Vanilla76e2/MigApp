using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class EmployeesWindow : Window
    {
        SQLConnectionClass sqlcc = new SQLConnectionClass();
        MiscClass mc = new MiscClass();
        DataTable table = new DataTable();
        string CurrentUser = MigApp.Properties.Settings.Default.UserLogin;
        string ID;
        bool Deleted;

        // true - Создание
        // false - Редактирование
        bool Mode;

        public EmployeesWindow(bool mode, string id, bool deleted)
        {
            InitializeComponent();
            Mode = mode;
            ID = id;
            Deleted = deleted;
            Start(id);
        }

        // Нажатие кнопки "Сохранить"
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (FIO.Text.Length > 0 && Group.Text.Length > 0 && Room.Text.Length > 0 && BirthDate.Text.Length > 0)
            {
                if (Convert.ToInt32(sqlcc.ReqRef($"SELECT COUNT(*) FROM Employees Where FIO LIKE '{FIO.Text}'")) < 1 || !Mode)
                {
                    if (Mode)
                    {
                        sqlcc.ReqNonRef($"INSERT INTO Employees (FIO, [Group], Room, Birthdate) Values ('{FIO.Text}', '{Group.Text}', '{Room.Text}', '{BirthDate.Text}')");
                        sqlcc.Loging(CurrentUser, "Создание", "Сотрудники", FIO.Text, "");
                    }
                    else
                    {
                        sqlcc.ReqNonRef($"UPDATE Employees SET FIO = '{FIO.Text}', [Group] = '{Group.Text}', Room = '{Room.Text}', Birthdate = '{BirthDate.Text}' WHERE ID LIKE {ID}");
                        sqlcc.Loging(CurrentUser, "Редактирование", "Сотрудники", FIO.Text, "");
                    }
                    DialogResult = true; Close();
                }
                else
                {
                    FIO.Focus();
                    MessageBox.Show("Такой сотрудник уже существует.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Нажатие кнопки "Удалить"
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (!Deleted)
            {
                if (MessageBox.Show("Вы уверены что хотите удалить запись?","Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    sqlcc.ReqNonRef($"UPDATE Employees SET Deleted = 1, DelDate = '{DateTime.Now}' WHERE ID LIKE {ID}");
                    sqlcc.Loging(CurrentUser, "Удаление", "Сотрудники", FIO.Text, "");
                    DialogResult = true; Close();
                }
            }
            else
            {
                if (MessageBox.Show("Запись будет безвозвратно удалена.\nХотите удалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    sqlcc.Delete_DeletedEmployee(ID);
                    sqlcc.Loging(CurrentUser, "Стирание", "Сотрудники", FIO.Text, "");
                    DialogResult = true; Close();
                }

            }
        }

        // Нажатие кнопки "Восстановить"
        private void RecoveryClick(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlcc.Recovery("Employees", "ID", ID);
                sqlcc.Loging(CurrentUser, "Восстановление", "Сотрудники", FIO.Text, "");
                DialogResult = true; Close();
            }
            catch { }
        }

        // Проверка даты
        private void DateCheck(object sender, SelectionChangedEventArgs e)
        {
            if (BirthDate.SelectedDate > DateTime.Now.AddYears(-14))
            {
                MessageBox.Show("Сотрудник не может быть младше 14 лет", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                BirthDate.SelectedDate = null;
            }
        }

        // Заполнение полей и изменение названия окна
        private void Start(string ID)
        {
            if (Mode)
            {
                Title = "Сотрудники (Создание)";
                DeleteButton.Visibility = Visibility.Collapsed;
            }
            else if (!Mode && !Deleted)
            {
                try
                {
                    Title = "Сотрудники (Редактирование)";
                    table = sqlcc.DataGridUpdate("*", "Employees_View", $"WHERE ID LIKE {ID}");
                    DataRow row = table.Rows[0];
                    FIO.Text = row["ФИО"].ToString();
                    Group.Text = row["Отдел"].ToString();
                    Room.Text = row["Кабинет"].ToString();
                    BirthDate.Text = row["Дата рождения"].ToString();
                }
                catch
                { MessageBox.Show("Запись не найдена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            else if (!Mode && Deleted)
            {
                try
                {
                    LockAll();
                    Title = "Сотрудники (Редактирование)";
                    table = sqlcc.DataGridUpdate("*", "Employees_Deleted", $"WHERE ID LIKE {ID}");
                    DataRow row = table.Rows[0];
                    FIO.Text = row["ФИО"].ToString();
                    Group.Text = row["Отдел"].ToString();
                    Room.Text = row["Кабинет"].ToString();
                    BirthDate.Text = row["Дата рождения"].ToString();
                }
                catch
                { MessageBox.Show("Запись не найдена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }

        private void NumOnly(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void LockAll()
        {
            FIO.IsReadOnly = true;
            Group.IsReadOnly = true;
            Room.IsReadOnly = true;
            BirthDate.IsEnabled = false;
        }
    }
}
