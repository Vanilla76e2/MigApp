using System;
using System.Data;
using System.Windows;

namespace MigApp.CRWindows.AdminPanel
{
    /// <summary>
    /// Логика взаимодействия для RoleWindow.xaml
    /// </summary>
    public partial class RoleWindow : Window
    {
        SQLConnectionClass sqlcc = SQLConnectionClass.getinstance();
        MiscClass mc = new MiscClass();
        DataTable table = new DataTable();
        string CurrentUser = MigApp.Properties.Settings.Default.userLogin;
        string ID;

        // true - Создание
        // false - Редактирование
        bool Mode;

        public RoleWindow(bool mode, string id)
        {
            InitializeComponent();
            Mode = mode;
            ID = id;
            Start(id);
        }

        // Нажатие кнопки "Сохранить"
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (RoleName.Text.Length > 0)
            {
                if (Convert.ToInt32(sqlcc.ReqRef($"SELECT COUNT(*) FROM Roles Where Name LIKE '{RoleName.Text}'")) < 1 || !Mode)
                {
                    if (Mode)
                    {
                        sqlcc.ReqNonRef($"INSERT INTO Roles (Name, EmpVis, EmpRed, GroupVis, GroupRed, PCVis, PCRed, NoteVis, NoteRed, TabVis, TabRed, OTVis, OTRed, MonVis, MonRed, RoutVis, RoutRed, SwitchVis, SwitchRed, FurnVis, FurnRed) Values ('{RoleName.Text}', " +
                            $"{mc.BoolToString(GroupRead.IsChecked)}, {mc.BoolToString(GroupRedact.IsChecked)}, " +
                            $"{mc.BoolToString(EmpRead.IsChecked)}, {mc.BoolToString(EmpRedact.IsChecked)}, " +
                            $"{mc.BoolToString(PCRead.IsChecked)}, {mc.BoolToString(PCRedact.IsChecked)}, " +
                            $"{mc.BoolToString(NotebookRead.IsChecked)}, {mc.BoolToString(NotebookRedact.IsChecked)}, " +
                            $"{mc.BoolToString(TabletsRead.IsChecked)}, {mc.BoolToString(TabletsRedact.IsChecked)}, " +
                            $"{mc.BoolToString(OrgTechRead.IsChecked)}, {mc.BoolToString(OrgTechRedact.IsChecked)}, " +
                            $"{mc.BoolToString(MonitorRead.IsChecked)}, {mc.BoolToString(MonitorRedact.IsChecked)}, " +
                            $"{mc.BoolToString(RoutRead.IsChecked)}, {mc.BoolToString(RoutRedact.IsChecked)}, " +
                            $"{mc.BoolToString(SwitchRead.IsChecked)}, {mc.BoolToString(SwitchRedact.IsChecked)}, " +
                            $"{mc.BoolToString(FurnRead.IsChecked)}, {mc.BoolToString(FurnRedact.IsChecked)})");
                        sqlcc.Loging(CurrentUser, "Создание", "Роли", RoleName.Text, "");
                    }
                    else
                    {
                        sqlcc.ReqNonRef($"UPDATE Roles SET Name = '{RoleName.Text}', EmpVis = {mc.BoolToString(EmpRead.IsChecked)}, EmpRed = {mc.BoolToString(EmpRedact.IsChecked)}, " +
                            $"GroupVis = {mc.BoolToString(GroupRead.IsChecked)}, GroupRed = {mc.BoolToString(GroupRedact.IsChecked)}, " +
                            $"PCVis = {mc.BoolToString(PCRead.IsChecked)}, PCRed = {mc.BoolToString(PCRedact.IsChecked)}, " +
                            $"NoteVis = {mc.BoolToString(NotebookRead.IsChecked)}, NoteRed = {mc.BoolToString(NotebookRedact.IsChecked)}, " +
                            $"TabVis = {mc.BoolToString(TabletsRead.IsChecked)}, TabRed = {mc.BoolToString(TabletsRedact.IsChecked)}, " +
                            $"OTVis = {mc.BoolToString(OrgTechRead.IsChecked)}, OTRed = {mc.BoolToString(OrgTechRedact.IsChecked)}, " +
                            $"MonVis = {mc.BoolToString(MonitorRead.IsChecked)}, MonRed = {mc.BoolToString(MonitorRedact.IsChecked)}, " +
                            $"RoutVis = {mc.BoolToString(RoutRead.IsChecked)}, RoutRed = {mc.BoolToString(RoutRedact.IsChecked)}, " +
                            $"SwitchVis = {mc.BoolToString(SwitchRead.IsChecked)}, SwitchRed = {mc.BoolToString(SwitchRedact.IsChecked)}, " +
                            $"FurnVis = {mc.BoolToString(FurnRead.IsChecked)}, FurnRed = {mc.BoolToString(FurnRedact.IsChecked)} WHERE ID LIKE {ID}");
                        sqlcc.Loging(CurrentUser, "Редактирование", "Роли", RoleName.Text, "");
                    }
                    DialogResult = true; Close();
                }
                else
                {
                    RoleName.Focus();
                    MessageBox.Show("Такая роль уже существует.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                RoleName.Focus();
                MessageBox.Show("Имя не заполнено!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Нажатие кнопки "Удалить"
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите удалить запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (ID != "1")
                {
                    sqlcc.ReqDel($"DELETE FROM Roles Where ID Like '{ID}'");
                    sqlcc.Loging(CurrentUser, "Удаление", "Роли", RoleName.Text, "");
                    DialogResult = true; Close();
                }
                else MessageBox.Show("Нельзя удалить администратора", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        #region Редактирование + Чтение

        // При включении редактирования
        private void EmpRedChecked(object sender, RoutedEventArgs e)
        {
            EmpRead.IsChecked = true;
            EmpRead.IsEnabled = false;
        }

        private void GroupRedChecked(object sender, RoutedEventArgs e)
        {
            GroupRead.IsChecked = true;
            GroupRead.IsEnabled = false;
        }

        private void PCRedChecked(object sender, RoutedEventArgs e)
        {
            PCRead.IsChecked = true;
            PCRead.IsEnabled = false;
        }

        private void NotebookRedChecked(object sender, RoutedEventArgs e)
        {
            NotebookRead.IsChecked = true;
            NotebookRead.IsEnabled = false;
        }

        private void TabletsRedChecked(object sender, RoutedEventArgs e)
        {
            TabletsRead.IsChecked = true;
            TabletsRead.IsEnabled = false;
        }

        private void OrgTechRedChecked(object sender, RoutedEventArgs e)
        {
            OrgTechRead.IsChecked = true;
            OrgTechRead.IsEnabled = false;
        }

        private void MonitorRedChecked(object sender, RoutedEventArgs e)
        {
            MonitorRead.IsChecked = true;
            MonitorRead.IsEnabled = false;
        }

        private void RoutRedChecked(object sender, RoutedEventArgs e)
        {
            RoutRead.IsChecked = true;
            RoutRead.IsEnabled = false;
        }

        private void SwitchRedChecked(object sender, RoutedEventArgs e)
        {
            SwitchRead.IsChecked = true;
            SwitchRead.IsEnabled = false;
        }

        private void FurnRedChecked(object sender, RoutedEventArgs e)
        {
            FurnRead.IsChecked = true;
            FurnRead.IsEnabled = false;
        }

        // При отключении редактирования
        private void EmpRedUnhecked(object sender, RoutedEventArgs e)
        {
            EmpRead.IsEnabled = true;
        }

        private void GroupRedUnhecked(object sender, RoutedEventArgs e)
        {
            GroupRead.IsEnabled = true;
        }

        private void PCRedUnhecked(object sender, RoutedEventArgs e)
        {
            PCRead.IsEnabled = true;
        }

        private void NotebookRedUnhecked(object sender, RoutedEventArgs e)
        {
            NotebookRead.IsEnabled = true;
        }

        private void TabletsRedUnhecked(object sender, RoutedEventArgs e)
        {
            TabletsRead.IsEnabled = true;
        }

        private void OrgTechRedUnhecked(object sender, RoutedEventArgs e)
        {
            OrgTechRead.IsEnabled = true;
        }

        private void MonitorRedUnhecked(object sender, RoutedEventArgs e)
        {
            MonitorRead.IsEnabled = true;
        }

        private void RoutRedUnhecked(object sender, RoutedEventArgs e)
        {
            RoutRead.IsEnabled = true;
        }

        private void SwitchRedUnhecked(object sender, RoutedEventArgs e)
        {
            SwitchRead.IsEnabled = true;
        }

        private void FurnRedUnhecked(object sender, RoutedEventArgs e)
        {
            FurnRead.IsEnabled = true;
        }
        #endregion

        // Заполнение полей и изменение названия окна
        private void Start(string ID)
        {
            if (Mode)
            {
                Title = "Роль (Создание)";
                DeleteButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                Title = "Роль (Редактирование)";
                table = sqlcc.DataGridUpdate("*", "Roles", $"Where ID Like '{ID}'");
                RoleName.Text = table.Rows[0].Field<string>("Name");
                EmpRead.IsChecked = table.Rows[0].Field<bool>("EmpVis");
                EmpRedact.IsChecked = table.Rows[0].Field<bool>("EmpRed");
                GroupRead.IsChecked = table.Rows[0].Field<bool>("GroupVis");
                GroupRedact.IsChecked = table.Rows[0].Field<bool>("GroupRed");
                PCRead.IsChecked = table.Rows[0].Field<bool>("PCVis");
                PCRedact.IsChecked = table.Rows[0].Field<bool>("PCRed");
                NotebookRead.IsChecked = table.Rows[0].Field<bool>("NoteVis");
                NotebookRedact.IsChecked = table.Rows[0].Field<bool>("NoteRed");
                TabletsRead.IsChecked = table.Rows[0].Field<bool>("TabVis");
                TabletsRedact.IsChecked = table.Rows[0].Field<bool>("TabRed");
                OrgTechRead.IsChecked = table.Rows[0].Field<bool>("OTVis");
                OrgTechRedact.IsChecked = table.Rows[0].Field<bool>("OTRed");
                MonitorRead.IsChecked = table.Rows[0].Field<bool>("MonVis");
                MonitorRedact.IsChecked = table.Rows[0].Field<bool>("MonRed");
                RoutRead.IsChecked = table.Rows[0].Field<bool>("RoutVis");
                RoutRedact.IsChecked = table.Rows[0].Field<bool>("RoutRed");
                SwitchRead.IsChecked = table.Rows[0].Field<bool>("SwitchVis");
                SwitchRedact.IsChecked = table.Rows[0].Field<bool>("SwitchRed");
                FurnRead.IsChecked = table.Rows[0].Field<bool>("FurnVis");
                FurnRedact.IsChecked = table.Rows[0].Field<bool>("FurnRed");
            }
        }
    }
}
