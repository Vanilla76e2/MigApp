using MigApp.Classes;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace MigApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SQLConnectionClass sqlcc = SQLConnectionClass.getinstance();
        MiscClass mc = new MiscClass();
        MainWindowHandler mwh = new MainWindowHandler();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = mwh;
            //RoleCheck();
            //UpdateAllTables();
            //UserLoginText.Text = CurrentUser;
        }


        #region OLD
        //#region Обновления
        //// Обновление всех таблиц
        //private void UpdateAllTables()
        //{
        //    // Таблицы
        //    FavTable.ItemsSource = sqlcc.DataGridUpdate("Дата, Таблица, Запись, Подробности", "Fav_View",$"WHERE [User] LIKE '{CurrentUser}'").DefaultView;
        //    if  (EmpRead || Admin)
        //        EmployeeTable.ItemsSource = sqlcc.DataGridUpdate("*", "Employees_View", $"{MigApp.Properties.Settings.Default.comEmp}").DefaultView;
        //    if (GrRead || Admin)
        //        GroupsTable.ItemsSource = sqlcc.DataGridUpdate("Name as 'Наименование'", "Group_View", "").DefaultView;
        //    if (PCRead || Admin)
        //        PCTable.ItemsSource = sqlcc.DataGridUpdate("*", "PC_View", $"{MigApp.Properties.Settings.Default.comPC}").DefaultView;
        //    if (NbRead || Admin)
        //        NotebookTable.ItemsSource = sqlcc.DataGridUpdate("*", "Notebooks_View", $"{MigApp.Properties.Settings.Default.comNB}").DefaultView;
        //    if (TabRead || Admin)
        //        TabletsTable.ItemsSource = sqlcc.DataGridUpdate("*", "Tablet_View", $"{MigApp.Properties.Settings.Default.comTab}").DefaultView;
        //    if (OTRead || Admin)
        //        PrintersTable.ItemsSource = sqlcc.DataGridUpdate("*", "OrgTech_View", $"{MigApp.Properties.Settings.Default.comOT}").DefaultView;
        //    if (MonRead || Admin)
        //        MonitorsTable.ItemsSource = sqlcc.DataGridUpdate("*", "Monitors_View", $"{MigApp.Properties.Settings.Default.comMon}").DefaultView;
        //    if (RoutRead || Admin)
        //        RoutersTable.ItemsSource = sqlcc.DataGridUpdate("*", "Routers_View", $"{MigApp.Properties.Settings.Default.comRout}").DefaultView;
        //    if (SwitchRead || Admin)
        //        SwitchesTable.ItemsSource = sqlcc.DataGridUpdate("*", "Switches_View", $"{MigApp.Properties.Settings.Default.comSwitch}").DefaultView;
        //    if (FurnRead || Admin)
        //    {
        //        FurnTypeTable.ItemsSource = sqlcc.DataGridUpdate("*", "FurnitureType_View","").DefaultView;
        //        FurnTable.ItemsSource = sqlcc.DataGridUpdate("*","Furniture_View",$"").DefaultView;
        //    }    
        //    if (Admin)
        //    {
        //        // Админпанель
        //        UsersTable.ItemsSource = sqlcc.DataGridUpdate("*", "Users_View", $"{MigApp.Properties.Settings.Default.comUsers}").DefaultView;
        //        RolesTable.ItemsSource = sqlcc.DataGridUpdate("*", "Roles_View", $"Where ID > 0").DefaultView;
        //        LogsTable.ItemsSource = sqlcc.DataGridUpdate("*", "Logs_View", $"{MigApp.Properties.Settings.Default.comLogs} ORDER BY ID DESC").DefaultView;
        //        // Архив
        //        EmployeesDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Employees_Deleted", $"{MigApp.Properties.Settings.Default.comEmpDel}").DefaultView;
        //        PCDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "PC_Deleted", $"{MigApp.Properties.Settings.Default.comPCDel}").DefaultView;
        //        NotebookDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Notebooks_Deleted", $"{MigApp.Properties.Settings.Default.comNBDel}").DefaultView;
        //        TabletsDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Tablet_Deleted", $"{MigApp.Properties.Settings.Default.comTabDel}").DefaultView;
        //        OrgTechDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "OrgTech_Deleted", $"{MigApp.Properties.Settings.Default.comOTDel}").DefaultView;
        //        MonitorsDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Monitors_Deleted", $"{MigApp.Properties.Settings.Default.comMonDel}").DefaultView;
        //        RoutersDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Routers_Deleted", $"{MigApp.Properties.Settings.Default.comRoutDel}").DefaultView;
        //        SwitchesDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Switches_Deleted", $"{MigApp.Properties.Settings.Default.comSwitchDel}").DefaultView;
        //        FurnDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Furniture_Deleted", $"{MigApp.Properties.Settings.Default.comFurnDel}").DefaultView;
        //    }
        //    // Отчёты
        //    //ReportPC.ItemsSource = sqlcc.DataGridUpdate("*", "Report_Computers", $"{MigApp.Properties.Settings.Default.comPCRep}").DefaultView;
        //    if (MigApp.Properties.Settings.Default.ParamsPCRep != null)
        //        ReportPC.ItemsSource = sqlcc.Report_PC_Filtered().DefaultView;
        //    else ReportPC.ItemsSource = sqlcc.Report_PC().DefaultView;
        //    ReportNB.ItemsSource = sqlcc.DataGridUpdate("*", "Report_Notebooks", $"{MigApp.Properties.Settings.Default.comNBRep}").DefaultView;
        //    ReportTab.ItemsSource = sqlcc.DataGridUpdate("*", "Report_Tablets", $"{MigApp.Properties.Settings.Default.comTabRep}").DefaultView;
        //    ReportIP.ItemsSource = sqlcc.Report_IP().DefaultView;
        //}
        //#endregion

        //#region Контекстное меню

        //#region Удаление
        //#region Таблицы пользователей
        //// Удаление сотрудников
        //private void Delete_Employee(object sender, RoutedEventArgs e)
        //{
        //    if (EmpRedPerm || Admin)
        //    try
        //    {
        //        if(MessageBox.Show("Вы уверены что хотите удалить записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in EmployeeTable.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string id = row.Row["ID"].ToString();
        //                string name = row.Row["ФИО"].ToString();
        //                sqlcc.ReqNonRef($"UPDATE Employees SET Deleted = 1, DelDate = '{DateTime.Now}' WHERE ID LIKE {id}");
        //                sqlcc.ReqNonRef($"DELETE FROM Favourite WHERE [Table] LIKE 'Сотрудники' AND Row LIKE '{id}'");
        //                sqlcc.Loging(CurrentUser, "Удаление", "Сотрудники", name, "");
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //// Удаление групп
        //private void Delete_Group(object sender, RoutedEventArgs e)
        //{
        //    if (GrRedPerm || Admin)
        //    try
        //    {
        //        if (MessageBox.Show("Вы уверены что хотите удалить записи?\nЗаписи будут безвозвратно удалены.", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in GroupsTable.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string name = row.Row["Наименование"].ToString();
        //                sqlcc.ReqNonRef($"DELETE FROM [Group] WHERE Name LIKE '{name}'");
        //                sqlcc.Loging(CurrentUser, "Стирание", "Отделы", name, "");
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }

        //}

        //// Удаление компьютеров
        //private void Delete_PC(object sender, RoutedEventArgs e)
        //{
        //    if (PCRedPerm || Admin)
        //    try
        //    {
        //        if (MessageBox.Show("Вы уверены что хотите удалить записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in PCTable.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string invnum = row.Row["Инвентарный номер"].ToString();
        //                sqlcc.ReqNonRef($"UPDATE Computers SET Deleted = 1, DelDate = '{DateTime.Now}' WHERE InvNum LIKE '{invnum}'");
        //                sqlcc.ReqNonRef($"DELETE FROM Favourite WHERE [Table] LIKE 'Компьютеры' AND Row LIKE '{invnum}'");
        //                sqlcc.Loging(CurrentUser, "Удаление", "Компьютеры", invnum, "");
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //// Удаление ноутбуков
        //private void Delete_Notebook(object sender, RoutedEventArgs e)
        //{
        //    if (NbRedPerm || Admin)
        //    try
        //    {
        //        if (MessageBox.Show("Вы уверены что хотите удалить записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in NotebookTable.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string invnum = row.Row["Инвентарный номер"].ToString();
        //                sqlcc.ReqNonRef($"UPDATE Notebooks SET Deleted = 1, DelDate = '{DateTime.Now}' WHERE InvNum LIKE '{invnum}'");
        //                sqlcc.ReqNonRef($"DELETE FROM Favourite WHERE [Table] LIKE 'Ноутбуки' AND Row LIKE '{invnum}'");
        //                sqlcc.Loging(CurrentUser, "Удаление", "Ноутбуки", invnum, "");
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //// Удаление планшетов
        //private void Delete_Tablet(object sender, RoutedEventArgs e)
        //{
        //    if (TabRedPerm || Admin)
        //    try
        //    {
        //        if (MessageBox.Show("Вы уверены что хотите удалить записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in TabletsTable.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string invnum = row.Row["Инвентарный номер"].ToString();
        //                sqlcc.ReqNonRef($"UPDATE Tablets SET Deleted = 1, DelDate = '{DateTime.Now}' WHERE InvNum LIKE '{invnum}'");
        //                sqlcc.ReqNonRef($"DELETE FROM Favourite WHERE [Table] LIKE 'Планшеты' AND Row LIKE '{invnum}'");
        //                sqlcc.Loging(CurrentUser, "Удаление", "Планшеты", invnum, "");
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //// Удаление орг. техники
        //private void Delete_Printer(object sender, RoutedEventArgs e)
        //{
        //    if (OTRedPerm || Admin)
        //    try
        //    {
        //        if (MessageBox.Show("Вы уверены что хотите удалить записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in PrintersTable.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string invnum = row.Row["Инвентарный номер"].ToString();
        //                sqlcc.ReqNonRef($"UPDATE OrgTech SET Deleted = 1, DelDate = '{DateTime.Now}' WHERE InvNum LIKE '{invnum}'");
        //                sqlcc.ReqNonRef($"DELETE FROM Favourite WHERE [Table] LIKE 'Орг.техника' AND Row LIKE '{invnum}'");
        //                sqlcc.Loging(CurrentUser, "Удаление", "Оргтехника", invnum, "");
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //// Удаление мониторов
        //private void Delete_Monitor(object sender, RoutedEventArgs e)
        //{
        //    if (MonRedPerm || Admin)
        //        try
        //        {
        //            if (MessageBox.Show("Вы уверены что хотите удалить записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //            {
        //                foreach (var items in MonitorsTable.SelectedItems)
        //                {
        //                    DataRowView row = (DataRowView)items;
        //                    string invnum = row.Row["Инвентарный номер"].ToString();
        //                    sqlcc.ReqNonRef($"UPDATE Monitor SET Deleted = 1, DelDate = '{DateTime.Now}' WHERE InvNum LIKE '{invnum}'");
        //                    sqlcc.ReqNonRef($"DELETE FROM Favourite WHERE [Table] LIKE 'Мониторы' AND Row LIKE '{invnum}'");
        //                    sqlcc.Loging(CurrentUser, "Удаление", "Мониторы", invnum, "");
        //                }
        //                UpdateAllTables();
        //            }
        //        }
        //        catch { }
        //}

        //// Удаление роутеров
        //private void Delete_Routers(object sender, RoutedEventArgs e)
        //{
        //    if (RoutRedPerm || Admin)
        //        try
        //        {
        //            if (MessageBox.Show("Вы уверены что хотите удалить записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //            {
        //                foreach (var items in RoutersTable.SelectedItems)
        //                {
        //                    DataRowView row = (DataRowView)items;
        //                    string invnum = row.Row["Инвентарный номер"].ToString();
        //                    sqlcc.ReqNonRef($"UPDATE Routers SET Deleted = 1, Deldate = '{DateTime.Now}' WHERE InvNum LIKE '{invnum}'");
        //                    sqlcc.ReqNonRef($"DELETE FROM Favourite WHERE [Table] LIKE 'Роутеры' AND Row LIKE '{invnum}'");
        //                    sqlcc.Loging(CurrentUser, "Удаление", "Роутеры", invnum, "");
        //                }
        //                UpdateAllTables();
        //            }
        //        }
        //        catch { }
        //}

        //// Удаление свитчей
        //private void Delete_Switches(object sender, RoutedEventArgs e)
        //{
        //    if (SwitchRedPerm || Admin)
        //        try
        //        {
        //            if (MessageBox.Show("Вы уверены что хотите удалить записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //            {
        //                foreach (var items in SwitchesTable.SelectedItems)
        //                {
        //                    DataRowView row = (DataRowView)items;
        //                    string invnum = row.Row["Инвентарный номер"].ToString();
        //                    sqlcc.ReqNonRef($"UPDATE Switches SET Deleted = 1, Deldate = '{DateTime.Now}' WHERE InvNum LIKE '{invnum}'");
        //                    sqlcc.ReqNonRef($"DELETE FROM Favourite WHERE [Table] LIKE 'Свитчи' AND Row LIKE '{invnum}'");
        //                    sqlcc.Loging(CurrentUser, "Удаление", "Свитчи", invnum, "");
        //                }
        //                UpdateAllTables();
        //            }
        //        }
        //        catch { }
        //}

        //// Удаление типов мебели
        //private void Delete_FurnType(object sender, RoutedEventArgs e)
        //{
        //    if (FurnRedPerm || Admin)
        //        try
        //        {
        //            if (MessageBox.Show("Вы уверены что хотите удалить записи?\nЗаписи будут безвозвратно удалены.", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //            {
        //                foreach (var items in FurnTypeTable.SelectedItems)
        //                {
        //                    DataRowView row = (DataRowView)items;
        //                    string id = row.Row["ID"].ToString();
        //                    sqlcc.ReqNonRef($"Delete From FurnitureType Where ID Like '{id}'");
        //                }
        //                UpdateAllTables();
        //            }
        //        }
        //        catch { }
        //}

        //// Удаление мебели
        //private void Delete_Furniture(object sender, RoutedEventArgs e)
        //{
        //    if (FurnRedPerm || Admin)
        //        try
        //        {
        //            if (MessageBox.Show("Вы уверены что хотите удалить записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //            {
        //                foreach (var items in FurnTable.SelectedItems)
        //                {
        //                    DataRowView row = (DataRowView)items;
        //                    string invnum = row.Row["Инвентарный номер"].ToString();
        //                    sqlcc.ReqNonRef($"UPDATE Furniture SET Deleted = 1, Deldate = '{DateTime.Now}' WHERE InvNum LIKE '{invnum}'");
        //                    sqlcc.ReqNonRef($"DELETE FROM Favourite WHERE [Table] LIKE 'Мебель' AND Row LIKE '{invnum}'");
        //                    sqlcc.Loging(CurrentUser, "Удаление", "Мебель", invnum, row.Row["Наименование"].ToString());
        //                }
        //                UpdateAllTables();
        //            }
        //        }
        //        catch { }
        //}

        //#endregion

        //#region Админпанель
        //// Удаление ролей
        //private void Delete_Role(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Вы уверены что хотите удалить записи?\nЗаписи будут безвозвратно удалены.", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning)  == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in RolesTable.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string id = row.Row["ID"].ToString();
        //                if (id != "1")
        //                {
        //                    sqlcc.ReqNonRef($"DELETE FROM Roles WHERE ID LIKE {id}");
        //                    sqlcc.Loging(CurrentUser, "Стирание", "Роли", row.Row["Наименование"].ToString(), "");
        //                }
        //                else MessageBox.Show("Нельзя удалить администратора.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //// Удаление пользователей
        //private void Delete_User(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Вы уверены что хотите удалить записи?\nЗаписи будут безвозвратно удалены.", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
        //            {
        //            foreach (var items in UsersTable.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string id = row.Row["ID"].ToString();
        //                if (Convert.ToInt32(sqlcc.ReqRef($"SELECT COUNT(*) FROM Users WHERE Role LIKE '1'")) > 1)
        //                {
        //                    sqlcc.ReqNonRef($"DELETE FROM Users WHERE ID LIKE {id}");
        //                    sqlcc.Loging(CurrentUser, "Стирание", "Пользователи", row.Row["Логин"].ToString(), "");
        //                }
        //                else MessageBox.Show("Удаление не возможно.\nВ системе должен быть как минимум 1 администратор.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}
        //#endregion

        //#region Архив
        //// Удаление сотрудников
        //private void Delete_DeletedEmployee(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Запись будет безвозвозвратно удалена.\nУдалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in EmployeesDeleted.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string id = row.Row["ID"].ToString();
        //                string name = row.Row["ФИО"].ToString();
        //                sqlcc.Delete_DeletedEmployee(id);
        //                sqlcc.Loging(CurrentUser, "Стирание", "Сотрудники", name, "");
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //// Удаление компьютеров
        //private void Delete_DeletedPC(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Запись будет безвозвозвратно удалена.\nУдалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in PCDeleted.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string invnum = row.Row["Инвентарный номер"].ToString();
        //                string spec = row.Row["Модель"].ToString();
        //                sqlcc.Delete_DeletedPC(invnum);
        //                sqlcc.Loging(CurrentUser, "Стирание", "Компьютеры", invnum, spec);
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //// Удаление ноутбуков
        //private void Delete_DeletedNotebook(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Запись будет безвозвозвратно удалена.\nУдалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in NotebookDeleted.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string invnum = row.Row["Инвентарный номер"].ToString();
        //                string spec = row.Row["Модель"].ToString();
        //                sqlcc.ReqDel($"Delete from Notebooks Where InvNum Like '{invnum}'");
        //                sqlcc.Loging(CurrentUser, "Стирание", "Ноутбуки", invnum, spec);
        //            }
        //            UpdateAllTables();
        //        }

        //    }
        //    catch { }
        //}

        //// Удаление планшетов
        //private void Delete_DeletedTablets(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Запись будет безвозвозвратно удалена.\nУдалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in TabletsDeleted.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string invnum = row.Row["Инвентарный номер"].ToString();
        //                string spec = row.Row["Модель"].ToString();
        //                sqlcc.ReqDel($"Delete from Tablets Where InvNum Like '{invnum}'");
        //                sqlcc.Loging(CurrentUser, "Стирание", "Планшеты", invnum, spec);
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //// Удаление орг.техники
        //private void Delete_DeletedOrgTech(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Запись будет безвозвозвратно удалена.\nУдалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in OrgTechDeleted.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string invnum = row.Row["Инвентарный номер"].ToString();
        //                string spec = row.Row["Модель"].ToString();
        //                sqlcc.ReqDel($"Delete from OrgTech Where InvNum Like '{invnum}'");
        //                sqlcc.Loging(CurrentUser, "Стирание", "Оргтехника", invnum, spec);
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //// Удаление мониторов
        //private void Delete_DeletedMonitor(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Запись будет безвозвозвратно удалена.\nУдалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in MonitorsDeleted.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string invnum = row.Row["Инвентарный номер"].ToString();
        //                string spec = row.Row["Модель"].ToString();
        //                sqlcc.ReqDel($"Delete from Monitor Where InvNum Like '{invnum}'");
        //                sqlcc.Loging(CurrentUser, "Стирание", "Мониторы", invnum, spec);
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //// Удаление роутеров
        //private void Delete_DeletedRouters(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Запись будет безвозвозвратно удалена.\nУдалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in RoutersDeleted.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string invnum = row.Row["Инвентарный номер"].ToString();
        //                string spec = row.Row["Модель"].ToString();
        //                sqlcc.ReqDel($"Delete from Routers Where InvNum Like '{invnum}'");
        //                sqlcc.Loging(CurrentUser, "Стирание", "Роутеры", invnum, spec);
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //// Удаление свитчей
        //private void Delete_DeletedSwitches(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Запись будет безвозвозвратно удалена.\nУдалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in SwitchesDeleted.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string invnum = row.Row["Инвентарный номер"].ToString();
        //                string spec = row.Row["Модель"].ToString();
        //                sqlcc.ReqDel($"Delete from Switches Where InvNum Like '{invnum}'");
        //                sqlcc.Loging(CurrentUser, "Стирание", "Свитчи", invnum, spec);
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //// Удаление мебели
        //private void Delete_DeletedFurniture(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Запись будет безвозвозвратно удалена.\nУдалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in FurnDeleted.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string invnum = row.Row["Инвентарный номер"].ToString();
        //                string spec = row.Row["Наименование"].ToString();
        //                sqlcc.ReqDel($"Delete from Furniture Where InvNum Like '{invnum}'");
        //                sqlcc.Loging(CurrentUser, "Стирание", "Мебель", invnum, spec);
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //#endregion
        //#endregion

        //#region Редактирование
        //// Редактировать сотрудника
        //private void Redact_Employee(object sender, RoutedEventArgs e)
        //{
        //    if (EmpRedPerm || Admin)
        //    try
        //    {
        //        DataRowView item = EmployeeTable.Items[EmployeeTable.SelectedIndex] as DataRowView;
        //        string id = item.Row[0].ToString();
        //        EmployeeCROpen(false, id, false);
        //    }
        //    catch { }
        //}

        //// Редактироваить отдел
        //private void Redact_Group(object sender, RoutedEventArgs e)
        //{
        //    if (GrRedPerm || Admin)
        //        try
        //        {
        //            DataRowView item = GroupsTable.Items[GroupsTable.SelectedIndex] as DataRowView;
        //            string name = item.Row[0].ToString();
        //            GroupCROpen(false, name);
        //        }
        //        catch { }
        //}

        //// Редактировать ПК
        //private void Redact_PC(object sender, RoutedEventArgs e)
        //{
        //    if (PCRedPerm || Admin)
        //    try
        //    {
        //        DataRowView item = PCTable.Items[PCTable.SelectedIndex] as DataRowView;
        //        string invnum = item.Row[0].ToString();
        //        PCCROpen(false, invnum, false);
        //    }
        //    catch { }
        //}

        //// Редактировать ноутбук
        //private void Redact_Notebook(object sender, RoutedEventArgs e)
        //{
        //    if (NbRedPerm || Admin)
        //    try
        //    {
        //        DataRowView item = NotebookTable.Items[NotebookTable.SelectedIndex] as DataRowView;
        //        string invnum = item.Row[0].ToString();
        //        NotebookCROpen(false, invnum, false);
        //    }
        //    catch { }
        //}

        //// Редактировать планшет
        //private void Redact_Tablet(object sender, RoutedEventArgs e)
        //{
        //    if (TabRedPerm || Admin)
        //    try
        //    {
        //        DataRowView item = TabletsTable.Items[TabletsTable.SelectedIndex] as DataRowView;
        //        string invnum = item.Row[0].ToString();
        //        TabletsCROpen(false, invnum, false);
        //    }
        //    catch { }
        //}

        //// Редактировать Орг.технику
        //private void Redact_Printer(object sender, RoutedEventArgs e)
        //{
        //    if (OTRedPerm || Admin)
        //    try
        //    {
        //        DataRowView item = PrintersTable.Items[PrintersTable.SelectedIndex] as DataRowView;
        //        string invnum = item.Row[0].ToString();
        //        OrgTechCROpen(false, invnum, false);
        //    }
        //    catch { }
        //}

        //// Редактировать монитор
        //private void Redact_Monitor(object sender, RoutedEventArgs e)
        //{
        //    if (MonRedPerm || Admin)
        //    try
        //    {
        //        DataRowView item = MonitorsTable.Items[MonitorsTable.SelectedIndex] as DataRowView;
        //        string invnum = item.Row[0].ToString();
        //        MonitorCROpen(false, invnum, false);
        //    }
        //    catch { }
        //}

        //// Редактировать пользователя
        //private void Redact_User(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = UsersTable.Items[UsersTable.SelectedIndex] as DataRowView;
        //        string id = item.Row[0].ToString();
        //        UserCROpen(false, id);
        //    }
        //    catch { }
        //}

        //// Редактировать роль
        //private void Redact_Role(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = RolesTable.Items[RolesTable.SelectedIndex] as DataRowView;
        //        string id = item.Row[0].ToString();
        //        RoleCROpen(false, id);
        //    }
        //    catch { }
        //}

        //// Редактировать роутер
        //private void Redact_Routers(object sender, RoutedEventArgs e)
        //{
        //    if (RoutRedPerm || Admin)
        //        try
        //        {
        //            DataRowView item = RoutersTable.Items[RoutersTable.SelectedIndex] as DataRowView;
        //            string invnum = item.Row[0].ToString();
        //            RoutersCROpen(false, invnum, false);
        //        }
        //        catch { }
        //}

        //// Редактировать свитчи
        //private void Redact_Switches(object sender, RoutedEventArgs e)
        //{
        //    if (SwitchRedPerm || Admin)
        //        try
        //        {
        //            DataRowView item = SwitchesTable.Items[SwitchesTable.SelectedIndex] as DataRowView;
        //            string invnum = item.Row["Инвентарный номер"].ToString();
        //            SwitchesCROpen(false, invnum, false);
        //        }
        //        catch { }
        //}

        //// Редактировать тип мебели
        //private void Redact_FurnType(object sender, RoutedEventArgs e)
        //{
        //    if (FurnRedPerm || Admin)
        //        try
        //        {
        //            DataRowView item = FurnTypeTable.Items[FurnTypeTable.SelectedIndex] as DataRowView;
        //            string id = item.Row[0].ToString();
        //            FurnitureTypeCROpen(false, id);
        //        }
        //        catch { }
        //}

        //// Редактировать мебель
        //private void Redact_Furniture(object sender, RoutedEventArgs e)
        //{
        //    if (FurnRedPerm || Admin)
        //        try
        //        {
        //            DataRowView item = FurnTable.Items[FurnTable.SelectedIndex] as DataRowView;
        //            string invnum = item.Row["Инвентарный номер"].ToString();
        //            FurnitureCROpen(false, invnum, false);
        //        }
        //        catch { }
        //}

        //// Редактировать из IP
        //private void Redact_IP(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = ReportIP.Items[ReportIP.SelectedIndex] as DataRowView;
        //        if (item.Row["Устройство"].ToString() == "Компьютер" && (PCRedPerm || Admin))
        //            PCCROpen(false, item.Row["Инвентарный номер"].ToString(), false);
        //        else if ((item.Row["Устройство"].ToString() == "МФУ" || item.Row["Устройство"].ToString() == "Принтер" || item.Row["Устройство"].ToString() == "Сканер") && (OTRedPerm || Admin))
        //            OrgTechCROpen(false, item.Row["Инвентарный номер"].ToString(), false);
        //        else if (item.Row["Устройство"].ToString() == "Роутер" && (RoutRedPerm || Admin))
        //            RoutersCROpen(false, item.Row["Инвентарный номер"].ToString(), false);
        //        else if (item.Row["Устройство"].ToString() == "Свитч" && (SwitchRedPerm || Admin))
        //            SwitchesCROpen(false, item.Row["Инвентарный номер"].ToString(), false);
        //    }
        //    catch {}
        //}

        //// Редактирование из избранного
        //private void Fav_Redact(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = FavTable.Items[FavTable.SelectedIndex] as DataRowView;
        //        if (item.Row["Таблица"].ToString() == "Сотрудники" && (EmpRedPerm || Admin))
        //        { EmployeeCROpen(false, sqlcc.ReqRef($"Select ID From Employees Where FIO Like '{item.Row["Запись"].ToString()}'"), false); }
        //        else if (item.Row["Таблица"].ToString() == "Компьютеры" && (PCRedPerm || Admin))
        //        { PCCROpen(false, item.Row["Запись"].ToString(), false); }
        //        else if (item.Row["Таблица"].ToString() == "Ноутбуки" && (NbRedPerm || Admin))
        //        { NotebookCROpen(false, item.Row["Запись"].ToString(), false); }
        //        else if (item.Row["Таблица"].ToString() == "Планшеты" && (TabRedPerm || Admin))
        //        { TabletsCROpen(false, item.Row["Запись"].ToString(), false); }
        //        else if (item.Row["Таблица"].ToString() == "Оргтехника" && (OTRedPerm || Admin))
        //        { OrgTechCROpen(false, item.Row["Запись"].ToString(), false); }
        //        else if (item.Row["Таблица"].ToString() == "Мониторы" && (MonRedPerm || Admin))
        //        { MonitorCROpen(false, item.Row["Запись"].ToString(), false); }  
        //        else if (item.Row["Таблица"].ToString() == "Роутеры" && (RoutRedPerm || Admin))
        //        { RoutersCROpen(false, item.Row["Запись"].ToString(), false); }
        //        else if (item.Row["Таблица"].ToString() == "Свитчи" && (SwitchRedPerm || Admin))
        //        { SwitchesCROpen(false, item.Row["Запись"].ToString(), false); }
        //        else if (item.Row["Таблица"].ToString() == "Мебель" && (FurnRedPerm || Admin))
        //        { FurnitureCROpen(false, item.Row["Запись"].ToString(), false); }
        //    }
        //    catch { }
        //}
        //#endregion

        //#region Избранное
        //// Добавить сотрудника в избранное
        //private void Fav_Employee(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = EmployeeTable.Items[EmployeeTable.SelectedIndex] as DataRowView;
        //        string id = item.Row["ID"].ToString();
        //        string FIO = item.Row["ФИО"].ToString();
        //        bool allow = true;
        //        foreach (DataRowView row in FavTable.Items)
        //        {
        //            if (row.Row["Таблица"].ToString() == "Сотрудники" && row.Row["Запись"].ToString() == id)
        //            {
        //                allow = false;
        //            }
        //        }
        //        if (allow)
        //        {
        //            sqlcc.ReqNonRef($"INSERT INTO Favourite ([User], Date, [Table], Row, Comment) VALUES ('{CurrentUser}', '{DateTime.Now.Date}', 'Сотрудники', '{FIO}', '')");
        //            FavTable.ItemsSource = sqlcc.DataGridUpdate("Дата, Таблица, Запись, Подробности", "Fav_View", $"WHERE [User] LIKE '{CurrentUser}'").DefaultView;
        //        }
        //        else
        //            MessageBox.Show("Запись уже находится в избранном", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
        //    }
        //    catch { }
        //}

        //// Добавить компьютер в избранное
        //private void Fav_PC(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = PCTable.Items[PCTable.SelectedIndex] as DataRowView;
        //        string invnum = item.Row["Инвентарный номер"].ToString();
        //        string name = item.Row["Имя"].ToString();
        //        bool allow = true;
        //        foreach (DataRowView row in FavTable.Items)
        //        {
        //            if (row.Row["Таблица"].ToString() == "Компьютеры" && row.Row["Запись"].ToString() == invnum)
        //            {
        //                allow = false;
        //            }
        //        }
        //        if (allow)
        //        {
        //            sqlcc.ReqNonRef($"INSERT INTO Favourite ([User], Date, [Table], Row, Comment) VALUES ('{CurrentUser}', '{DateTime.Now}', 'Компьютеры', '{invnum}','{name}')");
        //            FavTable.ItemsSource = sqlcc.DataGridUpdate("Дата, Таблица, Запись, Подробности", "Fav_View", $"WHERE [User] LIKE '{CurrentUser}'").DefaultView;
        //        }
        //        else
        //            MessageBox.Show("Запись уже находится в избранном", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
        //    }
        //    catch { }
        //}

        //// Добавить ноутбук в избранное
        //private void Fav_Notebook(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = NotebookTable.Items[NotebookTable.SelectedIndex] as DataRowView;
        //        string invnum = item.Row["Инвентарный номер"].ToString();
        //        string model = item.Row["Модель"].ToString();
        //        bool allow = true;
        //        foreach (DataRowView row in FavTable.Items)
        //        {
        //            if (row.Row["Таблица"].ToString() == "Ноутбуки" && row.Row["Запись"].ToString() == invnum)
        //            {
        //                allow = false;
        //            }
        //        }
        //        if (allow)
        //        {
        //            sqlcc.ReqNonRef($"INSERT INTO Favourite ([User], Date, [Table], Row, Comment) VALUES ('{CurrentUser}', '{DateTime.Now}', 'Ноутбуки', '{invnum}', '{model}')");
        //            FavTable.ItemsSource = sqlcc.DataGridUpdate("Дата, Таблица, Запись, Подробности", "Fav_View", $"WHERE [User] LIKE '{CurrentUser}'").DefaultView;
        //        }
        //        else
        //            MessageBox.Show("Запись уже находится в избранном", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
        //    }
        //    catch { }
        //}

        //// Добавить планшет в избранное
        //private void Fav_Tablets(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = TabletsTable.Items[TabletsTable.SelectedIndex] as DataRowView;
        //        string invnum = item.Row["Инвентарный номер"].ToString();
        //        string model = item.Row["Модель"].ToString();
        //        bool allow = true;
        //        foreach (DataRowView row in FavTable.Items)
        //        {
        //            if (row.Row["Таблица"].ToString() == "Планшеты" && row.Row["Запись"].ToString() == invnum)
        //            {
        //                allow = false;
        //            }
        //        }
        //        if (allow)
        //        {
        //            sqlcc.ReqNonRef($"INSERT INTO Favourite ([User], Date, [Table], Row, Comment) VALUES ('{CurrentUser}', '{DateTime.Now}', 'Планшеты', '{invnum}', '{model}')");
        //            FavTable.ItemsSource = sqlcc.DataGridUpdate("Дата, Таблица, Запись, Подробности", "Fav_View", $"WHERE [User] LIKE '{CurrentUser}'").DefaultView;
        //        }
        //        else
        //            MessageBox.Show("Запись уже находится в избранном", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
        //    }
        //    catch { }
        //}

        //// Добавить орг.технику в избранное
        //private void Fav_OrgTech(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = PrintersTable.Items[PrintersTable.SelectedIndex] as DataRowView;
        //        string invnum = item.Row["Инвентарный номер"].ToString();
        //        string model = item.Row["Модель"].ToString();
        //        bool allow = true;
        //        foreach (DataRowView row in FavTable.Items)
        //        {
        //            if (row.Row["Таблица"].ToString() == "Оргтехника" && row.Row["Запись"].ToString() == invnum)
        //            {
        //                allow = false;
        //            }
        //        }
        //        if (allow)
        //        {
        //            sqlcc.ReqNonRef($"INSERT INTO Favourite ([User], Date, [Table], Row, Comment) VALUES ('{CurrentUser}', '{DateTime.Now}', 'Орг.техника', '{invnum}', '{model}')");
        //            FavTable.ItemsSource = sqlcc.DataGridUpdate("Дата, Таблица, Запись, Подробности", "Fav_View", $"WHERE [User] LIKE '{CurrentUser}'").DefaultView;
        //        }
        //        else
        //            MessageBox.Show("Запись уже находится в избранном", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
        //    }
        //    catch { }
        //}

        //// Добавить монитор в избранное
        //private void Fav_Monitors(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = MonitorsTable.Items[MonitorsTable.SelectedIndex] as DataRowView;
        //        string invnum = item.Row["Инвентарный номер"].ToString();
        //        string model = item.Row["Модель"].ToString();
        //        bool allow = true;
        //        foreach (DataRowView row in FavTable.Items)
        //        {
        //            if (row.Row["Таблица"].ToString() == "Мониторы" && row.Row["Запись"].ToString() == invnum)
        //            {
        //                allow = false;
        //            }
        //        }
        //        if (allow)
        //        {
        //            sqlcc.ReqNonRef($"INSERT INTO Favourite ([User], Date, [Table], Row, Comment) VALUES ('{CurrentUser}', '{DateTime.Now}', 'Мониторы', '{invnum}', '{model}')");
        //            FavTable.ItemsSource = sqlcc.DataGridUpdate("Дата, Таблица, Запись, Подробности", "Fav_View", $"WHERE [User] LIKE '{CurrentUser}'").DefaultView;
        //        }
        //        else
        //            MessageBox.Show("Запись уже находится в избранном", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
        //    }
        //    catch { }
        //}

        //// Добавить роутер в избранное
        //private void Fav_Routers(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = RoutersTable.Items[RoutersTable.SelectedIndex] as DataRowView;
        //        string invnum = item.Row["Инвентарный номер"].ToString();
        //        string model = item.Row["Модель"].ToString();
        //        bool allow = true;
        //        foreach (DataRowView row in FavTable.Items)
        //        {
        //            if (row.Row["Таблица"].ToString() == "Роутеры" && row.Row["Запись"].ToString() == invnum)
        //            {
        //                allow = false;
        //            }
        //        }
        //        if (allow)
        //        {
        //            sqlcc.ReqNonRef($"INSERT INTO Favourite ([User], Date, [Table], Row, Comment) VALUES ('{CurrentUser}', '{DateTime.Now}', 'Роутеры', '{invnum}', '{model}')");
        //            FavTable.ItemsSource = sqlcc.DataGridUpdate("Дата, Таблица, Запись, Подробности", "Fav_View", $"WHERE [User] LIKE '{CurrentUser}'").DefaultView;
        //        }
        //        else
        //            MessageBox.Show("Запись уже находится в избранном", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
        //    }
        //    catch { }
        //}

        //// Добавить свитч в избранное
        //private void Fav_Switches(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = RoutersTable.Items[RoutersTable.SelectedIndex] as DataRowView;
        //        string invnum = item.Row["Инвентарный номер"].ToString();
        //        string model = item.Row["Модель"].ToString();
        //        bool allow = true;
        //        foreach (DataRowView row in FavTable.Items)
        //        {
        //            if (row.Row["Таблица"].ToString() == "Свитчи" && row.Row["Запись"].ToString() == invnum)
        //            {
        //                allow = false;
        //            }
        //        }
        //        if (allow)
        //        {
        //            sqlcc.ReqNonRef($"INSERT INTO Favourite ([User], Date, [Table], Row, Comment) VALUES ('{CurrentUser}', '{DateTime.Now}', 'Свитчи', '{invnum}', '{model}')");
        //            FavTable.ItemsSource = sqlcc.DataGridUpdate("Дата, Таблица, Запись, Подробности", "Fav_View", $"WHERE [User] LIKE '{CurrentUser}'").DefaultView;
        //        }
        //        else
        //            MessageBox.Show("Запись уже находится в избранном", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
        //    }
        //    catch { }
        //}

        //// Добавить мебель в избранное
        //private void Fav_Furniture(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = FurnTable.Items[FurnTable.SelectedIndex] as DataRowView;
        //        string invnum = item.Row["Инвентарный номер"].ToString();
        //        string name = item.Row["Наименование"].ToString();
        //        string type = item.Row["Тип"].ToString(); 
        //        bool allow = true;
        //        foreach (DataRowView row in FavTable.Items)
        //        {
        //            if (row.Row["Таблица"].ToString() == "Мебель" && row.Row["Запись"].ToString() == invnum)
        //            {
        //                allow = false;
        //            }
        //        }
        //        if (allow)
        //        {
        //            sqlcc.ReqNonRef($"INSERT INTO Favourite ([User], Date, [Table], Row, Comment) VALUES ('{CurrentUser}', '{DateTime.Now}', 'Мебель', '{invnum}', '{type + ": " + name}')");
        //            FavTable.ItemsSource = sqlcc.DataGridUpdate("Дата, Таблица, Запись, Подробности", "Fav_View", $"WHERE [User] LIKE '{CurrentUser}'").DefaultView;
        //        }
        //        else
        //            MessageBox.Show("Запись уже находится в избранном", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
        //    }
        //    catch { }
        //}

        //// Убрать из избранного
        //private void Fav_Remove(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = FavTable.Items[FavTable.SelectedIndex] as DataRowView;
        //        sqlcc.ReqNonRef($"DELETE FROM Favourite WHERE [User] LIKE '{CurrentUser}' AND [Table] LIKE '{item.Row["Таблица"].ToString()}' AND Row LIKE '{item.Row["Запись"].ToString()}'");
        //        FavTable.ItemsSource = sqlcc.DataGridUpdate("Дата, Таблица, Запись, Подробности", "Fav_View", $"WHERE [User] LIKE '{CurrentUser}'").DefaultView;
        //    }
        //    catch { }
        //}
        //#endregion

        //#region Открыть удалённое
        //// Сотрудники
        //private void Open_DeletedEmployee(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = EmployeesDeleted.Items[EmployeesDeleted.SelectedIndex] as DataRowView;
        //        string id = item.Row["ID"].ToString();
        //        EmployeeCROpen(false, id, true);
        //    }
        //    catch { }
        //}

        //// Компьютеры
        //private void Open_DeletedPC(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = PCDeleted.Items[PCDeleted.SelectedIndex] as DataRowView;
        //        string invnum = item.Row["Инвентарный номер"].ToString();
        //        PCCROpen(false, invnum, true);
        //    }
        //    catch { }
        //}

        //// Ноутбуки
        //private void Open_DeletedNotebook(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = NotebookDeleted.Items[NotebookDeleted.SelectedIndex] as DataRowView;
        //        string invnum = item.Row["Инвентарный номер"].ToString();
        //        NotebookCROpen(false, invnum, true);
        //    }
        //    catch { }
        //}

        //// Планшеты
        //private void Open_DeletedTablet(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = TabletsDeleted.Items[TabletsDeleted.SelectedIndex] as DataRowView;
        //        string invnum = item.Row["Инвентарный номер"].ToString();
        //        TabletsCROpen(false, invnum, true);
        //    }
        //    catch { }

        //}

        //// Орг.Техника
        //private void Open_DeletedOrgTech(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = OrgTechDeleted.Items[OrgTechDeleted.SelectedIndex] as DataRowView;
        //        string invnum = item.Row["Инвентарный номер"].ToString();
        //        OrgTechCROpen(false, invnum, true);
        //    }
        //    catch { }
        //}

        //// Мониторы
        //private void Open_DeletedMonitor(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = MonitorsDeleted.Items[MonitorsDeleted.SelectedIndex] as DataRowView;
        //        string invnum = item.Row["Инвентарный номер"].ToString();
        //        MonitorCROpen(false, invnum, true);
        //    }
        //    catch { }
        //}

        //// Роутеры
        //private void Open_DeletedRouters(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = RoutersDeleted.Items[RoutersDeleted.SelectedIndex] as DataRowView;
        //        string invnum = item.Row["Инвентарный номер"].ToString();
        //        RoutersCROpen(false, invnum, true);
        //    }
        //    catch { }
        //}

        //// Свитчи
        //private void Open_DeletedSwitches(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DataRowView item = SwitchesDeleted.Items[SwitchesDeleted.SelectedIndex] as DataRowView;
        //        string invnum = item.Row["Инвентарный номер"].ToString();
        //        SwitchesCROpen(false, invnum, true);
        //    }
        //    catch { }
        //}

        //// Мебель
        //private void Open_DeletedFurniture(object sender, RoutedEventArgs e)
        //{
        //    DataRowView item = FurnDeleted.Items[FurnDeleted.SelectedIndex] as DataRowView;
        //    string invnum = item.Row["Инвентарный номер"].ToString();
        //    FurnitureCROpen(false, invnum, true);
        //}
        //#endregion

        //#region Восстановление
        //// Сотрудники
        //private void Recovery_Employee(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Вы уверены что хотите восстановить запись?","Внимание",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in EmployeesDeleted.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string id = row.Row["ID"].ToString();
        //                string name = row.Row["ФИО"].ToString();
        //                sqlcc.Recovery("Employees", "ID", id);
        //                sqlcc.Loging(CurrentUser, "Восстановление", "Сотрудники", name, "");
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //// Компьютеры
        //private void Recovery_PC(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Вы уверены что хотите восстановить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in PCDeleted.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string invnum = row.Row["Инвентарный номер"].ToString();
        //                string spec = row.Row["Имя"].ToString();
        //                sqlcc.Recovery("Computers", "InvNum", invnum);
        //                sqlcc.Loging(CurrentUser, "Восстановление", "Компьютеры", invnum, spec);
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //// Ноутбуки
        //private void Recovery_Notebook(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Вы уверены что хотите восстановить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in NotebookDeleted.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string invnum = row.Row["Инвентарный номер"].ToString();
        //                string spec = row.Row["Модель"].ToString();
        //                sqlcc.Recovery("Notebooks", "InvNum", invnum);
        //                sqlcc.Loging(CurrentUser, "Восстановление", "Ноутбуки", invnum, spec);
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //// Планшеты
        //private void Recovery_Tablet(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Вы уверены что хотите восстановить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in TabletsDeleted.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string invnum = row.Row["Инвентарный номер"].ToString();
        //                string spec = row.Row["Модель"].ToString();
        //                sqlcc.Recovery("Tablets", "InvNum", invnum);
        //                sqlcc.Loging(CurrentUser, "Восстановление", "Ноутбуки", invnum, spec);
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //// Орг.техника
        //private void Recovery_OrgTech(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Вы уверены что хотите восстановить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in OrgTechDeleted.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string invnum = row.Row["Инвентарный номер"].ToString();
        //                string spec = row.Row["Модель"].ToString();
        //                sqlcc.Recovery("OrgTech", "InvNum", invnum);
        //                sqlcc.Loging(CurrentUser, "Восстановление", "Оргтехника", invnum, spec);
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //// Мониторы
        //private void Recovery_Monitor(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Вы уверены что хотите восстановить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in MonitorsDeleted.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string invnum = row.Row["Инвентарный номер"].ToString();
        //                string spec = row.Row["Производитель"].ToString() + " " + row.Row["Модель"].ToString();
        //                sqlcc.Recovery("Monitor", "InvNum", invnum);
        //                sqlcc.Loging(CurrentUser, "Восстановление", "Мониторы", invnum, spec);
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //// Роутеры
        //private void Recovery_Routers(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Вы уверены что хотите восстановить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in RoutersDeleted.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string invnum = row.Row["Инвентарный номер"].ToString();
        //                string spec = row.Row["Модель"].ToString();
        //                sqlcc.Recovery("Routers", "InvNum", invnum);
        //                sqlcc.Loging(CurrentUser, "Восстановление", "Роутеры", invnum, spec);
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //// Свитчи
        //private void Recovery_Switches(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Вы уверены что хотите восстановить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in SwitchesDeleted.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string invnum = row.Row["Инвентарный номер"].ToString();
        //                string spec = row.Row["Модель"].ToString();
        //                sqlcc.Recovery("Switches", "InvNum", invnum);
        //                sqlcc.Loging(CurrentUser, "Восстановление", "Свитчи", invnum, spec);
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}

        //// Мебель
        //private void Recovery_Furniture(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Вы уверены что хотите восстановить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //        {
        //            foreach (var items in FurnDeleted.SelectedItems)
        //            {
        //                DataRowView row = (DataRowView)items;
        //                string invnum = row.Row["Инвентарный номер"].ToString();
        //                string spec = row.Row["Наименование"].ToString();
        //                sqlcc.Recovery("Furniture", "InvNum", invnum);
        //                sqlcc.Loging(CurrentUser, "Восстановление", "Мебель", invnum, spec);
        //            }
        //            UpdateAllTables();
        //        }
        //    }
        //    catch { }
        //}
        //#endregion

        //#endregion

        //#region Фильтры

        //#region Включить фильтр

        //#region Фильтр пользовательских таблиц
        //// Фильтр сотрудников
        //private void SEmployee(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("Emp");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        EmployeeTable.Margin = new Thickness(0, 100, 5, 20);
        //        EmployeeTable.ItemsSource = sqlcc.DataGridUpdate("*", "Employees_View", $"{MigApp.Properties.Settings.Default.comEmp}").DefaultView;
        //        FilterEmpText.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsEmp);
        //    }
        //    BlindfallSwitch();
        //}

        //// Фильтр ПК
        //private void SPC(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("PC");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        PCTable.Margin = new Thickness(0, 100, 5, 20);
        //        PCTable.ItemsSource = sqlcc.DataGridUpdate("*", "PC_View", $"{MigApp.Properties.Settings.Default.comPC}").DefaultView;
        //        FilterPCText.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsPC);
        //    }
        //    BlindfallSwitch();
        //}

        //// Фильтр Ноутбуков
        //private void SNotebook(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("NB");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        NotebookTable.Margin = new Thickness(0, 100, 5, 20);
        //        NotebookTable.ItemsSource = sqlcc.DataGridUpdate("*", " Notebooks_View", $"{MigApp.Properties.Settings.Default.comNB}").DefaultView;
        //        FilterNotebookText.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsNB);
        //    }
        //    BlindfallSwitch();
        //}

        //// Фильтр Планшетов
        //private void STablets(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("Tab");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if(win.DialogResult == true)
        //    {
        //        TabletsTable.Margin = new Thickness(0, 100, 5, 20);
        //        TabletsTable.ItemsSource = sqlcc.DataGridUpdate("*", "Tablet_View", $"{MigApp.Properties.Settings.Default.comTab}").DefaultView;
        //        FilterTabletsText.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsTab);
        //    }
        //    BlindfallSwitch();
        //}

        //// Фильтр Орг.Техники
        //private void SOrgTech(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("OT");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        PrintersTable.Margin = new Thickness(0, 100, 5, 20);
        //        PrintersTable.ItemsSource = sqlcc.DataGridUpdate("*", "OrgTech_View", $"{MigApp.Properties.Settings.Default.comOT}").DefaultView;
        //        FilterOrgTechText.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsOT);
        //    }
        //    BlindfallSwitch();
        //}

        //// Фильтр Мониторов
        //private void SMonitors(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("Mon");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        MonitorsTable.Margin = new Thickness(0, 100, 5, 20);
        //        MonitorsTable.ItemsSource = sqlcc.DataGridUpdate("*", "Monitors_View", $"{MigApp.Properties.Settings.Default.comMon}").DefaultView;
        //        FilterMonitorsText.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsMon);
        //    }
        //    BlindfallSwitch();
        //}

        //// Фильтр роутеров
        //private void SRouters(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("Rout");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        RoutersTable.Margin = new Thickness(0, 100, 5, 20);
        //        RoutersTable.ItemsSource = sqlcc.DataGridUpdate("*", "Routers_View", $"{MigApp.Properties.Settings.Default.comRout}").DefaultView;
        //        FilterRoutersText.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsRout);
        //    }
        //    BlindfallSwitch();
        //}

        //// Фильтр свитчей
        //private void SSwitches(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("Switch");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        SwitchesTable.Margin = new Thickness(0, 100, 5, 20);
        //        SwitchesTable.ItemsSource = sqlcc.DataGridUpdate("*", "Switches_View", $"{MigApp.Properties.Settings.Default.comSwitch}").DefaultView;
        //        FilterSwitchesText.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsSwitch);
        //    }
        //    BlindfallSwitch();
        //}

        //// Фильтры мебели
        //private void SFurn(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("Furniture");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        FurnTable.Margin = new Thickness(0, 100, 5, 20);
        //        SwitchesTable.ItemsSource = sqlcc.DataGridUpdate("*", "Furniture_View", $"{MigApp.Properties.Settings.Default.comFurn}").DefaultView;
        //        FilterFurnText.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsFurn);
        //    }
        //    BlindfallSwitch();
        //}
        //#endregion

        //#region Фильтр Админпанель
        //// Пользователи
        //private void SUser(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("Users");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        UsersTable.Margin = new Thickness(0, 100, 5, 20);
        //        UsersTable.ItemsSource = sqlcc.DataGridUpdate("*", "Users_View", $"{MigApp.Properties.Settings.Default.comUsers}").DefaultView;
        //        FilterUsersText.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsUsers);
        //    }
        //    BlindfallSwitch();
        //}

        //// Логи
        //private void SLogs(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("Logs");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        LogsTable.Margin = new Thickness(0, 100, 5, 20);
        //        LogsTable.ItemsSource = sqlcc.DataGridUpdate("ID, Дата, Пользователь, Действие, Таблица, Запись", "Logs_View", $"{MigApp.Properties.Settings.Default.comLogs}").DefaultView;
        //        FilterLogsText.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsLogs);
        //    }
        //    BlindfallSwitch();
        //}
        //#endregion

        //#region Архив

        //// Сотрудники Архив
        //private void SEmployee_Del(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("EmpDel");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        EmployeesDeleted.Margin = new Thickness(0, 100, 5, 20);
        //        EmployeesDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Employees_Deleted", $"{MigApp.Properties.Settings.Default.comEmpDel}").DefaultView;
        //        FilterEmployeesText_Del.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsEmpDel);
        //    }
        //    BlindfallSwitch();
        //}

        //// ПК Архив
        //private void SPC_Del(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("PCDel");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        PCDeleted.Margin = new Thickness(0, 100, 5, 20);
        //        PCDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "PC_Deleted", $"{MigApp.Properties.Settings.Default.comPCDel}").DefaultView;
        //        FilterPCText_Del.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsPCDel);
        //    }
        //    BlindfallSwitch();
        //}

        //// Ноутбуки Архив
        //private void SNotebook_Del(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("NBDel");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        NotebookDeleted.Margin = new Thickness(0, 100, 5, 20);
        //        NotebookDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Notebooks_Deleted", $"{MigApp.Properties.Settings.Default.comNBDel}").DefaultView;
        //        FilterNotebooksText_Del.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsNBDel);
        //    }
        //    BlindfallSwitch();
        //}

        //// Планшеты Архив
        //private void STablets_Del(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("TabDel");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        TabletsDeleted.Margin = new Thickness(0, 100, 5, 20);
        //        TabletsDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Tablet_Deleted", $"{MigApp.Properties.Settings.Default.comTabDel}").DefaultView;
        //        FilterTabletsText_Del.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsTabDel);
        //    }
        //    BlindfallSwitch();
        //}

        //// Орг.техника Архив
        //private void SOrgTech_Del(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("OTDel");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        OrgTechDeleted.Margin = new Thickness(0, 100, 5, 20);
        //        OrgTechDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "OrgTech_Deleted", $"{MigApp.Properties.Settings.Default.comOTDel}").DefaultView;
        //        FilterOrgTechText_Del.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsOTDel);
        //    }
        //    BlindfallSwitch();
        //}

        //// Мониторы Архив
        //private void SMonitors_Del(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("MonDel");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        MonitorsDeleted.Margin = new Thickness(0, 100, 5, 20);
        //        MonitorsDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Monitors_Deleted", $"{MigApp.Properties.Settings.Default.comMonDel}").DefaultView;
        //        FilterMonitorsText_Del.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsMonDel);
        //    }
        //    BlindfallSwitch();
        //}

        //// Роутеры Архив
        //private void SRouters_Del(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("RoutDel");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        RoutersDeleted.Margin = new Thickness(0, 100, 5, 20);
        //        RoutersDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Routers_Deleted", $"{MigApp.Properties.Settings.Default.comRoutDel}").DefaultView;
        //        FilterRoutersText_Del.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsRoutDel);
        //    }
        //    BlindfallSwitch();
        //}

        //// Свитчи Архив
        //private void SSwitches_Del(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("SwitchDel");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        SwitchesDeleted.Margin = new Thickness(0, 100, 5, 20);
        //        SwitchesDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Switches_Deleted", $"{MigApp.Properties.Settings.Default.comSwitchDel}").DefaultView;
        //        FilterSwitchesText_Del.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsSwitchDel);
        //    }
        //    BlindfallSwitch();
        //}

        //// Мебель Архив
        //private void SFurn_Del(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("FurnitureDel");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        FurnDeleted.Margin = new Thickness(0, 100, 5, 20);
        //        FurnDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Furniture_Deleted", $"{MigApp.Properties.Settings.Default.comFurnDel}").DefaultView;
        //        FilterSwitchesText_Del.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsFurnDel);
        //    }
        //    BlindfallSwitch();
        //}

        //#endregion

        //#region Отчёты
        //// ПК
        //private void SReportPC(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("PCRep");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        ReportPC.Margin = new Thickness(0, 100, 5, 20);
        //        ReportPC.ItemsSource = null;
        //        ReportPC.ItemsSource = sqlcc.Report_PC_Filtered().DefaultView;
        //        FilterReportPCText.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsPCRep);
        //    }
        //    BlindfallSwitch();
        //}

        //// Ноутбуки
        //private void SReportNB(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("NBRep");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        ReportNB.Margin = new Thickness(0, 100, 5, 20);
        //        ReportNB.ItemsSource = sqlcc.DataGridUpdate("*", "Report_Notebooks", $"{MigApp.Properties.Settings.Default.comNBRep}").DefaultView;
        //        FilterReportNBText.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsNBRep);
        //    }
        //    BlindfallSwitch();
        //}

        //// Планшеты
        //private void SReportTab(object sender, RoutedEventArgs e)
        //{
        //    SearchWindow win = new SearchWindow("TabRep");
        //    BlindfallSwitch();
        //    win.Owner = this;
        //    win.ShowDialog();
        //    if (win.DialogResult == true)
        //    {
        //        ReportTab.Margin = new Thickness(0, 100, 5, 20);
        //        ReportTab.ItemsSource = sqlcc.DataGridUpdate("*", "Report_Tablets", $"{MigApp.Properties.Settings.Default.comTabRep}").DefaultView;
        //        FilterReportTabText.Text = mc.Splitter(MigApp.Properties.Settings.Default.ParamsTabRep);
        //    }
        //    BlindfallSwitch();
        //}

        //// IP
        //private void SReportIP(object sender, RoutedEventArgs e)
        //{

        //}
        //#endregion

        //#endregion

        //#region Выключить фильтр

        //#region Фильтр пользовательских таблиц
        //// Очистка фильтра сотрудников
        //private void FilterEmpClear(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.comEmp = null;
        //    MigApp.Properties.Settings.Default.ParamsEmp = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterEmpText.Text = "";
        //    EmployeeTable.Margin = new Thickness(0, 50, 5, 20);
        //    EmployeeTable.ItemsSource = sqlcc.DataGridUpdate("*", "Employees_View", "").DefaultView;
        //}

        //// Очистка фильтра компьютеров
        //private void FilterPCClear(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.comPC = null;
        //    MigApp.Properties.Settings.Default.ParamsPC = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterPCText.Text = "";
        //    PCTable.Margin = new Thickness(0, 50, 5, 20);
        //    PCTable.ItemsSource = sqlcc.DataGridUpdate("*", "PC_View", "").DefaultView;
        //}

        //// Очистка фильтра ноутбуков
        //private void FilterNotebookClear(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.comNB = null;
        //    MigApp.Properties.Settings.Default.ParamsNB = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterNotebookText.Text = "";
        //    NotebookTable.Margin = new Thickness(0, 50, 5, 20);
        //    NotebookTable.ItemsSource = sqlcc.DataGridUpdate("*", "Notebooks_View", "").DefaultView;
        //}

        //// Очистка фильтра планшетов
        //private void FilterTabletsClear(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.comTab = null;
        //    MigApp.Properties.Settings.Default.ParamsTab = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterTabletsText.Text = "";
        //    TabletsTable.Margin = new Thickness(0, 50, 5, 20);
        //    TabletsTable.ItemsSource = sqlcc.DataGridUpdate("*", "Tablet_View", "").DefaultView;
        //}

        //// Очистка фильтра орг.техники
        //private void FilterOrgTechClear(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.comOT = null;
        //    MigApp.Properties.Settings.Default.ParamsOT = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterOrgTechText.Text = "";
        //    PrintersTable.Margin = new Thickness(0, 50, 5, 20);
        //    PrintersTable.ItemsSource = sqlcc.DataGridUpdate("*", "OrgTech_View", "").DefaultView;
        //}

        //// Очистка фильтра мониторов
        //private void FilterMonitorsClear(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.comMon = null;
        //    MigApp.Properties.Settings.Default.ParamsMon = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterMonitorsText.Text = "";
        //    MonitorsTable.Margin = new Thickness(0, 50, 5, 20);
        //    MonitorsTable.ItemsSource = sqlcc.DataGridUpdate("*", "Monitors_View", "").DefaultView;
        //}

        //// Очистка фильтра роутеров
        //private void FilterRoutersClear(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.comRout = null;
        //    MigApp.Properties.Settings.Default.ParamsRout = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterRoutersText.Text = "";
        //    RoutersTable.Margin = new Thickness(0, 50, 5, 20);
        //    RoutersTable.ItemsSource = sqlcc.DataGridUpdate("*", "Routers_View", "").DefaultView;
        //}

        //// Очистка фильтра свитчей
        //private void FilterSwitchesClear(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.comSwitch = null;
        //    MigApp.Properties.Settings.Default.ParamsSwitch = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterSwitchesText.Text = "";
        //    SwitchesTable.Margin = new Thickness(0, 50, 5, 20);
        //    SwitchesTable.ItemsSource = sqlcc.DataGridUpdate("*", "Switches_View", "").DefaultView;
        //}

        //// Очистка фильтра мебели
        //private void FilterFurnClear(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.comFurn = null;
        //    MigApp.Properties.Settings.Default.ParamsFurn = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterFurnText.Text = "";
        //    FurnTable.Margin = new Thickness(0, 50, 5, 20);
        //    FurnTable.ItemsSource = sqlcc.DataGridUpdate("*", "Furniture_View", "").DefaultView;
        //}
        //#endregion

        //#region Фильтр Админпанель
        //// Пользователи
        //private void FilterUsersClear(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.comUsers = null;
        //    MigApp.Properties.Settings.Default.ParamsUsers = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterUsersText.Text = "";
        //    UsersTable.Margin = new Thickness(0, 50, 5, 20);
        //    UsersTable.ItemsSource = sqlcc.DataGridUpdate("*", "Users_View", "").DefaultView;
        //}

        //// Логи
        //private void FilterLogsClear(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.comLogs = null;
        //    MigApp.Properties.Settings.Default.ParamsLogs = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterLogsText.Text = "";
        //    LogsTable.Margin = new Thickness(0, 50, 5, 20);
        //    LogsTable.ItemsSource = sqlcc.DataGridUpdate("*", "Logs_View", "ORDER BY ID DESC").DefaultView;
        //}
        //#endregion

        //#region Архив
        //// Сотрудники Архив
        //private void FilterEmployeesClear_Del(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.comEmpDel = null;
        //    MigApp.Properties.Settings.Default.ParamsEmpDel = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterEmployeesText_Del.Text = "";
        //    EmployeesDeleted.Margin = new Thickness(0, 50, 5, 20);
        //    EmployeesDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Employees_Deleted", "").DefaultView;
        //}

        //// Компьютеры Архив
        //private void FilterPCClear_Del(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.comPCDel = null;
        //    MigApp.Properties.Settings.Default.ParamsPCDel = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterPCText_Del.Text = "";
        //    PCDeleted.Margin = new Thickness(0, 50, 5, 20);
        //    PCDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "PC_Deleted", "").DefaultView;
        //}

        //// Ноутбуки Архив
        //private void FilterNotebooksClear_Del(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.comNBDel = null;
        //    MigApp.Properties.Settings.Default.ParamsNBDel = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterNotebooksText_Del.Text = "";
        //    NotebookDeleted.Margin = new Thickness(0, 50, 5, 20);
        //    NotebookDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Notebooks_Deleted", "").DefaultView;
        //}

        //// Планшеты Архив
        //private void FilterTabletsClear_Del(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.comTabDel = null;
        //    MigApp.Properties.Settings.Default.ParamsTabDel = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterTabletsText_Del.Text = "";
        //    TabletsDeleted.Margin = new Thickness(0, 50, 5, 20);
        //    TabletsDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Tablets_Deleted", "").DefaultView;
        //}

        //// Орг.техника Архив
        //private void FilterOrgTechClear_Del(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.comOTDel = null;
        //    MigApp.Properties.Settings.Default.ParamsOTDel = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterOrgTechText_Del.Text = "";
        //    OrgTechDeleted.Margin = new Thickness(0, 50, 5, 20);
        //    OrgTechDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "OrgTech_Deleted", "").DefaultView;
        //}

        //// Мониторы Архив
        //private void FilterMonitorsClear_Del(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.comMonDel = null;
        //    MigApp.Properties.Settings.Default.ParamsMonDel = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterMonitorsText_Del.Text = "";
        //    MonitorsDeleted.Margin = new Thickness(0, 50, 5, 20);
        //    MonitorsDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Monitors_Deleted", "").DefaultView;
        //}

        //// Роутеры Архив
        //private void FilterRoutersClear_Del(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.comRoutDel = null;
        //    MigApp.Properties.Settings.Default.ParamsRoutDel = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterRoutersText_Del.Text = "";
        //    RoutersDeleted.Margin = new Thickness(0, 50, 5, 20);
        //    RoutersDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Routers_Deleted", "").DefaultView;
        //}

        //// Свитчи Архив
        //private void FilterSwitchesClear_Del(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.comSwitchDel = null;
        //    MigApp.Properties.Settings.Default.ParamsSwitchDel = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterSwitchesText_Del.Text = "";
        //    SwitchesDeleted.Margin = new Thickness(0, 50, 5, 20);
        //    SwitchesDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Switches_Deleted", "").DefaultView;
        //}

        //// Мебель Архив
        //private void FilterFurnClear_Del(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.comFurnDel = null;
        //    MigApp.Properties.Settings.Default.ParamsFurnDel = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterFurnText_Del.Text = "";
        //    FurnDeleted.Margin = new Thickness(0, 50, 5, 20);
        //    FurnDeleted.ItemsSource = sqlcc.DataGridUpdate("*", "Furniture_Deleted", "").DefaultView;
        //}
        //#endregion

        //#region Отчёты

        //// ПК
        //private void FilterReportPCClear(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.ParamsPCRep = null;
        //    MigApp.Properties.Settings.Default.comPCRep = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterReportPCText.Text = "";
        //    ReportPC.Margin = new Thickness(0, 50, 5, 20);
        //    ReportPC.ItemsSource = sqlcc.Report_PC().DefaultView;
        //}

        //// Ноутбуки
        //private void FilterReportNBClear(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.ParamsNBRep = null;
        //    MigApp.Properties.Settings.Default.comNBRep = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterReportNBText.Text = "";
        //    ReportNB.Margin = new Thickness(0, 50, 5, 20);
        //    ReportNB.ItemsSource = sqlcc.DataGridUpdate("*", "Report_Notebooks", "").DefaultView;
        //}

        //// Планшеты
        //private void FilterReportTabClear(object sender, RoutedEventArgs e)
        //{
        //    MigApp.Properties.Settings.Default.ParamsTabRep = null;
        //    MigApp.Properties.Settings.Default.comTabRep = null;
        //    MigApp.Properties.Settings.Default.Save();
        //    FilterReportTabText.Text = "";
        //    ReportTab.Margin = new Thickness(0, 50, 5, 20);
        //    ReportTab.ItemsSource = sqlcc.DataGridUpdate("*", "Report_Tablets", "").DefaultView;
        //}

        //// IP
        //private void FilterReportIPClear(object sender, RoutedEventArgs e)
        //{

        //}
        //#endregion

        //#endregion

        //#endregion

        //#region Кнопки создания
        //// Cоздать сотрудника
        //private void EmployeeCreateClick(object sender, RoutedEventArgs e)
        //{
        //    if (EmpRedPerm || Admin)
        //        EmployeeCROpen(true, null, false);
        //}

        //// Создать отдел
        //private void GroupCreateClick(object sender, RoutedEventArgs e)
        //{
        //    if (GrRedPerm || Admin)
        //        GroupCROpen(true, null);
        //}

        //// Создать компьютер
        //private void PCCreateClick(object sender, RoutedEventArgs e)
        //{
        //    if (PCRedPerm || Admin)
        //        PCCROpen(true, null, false);
        //}

        //// Создать ноутбук
        //private void NotebookCreateClick(object sender, RoutedEventArgs e)
        //{
        //    if (NbRedPerm || Admin)
        //        NotebookCROpen(true, null, false);
        //}

        //// Создать планшет
        //private void TabletsCreateClick(object sender, RoutedEventArgs e)
        //{
        //    if (TabRedPerm || Admin)
        //        TabletsCROpen(true, null, false);
        //}

        //// Создать ррг. технику
        //private void OrgTechCreateClick(object sender, RoutedEventArgs e)
        //{
        //    if (OTRedPerm || Admin)
        //        OrgTechCROpen(true, null, false);
        //}

        //// Создать монитор
        //private void MonitorCreateClick(object sender, RoutedEventArgs e)
        //{
        //    if (MonRedPerm || Admin)
        //        MonitorCROpen(true, null, false);
        //}

        //// Создать пользователя
        //private void UserCreateClick(object sender, RoutedEventArgs e)
        //{
        //    UserCROpen(true, null);
        //}

        //// Создать роль
        //private void CreateRoleClick(object sender, RoutedEventArgs e)
        //{
        //    RoleCROpen(true, null);
        //}

        //// Создать роутер
        //private void RoutersCreateClick(object sender, RoutedEventArgs e)
        //{
        //    if (RoutRedPerm || Admin)
        //        RoutersCROpen(true, null, false);
        //}

        //// Создать свитч
        //private void SwitchesCreateClick(object sender, RoutedEventArgs e)
        //{
        //    if (SwitchRedPerm || Admin)
        //        SwitchesCROpen(true, null, false);
        //}

        //// Создать тип мебели
        //private void FurnTypeCreateClick(object sender, RoutedEventArgs e)
        //{
        //    if (FurnRedPerm || Admin)
        //        FurnitureTypeCROpen(true, null);
        //}

        //// Создать мебель
        //private void FurnCreateClick(object sender, RoutedEventArgs e)
        //{
        //    if (FurnRedPerm || Admin)
        //        FurnitureCROpen(true, null, false);
        //}
        //#endregion

        //#region Открытие CR окон
        //// Открыть окно сотрудников
        //private void EmployeeCROpen(bool mode, string id, bool deleted)
        //{
        //    EmployeesWindow win = new EmployeesWindow(mode, id, deleted, GrRedPerm);
        //    if (!deleted)
        //    {
        //        BlindfallSwitch();
        //        win.ShowDialog();
        //        if (win.DialogResult == true) UpdateAllTables();
        //        BlindfallSwitch();
        //    }
        //    else
        //    {
        //        BlindfallSwitch();
        //        win.DoneButton.Visibility = Visibility.Collapsed;
        //        win.RecoveryButton.Visibility = Visibility.Visible;
        //        win.ShowDialog();
        //        if (win.DialogResult == true) UpdateAllTables();
        //        BlindfallSwitch();
        //    }
        //}

        //// Открыть окно отделов
        //private void GroupCROpen(bool mode, string name)
        //{
        //    EmpGroupWindow win = new EmpGroupWindow(mode, name);

        //        BlindfallSwitch();
        //        win.ShowDialog();
        //        if (win.DialogResult == true) UpdateAllTables();
        //        BlindfallSwitch();
        //}

        //// Открыть окно ПК
        //private void PCCROpen(bool mode, string invnum, bool deleted)
        //{
        //    PCWindow win = new PCWindow(mode, invnum, deleted, EmpRedPerm, GrRedPerm);
        //    if (!deleted)
        //    {
        //        try
        //        {
        //            BlindfallSwitch();
        //            win.ShowDialog();
        //        }
        //        catch { }
        //        if (win.DialogResult == true) UpdateAllTables();
        //        BlindfallSwitch();
        //    }
        //    else
        //    {
        //        BlindfallSwitch();
        //        win.DoneButton.Visibility = Visibility.Collapsed;
        //        win.RecoveryButton.Visibility = Visibility.Visible;
        //        win.ShowDialog();
        //        if (win.DialogResult == true) UpdateAllTables();
        //        BlindfallSwitch();
        //    }
        //}

        //// Открыть окно ноутбуков
        //private void NotebookCROpen(bool mode, string invnum, bool deleted)
        //{
        //    NotebookWindow win = new NotebookWindow(mode, invnum, deleted, EmpRedPerm, GrRedPerm);
        //    if (!deleted)
        //    {
        //        BlindfallSwitch();
        //        win.ShowDialog();
        //        if (win.DialogResult == true) UpdateAllTables();
        //        BlindfallSwitch();
        //    }
        //    else
        //    {
        //        BlindfallSwitch();
        //        win.DoneButton.Visibility = Visibility.Collapsed;
        //        win.RecoveryButton.Visibility = Visibility.Visible;
        //        win.ShowDialog();
        //        if (win.DialogResult == true) UpdateAllTables();
        //        BlindfallSwitch();

        //    }
        //}

        // // Открыть окно планшеты
        //private void TabletsCROpen(bool mode, string invnum, bool deleted)
        //{
        //    TabletsWindow win = new TabletsWindow(mode, invnum, deleted, EmpRedPerm, GrRedPerm);
        //    if (!deleted)
        //    {
        //        BlindfallSwitch();
        //        win.ShowDialog();
        //        if (win.DialogResult == true) UpdateAllTables();
        //        BlindfallSwitch();
        //    }
        //    else
        //    {
        //        BlindfallSwitch();
        //        win.DoneButton.Visibility = Visibility.Collapsed;
        //        win.RecoveryButton.Visibility = Visibility.Visible;
        //        win.ShowDialog();
        //        if (win.DialogResult == true) UpdateAllTables();
        //        BlindfallSwitch();
        //    }
        //}

        //// Открыть окно орг. техники
        //private void OrgTechCROpen(bool mode, string invnum, bool deleted)
        //{
        //    OrgTechWindow win = new OrgTechWindow(mode, invnum, deleted, EmpRedPerm, PCRedPerm, GrRedPerm);
        //    if (!deleted)
        //    {
        //        BlindfallSwitch();
        //        win.ShowDialog();
        //        if (win.DialogResult == true) UpdateAllTables();
        //        BlindfallSwitch();
        //    }
        //    else
        //    {
        //        BlindfallSwitch();
        //        win.DoneButton.Visibility = Visibility.Collapsed;
        //        win.RecoveryButton.Visibility = Visibility.Visible;
        //        win.ShowDialog();
        //        if (win.DialogResult == true) UpdateAllTables();
        //        BlindfallSwitch();
        //    }
        //}

        //// Открыть окно мониторов
        //private void MonitorCROpen(bool mode, string invnum, bool deleted)
        //{
        //    MonitorWindow win = new MonitorWindow(mode, invnum, deleted, EmpRedPerm, PCRedPerm, GrRedPerm);
        //    if (!deleted)
        //    {
        //        BlindfallSwitch();
        //        win.ShowDialog();
        //        if (win.DialogResult == true) UpdateAllTables();
        //        BlindfallSwitch();
        //    }
        //    else
        //    {
        //        BlindfallSwitch();
        //        win.DoneButton.Visibility = Visibility.Collapsed;
        //        win.RecoveryButton.Visibility = Visibility.Visible;
        //        win.ShowDialog();
        //        if (win.DialogResult == true) UpdateAllTables();
        //        BlindfallSwitch();
        //    }
        //}

        //// Открыть окно пользователей
        //private void UserCROpen(bool mode, string id)
        //{
        //    UsersWindow win = new UsersWindow(mode, id);
        //    BlindfallSwitch();
        //    win.ShowDialog();
        //    if (win.DialogResult == true) UpdateAllTables();
        //    BlindfallSwitch();
        //}

        //// Открыть окно ролей
        //private void RoleCROpen(bool mode, string id)
        //{
        //    RoleWindow win = new RoleWindow(mode, id);
        //    BlindfallSwitch();
        //    win.ShowDialog();
        //    if (win.DialogResult == true) UpdateAllTables();
        //    BlindfallSwitch();
        //}

        //// Открыть окно роутеров
        //private void RoutersCROpen(bool mode, string invnum, bool deleted)
        //{
        //    RoutersWindow win = new RoutersWindow(mode, invnum, deleted);
        //    if (!deleted)
        //    {
        //        BlindfallSwitch();
        //        win.ShowDialog();
        //        if (win.DialogResult == true) UpdateAllTables();
        //        BlindfallSwitch();
        //    }
        //    else
        //    {
        //        BlindfallSwitch();
        //        win.DoneButton.Visibility = Visibility.Collapsed;
        //        win.RecoveryButton.Visibility = Visibility.Visible;
        //        win.ShowDialog();
        //        if (win.DialogResult == true) UpdateAllTables();
        //        BlindfallSwitch();
        //    }
        //}

        //// Открыть окно роутеров
        //private void SwitchesCROpen(bool mode, string invnum, bool deleted)
        //{
        //    SwitchesWindow win = new SwitchesWindow(mode, invnum, deleted);
        //    if (!deleted)
        //    {
        //        BlindfallSwitch();
        //        win.ShowDialog();
        //        if (win.DialogResult == true) UpdateAllTables();
        //        BlindfallSwitch();
        //    }
        //    else
        //    {
        //        BlindfallSwitch();
        //        win.DoneButton.Visibility = Visibility.Collapsed;
        //        win.RecoveryButton.Visibility = Visibility.Visible;
        //        win.ShowDialog();
        //        if (win.DialogResult == true) UpdateAllTables();
        //        BlindfallSwitch();
        //    }
        //}

        //// Открыть окно типов мебели
        //private void FurnitureTypeCROpen(bool mode, string id)
        //{
        //    FurnitureTypeWindow win = new FurnitureTypeWindow(mode, id);

        //    BlindfallSwitch();
        //    win.ShowDialog();
        //    if (win.DialogResult == true) UpdateAllTables();
        //    BlindfallSwitch();
        //}

        //// Открыть окно мебели
        //private void FurnitureCROpen(bool mode, string invnum, bool deleted)
        //{
        //    FurnitureWindow win = new FurnitureWindow(mode, invnum, deleted);
        //    if (!deleted)
        //    {
        //        BlindfallSwitch();
        //        win.ShowDialog();
        //        if (win.DialogResult == true) UpdateAllTables();
        //        BlindfallSwitch();
        //    }
        //    else
        //    {
        //        BlindfallSwitch();
        //        win.DoneButton.Visibility = Visibility.Collapsed;
        //        win.RecoveryButton.Visibility = Visibility.Visible;
        //        win.ShowDialog();
        //        if (win.DialogResult == true) UpdateAllTables();
        //        BlindfallSwitch();
        //    }
        //}
        //#endregion

        //#region Запрет сотритровки

        //// Логи дата
        //private void LogsSorting(object sender, DataGridSortingEventArgs e)
        //{
        //    DataGridColumn column = e.Column;
        //    if (column.Header.ToString() == "Дата")
        //        e.Handled = true;
        //}

        //// Избранное дата
        //private void FavSorting(object sender, DataGridSortingEventArgs e)
        //{
        //    DataGridColumn column = e.Column;
        //    if (column.Header.ToString() == "Дата")
        //        e.Handled = true;
        //}

        //// IP
        //private void IPSorting(object sender, DataGridSortingEventArgs e)
        //{
        //    DataGridColumn column = e.Column;
        //    if (column.Header.ToString() == "IP")
        //        e.Handled = true;
        //}
        //#endregion

        //#region Роли

        //private void RoleCheck()
        //{
        //    DataTable table = new DataTable();
        //    string roleID = MigApp.Properties.Settings.Default.UserRole;
        //    table = sqlcc.DataGridUpdate("*", "Roles", $"WHERE ID LIKE '{roleID}'");
        //    DataRow row = table.Rows[0];
        //    if (row["EmpVis"].ToString() == "True" || row["AdminMode"].ToString() == "True")
        //    {
        //        EmployeesGroup.Visibility = Visibility.Visible;
        //        PersonsGroup.Visibility = Visibility.Visible;
        //        EmpRead = true;
        //    }
        //    if (row["EmpRed"].ToString() == "True" || row["AdminMode"].ToString() == "True")
        //        EmpRedPerm = true;

        //    if (row["GroupVis"].ToString() == "True" || row["AdminMode"].ToString() == "True")
        //    {
        //        EmployeesGroup.Visibility = Visibility.Visible;
        //        GroupsGroup.Visibility = Visibility.Visible;
        //        GrRead = true;
        //    }
        //    if (row["GroupRed"].ToString() == "True" || row["AdminMode"].ToString() == "True")
        //        GrRedPerm = true;

        //    if (row["PCVis"].ToString() == "True" || row["AdminMode"].ToString() == "True")
        //    {
        //        TechGroup.Visibility = Visibility.Visible;
        //        ComputersGroup.Visibility = Visibility.Visible;
        //        PCReport.Visibility = Visibility.Visible;
        //        IPReport.Visibility = Visibility.Visible;
        //        ReportsGroup.Visibility = Visibility.Visible;
        //        PCRead = true;
        //    }
        //    if (row["PCRed"].ToString() == "True" || row["AdminMode"].ToString() == "True")
        //        PCRedPerm = true;

        //    if (row["NoteVis"].ToString() == "True" || row["AdminMode"].ToString() == "True")
        //    {
        //        TechGroup.Visibility = Visibility.Visible;
        //        NotebookGroup.Visibility = Visibility.Visible;
        //        ReportsGroup.Visibility = Visibility.Visible;
        //        NBReport.Visibility = Visibility.Visible;
        //        IPReport.Visibility = Visibility.Visible;
        //        NbRead = true;
        //    }
        //    if (row["NoteRed"].ToString() == "True" || row["AdminMode"].ToString() == "True")
        //        NbRedPerm = true;

        //    if (row["TabVis"].ToString() == "True" || row["AdminMode"].ToString() == "True")
        //    {
        //        TechGroup.Visibility = Visibility.Visible;
        //        TabletGroup.Visibility = Visibility.Visible;
        //        ReportsGroup.Visibility = Visibility.Visible;
        //        TabReport.Visibility = Visibility.Visible;
        //        IPReport.Visibility = Visibility.Visible;
        //        TabRead = true;
        //    }
        //    if (row["TabRed"].ToString() == "True" || row["AdminMode"].ToString() == "True")
        //        TabRedPerm = true;

        //    if (row["OTVis"].ToString() == "True" || row["AdminMode"].ToString() == "True")
        //    {
        //        TechGroup.Visibility = Visibility.Visible;
        //        OTGroup.Visibility = Visibility.Visible;
        //        ReportsGroup.Visibility = Visibility.Visible;
        //        PCReport.Visibility = Visibility.Visible;
        //        IPReport.Visibility = Visibility.Visible;
        //        OTRead = true;
        //    }
        //    if (row["OTRed"].ToString() == "True" || row["AdminMode"].ToString() == "True")
        //        OTRedPerm = true;

        //    if (row["MonVis"].ToString() == "True" || row["AdminMode"].ToString() == "True")
        //    {
        //        TechGroup.Visibility = Visibility.Visible;
        //        MonitorGroup.Visibility = Visibility.Visible;
        //        ReportsGroup.Visibility = Visibility.Visible;
        //        PCReport.Visibility = Visibility.Visible;
        //        MonRead = true;
        //    }
        //    if (row["MonRed"].ToString() == "True" || row["AdminMode"].ToString() == "True")
        //        MonRedPerm = true;

        //    if (row["RoutVis"].ToString() == "True" || row["AdminMode"].ToString() == "True")
        //    {
        //        TechGroup.Visibility = Visibility.Visible;
        //        RouterGroup.Visibility = Visibility.Visible;
        //        ReportsGroup.Visibility = Visibility.Visible;
        //        IPReport.Visibility = Visibility.Visible;
        //        RoutRead = true;
        //    }
        //    if (row["RoutRed"].ToString() == "True" || row["AdminMode"].ToString() == "True")
        //        RoutRedPerm = true;

        //    if (row["SwitchVis"].ToString() == "True" || row["AdminMode"].ToString() == "True")
        //    {
        //        TechGroup.Visibility = Visibility.Visible;
        //        SwitchesGroup.Visibility = Visibility.Visible;
        //        ReportsGroup.Visibility = Visibility.Visible;
        //        IPReport.Visibility = Visibility.Visible;
        //        SwitchRead = true;
        //    }
        //    if (row["SwitchRed"].ToString() == "True" || row["AdminMode"].ToString() == "True")
        //        SwitchRedPerm = true;

        //    if (row["FurnVis"].ToString() == "True" || row["AdminMode"].ToString() == "True")
        //    {
        //        FurnitureGroup.Visibility = Visibility.Visible;
        //        FurnRead = true;
        //    }
        //    if (row["FurnRed"].ToString() == "True" || row["AdminMode"].ToString() == "True")
        //        FurnRedPerm = true;

        //    if (row["AdminMode"].ToString() == "True")
        //    {
        //        AdminGroup.Visibility = Visibility.Visible;
        //        DeletedGroup.Visibility = Visibility.Visible;
        //        Admin = true;
        //    }
        //}

        //#endregion

        //#region Exel Экспорт

        //// Экспорт отчёта ПК
        //private void ExportReportPC(object sender, RoutedEventArgs e)
        //{
        //    mc.ExcelExport(ReportPC);
        //}

        //// Экспорт отчёта Ноутбуков
        //private void ExportReportNB(object sender, RoutedEventArgs e)
        //{
        //    mc.ExcelExport(ReportNB);
        //}

        //// Экспорт отчёта Планшетов
        //private void ExportReportTab(object sender, RoutedEventArgs e)
        //{
        //    mc.ExcelExport(ReportTab);
        //}

        //// Экспорт отчёта IP
        //private void ExportReportIP(object sender, RoutedEventArgs e)
        //{
        //    mc.ExcelExport(ReportIP);
        //}

        //#endregion

        //#region Счётчики

        //private void SelectedCounter(object sender, SelectedCellsChangedEventArgs e)
        //{
        //    // Пользовательские таблицы
        //    if (sender.Equals(FavTable))
        //        FavSelectedCount.Text = "Выбрано: " + FavTable.SelectedItems.Count.ToString();
        //    else if (sender.Equals(EmployeeTable))
        //        EmpSelectedCount.Text = "Выбрано: " + EmployeeTable.SelectedItems.Count.ToString();
        //    else if (sender.Equals(GroupsTable))
        //        GrSelectedCount.Text = "Выбрано: " + GroupsTable.SelectedItems.Count.ToString();
        //    else if (sender.Equals(PCTable))
        //        PCSelectedCount.Text = "Выбрано: " + PCTable.SelectedItems.Count.ToString();
        //    else if (sender.Equals(NotebookTable))
        //        NBSelectedCount.Text = "Выбрано: " + NotebookTable.SelectedItems.Count.ToString();
        //    else if (sender.Equals(TabletsTable))
        //        TabSelectedCount.Text = "Выбрано: " + TabletsTable.SelectedItems.Count.ToString();
        //    else if (sender.Equals(PrintersTable))
        //        OTSelectedCount.Text = "Выбрано: " + PrintersTable.SelectedItems.Count.ToString();
        //    else if (sender.Equals(MonitorsTable))
        //        MonSelectedCount.Text = "Выбрано: " + MonitorsTable.SelectedItems.Count.ToString();
        //    else if (sender.Equals(RoutersTable))
        //        RoutSelectedCount.Text = "Выбрано: " + RoutersTable.SelectedItems.Count.ToString();
        //    else if (sender.Equals(SwitchesTable))
        //        SwitchSelectedCount.Text = "Выбрано: " + SwitchesTable.SelectedItems.Count.ToString();
        //    else if (sender.Equals(FurnTypeTable))
        //        FurnTypeSelectedCount.Text = "Выбрано: " + FurnTypeTable.SelectedItems.Count.ToString();
        //    else if (sender.Equals(FurnTable))
        //        FurnSelectedCount.Text = "Выбрано: " + FurnTable.SelectedItems.Count.ToString();

        //    // Отчёты
        //    else if (sender.Equals(ReportPC))
        //        PCRepSelectedCount.Text = "Выбрано: " + ReportPC.SelectedItems.Count.ToString();
        //    else if (sender.Equals(ReportNB))
        //        NBRepSelectedCount.Text = "Выбрано: " + ReportNB.SelectedItems.Count.ToString();
        //    else if (sender.Equals(ReportTab))
        //        TabRepSelectedCount.Text = "Выбрано: " + ReportTab.SelectedItems.Count.ToString();
        //    else if (sender.Equals(ReportIP))
        //        IPRepSelectedCount.Text = "Выбрано: " + ReportIP.SelectedItems.Count.ToString();

        //    // Админпанель
        //    else if (sender.Equals(UsersTable))
        //        UserSelectedCount.Text = "Выбрано: " + UsersTable.SelectedItems.Count.ToString();
        //    else if (sender.Equals(RolesTable))
        //        RoleSelectedCount.Text = "Выбрано: " + RolesTable.SelectedItems.Count.ToString();
        //    else if (sender.Equals(LogsTable))
        //        LogSelectedCount.Text = "Выбрано: " + LogsTable.SelectedItems.Count.ToString();

        //    // Архив
        //    else if (sender.Equals(EmployeesDeleted))
        //        EmpDelSelectedCount.Text = "Выбрано: " + EmployeesDeleted.SelectedItems.Count.ToString();
        //    else if (sender.Equals(PCDeleted))
        //        PCDelSelectedCount.Text = "Выбрано: " + PCDeleted.SelectedItems.Count.ToString();
        //    else if (sender.Equals(NotebookDeleted))
        //        NBDelSelectedCount.Text = "Выбрано: " + NotebookDeleted.SelectedItems.Count.ToString();
        //    else if (sender.Equals(TabletsDeleted))
        //        TabDelSelectedCount.Text = "Выбрано: " + TabletsDeleted.SelectedItems.Count.ToString();
        //    else if (sender.Equals(OrgTechDeleted))
        //        OTDelSelectedCount.Text = "Выбрано: " + OrgTechDeleted.SelectedItems.Count.ToString();
        //    else if (sender.Equals(MonitorsDeleted))
        //        MonDelSelectedCount.Text = "Выбрано: " + MonitorsDeleted.SelectedItems.Count.ToString();
        //    else if (sender.Equals(RoutersDeleted))
        //        RoutDelSelectedCount.Text = "Выбрано: " + RoutersDeleted.SelectedItems.Count.ToString();
        //    else if (sender.Equals(SwitchesDeleted))
        //        SwitchDelSelectedCount.Text = "Выбрано: " + SwitchesDeleted.SelectedItems.Count.ToString();
        //    else if (sender.Equals(FurnDeleted))
        //        FurnDelSelectedCount.Text = "Выбрано: " + FurnDeleted.SelectedItems.Count.ToString();
        //}

        //#endregion

        //// Затемнить окно
        //private void BlindfallSwitch()
        //{
        //    if (Blindfall.Visibility == Visibility.Collapsed)
        //        Blindfall.Visibility = Visibility.Visible;
        //    else
        //        Blindfall.Visibility = Visibility.Collapsed;
        //}

        //// Кнопка возврата к окну авторизации
        //private void ExitClick(object sender, RoutedEventArgs e)
        //{
        //    LoginWindow win = new LoginWindow();
        //    mc.ClearFilters();
        //    win.Show(); Close();
        //}

        //private void Manual_Open(object sender, RoutedEventArgs e)
        //{
        //    Process.Start(@"https://vanilla76e2.github.io/MigApp_Manual/");
        //}

        //private async void ReloadTables(object sender, RoutedEventArgs e)
        //{
        //    UpdateAllTables();
        //    ReloadButton.IsEnabled = false;
        //    await Task.Delay(5000);
        //    ReloadButton.IsEnabled = true;
        //}

        //private void HotKeys(object sender, System.Windows.Input.KeyEventArgs e)
        //{
        //    if (e.Key == Key.F1)
        //    {
        //        e.Handled = true;
        //        Process.Start(@"https://vanilla76e2.github.io/MigApp_Manual/");
        //    }
        //    else if (e.Key == Key.F5)
        //    {
        //        e.Handled = true;
        //        ReloadTables(sender, e);
        //    }
        //}

        #endregion

        #region CastomUI

        // Close navigation menu
        private void CustomUI_CloseMenu(object sender, MouseButtonEventArgs e)
        {
            AdminButton.IsChecked = false;
            MenuButton.IsChecked = false;
            ArchiveButton.IsChecked = false;
            e.Handled = true;
        }

        // Header control buttons
        private void CustomUI_WindowControl(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(Custom_MinimizeButton))
                this.WindowState = WindowState.Minimized;
            else if (sender.Equals(Custom_MaximizeButton))
                if (this.WindowState == WindowState.Maximized)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Maximized;
            else if (sender.Equals(Custom_CloseButton))
                Application.Current.Shutdown();
        }

        // Switch navigation menus
        private void CustomUI_MenuSwitch(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(MenuButton))
            {
                AdminButton.IsChecked = false;
                ArchiveButton.IsChecked = false;
                ArchiveMenu.Visibility = Visibility.Collapsed;
                AdminMenu.Visibility = Visibility.Collapsed;
                MainMenu.Visibility = Visibility.Visible;
            }
            else if (sender.Equals(AdminButton))
            {
                MenuButton.IsChecked = false;
                ArchiveButton.IsChecked = false;
                MainMenu.Visibility = Visibility.Collapsed;
                ArchiveMenu.Visibility = Visibility.Collapsed;
                AdminMenu.Visibility = Visibility.Visible;
            }
            else if (sender.Equals(ArchiveButton))
            {
                MenuButton.IsChecked = false;
                AdminButton.IsChecked = false;
                MainMenu.Visibility = Visibility.Collapsed;
                AdminMenu.Visibility = Visibility.Collapsed;
                ArchiveMenu.Visibility = Visibility.Visible;
            }
        }

        // Switch tables
        private void CustiomUI_SwitchBorder(object sender, RoutedEventArgs e)
        {
            //if (sender.Equals(FavouriteButton))
            //{
            //    HideAllBorders();
            //    FavouriteBorder.Visibility = Visibility.Visible;
            //}
            //else if(sender.Equals(EmployeesBorder))
            //{
            //    HideAllBorders();
            //    EmployeesBorder.Visibility = Visibility.Visible;
            //}
        }

        private void HideAllBorders()
        {
            foreach(var item in MainGrid.Children)
            {
                if(item is Border)
                {
                    Border border = (Border)item;
                    border.Visibility = Visibility.Collapsed;
                }
            }
        }
        #endregion
    }
}