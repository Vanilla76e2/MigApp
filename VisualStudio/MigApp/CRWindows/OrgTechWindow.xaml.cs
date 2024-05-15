using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
    /// Логика взаимодействия для OrgTechWindow.xaml
    /// </summary>
    public partial class OrgTechWindow : Window
    {
        SQLConnectionClass sqlcc = new SQLConnectionClass();
        MiscClass mc = new MiscClass();
        DataTable table = new DataTable();
        string CurrentUser = MigApp.Properties.Settings.Default.UserLogin;
        string InventoryNum;
        bool Deleted;

        // true - Создание
        // false - Редактирование
        bool Mode;

        public OrgTechWindow(bool mode, string invnum, bool deleted)
        {
            InitializeComponent();
            Mode = mode;
            InventoryNum = invnum;
            Deleted = deleted;
            Start(invnum);
            InvNum.Focus();
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
            if (ip1.Text.Length == 3)
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
            if (ip2.Text.Length == 3)
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
            else if (e.Key == Key.Back && ip1.Text.Length == 0)
            {
                e.Handled = true;
            }
        }

        private void NextIP2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip3.Focus();
            }
            else if (e.Key == Key.Back && ip2.Text.Length == 0)
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
            else if (e.Key == Key.Back && ip3.Text.Length == 0)
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
            else if (e.Key == Key.Back && ip4.Text.Length == 0)
            {
                e.Handled = true;
                ip3.Focus();
            }
        }

        #endregion

        // Нажатие кнопки "Сохранить"
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            // Проверка заполнения
            if (InvNum.Text.Length > 0 && Type.Text.Length > 0 && Model.Text.Length > 0 && SeriaNum.Text.Length > 0 && Cartrige.Text.Length > 0)
            {
                if (sqlcc.InvNumChecker(InvNum.Text) || !Mode)
                {
                    string pc = $"SELECT InvNum From Computers Where Name Like '{PC.Text}'";
                    // Если создание
                    if (Mode == true)
                    {
                        sqlcc.ReqNonRef($"INSERT INTO OrgTech (InvNum, Type, Model, SNum, Name, IP, Login, Password, Сartridge_Model, PC) Values ('{InvNum.Text}', '{Type.Text}', '{Model.Text}', '{SeriaNum.Text}', '{OTName.Text}', '{ip1.Text + "." + ip2.Text + "." + ip3.Text + "." + ip4.Text}', '{Login.Password}', '{Password.Password}', '{Cartrige.Text}', ({pc}))");
                        sqlcc.Loging(CurrentUser, "Создание", "Орг.техника", InvNum.Text, "");
                    }
                    // Если редактирование
                    else
                    {
                        sqlcc.ReqNonRef($"UPDATE OrgTech SET Type = '{Type.Text}', Model = '{Model.Text}', SNum = '{SeriaNum.Text}', Name = '{OTName.Text}', IP = '{ip1.Text + "." + ip2.Text + "." + ip3.Text + "." + ip4.Text}', Login = '{Login.Password}', Password = '{Password.Password}', Сartridge_Model = '{Cartrige.Text}', PC = ({pc}) Where InvNum LIKE '{InvNum.Text}'");
                        sqlcc.Loging(CurrentUser, "Редактирование", "Орг.техника", InvNum.Text, "");
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
                    sqlcc.ReqDel($"UPDATE OrgTech SET Deleted = 1 WHERE InvNum LIKE '{InventoryNum}'");
                    sqlcc.Loging(CurrentUser, "Удаление", "Орг.техника", InventoryNum, "");
                    DialogResult = true; Close();
                }
                else
                {
                    sqlcc.ReqDel($"DELETE FROM OrgTecg WHERE InvNum Like '{InvNum}'");
                    sqlcc.Loging(CurrentUser, "Стирание", "Орг.техника", InventoryNum, "");
                    DialogResult = true; Close();
                }
            }
        }

        // Нажатие кнопки "Восстановить"
        private void RecoveryClick(object sender, RoutedEventArgs e)
        {
            sqlcc.Recovery("OrgTech", "InvNum", InventoryNum);
            sqlcc.Loging(CurrentUser, "Восстановление", "Орг.техника", InventoryNum, "");
            DialogResult = true; Close();
        }

        #region User ComboBox

        private void UserListFill()
        {
            DataTable table = new DataTable();
            table = sqlcc.DataGridUpdate("FIO", "Employees", "WHERE Deleted = 0");
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
        private void Start(string Invnum)
        {
            UserListFill();
            if (Mode)
            {
                Title = "Оргтехника (Создание)";
                DeleteButton.Visibility = Visibility.Collapsed;
            }
            else if (!Mode && !Deleted)
            {
                try
                {
                    InvNum.IsReadOnly = true;
                    Title = "Оргтехника (Редактирование)";
                    InvNum.Text = Invnum;
                    table = sqlcc.DataGridUpdate("*", "OrgTech_View", $"WHERE [Инвентарный номер] Like '{Invnum}'");
                    DataRow row = table.Rows[0];
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
                    InvNum.IsReadOnly = true;
                    Title = "Оргтехника (Архив)";
                    InvNum.Text = Invnum;
                    table = sqlcc.DataGridUpdate("*", "OrgTech_Deleted", $"WHERE [Инвентарный номер] Like '{Invnum}'");
                    DataRow row = table.Rows[0];
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
        }
    }
}
