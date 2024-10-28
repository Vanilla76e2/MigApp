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
    /// Логика взаимодействия для MonitorWindow.xaml
    /// </summary>
    public partial class MonitorWindow : Window
    {
        SQLConnectionClass sqlcc = SQLConnectionClass.getinstance();
        MiscClass mc = new MiscClass();
        DataTable table = new DataTable();
        string sqlTable = "Monitor", logname = "Мониторы";
        string InventoryNum;
        string CurrentUser = MigApp.Properties.Settings.Default.userLogin;
        bool Deleted, EmpPerm, GrPerm;

        // true - Создание
        // false - Редактирование
        bool Mode;

        public MonitorWindow(bool mode, string invnum, bool deleted, bool emppermission, bool pcpermission, bool grouppermission)
        {
            InitializeComponent();
            Mode = mode;
            InventoryNum = invnum;
            Deleted = deleted;
            EmpPerm = emppermission;
            GrPerm = grouppermission;
            Start(invnum, emppermission, pcpermission);
            InvNum.Focus();
        }

        // Нажатие кнопки "Сохранить"
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            // Проверка заполнения
            if (InvNum.Text.Length > 0 && Firm.Text.Length > 0 && Model.Text.Length > 0 && SeriaNum.Text.Length > 0 && ScreenDiagonal.Text.Length > 0 && ScreenResolution.Text.Length > 0)
            {
                if (InvNumChecker(InvNum.Text) || !Mode)
                {
                    string pc = $"SELECT InvNum From Computers Where Name Like '{PC.Text}'";
                    // Если создание
                    if (Mode == true)
                    {
                        sqlcc.ReqNonRef($"INSERT INTO {sqlTable} (InvNum, Firm, Model, SNum, Diagonal, Resolution, Screen, PC, Comment) Values ('{InvNum.Text}', '{Firm.Text}', '{Model.Text}', '{SeriaNum.Text}', '{ScreenDiagonal.Text}', '{ScreenResolution.Text}', '{ScreenType.Text}', ({pc}), '{Comment.Text}')");
                        sqlcc.Loging(CurrentUser, "Создание", logname, InvNum.Text, Firm.Text + " " + Model.Text);
                    }
                    // Если редактирование
                    else
                    {
                        sqlcc.ReqNonRef($"UPDATE {sqlTable} SET InvNum = '{InvNum.Text}', Firm = '{Firm.Text}', Model = '{Model.Text}', SNum = '{SeriaNum.Text}', Diagonal = '{ScreenDiagonal.Text}', Resolution = '{ScreenResolution.Text}', Screen = '{ScreenType.Text}', PC = ({pc}), Comment = '{Comment.Text}' Where InvNum LIKE '{InventoryNum}'");
                        sqlcc.Loging(CurrentUser, "Редактирование", logname, InvNum.Text, Firm.Text + " " + Model.Text);
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
                    sqlcc.Loging(CurrentUser, "Удаление", logname, InvNum.Text, Firm.Text + " " + Model.Text);
                    DialogResult = true; Close();
                }
                else
                {
                    sqlcc.ReqDel($"DELETE FROM {sqlTable} WHERE InvNum LIKE '{InventoryNum}'");
                    sqlcc.Loging(CurrentUser, "Стирание", logname, InventoryNum, Firm.Text + " " + Model.Text);
                    DialogResult = true; Close();
                }
            }
        }

        // Нажатие кнопки "Восстановить"
        private void RecoveryClick(object sender, RoutedEventArgs e)
        {
            sqlcc.Recovery(sqlTable, "InvNum", InventoryNum);
            sqlcc.Loging(CurrentUser, "Восстановление", logname, InventoryNum, Firm.Text + " " + Model.Text);
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

        private void PCListFill(string user)
        {
            PC.IsEnabled = true;
            PCList.Clear();
            DataTable table1 = sqlcc.DataGridUpdate("Имя", "PC_View", $"WHERE [Пользователь] Like '{user}'");
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
        private void Start(string Invnum, bool emppermission, bool pcpermission)
        {
            if(emppermission)
            {
                EmployeeAdd.Visibility = Visibility.Collapsed;
                User.Width = 330;
            }
            if(pcpermission)
            {
                PCAdd.Visibility = Visibility.Collapsed;
                PC.Width = 330;
            }
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
            InvNum.IsReadOnly = true;
            Firm.IsReadOnly = true;
            Model.IsReadOnly = true;
            SeriaNum.IsReadOnly = true;
            ScreenDiagonal.IsReadOnly = true;
            ScreenResolution.IsReadOnly = true;
            ScreenType.IsReadOnly = true;
            User.IsEnabled = false;
            PC.IsEnabled = false;
            Comment.IsReadOnly = true;
        }

        private void CreateNewEmployee(object sender, RoutedEventArgs e)
        {
            User.Text = string.Empty;
            PC.Text = string.Empty;
            userList.Clear();
            PCList.Clear();
            EmployeesWindow win = new EmployeesWindow(true, null, false, GrPerm);
            win.ShowDialog();
            UserListFill();
        }

        private void ClearPC(object sender, RoutedEventArgs e)
        {
            PC.Text = string.Empty;
            PC.SelectedIndex = -1;
        }

        private void CreateNewPC(object sender, RoutedEventArgs e)
        {
            User.Text = string.Empty;
            PC.Text = string.Empty;
            userList.Clear();
            PCList.Clear();
            PCListFill(User.Text);
            if (PCList.Count() == 0)
            {
                PC.IsEnabled = false;
            }
        }

        private bool InvNumChecker(string invnum)
        {
            if (Convert.ToUInt32(sqlcc.ReqRef($"SELECT COUNT(*) FROM {sqlTable} WHERE InvNum LIKE '{invnum}'")) < 1)
                return true;
            else return false;
        }
    }
}
