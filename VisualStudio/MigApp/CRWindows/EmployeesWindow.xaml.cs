using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace MigApp.CRWindows
{
    public partial class EmployeesWindow : Window
    {
        SQLConnectionClass sqlcc = SQLConnectionClass.getinstance();
        MiscClass mc = new MiscClass();
        DataTable table = new DataTable();
        string sqlTable = "Employees", logname = "Сотрудники";
        string CurrentUser = MigApp.Properties.Settings.Default.userLogin;
        string ID;
        bool Deleted;

        // true - Создание
        // false - Редактирование
        bool Mode;

        public EmployeesWindow(bool mode, string id, bool deleted, bool emppermission)
        {
            InitializeComponent();
            Mode = mode;
            ID = id;
            Deleted = deleted;
            Start(id, emppermission);
        }

        // Нажатие кнопки "Сохранить"
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (FIO.Text.Length > 0 && Group.Text.Length > 0 && Room.Text.Length > 0)
            {
                if (Convert.ToInt32(sqlcc.ReqRef($"SELECT COUNT(*) FROM {sqlTable} Where FIO LIKE '{FIO.Text}'")) < 1 || !Mode)
                {
                    if (Mode)
                    {                      
                        sqlcc.ReqNonRef($"INSERT INTO {sqlTable} (FIO, [Group], Room) Values ('{FIO.Text}', '{Group.Text}', '{Room.Text}')");
                        sqlcc.Loging(CurrentUser, "Создание", logname, FIO.Text, "");
                    }
                    else
                    {
                        sqlcc.ReqNonRef($"UPDATE {sqlTable} SET FIO = '{FIO.Text}', [Group] = '{Group.Text}', Room = '{Room.Text}' WHERE ID LIKE {ID}");
                        sqlcc.Loging(CurrentUser, "Редактирование", logname, FIO.Text, "");
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
                    sqlcc.ReqNonRef($"UPDATE {sqlTable} SET Deleted = 1, DelDate = '{DateTime.Now}' WHERE ID LIKE {ID}");
                    sqlcc.Loging(CurrentUser, "Удаление", logname, FIO.Text, "");
                    DialogResult = true; Close();
                }
            }
            else
            {
                if (MessageBox.Show("Запись будет безвозвратно удалена.\nХотите удалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    sqlcc.Delete_DeletedEmployee(ID);
                    sqlcc.Loging(CurrentUser, "Стирание", logname, FIO.Text, "");
                    DialogResult = true; Close();
                }

            }
        }

        // Нажатие кнопки "Восстановить"
        private void RecoveryClick(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlcc.Recovery(sqlTable, "ID", ID);
                sqlcc.Loging(CurrentUser, "Восстановление", logname , FIO.Text, "");
                DialogResult = true; Close();
            }
            catch { }
        }

        private void ListFill()
        {
            //groupList.Clear();
            //DataTable table = new DataTable();
            //table = sqlcc.DataGridUpdate("*", "Group_View","");
            //foreach (DataRow row in table.Rows)
            //{
            //    groupList.Add(row["Name"].ToString());
            //}
            //Group.ItemsSource = null;
            //Group.ItemsSource = groupList;
        }

        private List<string> groupList = new List<string>();

        // Заполнение полей и изменение названия окна
        private void Start(string ID, bool perm)
        {
            //if (!perm)
            //{
            //    GroupAdd.Visibility = Visibility.Collapsed;
            //    Group.Width = 380;
            //}

            //if (Mode)
            //{
            //    Title = "Сотрудники (Создание)";
            //    DeleteButton.Visibility = Visibility.Collapsed;
            //    ListFill();
            //}
            //else if (!Mode && !Deleted)
            //{
            //    try
            //    {
            //        Title = "Сотрудники (Редактирование)";
            //        ListFill();
            //        table = sqlcc.DataGridUpdate("*", "Employees_View", $"WHERE ID LIKE {ID}");
            //        DataRow row = table.Rows[0];
            //        FIO.Text = row["ФИО"].ToString(); 
            //        oldfavrow = row["ФИО"].ToString();
            //        Group.Text = row["Отдел"].ToString();
            //        Room.Text = row["Кабинет"].ToString();
            //    }
            //    catch
            //    { MessageBox.Show("Запись не найдена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            //}
            //else if (!Mode && Deleted)
            //{
            //    try
            //    {
            //        LockAll();
            //        Title = "Сотрудники (Редактирование)";
            //        table = sqlcc.DataGridUpdate("*", "Employees_Deleted", $"WHERE ID LIKE {ID}");
            //        DataRow row = table.Rows[0];
            //        FIO.Text = row["ФИО"].ToString();
            //        Group.Text = row["Отдел"].ToString();
            //        Room.Text = row["Кабинет"].ToString();
            //    }
            //    catch
            //    { MessageBox.Show("Запись не найдена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            //}
        }

        private void NumOnly(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!Char.IsDigit(e.Text, 0))
                {
                    e.Handled = true;
                }
            }
            catch { }
        }

        private void LockAll()
        {
            FIO.IsReadOnly = true;
            Group.IsEnabled = false;
            Room.IsReadOnly = true;
            //BirthDate.IsEnabled = false;
        }

        private void CreateNewGroup(object sender, RoutedEventArgs e)
        {
            EmpGroupWindow win = new EmpGroupWindow(true, "");
            win.ShowDialog();
            ListFill();
        }
    }
}
