using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для PCWindow.xaml
    /// </summary>
    public partial class PCWindow : Window
    {
        SQLConnectionClass sqlcc = SQLConnectionClass.getinstance();
        MiscClass mc = new MiscClass();
        DataTable table = new DataTable();
        string sqlTable = "Computers", logname = "Компьютеры";
        string CurrentUser = MigApp.Properties.Settings.Default.UserLogin;
        string InventoryNum;
        string ip;
        bool Deleted, GrPerm;

        // true - Создание
        // false - Редактирование
        bool Mode;

        public PCWindow(bool mode, string invnum, bool deleted, bool emppermission, bool grouppermission)
        {
            InitializeComponent();
            Mode = mode;
            InventoryNum = invnum;
            InvNum.Focus();
            Deleted = deleted;
            GrPerm = grouppermission;
            Start(invnum, emppermission);
        }

        // Нажатие кнопки "Сохранить"
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            // Проверка заполнения
            if (InvNum.Text.Length > 0 && Motherboard.Text.Length > 0 && Processor.Text.Length > 0 && RAM.Text.Length > 0 && Drive.Text.Length > 0)
            {
                if (InvNumChecker(InvNum.Text) || !Mode)
                {
                    // Если создание
                    if (Mode == true)
                    {
                        if (ip1.Text.Length > 0 && ip2.Text.Length > 0 && ip3.Text.Length > 0 && ip4.Text.Length > 0)
                            ip = ip1.Text + "." + ip2.Text + "." + ip3.Text + "." + ip4.Text;
                        else ip = "...";
                        sqlcc.ReqNonRef($"INSERT INTO {sqlTable} (InvNum, Name, IP, [User], Admin_Login, Admin_Password, OS, Motherboard, Processor, RAM, Drive, Other, Comment) Values ('{InvNum.Text}', '{PCName.Text}', '{ip}', (SELECT ID FROM Employees WHERE FIO LIKE '{User.Text}'), '{AdminLogin.Password}', '{AdminPass.Password}', '{OS.Text}', '{Motherboard.Text}', '{Processor.Text}', '{RAM.Text}', '{Drive.Text}', '{Other.Text}', '{Comment.Text}')");
                        sqlcc.Loging(CurrentUser, "Создание", logname, InvNum.Text, PCName.Text);
                    }
                    // Если редактирование
                    else
                    {
                        if (ip1.Text.Length > 0 && ip2.Text.Length > 0 && ip3.Text.Length > 0 && ip4.Text.Length > 0)
                            ip = ip1.Text + "." + ip2.Text + "." + ip3.Text + "." + ip4.Text;
                        else ip = "...";
                        sqlcc.ReqNonRef($"UPDATE {sqlTable} SET InvNum = '{InvNum.Text}', Name = '{PCName.Text}', IP = '{ip}', [User] = (SELECT ID FROM Employees WHERE FIO LIKE '{User.Text}'), Admin_Login = '{AdminLogin.Password}', Admin_Password = '{AdminPass.Password}', OS = '{OS.Text}', Motherboard = '{Motherboard.Text}', Processor = '{Processor.Text}', RAM = '{RAM.Text}', Drive = '{Drive.Text}', Other = '{Other.Text}', Comment = '{Comment.Text}' Where InvNum LIKE '{InventoryNum}'");
                        sqlcc.Loging(CurrentUser, "Редактирование", logname, InvNum.Text, PCName.Text);
                    }
                    DialogResult = true; Close();
                }
                else
                {
                    InvNum.Focus();
                    MessageBox.Show("Инвентарный номер уже занят.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            // Если не заполнены обязательные поля
            else
            {
                if (InvNum.Text.Length < 1) InvNum.BorderBrush = Brushes.Red;
                else InvNum.BorderBrush = Brushes.LightGray;
                if (Motherboard.Text.Length < 1) Motherboard.BorderBrush = Brushes.Red;
                else Motherboard.BorderBrush = Brushes.LightGray;
                if (Processor.Text.Length < 1) Processor.BorderBrush = Brushes.Red;
                else Processor.BorderBrush = Brushes.LightGray;
                if (RAM.Text.Length < 1) RAM.BorderBrush = Brushes.Red;
                else RAM.BorderBrush = Brushes.LightGray;
                if (Drive.Text.Length < 1) Drive.BorderBrush = Brushes.Red;
                else Drive.BorderBrush = Brushes.LightGray;
                MessageBox.Show("Не все обязательные поля заполнены!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Нажатие кнопки "Удалить"
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (!Deleted)
            {
                if (MessageBox.Show("Вы уверены что хотите удалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    sqlcc.ReqDel($"UPDATE {sqlTable} SET Deleted = 1, DelDate = '{DateTime.Now}' WHERE InvNum LIKE '{InventoryNum}'");
                    sqlcc.Loging(CurrentUser, "Удаление", logname, InvNum.Text, PCName.Text);
                    DialogResult = true; Close();
                }
            }
            else
            {
                if (MessageBox.Show("Запись будет безвозвратно удалена.\nХотите удалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    sqlcc.Delete_DeletedPC(InventoryNum);
                    sqlcc.Loging(CurrentUser, "Стирание", logname, InventoryNum, PCName.Text);
                    DialogResult = true; Close();
                }
            }
        }

        // Нажатие кнопки "Восстановить"
        private void RecoveryClick(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlcc.Recovery(sqlTable, "InvNum", InventoryNum);
                sqlcc.Loging(CurrentUser, "Восстановление", logname, InventoryNum, PCName.Text);
                DialogResult = true; Close();
            }
            catch { }
        }

        #region IP Box

        // Проверка на цифры
        private void NumOnlyIP(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        // Проверка до 255 и переключение на следующий
        private void IPcheck1(object sender, TextChangedEventArgs e)
        {
            if (ip1.Text.Length == 3 && ip1.Focus())
                ip2.Focus();
            try
            {
                if (Convert.ToInt32(ip1.Text) > 255)
                    ip1.Text = "255";
            }
            catch { }
        }

        private void IPcheck2(object sender, TextChangedEventArgs e)
        {
            if (ip2.Text.Length == 3 && ip2.Focus())
                ip3.Focus();
            try
            {
                if (Convert.ToInt32(ip2.Text) > 255)
                    ip2.Text = "255";
            }
            catch { }
        }

        private void IPcheck3(object sender, TextChangedEventArgs e)
        {
            if (ip3.Text.Length == 3)
                ip4.Focus();
            try
            {
                if (Convert.ToInt32(ip3.Text) > 255)
                    ip3.Text = "255";
            }
            catch { }
        }

        private void IPcheck4(object sender, TextChangedEventArgs e)
        {
            if (ip4.Text.Length == 3)
                e.Handled = false;
            try
            {
                if (Convert.ToInt32(ip4.Text) > 255)
                    ip4.Text = "255";
            }
            catch { }
        }

        private void IPfocus(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            e.Handled = true;
            if (textBox != null)
                textBox.SelectAll();
        }

        // Запрет на пробелы
        private void NextIP1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip2.Focus();
            }
        }

        private void NextIP2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip3.Focus();
            }
            if (e.Key == Key.Back && ip2.Text.Length == 0)
            {
                e.Handled = true;
                ip1.Focus();
            }
            
        }

        private void NextIP3(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip4.Focus();
            }
            if (e.Key == Key.Back && ip3.Text.Length == 0)
            {
                e.Handled = true;
                ip2.Focus();
            }
        }

        private void NextIP4(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Back && ip4.Text.Length == 0)
            {
                e.Handled = true;
                ip3.Focus();
            }
        }

        #endregion

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
                Title = "Компьютер (Создание)";
                DeleteButton.Visibility = Visibility.Collapsed;
            }
            else if (!Mode && !Deleted)
            {
                try
                {
                    Title = "Компьютер (Редактирование)";
                    InvNum.Text = Invnum;
                    table = sqlcc.DataGridUpdate("*", "PC_View", $"WHERE [Инвентарный номер] Like '{Invnum}'");
                    DataRow row = table.Rows[0];
                    PCName.Text = row["Имя"].ToString();
                    string[] ip = mc.IPSplitter(row["IP"].ToString());
                    ip1.Text = ip[0];
                    ip2.Text = ip[1];
                    ip3.Text = ip[2];
                    ip4.Text = ip[3];
                    User.Text = row["Пользователь"].ToString();
                    AdminLogin.Password = sqlcc.ReqRef($"SELECT Admin_Login FROM Computers WHERE InvNum LIKE '{Invnum}'");
                    AdminPass.Password = sqlcc.ReqRef($"SELECT Admin_Password FROM Computers WHERE InvNum LIKE '{Invnum}'");
                    OS.Text = row["ОС"].ToString();
                    Motherboard.Text = row["Материнская плата"].ToString();
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
                    Title = "Компьютер (Архив)";
                    InvNum.Text = Invnum;
                    table = sqlcc.DataGridUpdate("*", "PC_Deleted", $"WHERE [Инвентарный номер] Like '{Invnum}'");
                    DataRow row = table.Rows[0];
                    PCName.Text = row["Имя"].ToString();
                    string[] ip = mc.IPSplitter(row["IP"].ToString());
                    ip1.Text = ip[0];
                    ip2.Text = ip[1];
                    ip3.Text = ip[2];
                    ip4.Text = ip[3];
                    User.Text = row["Пользователь"].ToString();
                    AdminLogin.Password = sqlcc.ReqRef($"SELECT Admin_Login FROM Computers WHERE InvNum LIKE '{Invnum}'");
                    AdminPass.Password = sqlcc.ReqRef($"SELECT Admin_Password FROM Computers WHERE InvNum LIKE '{Invnum}'");
                    OS.Text = row["ОС"].ToString();
                    Motherboard.Text = row["Материнская плата"].ToString();
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
            PCName.IsReadOnly = true;
            ip1.IsReadOnly = true;
            ip2.IsReadOnly = true;
            ip3.IsReadOnly = true;
            ip4.IsReadOnly = true;
            User.IsReadOnly = true;
            AdminLogin.IsEnabled = false;
            AdminPass.IsEnabled = false;
            OS.IsReadOnly = true;
            Motherboard.IsReadOnly = true;
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
            if (Convert.ToUInt32(sqlcc.ReqRef($"SELECT COUNT(*) FROM {sqlTable} WHERE InvNum LIKE '{invnum}'")) < 1)
                return true;
            else return false;
        }
    }
}
 