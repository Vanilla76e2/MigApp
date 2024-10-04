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
    /// <summary>
    /// Логика взаимодействия для SwitchesWindow.xaml
    /// </summary>
    public partial class SwitchesWindow : Window
    {
        SQLConnectionClass sqlcc = SQLConnectionClass.getinstance();
        MiscClass mc = new MiscClass();
        DataTable table = new DataTable();
        string sqlTable = "Switches", logname = "Свитчи";
        string CurrentUser = MigApp.Properties.Settings.Default.UserLogin;
        string InventoryNum;
        bool Deleted;
        string ip;

        // true - Создание
        // false - Редактирование
        bool Mode;

        public SwitchesWindow(bool mode, string invnum, bool deleted)
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
            if (InvNum.Text.Length > 0 && SwitchModel.Text.Length > 0)
            {
                if (InvNumChecker(InvNum.Text) || !Mode)
                {
                    // Если создание
                    if (Mode == true)
                    {
                        if (ip1.Text.Length > 0 && ip2.Text.Length > 0 && ip3.Text.Length > 0 && ip4.Text.Length > 0)
                            ip = ip1.Text + "." + ip2.Text + "." + ip3.Text + "." + ip4.Text;
                        else ip = "...";

                        sqlcc.ReqNonRef($"INSERT INTO {sqlTable} (InvNum, Name, Model, IP, Login, Password, Comment) Values ('{InvNum.Text}', '{SwitchName.Text}', '{SwitchModel.Text}', '{ip}', '{AdminLogin.Password}', '{AdminPass.Password}', '{Comment.Text}')");
                        sqlcc.Loging(CurrentUser, "Создание", logname, InvNum.Text, SwitchModel.Text);
                    }
                    // Если редактирование
                    else
                    {
                        if (ip1.Text.Length > 0 && ip2.Text.Length > 0 && ip3.Text.Length > 0 && ip4.Text.Length > 0)
                            ip = ip1.Text + "." + ip2.Text + "." + ip3.Text + "." + ip4.Text;
                        else ip = "...";

                        sqlcc.ReqNonRef($"UPDATE {sqlTable} SET InvNum = '{InvNum.Text}', Name = '{SwitchName.Text}', Model = '{SwitchModel.Text}', IP = '{ip}', Login = '{AdminLogin.Password}', Password = '{AdminPass.Password}', Comment = '{Comment.Text}' Where InvNum LIKE '{InventoryNum}'");
                        sqlcc.Loging(CurrentUser, "Редактирование", logname, InvNum.Text, SwitchModel.Text);
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
                if (SwitchModel.Text.Length < 1) SwitchModel.BorderBrush = Brushes.Red;
                else SwitchModel.BorderBrush = Brushes.LightGray;
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
                    sqlcc.Loging(CurrentUser, "Удаление", logname, InvNum.Text, SwitchModel.Text);
                    DialogResult = true; Close();
                }
            }
            else
            {
                if (MessageBox.Show("Запись будет безвозвратно удалена.\nХотите удалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    sqlcc.ReqDel($"DELETE FROM {sqlTable} WHERE InvNum LIKE '{InventoryNum}'");
                    sqlcc.Loging(CurrentUser, "Стирание", logname, InventoryNum, SwitchModel.Text);
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
                sqlcc.Loging(CurrentUser, "Восстановление", logname, InventoryNum, SwitchModel.Text);
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

        // Заполнение полей и изменение названия окна
        private void Start(string Invnum)
        {
            if (Mode)
            {
                Title = "Свитч (Создание)";
                DeleteButton.Visibility = Visibility.Collapsed;
            }
            else if (!Mode && !Deleted)
            {
                try
                {
                    Title = "Свитч (Редактирование)";
                    InvNum.Text = Invnum;
                    table = sqlcc.DataGridUpdate("*", "Switches_View", $"WHERE [Инвентарный номер] Like '{Invnum}'");
                    DataRow row = table.Rows[0];
                    SwitchName.Text = row["Имя"].ToString();
                    SwitchModel.Text = row["Модель"].ToString();

                    string[] ip = mc.IPSplitter(row["IP"].ToString());
                    ip1.Text = ip[0];
                    ip2.Text = ip[1];
                    ip3.Text = ip[2];
                    ip4.Text = ip[3];

                    AdminLogin.Password = sqlcc.ReqRef($"SELECT Login FROM Switches WHERE InvNum LIKE '{Invnum}'");
                    AdminPass.Password = sqlcc.ReqRef($"SELECT Password FROM Switches WHERE InvNum LIKE '{Invnum}'");
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
                    Title = "Свитч (Архив)";
                    InvNum.Text = Invnum;
                    table = sqlcc.DataGridUpdate("*", "Switches_Deleted", $"WHERE [Инвентарный номер] Like '{Invnum}'");
                    DataRow row = table.Rows[0];
                    SwitchName.Text = row["Имя"].ToString();
                    SwitchModel.Text = row["Модель"].ToString();

                    string[] ip = mc.IPSplitter(row["IP"].ToString());
                    ip1.Text = ip[0];
                    ip2.Text = ip[1];
                    ip3.Text = ip[2];
                    ip4.Text = ip[3];

                    AdminLogin.Password = sqlcc.ReqRef($"SELECT Login FROM Switches WHERE InvNum LIKE '{Invnum}'");
                    AdminPass.Password = sqlcc.ReqRef($"SELECT Password FROM Switches WHERE InvNum LIKE '{Invnum}'");
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
            SwitchName.IsReadOnly = true;
            SwitchModel.IsReadOnly = true;
            ip1.IsReadOnly = true;
            ip2.IsReadOnly = true;
            ip3.IsReadOnly = true;
            ip4.IsReadOnly = true;
            Comment.IsReadOnly = false;
        }

        private bool InvNumChecker(string invnum)
        {
            if (Convert.ToUInt32(sqlcc.ReqRef($"SELECT COUNT(*) FROM Switches WHERE InvNum LIKE '{invnum}'")) < 1)
                return true;
            else return false;
        }
    }
}
