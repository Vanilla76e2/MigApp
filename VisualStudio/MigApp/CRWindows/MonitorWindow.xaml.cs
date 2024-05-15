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
    /// Логика взаимодействия для MonitorWindow.xaml
    /// </summary>
    public partial class MonitorWindow : Window
    {
        SQLConnectionClass sqlcc = new SQLConnectionClass();
        MiscClass mc = new MiscClass();
        DataTable table = new DataTable();
        string InventoryNum;
        string CurrentUser = MigApp.Properties.Settings.Default.UserLogin;
        bool Deleted;

        // true - Создание
        // false - Редактирование
        bool Mode;

        public MonitorWindow(bool mode, string invnum, bool deleted)
        {
            InitializeComponent();
            Mode = mode;
            InventoryNum = invnum;
            Deleted = deleted;
            Start(invnum);
            InvNum.Focus();
        }

        // Нажатие кнопки "Сохранить"
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            // Проверка заполнения
            if (InvNum.Text.Length > 0 && Firm.Text.Length > 0 && Model.Text.Length > 0 && SeriaNum.Text.Length > 0 && ScreenDiagonal.Text.Length > 0 && ScreenResolution.Text.Length > 0 && ScreenType.Text.Length > 0)
            {
                if (sqlcc.InvNumChecker(InvNum.Text) || !Mode)
                {
                    string pc = $"SELECT InvNum From Computers Where Name Like '{PC.Text}'";
                    // Если создание
                    if (Mode == true)
                    {
                        sqlcc.ReqNonRef($"INSERT INTO Monitor (InvNum, Firm, Model, SNum, Diagonal, Resolution, Screen, PC) Values ('{InvNum.Text}', '{Firm.Text}', '{Model.Text}', '{SeriaNum.Text}', '{ScreenDiagonal.Text}', '{ScreenResolution.Text}', '{ScreenType.Text}', ({pc}))");
                        sqlcc.Loging(CurrentUser, "Создание", "Мониторы", InvNum.Text, "");
                    }
                    // Если редактирование
                    else
                    {
                        sqlcc.ReqNonRef($"UPDATE Monitor SET Firm = '{Firm.Text}', Model = '{Model.Text}', SNum = '{SeriaNum.Text}', Diagonal = '{ScreenDiagonal.Text}', Resolution = '{ScreenResolution.Text}', Screen = '{ScreenType.Text}', PC = ({pc}) Where InvNum LIKE '{InvNum.Text}'");
                        sqlcc.Loging(CurrentUser, "Редактирование", "Мониторы", InvNum.Text, "");
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
                if (Firm.Text.Length < 1) Firm.BorderBrush = Brushes.Red;
                else Firm.BorderBrush = Brushes.LightGray;
                if (Model.Text.Length < 1) Model.BorderBrush = Brushes.Red;
                else Model.BorderBrush = Brushes.LightGray;
                if (SeriaNum.Text.Length < 1) SeriaNum.BorderBrush = Brushes.Red;
                else SeriaNum.BorderBrush = Brushes.LightGray;
                if (ScreenDiagonal.Text.Length < 1) ScreenDiagonal.BorderBrush = Brushes.Red;
                else ScreenDiagonal.BorderBrush = Brushes.LightGray;
                if (ScreenType.Text.Length < 1) ScreenDiagonal.BorderBrush = Brushes.Red;
                else ScreenType.BorderBrush = Brushes.LightGray;
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
                    sqlcc.ReqDel($"UPDATE Monitors SET Deleted = 1 WHERE InvNum LIKE '{InventoryNum}'");
                    sqlcc.Loging(CurrentUser, "Удаление", "Мониторы", InvNum.Text, "");
                    DialogResult = true; Close();
                }
                else
                {
                    sqlcc.ReqDel($"DELETE FROM Monitors WHERE InvNum LIKE '{InventoryNum}'");
                    sqlcc.Loging(CurrentUser, "Стирание", "Мониторы", InventoryNum, "");
                    DialogResult = true; Close();
                }
            }
        }

        // Нажатие кнопки "Восстановить"
        private void RecoveryClick(object sender, RoutedEventArgs e)
        {
            sqlcc.Recovery("Monitors", "InvNum", InventoryNum);
            sqlcc.Loging(CurrentUser, "Восстановление", "Мониторы", InventoryNum, "");
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
            PC.IsEnabled = true;
            PCList.Clear();
            DataTable table1 = sqlcc.DataGridUpdate("Имя", "PC_View", $"WHERE [Пользователь] Like '{User.Text}'");
            foreach (DataRow row in table1.Rows)
            {
                PCList.Add(row["Имя"].ToString());
            }
            PC.ItemsSource = PCList;
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

        #endregion

        // Заполнение полей и изменение названия окна
        private void Start(string Invnum)
        {
            UserListFill();
            if (Mode)
            {
                Title = "Монитор (Создание)";
                DeleteButton.Visibility = Visibility.Collapsed;
            }
            else if (!Mode && !Deleted)
            {
                try
                {
                    InvNum.IsReadOnly = true;
                    Title = "Монитор (Редактирование)";
                    InvNum.Text = Invnum;
                    table = sqlcc.DataGridUpdate("*", "Monitors_View", $"WHERE [Инвентарный номер] Like '{Invnum}'");
                    DataRow row = table.Rows[0];
                    Firm.Text = row["Производитель"].ToString();
                    Model.Text = row["Модель"].ToString();
                    SeriaNum.Text = row["Серийный номер"].ToString();
                    ScreenDiagonal.Text = row["Диагональ экрана"].ToString();
                    ScreenResolution.Text = row["Разрешение экрана"].ToString();
                    ScreenType.Text = row["Тип экрана"].ToString();
                    string user = row["Пользователь"].ToString();
                    if (user.Length > 0)
                    {
                        User.Text = user;
                        User.SelectedValue = user;
                    }
                    string pc = sqlcc.ReqRef($"SELECT Name From Mon_PC_Support WHERE InvNum Like '{InvNum.Text}'");
                    if ( pc.Length > 0 )
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
                    Title = "Монитор (Редактирование)";
                    InvNum.Text = Invnum;
                    table = sqlcc.DataGridUpdate("*", "Monitors_Deleted", $"WHERE [Инвентарный номер] Like '{Invnum}'");
                    DataRow row = table.Rows[0];
                    Firm.Text = row["Производитель"].ToString();
                    Model.Text = row["Модель"].ToString();
                    SeriaNum.Text = row["Серийный номер"].ToString();
                    ScreenDiagonal.Text = row["Диагональ экрана"].ToString();
                    ScreenResolution.Text = row["Разрешение экрана"].ToString();
                    ScreenType.Text = row["Тип экрана"].ToString();
                    string user = row["Пользователь"].ToString();
                    if (user.Length > 0)
                    {
                        User.SelectedValue = user;
                    }
                    string pc = sqlcc.ReqRef($"SELECT Name From Mon_PC_Support WHERE InvNum Like '{InvNum.Text}'");
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

        private void DotNumOnly(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ".")
               && (!ScreenDiagonal.Text.Contains(".")
               && ScreenDiagonal.Text.Length != 0)))
            {
                e.Handled = true;
            }
        }

        private void LockAll()
        {
            Firm.IsReadOnly = true;
            Model.IsReadOnly = true;
            SeriaNum.IsReadOnly = true;
            ScreenDiagonal.IsReadOnly = true;
            ScreenResolution.IsReadOnly = true;
            ScreenType.IsReadOnly = true;
            User.IsEnabled = false;
            PC.IsEnabled = false;
        }
    }
}
