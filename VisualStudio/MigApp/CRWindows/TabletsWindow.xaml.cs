using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MigApp.CRWindows
{
    /// <summary>
    /// Логика взаимодействия для TabletsWindow.xaml
    /// </summary>
    public partial class TabletsWindow : Window
    {
        SQLConnectionClass sqlcc = SQLConnectionClass.getinstance();
        MiscClass mc = new MiscClass();
        DataTable table = new DataTable();
        string sqlTable = "Tablets", logname = "Планшеты";
        string CurrentUser = MigApp.Properties.Settings.Default.userLogin;
        string InventoryNum;
        bool Deleted, GrPerm;

        // true - Создание
        // false - Редактирование
        bool Mode;

        public TabletsWindow(bool mode, string invnum, bool deleted, bool emppermission, bool grouppermission)
        {
            InitializeComponent();
            Mode = mode;
            InvNum.Focus();
            InventoryNum = invnum;
            Deleted = deleted;
            GrPerm = grouppermission;
            Start(invnum, emppermission);
        }

        // Нажатие кнопки "Сохранить"
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            // Проверка заполнения
            if (InvNum.Text.Length > 0 && Model.Text.Length > 0 && SeriaNum.Text.Length > 0 && ScreenDiagonal.Text.Length > 0 && Processor.Text.Length > 0 && RAM.Text.Length > 0 && Drive.Text.Length > 0)
            {
                if (InvNumChecker(InvNum.Text) || !Mode)
                {
                    // Если создание
                    if (Mode == true)
                    {
                        sqlcc.ReqNonRef($"INSERT INTO {sqlTable} (InvNum, Model, SNum, [User], OS, Diagonal, Processor, RAM, Drive, Other, Comment) Values ('{InvNum.Text}', '{Model.Text}', '{SeriaNum.Text}', (SELECT ID FROM Employees WHERE FIO LIKE '{User.Text}'), '{OS.Text}', '{ScreenDiagonal.Text}', '{Processor.Text}', '{RAM.Text}', '{Drive.Text}', '{Other.Text}', '{Comment.Text}')");
                        sqlcc.Loging(CurrentUser, "Создание", logname, InvNum.Text, Model.Text);
                    }
                    // Если редактирование
                    else
                    {
                        sqlcc.ReqNonRef($"UPDATE {sqlTable} SET InvNum = '{InvNum.Text}', Model = '{Model.Text}', SNum = '{SeriaNum.Text}', [User] = (SELECT ID FROM Employees WHERE FIO LIKE '{User.Text}'), OS = '{OS.Text}', Diagonal = '{ScreenDiagonal.Text}', Processor = '{Processor.Text}', RAM = '{RAM.Text}', Drive = '{Drive.Text}', Other = '{Other.Text}', Comment = '{Comment.Text}' Where InvNum LIKE '{InventoryNum}'");
                        sqlcc.Loging(CurrentUser, "Редактирование", logname, InvNum.Text, Model.Text);
                    }
                    DialogResult = true; Close();
                }
                else
                {
                    InvNum.Focus();
                    MessageBox.Show("Инвентарный номер уже занят.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            // Если не заполнены обязательные поля
            else
            {
                if (InvNum.Text.Length < 1) InvNum.BorderBrush = Brushes.Red;
                if (Model.Text.Length < 1) Model.BorderBrush = Brushes.Red;
                if (SeriaNum.Text.Length < 1) SeriaNum.BorderBrush = Brushes.Red;
                if (ScreenDiagonal.Text.Length < 1) ScreenDiagonal.BorderBrush = Brushes.Red;
                if (Processor.Text.Length < 1) Processor.BorderBrush = Brushes.Red;
                if (RAM.Text.Length < 1) RAM.BorderBrush = Brushes.Red;
                if (Drive.Text.Length < 1) Drive.BorderBrush = Brushes.Red;
                MessageBox.Show("Не все обязательные поля заполнены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Нажатие кнопки "Удалить"
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите удалить запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (!Deleted)
                {
                    sqlcc.ReqDel($"UPDATE {sqlTable} SET Deleted = 1 WHERE InvNum LIKE '{InventoryNum}'");
                    sqlcc.Loging(CurrentUser, "Удаление", logname, InvNum.Text, Model.Text);
                    DialogResult = true; Close();
                }
                else
                {
                    sqlcc.ReqDel($"DELETE FROM {sqlTable} WHERE InvNum LIKE '{InventoryNum}'");
                    sqlcc.Loging(CurrentUser, "Стирание", logname, InventoryNum, Model.Text);
                    DialogResult = true; Close();
                }
            }
        }

        // Нажатие кнопки "Восстановить"
        private void RecoveryClick(object sender, RoutedEventArgs e)
        {
            sqlcc.Recovery(sqlTable, "InvNum", InventoryNum);
            sqlcc.Loging(CurrentUser, "Восстановление", "Планшеты", InventoryNum, Model.Text);
            DialogResult = true; Close();
        }

        #region User ComboBox

        private void ListFill()
        {
            userList.Clear();
            DataTable table = new DataTable();
            table = sqlcc.DataGridUpdate("FIO", "Employees", "WHERE Deleted = 0 ORDER BY FIO ASC");
            foreach (DataRow row in table.Rows)
            {
                userList.Add(row["FIO"].ToString());
            }
            User.ItemsSource = userList;
        }

        private List<string> userList = new List<string>();

        private void UserChanged(object sender, TextChangedEventArgs e)
        {
            User.IsDropDownOpen = true;
            User.SelectedIndex = -1;
            string temp = ((ComboBox)sender).Text;
            var newList = userList.Where(x => x.Contains(temp));
            User.ItemsSource = newList.ToList();
        }

        #endregion

        // Заполнение полей и изменение названия окна
        private void Start(string Invnum, bool perm)
        {
            if (!perm) 
            {
                EmployeeAdd.Visibility = Visibility.Collapsed;
                User.Width = 330;
            }

            ListFill();
            if (Mode)
            {
                Title = "Планшет (Создание)";
                DeleteButton.Visibility = Visibility.Collapsed;
            }
            else if (!Mode && !Deleted)
            {
                try
                {
                    Title = "Планшет (Редактирование)";
                    InvNum.Text = Invnum;
                    table = sqlcc.DataGridUpdate("*", "Tablet_View", $"WHERE [Инвентарный номер] Like '{Invnum}'");
                    DataRow row = table.Rows[0];
                    Model.Text = row["Модель"].ToString();
                    SeriaNum.Text = row["Серийный номер"].ToString();
                    User.Text = row["Пользователь"].ToString();
                    OS.Text = row["ОС"].ToString();
                    ScreenDiagonal.Text = row["Диагональ экрана"].ToString();
                    Processor.Text = row["Процессор"].ToString();
                    RAM.Text = row["ОЗУ"].ToString();
                    Drive.Text = row["Накопители"].ToString();
                    Other.Text = row["Другое"].ToString();
                    Comment.Text = row["Комментарий"].ToString();
                }
                catch
                {
                    MessageBox.Show("Запись не найдена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (!Mode && Deleted)
            {
                try
                {
                    LockAll();
                    Title = "Планшет (Архив)";
                    InvNum.Text = Invnum;
                    table = sqlcc.DataGridUpdate("*", "Tablet_Deleted", $"WHERE [Инвентарный номер] Like '{Invnum}'");
                    DataRow row = table.Rows[0];
                    Model.Text = row["Модель"].ToString();
                    SeriaNum.Text = row["Серийный номер"].ToString();
                    User.Text = row["Пользователь"].ToString();
                    OS.Text = row["ОС"].ToString();
                    ScreenDiagonal.Text = row["Диагональ экрана"].ToString();
                    Processor.Text = row["Процессор"].ToString();
                    RAM.Text = row["ОЗУ"].ToString();
                    Drive.Text = row["Накопители"].ToString();
                    Other.Text = row["Другое"].ToString();
                    Comment.Text = row["Комментарий"].ToString();
                }
                catch
                {
                    MessageBox.Show("Запись не найдена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DotNumOnly(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ".")
               && (!ScreenDiagonal.Text.Contains(".")
               && ScreenDiagonal.Text.Length != 0)))
            {
                e.Handled = true;
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
            InvNum.IsReadOnly = true;
            Model.IsReadOnly = true;
            SeriaNum.IsReadOnly = true;
            User.IsEnabled = false;
            OS.IsReadOnly = true;
            ScreenDiagonal.IsReadOnly = true;
            Processor.IsReadOnly = true;
            RAM.IsReadOnly = true;
            Drive.IsReadOnly = true;
            Other.IsReadOnly = true;
        }

        private void CreateNewEmployee(object sender, RoutedEventArgs e)
        {
            EmployeesWindow win = new EmployeesWindow(true, null, false, GrPerm);
            win.ShowDialog();
            ListFill();
        }

        private bool InvNumChecker(string invnum)
        {
            if (Convert.ToUInt32(sqlcc.ReqRef($"SELECT COUNT(*) FROM Tablets WHERE InvNum LIKE '{invnum}'")) < 1)
                return true;
            else return false;
        }
    }
}
