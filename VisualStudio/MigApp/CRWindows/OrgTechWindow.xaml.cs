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
    /// Логика взаимодействия для OrgTechWindow.xaml
    /// </summary>
    public partial class OrgTechWindow : Window
    {
        SQLConnectionClass sqlcc = SQLConnectionClass.getinstance();
        MiscClass mc = new MiscClass();
        DataTable table = new DataTable();
        string sqlTable = "OrgTech", logname = "Оргтехника";
        string CurrentUser = MigApp.Properties.Settings.Default.userLogin;
        string InventoryNum;
        bool Deleted, EmpPerm, GrPerm;
        string ip = "";

        // true - Создание
        // false - Редактирование
        bool Mode;

        public OrgTechWindow(bool mode, string invnum, bool deleted, bool emppermission, bool pcpermission, bool grouppermission)
        {
            InitializeComponent();
            Mode = mode;
            InventoryNum = invnum;
            Deleted = deleted;
            EmpPerm = emppermission;
            GrPerm = grouppermission;
            Start(invnum, emppermission, pcpermission);
            InvNum.Focus();
            Tip.ToolTip = "Ethernet: Введите номер кабинета\nUSB: Выберите пользователя и компьютер к которому подключено устройство";
        }

        // Нажатие кнопки "Сохранить"
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            // Проверка заполнения
            if (InvNum.Text.Length > 0 && Type.Text.Length > 0 && Model.Text.Length > 0 && SeriaNum.Text.Length > 0 && Cartrige.Text.Length > 0)
            {
                if (InvNumChecker(InvNum.Text) || !Mode)
                {
                    string pc = $"SELECT InvNum From Computers Where Name Like '{PC.Text}'";
                    // Если создание
                    if (Mode == true)
                    {
                        if (ip1.Text.Length > 0 && ip2.Text.Length > 0 && ip3.Text.Length > 0 && ip4.Text.Length > 0)
                            ip = ip1.Text + "." + ip2.Text + "." + ip3.Text + "." + ip4.Text;
                        else ip = "...";
                        if (Ethernet.IsChecked == true)
                            sqlcc.ReqNonRef($"INSERT INTO {sqlTable} (InvNum, Type, Model, SNum, Name, IP, Login, Password, Сartridge_Model, Room, Comment) Values ('{InvNum.Text}', '{Type.Text}', '{Model.Text}', '{SeriaNum.Text}', '{OTName.Text}', '{ip}', '{Login.Password}', '{Password.Password}', '{Cartrige.Text}', ({Room.Text}), '{Comment.Text}')");
                        else if (USB.IsChecked == true)
                            sqlcc.ReqNonRef($"INSERT INTO {sqlTable} (InvNum, Type, Model, SNum, Name, Login, Password, Сartridge_Model, PC, Comment, IP) Values ('{InvNum.Text}', '{Type.Text}', '{Model.Text}', '{SeriaNum.Text}', '{OTName.Text}', '{Login.Password}', '{Password.Password}', '{Cartrige.Text}', ({pc}), '{Comment.Text}', '...')");
                        sqlcc.Loging(CurrentUser, "Создание", logname, InvNum.Text, Type.Text + " " + Model.Text);
                    }
                    // Если редактирование
                    else
                    {
                        if (ip1.Text.Length > 0 && ip2.Text.Length > 0 && ip3.Text.Length > 0 && ip4.Text.Length > 0)
                            ip = ip1.Text + "." + ip2.Text + "." + ip3.Text + "." + ip4.Text;
                        else ip = "...";
                        if (Ethernet.IsChecked == true)
                            sqlcc.ReqNonRef($"UPDATE {sqlTable} SET InvNum = '{InvNum.Text}', Type = '{Type.Text}', Model = '{Model.Text}', SNum = '{SeriaNum.Text}', Name = '{OTName.Text}', IP = '{ip}', Login = '{Login.Password}', Password = '{Password.Password}', Сartridge_Model = '{Cartrige.Text}', Room = '{Room.Text}', PC = NULL, Comment = '{Comment.Text}' Where InvNum LIKE '{InventoryNum}'");
                        else if (USB.IsChecked == true)
                            sqlcc.ReqNonRef($"UPDATE {sqlTable} SET InvNum = '{InvNum.Text}', Type = '{Type.Text}', Model = '{Model.Text}', SNum = '{SeriaNum.Text}', Name = '{OTName.Text}', IP = '{ip}', Login = '{Login.Password}', Password = '{Password.Password}', Сartridge_Model = '{Cartrige.Text}', Room = NULL, PC = ({pc}), Comment = '{Comment.Text}' Where InvNum LIKE '{InventoryNum}'");
                        sqlcc.Loging(CurrentUser, "Редактирование", logname, InvNum.Text, Type.Text + " " + Model.Text);
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
                if (Type.Text.Length < 1) Type.BorderBrush = Brushes.Red;
                else Type.BorderBrush = Brushes.LightGray;
                if (Model.Text.Length < 1) Model.BorderBrush = Brushes.Red;
                else Model.BorderBrush = Brushes.LightGray;
                if (SeriaNum.Text.Length < 1) SeriaNum.BorderBrush = Brushes.Red;
                else SeriaNum.BorderBrush = Brushes.LightGray;
                if (Cartrige.Text.Length < 1) Cartrige.BorderBrush = Brushes.Red;
                else Cartrige.BorderBrush = Brushes.LightGray;
                MessageBox.Show("Не все обязательные поля заполнены!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    sqlcc.Loging(CurrentUser, "Удаление", logname, InventoryNum, Type.Text + " " + Model.Text);
                    DialogResult = true; Close();
                }
                else
                {
                    sqlcc.ReqDel($"DELETE FROM {sqlTable} WHERE InvNum Like '{InvNum}'");
                    sqlcc.Loging(CurrentUser, "Стирание", logname, InventoryNum, Type.Text + " " + Model.Text);
                    DialogResult = true; Close();
                }
            }
        }

        // Нажатие кнопки "Восстановить"
        private void RecoveryClick(object sender, RoutedEventArgs e)
        {
            sqlcc.Recovery(sqlTable, "InvNum", InventoryNum);
            sqlcc.Loging(CurrentUser, "Восстановление", logname, InventoryNum, Type.Text + " " + Model.Text);
            DialogResult = true; Close();
        }

        #region User ComboBox

        private void UserListFill()
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

        private void UserSelected(object sender, SelectionChangedEventArgs e)
        {
            PCListFill(User.Text);
        }

        private List<string> userList = new List<string>();
        private List<string> PCList = new List<string>();

        private void UserChanged(object sender, TextChangedEventArgs e)
        {
            User.IsDropDownOpen = true;
            User.SelectedIndex = -1;
            string temp = ((ComboBox)sender).Text;
            var newList = userList.Where(x => x.Contains(temp));
            User.ItemsSource = newList.ToList();
        }
        
        private void PCListFill (string user)
        {
            PC.IsEnabled = true;
            PCList.Clear();
            DataTable table = sqlcc.DataGridUpdate("Имя", "PC_View", $"WHERE [Пользователь] Like '{user}'");
            foreach (DataRow row in table.Rows)
            {
                PCList.Add(row["Имя"].ToString());
            }
            PC.ItemsSource = PCList;
        }

        #endregion

        // Заполнение полей и изменение названия окна
        private void Start(string Invnum, bool empperm, bool pcperm)
        {
            if (!empperm)
            {
                EmployeeAdd.Visibility = Visibility.Collapsed;
                User.Width = 320;
            }
            if (!pcperm)
            {
                PCAdd.Visibility = Visibility.Collapsed;
                PC.Width = 320;
            }
            UserListFill();
            if (Mode)
            {
                Title = "Оргтехника (Создание)";
                DeleteButton.Visibility = Visibility.Collapsed;
                Ethernet.IsChecked = true;
            }
            else if (!Mode && !Deleted)
            {
                try
                {

                    Title = "Оргтехника (Редактирование)";
                    InvNum.Text = Invnum;
                    table = sqlcc.DataGridUpdate("*", "OrgTech_View", $"WHERE [Инвентарный номер] Like '{Invnum}'");
                    DataRow row = table.Rows[0];

                    if (row["Пользователь"].ToString().Length > 0)
                        USB.IsChecked = true;
                    else
                    {
                        Ethernet.IsChecked = true;
                        Room.Text = row["Кабинет"].ToString();
                    }

                    Type.Text = row["Тип"].ToString();
                    Model.Text = row["Модель"].ToString();
                    SeriaNum.Text = row["Серийный номер"].ToString();
                    OTName.Text = row["Имя"].ToString();
                    string[] ip = mc.IPSplitter(row["IP"].ToString());
                    ip1.Text = ip[0];
                    ip2.Text = ip[1];
                    ip3.Text = ip[2];
                    ip4.Text = ip[3];
                    Login.Password = sqlcc.ReqRef($"SELECT Login FROM OrgTech WHERE InvNum LIKE '{InvNum.Text}'");
                    Password.Password = sqlcc.ReqRef($"SELECT Password FROM OrgTech WHERE InvNum LIKE '{InvNum.Text}'");
                    Cartrige.Text = row["Картридж"].ToString();
                    string user = row["Пользователь"].ToString();
                    if (user.Length > 0)
                    {
                        User.Text = user;
                        User.SelectedValue = user;
                    }
                    string pc = sqlcc.ReqRef($"SELECT Name FROM OT_PC_Support WHERE InvNum Like '{Invnum}'");
                    if ( pc.Length > 0)
                    {
                        PC.SelectedValue = pc;
                    }
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
                    Title = "Оргтехника (Архив)";
                    InvNum.Text = Invnum;
                    table = sqlcc.DataGridUpdate("*", "OrgTech_Deleted", $"WHERE [Инвентарный номер] Like '{Invnum}'");
                    DataRow row = table.Rows[0];

                    if (row["Пользователь"].ToString().Length > 0)
                        USB.IsChecked = true;
                    else
                    {
                        Ethernet.IsChecked = true;
                        Room.Text = row["Кабинет"].ToString();
                    }

                    Type.Text = row["Тип"].ToString();
                    Model.Text = row["Модель"].ToString();
                    SeriaNum.Text = row["Серийный номер"].ToString();
                    OTName.Text = row["Имя"].ToString();
                    string[] ip = mc.IPSplitter(row["IP"].ToString());
                    ip1.Text = ip[0];
                    ip2.Text = ip[1];
                    ip3.Text = ip[2];
                    ip4.Text = ip[3];
                    Login.Password = sqlcc.ReqRef($"SELECT Login FROM OrgTech WHERE InvNum LIKE '{InvNum.Text}'");
                    Password.Password = sqlcc.ReqRef($"SELECT Password FROM OrgTech WHERE InvNum LIKE '{InvNum.Text}'");
                    Cartrige.Text = row["Картридж"].ToString();
                    string user = row["Пользователь"].ToString();
                    if (user.Length > 0)
                    {
                        User.Text = user;
                        User.SelectedValue = user;
                    }
                    string pc = sqlcc.ReqRef($"SELECT Name FROM OT_PC_Support WHERE InvNum Like '{InvNum.Text}'");
                    if (pc.Length > 0)
                    {
                        PC.SelectedValue = pc;
                    }
                    Comment.Text = row["Комментарий"].ToString() ;
                }
                catch
                {
                    MessageBox.Show("Запись не найдена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #region IP Box

        // Проверка на цифры
        private void NumOnlyIP(object sender, TextCompositionEventArgs e)
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
            InvNum.IsReadOnly = true;
            Type.IsEnabled = false;
            Model.IsReadOnly = true;
            SeriaNum.IsReadOnly = true;
            OTName.IsReadOnly = true;
            ip1.IsReadOnly = true;
            ip2.IsReadOnly = true;
            ip3.IsReadOnly = true;
            ip4.IsReadOnly = true;
            Login.IsEnabled = false;
            Password.IsEnabled = false;
            Cartrige.IsReadOnly = true;
            User.IsEnabled = false; 
            PC.IsEnabled = false;
            Comment.IsReadOnly = true;
        }

        private void CreateNewEmployee(object sender, RoutedEventArgs e)
        {
            EmployeesWindow win = new EmployeesWindow(true, null, false, GrPerm);
            win.ShowDialog();
            UserListFill();
        }

        private void CreateNewPC(object sender, RoutedEventArgs e)
        {
            PCWindow win = new PCWindow(true, null, false, EmpPerm, GrPerm);
            win.ShowDialog();
            PCListFill(User.Text);
            if (PCList.Count() == 0)
            {
                PC.IsEnabled = false;
            }
        }

        private void ClearPC(object sender, RoutedEventArgs e)
        {
            PC.Text = string.Empty;
            PC.SelectedIndex = -1;
        }

        private bool InvNumChecker(string invnum)
        {
            if (Convert.ToUInt32(sqlcc.ReqRef($"SELECT COUNT(*) FROM {sqlTable} WHERE InvNum LIKE '{invnum}'")) < 1)
                return true;
            else return false;
        }

        private void Switchged(object sender, RoutedEventArgs e)
        {
            if (Ethernet.IsChecked == true)
            {
                EthernetPanel.Visibility = Visibility.Visible;
                USBPanel.Visibility = Visibility.Collapsed;
                User.Text = string.Empty;
                PC.Text = string.Empty;
            }
            else if (USB.IsChecked == true)
            {
                EthernetPanel.Visibility = Visibility.Collapsed;
                USBPanel.Visibility = Visibility.Visible;
                Room.Text = string.Empty;
                ip1.Text = string.Empty;
                ip2.Text = string.Empty;
                ip3.Text = string.Empty;
                ip4.Text = string.Empty;
            }

        }
    }
}
