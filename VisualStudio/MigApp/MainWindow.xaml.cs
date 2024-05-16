using MigApp.CRWindows;
using MigApp.CRWindows.AdminPanel;
using System;
using System.IO;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace MigApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SQLConnectionClass sqlcc = new SQLConnectionClass();
        MiscClass mc = new MiscClass();
        #region Переменные
        string CurrentUser;
        bool EmpRedPerm = false;
        bool EmpRead = false;
        bool PCRedPerm = false;
        bool PCRead = false;
        bool NbRedPerm = false;
        bool NbRead = false;
        bool TabRedPerm = false;
        bool TabRead = false;
        bool OTRedPerm = false;
        bool OTRead = false;
        bool MonRedPerm = false;
        bool MonRead = false;
        bool Admin = false;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            this.MaxWidth = System.Windows.SystemParameters.PrimaryScreenWidth + 50;
            this.MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight + 50;
            ClearFilters();
            CurrentUser = MigApp.Properties.Settings.Default.UserLogin;
            RoleCheck();
            UpdateAllTables();
        }

        #region Обновления
        // Обновление всех таблиц
        private void UpdateAllTables()
        {
            // Таблицы
            FavTable.ItemsSource = sqlcc.DataGridUpdate("Дата, Таблица, Запись", "Fav_View",$"WHERE [User] LIKE '{CurrentUser}'").DefaultView;
            if  (EmpRead == true)
                EmployeeTable.ItemsSource = sqlcc.DataGridUpdate("*", "Employees_View", $"{MigApp.Properties.Settings.Default.com0}").DefaultView;
            if (PCRead == true)
                PCTable.ItemsSource = sqlcc.DataGridUpdate("*", "PC_View", $"{MigApp.Properties.Settings.Default.com1}").DefaultView;
            if (NbRead == true)
                NotebookTable.ItemsSource = sqlcc.DataGridUpdate("*", "Notebooks_View", $"{MigApp.Properties.Settings.Default.com2}").DefaultView;
            if (TabRead == true)
                TabletsTable.ItemsSource = sqlcc.DataGridUpdate("*", "Tablet_View", $"{MigApp.Properties.Settings.Default.com3}").DefaultView;
            if (OTRead == true)
                PrintersTable.ItemsSource = sqlcc.DataGridUpdate("*", "OrgTech_View", $"{MigApp.Properties.Settings.Default.com4}").DefaultView;
            if (MonRead == true)
                MonitorsTable.ItemsSource = sqlcc.DataGridUpdate("*", "Monitors_View", $"{MigApp.Properties.Settings.Default.com5}").DefaultView;
            if (Admin == true)
            {
                // Админпанель
                UsersTable.ItemsSource = sqlcc.DataGridUpdate("*", "Users_View", $"{MigApp.Properties.Settings.Default.com6}").DefaultView;
                RolesTable.ItemsSource = sqlcc.DataGridUpdate("*", "Roles_View", $"Where ID > 0").DefaultView;
                LogsTable.ItemsSource = sqlcc.DataGridUpdate("ID, Дата, Пользователь, Действие, Таблица, Запись", "Logs_View", $"{MigApp.Properties.Settings.Default.com7}").DefaultView;
                // Архив
                EmployeesDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Employees_Deleted", $"{MigApp.Properties.Settings.Default.com8}").DefaultView;
                PCDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "PC_Deleted", $"{MigApp.Properties.Settings.Default.com9}").DefaultView;
                NotebookDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Notebooks_Deleted", $"{MigApp.Properties.Settings.Default.com10}").DefaultView;
                TabletsDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Tablet_Deleted", $"{MigApp.Properties.Settings.Default.com11}").DefaultView;
                OrgTechDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "OrgTech_Deleted", $"{MigApp.Properties.Settings.Default.com12}").DefaultView;
                MonitorsDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Monitors_Deleted", $"{MigApp.Properties.Settings.Default.com13}").DefaultView;
            }
            // Отчёты
            Report1.ItemsSource = sqlcc.DataGridUpdate("*", "Report_Computers", $"{MigApp.Properties.Settings.Default.com14}").DefaultView;
            Report2.ItemsSource = sqlcc.DataGridUpdate("*", "Report_Notebooks", $"{MigApp.Properties.Settings.Default.com15}").DefaultView;
            Report3.ItemsSource = sqlcc.DataGridUpdate("*", "Report_Tablets", $"{MigApp.Properties.Settings.Default.com16}").DefaultView;
        }
        #endregion

        // Затемнить окно
        private void BlindfallSwitch()
        {
            if (Blindfall.Visibility == Visibility.Collapsed)
                Blindfall.Visibility = Visibility.Visible;
            else
                Blindfall.Visibility = Visibility.Collapsed;
        }

        // Кнопка возврата к окну авторизации
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            LoginWindow win = new LoginWindow();
            ClearFilters();
            win.Show(); Close();
        }

        #region Контекстное меню

        #region Удаление
        #region Таблицы пользователей
        // Удаление сотрудников
        private void Delete_Employee(object sender, RoutedEventArgs e)
        {
            if (EmpRedPerm == true)
            try
            {
                if(MessageBox.Show("Вы уверены что хотите удалить записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    foreach (var items in EmployeeTable.SelectedItems)
                    {
                        DataRowView row = (DataRowView)items;
                        string id = row.Row["ID"].ToString();
                        string name = row.Row["ФИО"].ToString();
                        sqlcc.ReqNonRef($"UPDATE Employees SET Deleted = 1, DelDate = '{DateTime.Now}' WHERE ID LIKE {id}");
                        sqlcc.ReqNonRef($"DELETE FROM Favourite WHERE [Table] LIKE 'Сотрудники' AND Row LIKE '{id}'");
                        sqlcc.Loging(CurrentUser, "Удаление", "Сотрудники", name, "");
                    }
                    UpdateAllTables();
                }
            }
            catch { }
        }

        // Удаление компьютеров
        private void Delete_PC(object sender, RoutedEventArgs e)
        {
            if (PCRedPerm == true)
            try
            {
                if (MessageBox.Show("Вы уверены что хотите удалить записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    foreach (var items in PCTable.SelectedItems)
                    {
                        DataRowView row = (DataRowView)items;
                        string invnum = row.Row["Инвентарный номер"].ToString();
                        sqlcc.ReqNonRef($"UPDATE Computers SET Deleted = 1, DelDate = '{DateTime.Now}' WHERE InvNum LIKE '{invnum}'");
                        sqlcc.ReqNonRef($"DELETE FROM Favourite WHERE [Table] LIKE 'Компьютеры' AND Row LIKE '{invnum}'");
                        sqlcc.Loging(CurrentUser, "Удаление", "Компьютеры", invnum, "");
                    }
                    UpdateAllTables();
                }
            }
            catch { }
        }

        // Удаление ноутбуков
        private void Delete_Notebook(object sender, RoutedEventArgs e)
        {
            if (NbRedPerm == true)
            try
            {
                if (MessageBox.Show("Вы уверены что хотите удалить записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    foreach (var items in NotebookTable.SelectedItems)
                    {
                        DataRowView row = (DataRowView)items;
                        string invnum = row.Row["Инвентарный номер"].ToString();
                        sqlcc.ReqNonRef($"UPDATE Notebooks SET Deleted = 1, DelDate = '{DateTime.Now}' WHERE InvNum LIKE '{invnum}'");
                        sqlcc.ReqNonRef($"DELETE FROM Favourite WHERE [Table] LIKE 'Ноутбуки' AND Row LIKE '{invnum}'");
                        sqlcc.Loging(CurrentUser, "Удаление", "Ноутбуки", invnum, "");
                    }
                    UpdateAllTables();
                }
            }
            catch { }
        }

        // Удаление планшетов
        private void Delete_Tablet(object sender, RoutedEventArgs e)
        {
            if (TabRedPerm == true)
            try
            {
                if (MessageBox.Show("Вы уверены что хотите удалить записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    foreach (var items in TabletsTable.SelectedItems)
                    {
                        DataRowView row = (DataRowView)items;
                        string invnum = row.Row["Инвентарный номер"].ToString();
                        sqlcc.ReqNonRef($"UPDATE Tablets SET Deleted = 1, DelDate = '{DateTime.Now}' WHERE InvNum LIKE '{invnum}'");
                        sqlcc.ReqNonRef($"DELETE FROM Favourite WHERE [Table] LIKE 'Планшеты' AND Row LIKE '{invnum}'");
                        sqlcc.Loging(CurrentUser, "Удаление", "Планшеты", invnum, "");
                    }
                    UpdateAllTables();
                }
            }
            catch { }
        }

        // Удаление орг. техники
        private void Delete_Printer(object sender, RoutedEventArgs e)
        {
            if (OTRedPerm == true)
            try
            {
                if (MessageBox.Show("Вы уверены что хотите удалить записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    foreach (var items in PrintersTable.SelectedItems)
                    {
                        DataRowView row = (DataRowView)items;
                        string invnum = row.Row["Инвентарный номер"].ToString();
                        sqlcc.ReqNonRef($"UPDATE OrgTech SET Deleted = 1, DelDate = '{DateTime.Now}' WHERE InvNum LIKE '{invnum}'");
                        sqlcc.ReqNonRef($"DELETE FROM Favourite WHERE [Table] LIKE 'Орг.техника' AND Row LIKE '{invnum}'");
                        sqlcc.Loging(CurrentUser, "Удаление", "Оргтехника", invnum, "");
                    }
                    UpdateAllTables();
                }
            }
            catch { }
        }

        // Удаление мониторов
        private void Delete_Monitor(object sender, RoutedEventArgs e)
        {
            if (MonRedPerm == true)
                try
                {
                    if (MessageBox.Show("Вы уверены что хотите удалить записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        foreach (var items in MonitorsTable.SelectedItems)
                        {
                            DataRowView row = (DataRowView)items;
                            string invnum = row.Row["Инвентарный номер"].ToString();
                            sqlcc.ReqNonRef($"UPDATE Monitor SET Deleted = 1, DelDate = '{DateTime.Now}' WHERE InvNum LIKE '{invnum}'");
                            sqlcc.ReqNonRef($"DELETE FROM Favourite WHERE [Table] LIKE 'Мониторы' AND Row LIKE '{invnum}'");
                            sqlcc.Loging(CurrentUser, "Удаление", "Мониторы", invnum, "");
                        }
                        UpdateAllTables();
                    }
                }
                catch { }
        }
        #endregion

        #region Админпанель
        // Удаление ролей
        private void Delete_Role(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены что хотите удалить записи?\nЗаписи будут безвозвратно удалены.", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning)  == MessageBoxResult.Yes)
                {
                    foreach (var items in RolesTable.SelectedItems)
                    {
                        DataRowView row = (DataRowView)items;
                        string id = row.Row["ID"].ToString();
                        if (id != "1")
                        {
                            sqlcc.ReqNonRef($"DELETE FROM Roles WHERE ID LIKE {id}");
                            sqlcc.Loging(CurrentUser, "Стирание", "Роли", row.Row["Наименование"].ToString(), "");
                        }
                        else MessageBox.Show("Нельзя удалить администратора.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                    UpdateAllTables();
                }
            }
            catch { }
        }

        // Удаление пользователей
        private void Delete_User(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены что хотите удалить записи?\nЗаписи будут безвозвратно удалены.", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                    foreach (var items in UsersTable.SelectedItems)
                    {
                        DataRowView row = (DataRowView)items;
                        string id = row.Row["ID"].ToString();
                        if (Convert.ToInt32(sqlcc.ReqRef($"SELECT COUNT(*) FROM Users WHERE Role LIKE 'True'")) > 1)
                        {
                            sqlcc.ReqNonRef($"DELETE FROM Users WHERE ID LIKE {id}");
                            sqlcc.Loging(CurrentUser, "Стирание", "Пользователи", row.Row["Логин"].ToString(), "");
                        }
                        else MessageBox.Show("Удаление не возможно.\nВ системе должен быть как минимум 1 администратор.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                    UpdateAllTables();
                }
            }
            catch { }
        }
        #endregion

        #region Архив
        // Удаление сотрудников
        private void Delete_DeletedEmployee(object sender, RoutedEventArgs e)
        {
            try 
            {
                DataRowView item = EmployeesDeleted.Items[EmployeesDeleted.SelectedIndex] as DataRowView;
                string id = item.Row["ID"].ToString();
                if (MessageBox.Show("Запись будет безвозвозвратно удалена.\nУдалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    sqlcc.Delete_DeletedEmployee(id);
                    sqlcc.Loging(CurrentUser, "Стирание", "Сотрудники", item.Row["ФИО"].ToString(), "");
                }
                UpdateAllTables();
            }
            catch { }
        }

        // Удаление компьютеров
        private void Delete_DeletedPC(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView item = PCDeleted.Items[PCDeleted.SelectedIndex] as DataRowView;
                string invnum = item.Row["Инвентарный номер"].ToString();
                if (MessageBox.Show("Запись будет безвозвозвратно удалена.\nУдалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    sqlcc.Delete_DeletedPC(invnum);
                    sqlcc.Loging(CurrentUser, "Стирание", "Компьютеры", invnum, "");
                }
                UpdateAllTables();
            }
            catch { }
        }

        // Удаление ноутбуков
        private void Delete_DeletedNotebook(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView item = NotebookDeleted.Items[NotebookDeleted.SelectedIndex] as DataRowView;
                string invnum = item.Row["Инвентарный номер"].ToString();
                if (MessageBox.Show("Запись будет безвозвозвратно удалена.\nУдалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    sqlcc.ReqNonRef($"DELETE FROM Notebooks WHERE InvNum LIKE '{invnum}'");
                    sqlcc.Loging(CurrentUser, "Стирание", "Ноутбуки", invnum, "");
                }
                UpdateAllTables();
            }
            catch { }
        }

        // Удаление планшетов
        private void Delete_DeletedTablets(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView item = TabletsDeleted.Items[TabletsDeleted.SelectedIndex] as DataRowView;
                string invnum = item.Row["Инвентарный номер"].ToString();
                if (MessageBox.Show("Запись будет безвозвозвратно удалена.\nУдалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    sqlcc.ReqNonRef($"DELETE FROM Tablets WHERE InvNum LIKE '{invnum}'");
                    sqlcc.Loging(CurrentUser, "Стирание", "Планшеты", invnum, "");
                }
                UpdateAllTables();
            }
            catch { }
        }

        // Удаление орг.техники
        private void Delete_DeletedOrgTech(object sender, RoutedEventArgs e)
        {
            DataRowView item = OrgTechDeleted.Items[OrgTechDeleted.SelectedIndex] as DataRowView;
            string invnum = item.Row["Инвентарный номер"].ToString();
            if (MessageBox.Show("Запись будет безвозвозвратно удалена.\nУдалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                sqlcc.ReqNonRef($"DELETE FROM OrgTech WHERE InvNum LIKE '{invnum}'");
                sqlcc.Loging(CurrentUser, "Стирание", "Оргтехника", invnum, "");
            }
            UpdateAllTables();
        }

        // Удаление мониторов
        private void Delete_DeletedMonitor(object sender, RoutedEventArgs e)
        {
            DataRowView item = MonitorsDeleted.Items[MonitorsDeleted.SelectedIndex] as DataRowView;
            string invnum = item.Row["Инвентарный номер"].ToString();
            if (MessageBox.Show("Запись будет безвозвозвратно удалена.\nУдалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                sqlcc.ReqNonRef($"DELETE FROM Monitor WHERE InvNum LIKE '{invnum}'");
                sqlcc.Loging(CurrentUser, "Стирание", "Мониторы", invnum, "");
            }
            UpdateAllTables();
        }
        #endregion
        #endregion

        #region Редактирование
        // Редактировать сотрудника
        private void Redact_Employee(object sender, RoutedEventArgs e)
        {
            if (EmpRedPerm == true)
            try
            {
                DataRowView item = EmployeeTable.Items[EmployeeTable.SelectedIndex] as DataRowView;
                string id = item.Row[0].ToString();
                EmployeeCROpen(false, id, false);
            }
            catch { }
        }

        // Редактировать ПК
        private void Redact_PC(object sender, RoutedEventArgs e)
        {
            if (PCRedPerm == true)
            try
            {
                DataRowView item = PCTable.Items[PCTable.SelectedIndex] as DataRowView;
                string invnum = item.Row[0].ToString();
                PCCROpen(false, invnum, false);
            }
            catch { }
        }

        // Редактировать ноутбук
        private void Redact_Notebook(object sender, RoutedEventArgs e)
        {
            if (NbRedPerm == true)
            try
            {
                DataRowView item = NotebookTable.Items[NotebookTable.SelectedIndex] as DataRowView;
                string invnum = item.Row[0].ToString();
                NotebookCROpen(false, invnum, false);
            }
            catch { }
        }

        // Редактировать планшет
        private void Redact_Tablet(object sender, RoutedEventArgs e)
        {
            if (TabRedPerm == true)
            try
            {
                DataRowView item = TabletsTable.Items[TabletsTable.SelectedIndex] as DataRowView;
                string invnum = item.Row[0].ToString();
                TabletsCROpen(false, invnum, false);
            }
            catch { }
        }

        // Редактировать Орг.технику
        private void Redact_Printer(object sender, RoutedEventArgs e)
        {
            if (OTRedPerm == true)
            try
            {
                DataRowView item = PrintersTable.Items[PrintersTable.SelectedIndex] as DataRowView;
                string invnum = item.Row[0].ToString();
                OrgTechCROpen(false, invnum, false);
            }
            catch { }
        }

        // Редактировать монитор
        private void Redact_Monitor(object sender, RoutedEventArgs e)
        {
            if (MonRedPerm == true)
            try
            {
                DataRowView item = MonitorsTable.Items[MonitorsTable.SelectedIndex] as DataRowView;
                string invnum = item.Row[0].ToString();
                MonitorCROpen(false, invnum, false);
            }
            catch { }
        }

        // Редактировать пользователя
        private void Redact_User(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView item = UsersTable.Items[UsersTable.SelectedIndex] as DataRowView;
                string id = item.Row[0].ToString();
                UserCROpen(false, id);
            }
            catch { }
        }

        // Редактировать роль
        private void Redact_Role(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView item = RolesTable.Items[RolesTable.SelectedIndex] as DataRowView;
                string id = item.Row[0].ToString();
                RoleCROpen(false, id);
            }
            catch { }
        }

        // Редактирование из избранного
        private void Fav_Redact(object sender, RoutedEventArgs e)
        {
            
            try
            {
                DataRowView item = FavTable.Items[FavTable.SelectedIndex] as DataRowView;
                if (item.Row["Таблица"].ToString() == "Сотрудники" && EmpRedPerm == true)
                { EmployeeCROpen(false, item.Row["Запись"].ToString(), false); }
                else if (item.Row["Таблица"].ToString() == "Компьютеры" && PCRedPerm == true)
                { PCCROpen(false, item.Row["Запись"].ToString(), false); }
                else if (item.Row["Таблица"].ToString() == "Ноутбуки" && NbRedPerm == true)
                { NotebookCROpen(false, item.Row["Запись"].ToString(), false); }
                else if (item.Row["Таблица"].ToString() == "Планшеты" && TabRedPerm == true)
                { TabletsCROpen(false, item.Row["Запись"].ToString(), false); }
                else if (item.Row["Таблица"].ToString() == "Оргтехника" && OTRedPerm == true)
                { OrgTechCROpen(false, item.Row["Запись"].ToString(), false); }
                else if (item.Row["Таблица"].ToString() == "Мониторы" && MonRedPerm == true)
                { MonitorCROpen(false, item.Row["Запись"].ToString(), false); }    
            }
            catch { }
        }
        #endregion

        #region Избранное
        // Добавить сотрудника в избранное
        private void Fav_Employee(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView item = EmployeeTable.Items[EmployeeTable.SelectedIndex] as DataRowView;
                string id = item.Row["ID"].ToString();
                bool allow = true;
                foreach (DataRowView row in FavTable.Items)
                {
                    if (row.Row["Таблица"].ToString() == "Сотрудники" && row.Row["Запись"].ToString() == id)
                    {
                        allow = false;
                    }
                }
                if (allow)
                {
                    sqlcc.ReqNonRef($"INSERT INTO Favourite ([User], Date, [Table], Row) VALUES ('{CurrentUser}', '{DateTime.Now.Date}', 'Сотрудники', '{id}')");
                    FavTable.ItemsSource = sqlcc.DataGridUpdate("Дата, Таблица, Запись", "Fav_View", $"WHERE [User] LIKE '{CurrentUser}'").DefaultView;
                }
                else
                    MessageBox.Show("Запись уже находится в избранном", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            catch { }
        }

        // Добавить компьютер в избранное
        private void Fav_PC(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView item = PCTable.Items[PCTable.SelectedIndex] as DataRowView;
                string invnum = item.Row[0].ToString();
                bool allow = true;
                foreach (DataRowView row in FavTable.Items)
                {
                    if (row.Row["Таблица"].ToString() == "Компьютеры" && row.Row["Запись"].ToString() == invnum)
                    {
                        allow = false;
                    }
                }
                if (allow)
                {
                    sqlcc.ReqNonRef($"INSERT INTO Favourite ([User], Date, [Table], Row) VALUES ('{CurrentUser}', '{DateTime.Now}', 'Компьютеры', '{invnum}')");
                    FavTable.ItemsSource = sqlcc.DataGridUpdate("Дата, Таблица, Запись", "Fav_View", $"WHERE [User] LIKE '{CurrentUser}'").DefaultView;
                }
                else
                    MessageBox.Show("Запись уже находится в избранном", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            catch { }
        }

        // Добавить ноутбук в избранное
        private void Fav_Notebook(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView item = NotebookTable.Items[NotebookTable.SelectedIndex] as DataRowView;
                string invnum = item.Row[0].ToString();
                bool allow = true;
                foreach (DataRowView row in FavTable.Items)
                {
                    if (row.Row["Таблица"].ToString() == "Ноутбуки" && row.Row["Запись"].ToString() == invnum)
                    {
                        allow = false;
                    }
                }
                if (allow)
                {
                    sqlcc.ReqNonRef($"INSERT INTO Favourite ([User], Date, [Table], Row) VALUES ('{CurrentUser}', '{DateTime.Now}', 'Ноутбуки', '{invnum}')");
                    FavTable.ItemsSource = sqlcc.DataGridUpdate("Дата, Таблица, Запись", "Fav_View", $"WHERE [User] LIKE '{CurrentUser}'").DefaultView;
                }
                else
                    MessageBox.Show("Запись уже находится в избранном", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            catch { }
        }

        // Добавить планшет в избранное
        private void Fav_Tablets(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView item = TabletsTable.Items[TabletsTable.SelectedIndex] as DataRowView;
                string invnum = item.Row[0].ToString();
                bool allow = true;
                foreach (DataRowView row in FavTable.Items)
                {
                    if (row.Row["Таблица"].ToString() == "Планшеты" && row.Row["Запись"].ToString() == invnum)
                    {
                        allow = false;
                    }
                }
                if (allow)
                {
                    sqlcc.ReqNonRef($"INSERT INTO Favourite ([User], Date, [Table], Row) VALUES ('{CurrentUser}', '{DateTime.Now}', 'Планшеты', '{invnum}')");
                    FavTable.ItemsSource = sqlcc.DataGridUpdate("Дата, Таблица, Запись", "Fav_View", $"WHERE [User] LIKE '{CurrentUser}'").DefaultView;
                }
                else
                    MessageBox.Show("Запись уже находится в избранном", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            catch { }
        }

        // Добавить орг.технику в избранное
        private void Fav_OrgTech(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView item = PrintersTable.Items[PrintersTable.SelectedIndex] as DataRowView;
                string invnum = item.Row[0].ToString();
                bool allow = true;
                foreach (DataRowView row in FavTable.Items)
                {
                    if (row.Row["Таблица"].ToString() == "Оргтехника" && row.Row["Запись"].ToString() == invnum)
                    {
                        allow = false;
                    }
                }
                if (allow)
                {
                    sqlcc.ReqNonRef($"INSERT INTO Favourite ([User], Date, [Table], Row) VALUES ('{CurrentUser}', '{DateTime.Now}', 'Орг.техника', '{invnum}')");
                    FavTable.ItemsSource = sqlcc.DataGridUpdate("Дата, Таблица, Запись", "Fav_View", $"WHERE [User] LIKE '{CurrentUser}'").DefaultView;
                }
                else
                    MessageBox.Show("Запись уже находится в избранном", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            catch { }
        }

        // Добавить монитор в избранное
        private void Fav_Monitors(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView item = MonitorsTable.Items[MonitorsTable.SelectedIndex] as DataRowView;
                string invnum = item.Row[0].ToString();
                bool allow = true;
                foreach (DataRowView row in FavTable.Items)
                {
                    if (row.Row["Таблица"].ToString() == "Мониторы" && row.Row["Запись"].ToString() == invnum)
                    {
                        allow = false;
                    }
                }
                if (allow)
                {
                    sqlcc.ReqNonRef($"INSERT INTO Favourite ([User], Date, [Table], Row) VALUES ('{CurrentUser}', '{DateTime.Now}', 'Мониторы', '{invnum}')");
                    FavTable.ItemsSource = sqlcc.DataGridUpdate("Дата, Таблица, Запись", "Fav_View", $"WHERE [User] LIKE '{CurrentUser}'").DefaultView;
                }
                else
                    MessageBox.Show("Запись уже находится в избранном", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            catch { }
        }

        // Убрать из избранного
        private void Fav_Remove(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView item = FavTable.Items[FavTable.SelectedIndex] as DataRowView;
                sqlcc.ReqNonRef($"DELETE FROM Favourite WHERE [User] LIKE '{CurrentUser}' AND [Table] LIKE '{item.Row["Таблица"].ToString()}' AND Row LIKE '{item.Row["Запись"].ToString()}'");
                FavTable.ItemsSource = sqlcc.DataGridUpdate("Дата, Таблица, Запись", "Fav_View", $"WHERE [User] LIKE '{CurrentUser}'").DefaultView;
            }
            catch { }
        }
        #endregion

        #region Открыть удалённое
        // Сотрудники
        private void Open_DeletedEmployee(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView item = EmployeesDeleted.Items[EmployeesDeleted.SelectedIndex] as DataRowView;
                string id = item.Row["ID"].ToString();
                EmployeeCROpen(false, id, true);
            }
            catch { }
        }

        // Компьютеры
        private void Open_DeletedPC(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView item = PCDeleted.Items[PCDeleted.SelectedIndex] as DataRowView;
                string invnum = item.Row["Инвентарный номер"].ToString();
                PCCROpen(false, invnum, true);
            }
            catch { }
        }

        // Ноутбуки
        private void Open_DeletedNotebook(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView item = NotebookDeleted.Items[NotebookDeleted.SelectedIndex] as DataRowView;
                string invnum = item.Row["Инвентарный номер"].ToString();
                NotebookCROpen(false, invnum, true);
            }
            catch { }
        }

        // Планшеты
        private void Open_DeletedTablet(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView item = TabletsDeleted.Items[TabletsDeleted.SelectedIndex] as DataRowView;
                string invnum = item.Row["Инвентарный номер"].ToString();
                TabletsCROpen(false, invnum, true);
            }
            catch { }

        }

        // Орг.Техника
        private void Open_DeletedOrgTech(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView item = OrgTechDeleted.Items[OrgTechDeleted.SelectedIndex] as DataRowView;
                string invnum = item.Row["Инвентарный номер"].ToString();
                OrgTechCROpen(false, invnum, true);
            }
            catch { }
        }

        // Мониторы
        private void Open_DeletedMonitor(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView item = MonitorsDeleted.Items[MonitorsDeleted.SelectedIndex] as DataRowView;
                string invnum = item.Row["Инвентарный номер"].ToString();
                MonitorCROpen(false, invnum, true);
            }
            catch { }
        }

        #endregion

        #region Восстановление
        // Сотрудники
        private void Recovery_Employee(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены что хотите восстановить запись?","Внимание",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    foreach (var items in EmployeesDeleted.SelectedItems)
                    {
                        DataRowView row = (DataRowView)items;
                        string id = row.Row["ID"].ToString();
                        string name = row.Row["ФИО"].ToString();
                        sqlcc.Recovery("Employees", "ID", id);
                        sqlcc.Loging(CurrentUser, "Восстановление", "Сотрудники", name, "");
                    }
                    UpdateAllTables();
                }
            }
            catch { }
        }

        // Компьютеры
        private void Recovery_PC(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены что хотите восстановить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    foreach (var items in PCDeleted.SelectedItems)
                    {
                        DataRowView row = (DataRowView)items;
                        string invnum = row.Row["Инвентарный номер"].ToString();
                        sqlcc.Recovery("Computers", "InvNum", invnum);
                        sqlcc.Loging(CurrentUser, "Восстановление", "Компьютеры", invnum, "");
                    }
                    UpdateAllTables();
                }
            }
            catch { }
        }

        // Ноутбуки
        private void Recovery_Notebook(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены что хотите восстановить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    foreach (var items in NotebookDeleted.SelectedItems)
                    {
                        DataRowView row = (DataRowView)items;
                        string invnum = row.Row["Инвентарный номер"].ToString();
                        sqlcc.Recovery("Notebooks", "InvNum", invnum);
                        sqlcc.Loging(CurrentUser, "Восстановление", "Ноутбуки", invnum, "");
                    }
                    UpdateAllTables();
                }
            }
            catch { }
        }

        // Планшеты
        private void Recovery_Tablet(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены что хотите восстановить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    foreach (var items in TabletsDeleted.SelectedItems)
                    {
                        DataRowView row = (DataRowView)items;
                        string invnum = row.Row["Инвентарный номер"].ToString();
                        sqlcc.Recovery("Tablets", "InvNum", invnum);
                        sqlcc.Loging(CurrentUser, "Восстановление", "Ноутбуки", invnum, "");
                    }
                    UpdateAllTables();
                }
            }
            catch { }
        }

        // Орг.техника
        private void Recovery_OrgTech(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены что хотите восстановить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    foreach (var items in OrgTechDeleted.SelectedItems)
                    {
                        DataRowView row = (DataRowView)items;
                        string invnum = row.Row["Инвентарный номер"].ToString();
                        sqlcc.Recovery("OrgTech", "InvNum", invnum);
                        sqlcc.Loging(CurrentUser, "Восстановление", "Оргтехника", invnum, "");
                    }
                    UpdateAllTables();
                }
            }
            catch { }
        }

        // Мониторы
        private void Recovery_Monitor(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены что хотите восстановить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    foreach (var items in MonitorsDeleted.SelectedItems)
                    {
                        DataRowView row = (DataRowView)items;
                        string invnum = row.Row["Инвентарный номер"].ToString();
                        sqlcc.Recovery("Monitor", "InvNum", invnum);
                        sqlcc.Loging(CurrentUser, "Восстановление", "Мониторы", invnum, "");
                    }
                    UpdateAllTables();
                }
            }
            catch { }
        }
        #endregion

        #endregion

        #region Фильтры

        #region Включить фильтр

        #region Фильтр пользовательских таблиц
        // Фильтр сотрудников
        private void SEmployee(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow(0);
            BlindfallSwitch();
            win.Owner = this;
            win.ShowDialog();
            if (win.DialogResult == true)
            {
                EmployeeTable.Margin = new Thickness(0, 100, 5, 5);
                EmployeeTable.ItemsSource = sqlcc.DataGridUpdate("*", "Employees_View", $"{MigApp.Properties.Settings.Default.com0}").DefaultView;
                FilterEmpText.Text = mc.Splitter(MigApp.Properties.Settings.Default.Params0);
            }
            BlindfallSwitch();
        }

        // Фильтр ПК
        private void SPC(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow(1);
            BlindfallSwitch();
            win.Owner = this;
            win.ShowDialog();
            if (win.DialogResult == true)
            {
                PCTable.Margin = new Thickness(0, 100, 5, 5);
                PCTable.ItemsSource = sqlcc.DataGridUpdate("*", "PC_View", $"{MigApp.Properties.Settings.Default.com1}").DefaultView;
                FilterPCText.Text = mc.Splitter(MigApp.Properties.Settings.Default.Params1);
            }
            BlindfallSwitch();
        }

        // Фильтр Ноутбуков
        private void SNotebook(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow(2);
            BlindfallSwitch();
            win.Owner = this;
            win.ShowDialog();
            if (win.DialogResult == true)
            {
                NotebookTable.Margin = new Thickness(0, 100, 5, 5);
                NotebookTable.ItemsSource = sqlcc.DataGridUpdate("*", " Notebooks_View", $"{MigApp.Properties.Settings.Default.com2}").DefaultView;
                FilterNotebookText.Text = mc.Splitter(MigApp.Properties.Settings.Default.Params2);
            }
            BlindfallSwitch();
        }

        // Фильтр Планшетов
        private void STablets(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow(3);
            BlindfallSwitch();
            win.Owner = this;
            win.ShowDialog();
            if(win.DialogResult == true)
            {
                TabletsTable.Margin = new Thickness(0, 100, 5, 5);
                TabletsTable.ItemsSource = sqlcc.DataGridUpdate("*", "Tablet_View", $"{MigApp.Properties.Settings.Default.com3}").DefaultView;
                FilterTabletsText.Text = mc.Splitter(MigApp.Properties.Settings.Default.Params3);
            }
            BlindfallSwitch();
        }

        // Фильтр Орг.Техники
        private void SOrgTech(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow(4);
            BlindfallSwitch();
            win.Owner = this;
            win.ShowDialog();
            if (win.DialogResult == true)
            {
                PrintersTable.Margin = new Thickness(0, 100, 5, 5);
                PrintersTable.ItemsSource = sqlcc.DataGridUpdate("*", "OrgTech_View", $"{MigApp.Properties.Settings.Default.com4}").DefaultView;
                FilterOrgTechText.Text = mc.Splitter(MigApp.Properties.Settings.Default.Params4);
            }
            BlindfallSwitch();
        }

        // Фильтр Мониторов
        private void SMonitors(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow(5);
            BlindfallSwitch();
            win.Owner = this;
            win.ShowDialog();
            if (win.DialogResult == true)
            {
                MonitorsTable.Margin = new Thickness(0, 100, 5, 5);
                MonitorsTable.ItemsSource = sqlcc.DataGridUpdate("*", "Monitors_View", $"{MigApp.Properties.Settings.Default.com5}").DefaultView;
                FilterMonitorsText.Text = mc.Splitter(MigApp.Properties.Settings.Default.Params5);
            }
            BlindfallSwitch();
        }
        #endregion

        #region Фильтр Админпанель
        // Пользователи
        private void SUser(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow(6);
            BlindfallSwitch();
            win.Owner = this;
            win.ShowDialog();
            if (win.DialogResult == true)
            {
                UsersTable.Margin = new Thickness(0, 100, 5, 5);
                UsersTable.ItemsSource = sqlcc.DataGridUpdate("*", "Users_View", $"{MigApp.Properties.Settings.Default.com6}").DefaultView;
                FilterUsersText.Text = mc.Splitter(MigApp.Properties.Settings.Default.Params6);
            }
            BlindfallSwitch();
        }

        // Логи
        private void SLogs(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow(7);
            BlindfallSwitch();
            win.Owner = this;
            win.ShowDialog();
            if (win.DialogResult == true)
            {
                LogsTable.Margin = new Thickness(0, 100, 5, 5);
                LogsTable.ItemsSource = sqlcc.DataGridUpdate("ID, Дата, Пользователь, Действие, Таблица, Запись", "Logs_View", $"{MigApp.Properties.Settings.Default.com7}").DefaultView;
                FilterLogsText.Text = mc.Splitter(MigApp.Properties.Settings.Default.Params7);
            }
            BlindfallSwitch();
        }
        #endregion

        #region Архив

        // Сотрудники Архив
        private void SEmployee_Del(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow(8);
            BlindfallSwitch();
            win.Owner = this;
            win.ShowDialog();
            if (win.DialogResult == true)
            {
                EmployeesDeleted.Margin = new Thickness(0, 100, 5, 5);
                EmployeesDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Employees_Deleted", $"{MigApp.Properties.Settings.Default.com8}").DefaultView;
                FilterEmployeesText_Del.Text = mc.Splitter(MigApp.Properties.Settings.Default.Params8);
            }
            BlindfallSwitch();
        }

        // ПК Архив
        private void SPC_Del(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow(9);
            BlindfallSwitch();
            win.Owner = this;
            win.ShowDialog();
            if (win.DialogResult == true)
            {
                PCDeleted.Margin = new Thickness(0, 100, 5, 5);
                PCDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "PC_Deleted", $"{MigApp.Properties.Settings.Default.com9}").DefaultView;
                FilterPCText_Del.Text = mc.Splitter(MigApp.Properties.Settings.Default.Params9);
            }
            BlindfallSwitch();
        }

        // Ноутбуки Архив
        private void SNotebook_Del(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow(10);
            BlindfallSwitch();
            win.Owner = this;
            win.ShowDialog();
            if (win.DialogResult == true)
            {
                NotebookDeleted.Margin = new Thickness(0, 100, 5, 5);
                NotebookDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Notebooks_Deleted", $"{MigApp.Properties.Settings.Default.com10}").DefaultView;
                FilterNotebooksText_Del.Text = mc.Splitter(MigApp.Properties.Settings.Default.Params10);
            }
            BlindfallSwitch();
        }

        // Планшеты Архив
        private void STablets_Del(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow(11);
            BlindfallSwitch();
            win.Owner = this;
            win.ShowDialog();
            if (win.DialogResult == true)
            {
                TabletsDeleted.Margin = new Thickness(0, 100, 5, 5);
                TabletsDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Tablet_Deleted", $"{MigApp.Properties.Settings.Default.com11}").DefaultView;
                FilterTabletsText_Del.Text = mc.Splitter(MigApp.Properties.Settings.Default.Params11);
            }
            BlindfallSwitch();
        }

        // Орг.техника Архив
        private void SOrgTech_Del(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow(12);
            BlindfallSwitch();
            win.Owner = this;
            win.ShowDialog();
            if (win.DialogResult == true)
            {
                OrgTechDeleted.Margin = new Thickness(0, 100, 5, 5);
                OrgTechDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "OrgTech_Deleted", $"{MigApp.Properties.Settings.Default.com12}").DefaultView;
                FilterOrgTechText_Del.Text = mc.Splitter(MigApp.Properties.Settings.Default.Params12);
            }
            BlindfallSwitch();
        }

        // Мониторы Архив
        private void SMonitors_Del(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow(13);
            BlindfallSwitch();
            win.Owner = this;
            win.ShowDialog();
            if (win.DialogResult == true)
            {
                MonitorsDeleted.Margin = new Thickness(0, 100, 5, 5);
                MonitorsDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Monitors_Deleted", $"{MigApp.Properties.Settings.Default.com13}").DefaultView;
                FilterMonitorsText_Del.Text = mc.Splitter(MigApp.Properties.Settings.Default.Params13);
            }
            BlindfallSwitch();
        }
        #endregion

        #region Отчёты
        // ПК
        private void SReport1(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow(14);
            BlindfallSwitch();
            win.Owner = this;
            win.ShowDialog();
            if (win.DialogResult == true)
            {
                Report1.Margin = new Thickness(0, 100, 5, 5);
                Report1.ItemsSource = sqlcc.DataGridUpdate("*", "Report_Computers", $"{MigApp.Properties.Settings.Default.com14}").DefaultView;
                FilterReport1Text.Text = mc.Splitter(MigApp.Properties.Settings.Default.Params14);
            }
            BlindfallSwitch();
        }

        // Ноутбуки
        private void SReport2(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow(15);
            BlindfallSwitch();
            win.Owner = this;
            win.ShowDialog();
            if (win.DialogResult == true)
            {
                Report2.Margin = new Thickness(0, 100, 5, 5);
                Report2.ItemsSource = sqlcc.DataGridUpdate("*", "Report_Notebooks", $"{MigApp.Properties.Settings.Default.com15}").DefaultView;
                FilterReport2Text.Text = mc.Splitter(MigApp.Properties.Settings.Default.Params15);
            }
            BlindfallSwitch();
        }

        // Планшеты
        private void SReport3(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow(16);
            BlindfallSwitch();
            win.Owner = this;
            win.ShowDialog();
            if (win.DialogResult == true)
            {
                Report3.Margin = new Thickness(0, 100, 5, 5);
                Report3.ItemsSource = sqlcc.DataGridUpdate("*", "Report_Tablets", $"{MigApp.Properties.Settings.Default.com16}").DefaultView;
                FilterReport3Text.Text = mc.Splitter(MigApp.Properties.Settings.Default.Params16);
            }
            BlindfallSwitch();
        }

        #endregion

        #endregion

        #region Выключить фильтр

        #region Фильтр пользовательских таблиц
        // Очистка фильтра сотрудников
        private void FilterEmpClear(object sender, RoutedEventArgs e)
        {
            MigApp.Properties.Settings.Default.com0 = null;
            MigApp.Properties.Settings.Default.Params0 = null;
            MigApp.Properties.Settings.Default.Save();
            FilterEmpText.Text = "";
            EmployeeTable.Margin = new Thickness(0, 50, 5, 5);
            EmployeeTable.ItemsSource = sqlcc.DataGridUpdate("*", "Employees_View", "").DefaultView;
        }

        // Очистка фильтра компьютеров
        private void FilterPCClear(object sender, RoutedEventArgs e)
        {
            MigApp.Properties.Settings.Default.com1 = null;
            MigApp.Properties.Settings.Default.Params1 = null;
            MigApp.Properties.Settings.Default.Save();
            FilterPCText.Text = "";
            PCTable.Margin = new Thickness(0, 50, 5, 5);
            PCTable.ItemsSource = sqlcc.DataGridUpdate("*", "PC_View", "").DefaultView;
        }

        // Очистка фильтра ноутбуков
        private void FilterNotebookClear(object sender, RoutedEventArgs e)
        {
            MigApp.Properties.Settings.Default.com2 = null;
            MigApp.Properties.Settings.Default.Params2 = null;
            MigApp.Properties.Settings.Default.Save();
            FilterNotebookText.Text = "";
            NotebookTable.Margin = new Thickness(0, 50, 5, 5);
            NotebookTable.ItemsSource = sqlcc.DataGridUpdate("*", "Notebooks_View", "").DefaultView;
        }

        // Очистка фильтра планшетов
        private void FilterTabletsClear(object sender, RoutedEventArgs e)
        {
            MigApp.Properties.Settings.Default.com3 = null;
            MigApp.Properties.Settings.Default.Params3 = null;
            MigApp.Properties.Settings.Default.Save();
            FilterTabletsText.Text = "";
            TabletsTable.Margin = new Thickness(0, 50, 5, 5);
            TabletsTable.ItemsSource = sqlcc.DataGridUpdate("*", "Tablet_View", "").DefaultView;
        }

        // Очистка фильтра орг.техники
        private void FilterOrgTechClear(object sender, RoutedEventArgs e)
        {
            MigApp.Properties.Settings.Default.com4 = null;
            MigApp.Properties.Settings.Default.Params4 = null;
            MigApp.Properties.Settings.Default.Save();
            FilterOrgTechText.Text = "";
            PrintersTable.Margin = new Thickness(0, 50, 5, 5);
            PrintersTable.ItemsSource = sqlcc.DataGridUpdate("*", "OrgTech_View", "").DefaultView;
        }

        // Очистка фильтра мониторов
        private void FilterMonitorsClear(object sender, RoutedEventArgs e)
        {
            MigApp.Properties.Settings.Default.com5 = null;
            MigApp.Properties.Settings.Default.Params5 = null;
            MigApp.Properties.Settings.Default.Save();
            FilterMonitorsText.Text = "";
            MonitorsTable.Margin = new Thickness(0, 50, 5, 5);
            MonitorsTable.ItemsSource = sqlcc.DataGridUpdate("*", "Monitors_View", "").DefaultView;
        }

        #endregion

        #region Фильтр Админпанель
        // Пользователи
        private void FilterUsersClear(object sender, RoutedEventArgs e)
        {
            MigApp.Properties.Settings.Default.com6 = null;
            MigApp.Properties.Settings.Default.Params6 = null;
            MigApp.Properties.Settings.Default.Save();
            FilterUsersText.Text = "";
            UsersTable.Margin = new Thickness(0, 50, 5, 5);
            UsersTable.ItemsSource = sqlcc.DataGridUpdate("*", "Users_View", "").DefaultView;
        }

        // Логи
        private void FilterLogsClear(object sender, RoutedEventArgs e)
        {
            MigApp.Properties.Settings.Default.com7 = null;
            MigApp.Properties.Settings.Default.Params7 = null;
            MigApp.Properties.Settings.Default.Save();
            FilterLogsText.Text = "";
            LogsTable.Margin = new Thickness(0, 50, 5, 5);
            LogsTable.ItemsSource = sqlcc.DataGridUpdate("*", "Logs_View", "").DefaultView;
        }
        #endregion

        #region Архив
        // Сотрудники Архив
        private void FilterEmployeesClear_Del(object sender, RoutedEventArgs e)
        {
            MigApp.Properties.Settings.Default.com8 = null;
            MigApp.Properties.Settings.Default.Params8 = null;
            MigApp.Properties.Settings.Default.Save();
            FilterEmployeesText_Del.Text = "";
            EmployeesDeleted.Margin = new Thickness(0, 50, 5, 5);
            EmployeesDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Employees_Deleted", "").DefaultView;
        }

        // Компьютеры Архив
        private void FilterPCClear_Del(object sender, RoutedEventArgs e)
        {
            MigApp.Properties.Settings.Default.com9 = null;
            MigApp.Properties.Settings.Default.Params9 = null;
            MigApp.Properties.Settings.Default.Save();
            FilterPCText_Del.Text = "";
            PCDeleted.Margin = new Thickness(0, 50, 5, 5);
            PCDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "PC_Deleted", "").DefaultView;
        }

        // Ноутбуки Архив
        private void FilterNotebooksClear_Del(object sender, RoutedEventArgs e)
        {
            MigApp.Properties.Settings.Default.com10 = null;
            MigApp.Properties.Settings.Default.Params10 = null;
            MigApp.Properties.Settings.Default.Save();
            FilterNotebooksText_Del.Text = "";
            NotebookDeleted.Margin = new Thickness(0, 50, 5, 5);
            NotebookDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Notebooks_Deleted", "").DefaultView;
        }

        // Планшеты Архив
        private void FilterTabletsClear_Del(object sender, RoutedEventArgs e)
        {
            MigApp.Properties.Settings.Default.com11 = null;
            MigApp.Properties.Settings.Default.Params11 = null;
            MigApp.Properties.Settings.Default.Save();
            FilterTabletsText_Del.Text = "";
            TabletsDeleted.Margin = new Thickness(0, 50, 5, 5);
            TabletsDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Tablets_Deleted", "").DefaultView;
        }

        // Орг.техника Архив
        private void FilterOrgTechClear_Del(object sender, RoutedEventArgs e)
        {
            MigApp.Properties.Settings.Default.com12 = null;
            MigApp.Properties.Settings.Default.Params12 = null;
            MigApp.Properties.Settings.Default.Save();
            FilterOrgTechText_Del.Text = "";
            OrgTechDeleted.Margin = new Thickness(0, 50, 5, 5);
            OrgTechDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "OrgTech_Deleted", "").DefaultView;
        }

        // Мониторы Архив
        private void FilterMonitorsClear_Del(object sender, RoutedEventArgs e)
        {
            MigApp.Properties.Settings.Default.com13 = null;
            MigApp.Properties.Settings.Default.Params13 = null;
            MigApp.Properties.Settings.Default.Save();
            FilterMonitorsText_Del.Text = "";
            MonitorsDeleted.Margin = new Thickness(0, 50, 5, 5);
            MonitorsDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Monitors_Deleted", "").DefaultView;
        }
        #endregion

        #region Отчёты

        // ПК
        private void FilterReport1Clear(object sender, RoutedEventArgs e)
        {
            MigApp.Properties.Settings.Default.Params14 = null;
            MigApp.Properties.Settings.Default.com14 = null;
            MigApp.Properties.Settings.Default.Save();
            FilterReport1Text.Text = "";
            Report1.Margin = new Thickness(0, 50, 5, 5);
            Report1.ItemsSource = sqlcc.DataGridUpdate("*", "Report_Computers", "").DefaultView;
        }

        // Ноутбуки
        private void FilterReport2Clear(object sender, RoutedEventArgs e)
        {
            MigApp.Properties.Settings.Default.Params15 = null;
            MigApp.Properties.Settings.Default.com15 = null;
            MigApp.Properties.Settings.Default.Save();
            FilterReport2Text.Text = "";
            Report2.Margin = new Thickness(0, 50, 5, 5);
            Report2.ItemsSource = sqlcc.DataGridUpdate("*", "Report_Notebooks", "").DefaultView;
        }

        // Планшеты
        private void FilterReport3Clear(object sender, RoutedEventArgs e)
        {
            MigApp.Properties.Settings.Default.Params16 = null;
            MigApp.Properties.Settings.Default.com16 = null;
            MigApp.Properties.Settings.Default.Save();
            FilterReport3Text.Text = "";
            Report3.Margin = new Thickness(0, 50, 5, 5);
            Report3.ItemsSource = sqlcc.DataGridUpdate("*", "Report_Tablets", "").DefaultView;
        }

        #endregion

        // Очистка фильтров при запуске
        private void ClearFilters()
        {
            MigApp.Properties.Settings.Default.com0 = null;
            MigApp.Properties.Settings.Default.com1 = null;
            MigApp.Properties.Settings.Default.com2 = null;
            MigApp.Properties.Settings.Default.com3 = null;
            MigApp.Properties.Settings.Default.com4 = null;
            MigApp.Properties.Settings.Default.com5 = null;
            MigApp.Properties.Settings.Default.com6 = null;
            MigApp.Properties.Settings.Default.com7 = null;
            MigApp.Properties.Settings.Default.com8 = null;
            MigApp.Properties.Settings.Default.com9 = null;
            MigApp.Properties.Settings.Default.com10 = null;
            MigApp.Properties.Settings.Default.com11 = null;
            MigApp.Properties.Settings.Default.com12 = null;
            MigApp.Properties.Settings.Default.com13 = null;
            MigApp.Properties.Settings.Default.com14 = null;
            MigApp.Properties.Settings.Default.com15 = null;
            MigApp.Properties.Settings.Default.com16 = null;
            MigApp.Properties.Settings.Default.Params0 = null;
            MigApp.Properties.Settings.Default.Params1 = null;
            MigApp.Properties.Settings.Default.Params2 = null;
            MigApp.Properties.Settings.Default.Params3 = null;
            MigApp.Properties.Settings.Default.Params4 = null;
            MigApp.Properties.Settings.Default.Params5 = null;
            MigApp.Properties.Settings.Default.Params6 = null;
            MigApp.Properties.Settings.Default.Params7 = null;
            MigApp.Properties.Settings.Default.Params8 = null;
            MigApp.Properties.Settings.Default.Params9 = null;
            MigApp.Properties.Settings.Default.Params10 = null;
            MigApp.Properties.Settings.Default.Params11 = null;
            MigApp.Properties.Settings.Default.Params12 = null;
            MigApp.Properties.Settings.Default.Params13 = null;
            MigApp.Properties.Settings.Default.Params14 = null;
            MigApp.Properties.Settings.Default.Params15 = null;
            MigApp.Properties.Settings.Default.Params16 = null;
            MigApp.Properties.Settings.Default.Save();
        }

        #endregion
        
        #endregion

        #region Кнопки создания
        // Cоздать сотрудника
        private void EmployeeCreateClick(object sender, RoutedEventArgs e)
        {
            if (EmpRedPerm)
            EmployeeCROpen(true, null, false);
        }

        // Создать компьютер
        private void PCCreateClick(object sender, RoutedEventArgs e)
        {
            if (PCRedPerm)
            PCCROpen(true, null, false);
        }

        // Создать ноутбук
        private void NotebookCreateClick(object sender, RoutedEventArgs e)
        {
            if (NbRedPerm)
            NotebookCROpen(true, null, false);
        }

        // Создать планшет
        private void TabletsCreateClick(object sender, RoutedEventArgs e)
        {
            if (TabRedPerm)
            TabletsCROpen(true, null, false);
        }

        // Создать ррг. технику
        private void OrgTechCreateClick(object sender, RoutedEventArgs e)
        {
            if (OTRedPerm)
            OrgTechCROpen(true, null, false);
        }

        // Создать монитор
        private void MonitorCreateClick(object sender, RoutedEventArgs e)
        {
            if (MonRedPerm)
            MonitorCROpen(true, null, false);
        }

        // Создать пользователя
        private void UserCreateClick(object sender, RoutedEventArgs e)
        {
            UserCROpen(true, null);
        }

        // Создать роль
        private void CreateRoleClick(object sender, RoutedEventArgs e)
        {
            RoleCROpen(true, null);
        }
        #endregion

        #region Открытие CR окон
        // Открыть окно сотрудников
        private void EmployeeCROpen(bool mode, string id, bool deleted)
        {
            EmployeesWindow win = new EmployeesWindow(mode, id, deleted);
            if (!deleted)
            {
                BlindfallSwitch();
                win.ShowDialog();
                if (win.DialogResult == true) UpdateAllTables();
                BlindfallSwitch();
            }
            else
            {
                BlindfallSwitch();
                win.DoneButton.Visibility = Visibility.Collapsed;
                win.RecoveryButton.Visibility = Visibility.Visible;
                win.ShowDialog();
                if (win.DialogResult == true) UpdateAllTables();
                BlindfallSwitch();
            }
        }
        
        // Открыть окно ПК
        private void PCCROpen(bool mode, string invnum, bool deleted)
        {
            PCWindow win = new PCWindow(mode, invnum, deleted);
            if (!deleted)
            {
                BlindfallSwitch();
                win.ShowDialog();
                if (win.DialogResult == true) UpdateAllTables();
                BlindfallSwitch();
            }
            else
            {
                BlindfallSwitch();
                win.DoneButton.Visibility = Visibility.Collapsed;
                win.RecoveryButton.Visibility = Visibility.Visible;
                win.ShowDialog();
                if (win.DialogResult == true) UpdateAllTables();
                BlindfallSwitch();
            }
        }

        // Открыть окно ноутбуков
        private void NotebookCROpen(bool mode, string invnum, bool deleted)
        {
            NotebookWindow win = new NotebookWindow(mode, invnum, deleted);
            if (!deleted)
            {
                BlindfallSwitch();
                win.ShowDialog();
                if (win.DialogResult == true) UpdateAllTables();
                BlindfallSwitch();
            }
            else
            {
                BlindfallSwitch();
                win.DoneButton.Visibility = Visibility.Collapsed;
                win.RecoveryButton.Visibility = Visibility.Visible;
                win.ShowDialog();
                if (win.DialogResult == true) UpdateAllTables();
                BlindfallSwitch();

            }
        }

         // Открыть окно планшеты
        private void TabletsCROpen(bool mode, string invnum, bool deleted)
        {
            TabletsWindow win = new TabletsWindow(mode, invnum, deleted);
            if (!deleted)
            {
                BlindfallSwitch();
                win.ShowDialog();
                if (win.DialogResult == true) UpdateAllTables();
                BlindfallSwitch();
            }
            else
            {
                BlindfallSwitch();
                win.DoneButton.Visibility = Visibility.Collapsed;
                win.RecoveryButton.Visibility = Visibility.Visible;
                win.ShowDialog();
                if (win.DialogResult == true) UpdateAllTables();
                BlindfallSwitch();
            }
        }

        // Открыть окно орг. техники
        private void OrgTechCROpen(bool mode, string invnum, bool deleted)
        {
            OrgTechWindow win = new OrgTechWindow(mode, invnum, deleted);
            if (!deleted)
            {
                BlindfallSwitch();
                win.ShowDialog();
                if (win.DialogResult == true) UpdateAllTables();
                BlindfallSwitch();
            }
            else
            {
                BlindfallSwitch();
                win.DoneButton.Visibility = Visibility.Collapsed;
                win.RecoveryButton.Visibility = Visibility.Visible;
                win.ShowDialog();
                if (win.DialogResult == true) UpdateAllTables();
                BlindfallSwitch();
            }
        }

        // Открыть окно мониторов
        private void MonitorCROpen(bool mode, string invnum, bool deleted)
        {
            MonitorWindow win = new MonitorWindow(mode, invnum, deleted);
            if (!deleted)
            {
                BlindfallSwitch();
                win.ShowDialog();
                if (win.DialogResult == true) UpdateAllTables();
                BlindfallSwitch();
            }
            else
            {
                BlindfallSwitch();
                win.DoneButton.Visibility = Visibility.Collapsed;
                win.RecoveryButton.Visibility = Visibility.Visible;
                win.ShowDialog();
                if (win.DialogResult == true) UpdateAllTables();
                BlindfallSwitch();
            }
        }

        // Открыть окно пользователей
        private void UserCROpen(bool mode, string id)
        {
            UsersWindow win = new UsersWindow(mode, id);
            BlindfallSwitch();
            win.ShowDialog();
            if (win.DialogResult == true) UpdateAllTables();
            BlindfallSwitch();
        }

        // Открыть окно ролей
        private void RoleCROpen(bool mode, string id)
        {
            RoleWindow win = new RoleWindow(mode, id);
            BlindfallSwitch();
            win.ShowDialog();
            if (win.DialogResult == true) UpdateAllTables();
            BlindfallSwitch();
        }












        #endregion

        #region Запрет сотритровки даты
        // Логи
        private void LogsSorting(object sender, DataGridSortingEventArgs e)
        {
            DataGridColumn column = e.Column;
            if (column.Header.ToString() == "Дата")
                e.Handled = true;
        }








        #endregion

        #region Роли

        private void RoleCheck()
        {
            DataTable table = new DataTable();
            string roleID = MigApp.Properties.Settings.Default.UserRole;
            table = sqlcc.DataGridUpdate("*", "Roles", $"WHERE ID LIKE '{roleID}'");
            DataRow row = table.Rows[0];
            if (row["EmpVis"].ToString() == "True")
            {
                EmployeesGroup.Visibility = Visibility.Visible;
                EmpRead = true;
            }
            if (row["EmpRed"].ToString() == "True")
                EmpRedPerm = true;

            if (row["PCVis"].ToString() == "True")
            {
                TechGroup.Visibility = Visibility.Visible;
                ComputersGroup.Visibility = Visibility.Visible;
                PCRead = true;
            }
            if (row["PCRed"].ToString() == "True")
                PCRedPerm = true;

            if (row["NoteVis"].ToString() == "True")
            {
                TechGroup.Visibility = Visibility.Visible;
                NotebookGroup.Visibility = Visibility.Visible;
                NbRead = true;
            }
            if (row["NoteRed"].ToString() == "True")
                NbRedPerm = true;

            if (row["TabVis"].ToString() == "True")
            {
                TechGroup.Visibility = Visibility.Visible;
                TabletGroup.Visibility = Visibility.Visible;
                TabRead = true;
            }
            if (row["TabRed"].ToString() == "True")
                TabRedPerm = true;

            if (row["OTVis"].ToString() == "True")
            {
                TechGroup.Visibility = Visibility.Visible;
                OTGroup.Visibility = Visibility.Visible;
                OTRead = true;
            }
            if (row["OTRed"].ToString() == "True")
                OTRedPerm = true;

            if (row["MonVis"].ToString() == "True")
            {
                TechGroup.Visibility = Visibility.Visible;
                MonitorGroup.Visibility = Visibility.Visible;
                MonRead = true;
            }
            if (row["MonRed"].ToString() == "True")
                MonRedPerm = true;

            if (row["AdminMode"].ToString() == "True")
            {
                EmployeesGroup.Visibility = Visibility.Visible;
                TechGroup.Visibility = Visibility.Visible;
                ComputersGroup.Visibility = Visibility.Visible;
                NotebookGroup.Visibility = Visibility.Visible;
                TabletGroup.Visibility = Visibility.Visible;
                OTGroup.Visibility = Visibility.Visible;
                MonitorGroup.Visibility = Visibility.Visible;
                AdminGroup.Visibility = Visibility.Visible;
                DeletedGroup.Visibility = Visibility.Visible;
                EmpRedPerm = true;
                PCRedPerm = true;
                NbRedPerm = true;
                TabRedPerm = true;
                OTRedPerm = true;
                MonRedPerm = true;
                Admin = true;
            }
        }



        #endregion

        #region Exel Экспорт

        // Экспорт отчёта ПК
        private void ExportReport1(object sender, RoutedEventArgs e)
        {
            mc.ExcelExport(Report1);
        }

        // Экспорт отчёта Ноутбуков
        private void ExportReport2(object sender, RoutedEventArgs e)
        {
            mc.ExcelExport(Report2);
        }

        // Экспорт отчёта Планшетов
        private void ExportReport3(object sender, RoutedEventArgs e)
        {
            mc.ExcelExport(Report3);
        }

        #endregion

        private void Manual_Open(object sender, RoutedEventArgs e)
        {
            Process.Start(@"https://vanilla76e2.github.io/MigApp_Manual/");
        }
    }
}
