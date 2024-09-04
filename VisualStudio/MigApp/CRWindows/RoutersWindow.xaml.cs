using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace MigApp.CRWindows
{
    /// <summary>
    /// Логика взаимодействия для RoutersWindow.xaml
    /// </summary>
    public partial class RoutersWindow : Window
    {
        SQLConnectionClass sqlcc = SQLConnectionClass.getinstance();
        MiscClass mc = new MiscClass();
        DataTable table = new DataTable();
        string CurrentUser = MigApp.Properties.Settings.Default.UserLogin;
        string InventoryNum;
        bool Deleted;
        string ip, dhcp;

        // true - Создание
        // false - Редактирование
        bool Mode;

        public RoutersWindow(bool mode, string invnum, bool deleted)
        {
            InitializeComponent();
            Mode = mode;
            InventoryNum = invnum;
            InvNum.Focus();
            Deleted = deleted;
            Start(invnum);
        }

        // Нажатие кнопки "Сохранить"
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            // Проверка заполнения
            if (InvNum.Text.Length > 0 && RouterModel.Text.Length > 0)
            {
                if (InvNumChecker(InvNum.Text) || !Mode)
                {
                    // Если создание
                    if (Mode == true)
                    {
                        if (ip1.Text.Length > 0 && ip2.Text.Length > 0 && ip3.Text.Length > 0 && ip4.Text.Length > 0)
                            ip = ip1.Text + "." + ip2.Text + "." + ip3.Text + "." + ip4.Text;
                        else ip = "...";

                        if (dhcp1.Text.Length > 0 && dhcp2.Text.Length > 0 && dhcp3.Text.Length > 0 && dhcp4.Text.Length > 0 && dhcp5.Text.Length > 0)
                            dhcp = dhcp1.Text + "." + dhcp2.Text + "." + dhcp3.Text + "." + dhcp4.Text + "-" + dhcp5.Text;
                        else dhcp = "...-";

                        sqlcc.ReqNonRef($"INSERT INTO Routers (InvNum, Name, Model, IP, DHCP, Login, Password, WiFi_Name, WiFi_Password, Comment) Values ('{InvNum.Text}', '{RouterName.Text}', '{RouterModel.Text}', '{ip}', '{dhcp}', '{AdminLogin.Password}', '{AdminPass.Password}', '{WiFiLogin.Text}', '{WiFiPass.Text}', '{Comment.Text}')");
                        sqlcc.Loging(CurrentUser, "Создание", "Роутеры", InvNum.Text, "");
                    }
                    // Если редактирование
                    else
                    {
                        if (ip1.Text.Length > 0 && ip2.Text.Length > 0 && ip3.Text.Length > 0 && ip4.Text.Length > 0)
                            ip = ip1.Text + "." + ip2.Text + "." + ip3.Text + "." + ip4.Text;
                        else ip = "...";

                        if (dhcp1.Text.Length > 0 && dhcp2.Text.Length > 0 && dhcp3.Text.Length > 0 && dhcp4.Text.Length > 0 && dhcp5.Text.Length > 0)
                            dhcp = dhcp1.Text + "." + dhcp2.Text + "." + dhcp3.Text + "." + dhcp4.Text + "-" + dhcp5.Text;
                        else dhcp = "...-";

                        sqlcc.ReqNonRef($"UPDATE Routers SET InvNum = '{InvNum.Text}', Name = '{RouterName.Text}', Model = '{RouterModel.Text}', IP = '{ip}', DHCP = '{dhcp}', Login = '{AdminLogin.Password}', Password = '{AdminPass.Password}', WiFi_Name = '{WiFiLogin.Text}', WiFi_Password = '{WiFiPass.Text}', Comment = '{Comment.Text}' Where InvNum LIKE '{InventoryNum}'");
                        sqlcc.Loging(CurrentUser, "Редактирование", "Роутеры", InvNum.Text, "");
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
                if (RouterModel.Text.Length < 1) RouterModel.BorderBrush = Brushes.Red;
                else RouterModel.BorderBrush = Brushes.LightGray;
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
                    sqlcc.ReqDel($"UPDATE Routers SET Deleted = 1, DelDate = '{DateTime.Now}' WHERE InvNum LIKE '{InventoryNum}'");
                    sqlcc.Loging(CurrentUser, "Удаление", "Роутеры", InvNum.Text, "");
                    DialogResult = true; Close();
                }
            }
            else
            {
                if (MessageBox.Show("Запись будет безвозвратно удалена.\nХотите удалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    sqlcc.ReqDel($"DELETE FROM Routers WHERE InvNum LIKE '{InventoryNum}'");
                    sqlcc.Loging(CurrentUser, "Стирание", "Роутеры", InventoryNum, "");
                    DialogResult = true; Close();
                }
            }
        }

        // Нажатие кнопки "Восстановить"
        private void RecoveryClick(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlcc.Recovery("Routers", "InvNum", InventoryNum);
                sqlcc.Loging(CurrentUser, "Восстановление", "Роутеры", InventoryNum, "");
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

        #region DHCP Box

        // Проверка на цифры
        private void NumOnlyDHCP(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        // Проверка до 255 и переключение на следующий
        private void DHCPcheck1(object sender, TextChangedEventArgs e)
        {
            if (dhcp1.Text.Length == 3 && dhcp1.Focus())
                dhcp2.Focus();
            try
            {
                if (Convert.ToInt32(dhcp1.Text) > 255)
                    dhcp1.Text = "255";
            }
            catch { }
        }

        private void DHCPcheck2(object sender, TextChangedEventArgs e)
        {
            if (dhcp2.Text.Length == 3 && dhcp2.Focus())
                dhcp3.Focus();
            try
            {
                if (Convert.ToInt32(dhcp2.Text) > 255)
                    dhcp2.Text = "255";
            }
            catch { }
        }

        private void DHCPcheck3(object sender, TextChangedEventArgs e)
        {
            if (dhcp3.Text.Length == 3)
                dhcp4.Focus();
            try
            {
                if (Convert.ToInt32(dhcp3.Text) > 255)
                    dhcp3.Text = "255";
            }
            catch { }
        }

        private void DHCPcheck4(object sender, TextChangedEventArgs e)
        {
            if (dhcp4.Text.Length == 3)
                e.Handled = false;
            try
            {
                if (Convert.ToInt32(dhcp4.Text) > 255)
                    dhcp4.Text = "255";
            }
            catch { }
        }

        private void DHCPcheck5(object sender, TextChangedEventArgs e)
        {
            if (dhcp5.Text.Length == 3)
                e.Handled = false;
            try
            {
                if (Convert.ToInt32(dhcp5.Text) > 255)
                    dhcp5.Text = "255";
            }
            catch { }
        }

        private void DHCPfocus(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            e.Handled = true;
            if (textBox != null)
                textBox.SelectAll();
        }

        // Запрет на пробелы
        private void NextDHCP1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                dhcp2.Focus();
            }
        }

        private void NextDHCP2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                dhcp3.Focus();
            }
            if (e.Key == Key.Back && dhcp2.Text.Length == 0)
            {
                e.Handled = true;
                dhcp1.Focus();
            }

        }

        private void NextDHCP3(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                dhcp4.Focus();
            }
            if (e.Key == Key.Back && dhcp3.Text.Length == 0)
            {
                e.Handled = true;
                dhcp2.Focus();
            }
        }

        private void NextDHCP4(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Back && dhcp4.Text.Length == 0)
            {
                e.Handled = true;
                dhcp3.Focus();
            }
        }

        private void NextDHCP5(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Back && dhcp5.Text.Length == 0)
            {
                e.Handled = true;
                dhcp4.Focus();
            }
        }
        #endregion

        // Заполнение полей и изменение названия окна
        private void Start(string Invnum)
        {
            if (Mode)
            {
                Title = "Роутер (Создание)";
                DeleteButton.Visibility = Visibility.Collapsed;
            }
            else if (!Mode && !Deleted)
            {
                try
                {
                    Title = "Роутер (Редактирование)";
                    InvNum.Text = Invnum;
                    table = sqlcc.DataGridUpdate("*", "Routers_View", $"WHERE [Инвентарный номер] Like '{Invnum}'");
                    DataRow row = table.Rows[0];
                    RouterName.Text = row["Имя"].ToString();
                    RouterModel.Text = row["Модель"].ToString();

                    string[] ip = mc.IPSplitter(row["IP"].ToString());
                    ip1.Text = ip[0];
                    ip2.Text = ip[1];
                    ip3.Text = ip[2];
                    ip4.Text = ip[3];

                    string[] dhcp = mc.IPSplitter(row["DHCP"].ToString());
                    dhcp1.Text = dhcp[0];
                    dhcp2.Text = dhcp[1];
                    dhcp3.Text = dhcp[2];
                    string[] dhcp45 = mc.DHCPSplitter(dhcp[3].ToString());
                    dhcp4.Text = dhcp45[0];
                    dhcp5.Text = dhcp45[1];

                    AdminLogin.Password = sqlcc.ReqRef($"SELECT Login FROM Routers WHERE InvNum LIKE '{Invnum}'");
                    AdminPass.Password = sqlcc.ReqRef($"SELECT Password FROM Routers WHERE InvNum LIKE '{Invnum}'");
                    WiFiLogin.Text = row["Имя сети"].ToString();
                    WiFiPass.Text = row["Пароль сети"].ToString();
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
                    Title = "Роутер (Архив)";
                    InvNum.Text = Invnum;
                    table = sqlcc.DataGridUpdate("*", "Routers_Deleted", $"WHERE [Инвентарный номер] Like '{Invnum}'");
                    DataRow row = table.Rows[0];
                    RouterName.Text = row["Имя"].ToString();
                    RouterModel.Text = row["Модель"].ToString();

                    string[] ip = mc.IPSplitter(row["IP"].ToString());
                    ip1.Text = ip[0];
                    ip2.Text = ip[1];
                    ip3.Text = ip[2];
                    ip4.Text = ip[3];

                    string[] dhcp = mc.IPSplitter(row["DHCP"].ToString());
                    dhcp1.Text = dhcp[0];
                    dhcp2.Text = dhcp[1];
                    dhcp3.Text = dhcp[2];
                    string[] dhcp45 = mc.DHCPSplitter(dhcp[3].ToString());
                    dhcp4.Text = dhcp45[0];
                    dhcp5.Text = dhcp45[1];

                    AdminLogin.Password = sqlcc.ReqRef($"SELECT Login FROM Routers WHERE InvNum LIKE '{Invnum}'");
                    AdminPass.Password = sqlcc.ReqRef($"SELECT Password FROM Routers WHERE InvNum LIKE '{Invnum}'");
                    WiFiLogin.Text = row["Имя сети"].ToString();
                    WiFiPass.Text = row["Пароль сети"].ToString();
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
            RouterName.IsReadOnly = true;
            RouterModel.IsReadOnly = true;
            ip1.IsReadOnly = true;
            ip2.IsReadOnly = true;
            ip3.IsReadOnly = true;
            ip4.IsReadOnly = true;
            dhcp1.IsReadOnly = true;
            dhcp2.IsReadOnly = true;
            dhcp3.IsReadOnly = true;
            dhcp4.IsReadOnly = true;
            dhcp5.IsReadOnly = true;
            WiFiLogin.IsReadOnly = false;
            WiFiPass.IsReadOnly = false;
            Comment.IsReadOnly = false;
        }

        private bool InvNumChecker(string invnum)
        {
            if (Convert.ToUInt32(sqlcc.ReqRef($"SELECT COUNT(*) FROM Routers WHERE InvNum LIKE '{invnum}'")) < 1)
                return true;
            else return false;
        }
    }
}
