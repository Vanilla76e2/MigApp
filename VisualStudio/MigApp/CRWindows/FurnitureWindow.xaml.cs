using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MigApp.CRWindows
{
    /// <summary>
    /// Логика взаимодействия для FurnitureWindow.xaml
    /// </summary>
    public partial class FurnitureWindow : Window
    {
        SQLConnectionClass sqlcc = SQLConnectionClass.getinstance();
        MiscClass mc = new MiscClass();
        DataTable table = new DataTable();
        string sqlTable = "Furniture", logname = "Мебель";
        string CurrentUser = MigApp.Properties.Settings.Default.UserLogin;
        string InventoryNum;
        bool Deleted;

        // true - Создание
        // false - Редактирование
        bool Mode;

        public FurnitureWindow(bool mode, string invnum, bool deleted)
        {
            InitializeComponent();
            Mode = mode;
            InventoryNum = invnum;
            Deleted = deleted;
            Start(invnum);
        }

        // Нажатие кнопки "Сохранить"
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            // Проверка заполнения
            if (InvNum.Text.Length > 0 && Type.Text.Length > 0 && FurnitureName.Text.Length > 0 && Room.Text.Length > 0)
            {
                if (InvNumChecker(InvNum.Text) || !Mode)
                {
                    // Если создание
                    if (Mode == true)
                    {
                        sqlcc.ReqNonRef($"INSERT INTO {sqlTable} (InvNum, Type, Name, Room, Comment) Values ('{InvNum.Text}', (Select ID From FurnitureType Where Name Like '{Type.Text}'), '{FurnitureName.Text}', '{Room.Text}', '{Comment.Text}')");
                        sqlcc.Loging(CurrentUser, "Создание", logname, InvNum.Text, FurnitureName.Text);
                    }
                    // Если редактирование
                    else
                    {
                        sqlcc.ReqNonRef($"UPDATE {sqlTable} SET InvNum = '{InvNum.Text}', Type = (Select ID From FurnitureType Where Name Like '{Type.Text}'), Name = '{FurnitureName.Text}', Room = '{Room.Text}', Comment = '{Comment.Text}' Where InvNum LIKE '{InventoryNum}'");
                        sqlcc.Loging(CurrentUser, "Редактирование", logname, InvNum.Text, FurnitureName.Text);
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
                if (FurnitureName.Text.Length < 1) FurnitureName.BorderBrush = Brushes.Red;
                else FurnitureName.BorderBrush= Brushes.LightGray;
                if (Room.Text.Length < 1) Room.BorderBrush = Brushes.Red;
                else Room.BorderBrush = Brushes.LightGray;
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
                    sqlcc.Loging(CurrentUser, "Удаление", logname, InventoryNum, FurnitureName.Text);
                    DialogResult = true; Close();
                }
            }
            else
            {
                if (MessageBox.Show("Запись будет безвозвратно удалена.\nХотите удалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    sqlcc.ReqDel($"DELETE FROM {sqlTable} WHERE InvNum Like '{InventoryNum}'");
                    sqlcc.Loging(CurrentUser, "Стирание", logname, InventoryNum, FurnitureName.Text);
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
                sqlcc.Loging(CurrentUser, "Восстановление", logname, InventoryNum, FurnitureName.Text);
                DialogResult = true; Close();
            }
            catch { }
        }

        private void ListFill()
        {
            typeList.Clear();
            DataTable table = new DataTable();
            table = sqlcc.DataGridUpdate("*", "FurnitureType_View", "");
            foreach (DataRow row in table.Rows)
            {
                typeList.Add(row["Наименование"].ToString());
            }
            Type.ItemsSource = null;
            Type.ItemsSource = typeList;
        }

        private List<string> typeList = new List<string>();

        // Заполнение полей и изменение названия окна
        private void Start(string Invnum)
        {
            ListFill();
            if (Mode)
            {
                Title = "Мебель (Создание)";
                DeleteButton.Visibility = Visibility.Collapsed;
            }
            else if (!Mode && !Deleted)
            {
                try
                {
                    Title = "Мебель (Редактирование)";
                    InvNum.Text = Invnum;
                    table = sqlcc.DataGridUpdate("*", "Furniture_View", $"WHERE [Инвентарный номер] Like '{Invnum}'");
                    DataRow row = table.Rows[0];
                    Type.SelectedValue = row["Тип"].ToString();
                    FurnitureName.Text = row["Наименование"].ToString();
                    Room.Text = row["Расположение"].ToString();
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
                    Title = "Мебель (Архив)";
                    InvNum.Text = Invnum;
                    table = sqlcc.DataGridUpdate("*", "Furniture_Deleted", $"WHERE [Инвентарный номер] Like '{Invnum}'");
                    DataRow row = table.Rows[0];
                    Type.Text = row["Тип"].ToString();
                    FurnitureName.Text = row["Наименование"].ToString();
                    Room.Text = row["Расположение"].ToString();
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
            Type.IsReadOnly = true;
            FurnitureName.IsReadOnly = true;
            Room.IsReadOnly = true;
            Comment.IsReadOnly = true;
        }

        private void CreateNewType(object sender, RoutedEventArgs e)
        {
            FurnitureTypeWindow win = new FurnitureTypeWindow(true, null);
            win.ShowDialog();
            if (win.DialogResult == true)
            {
                ListFill();
                Type.SelectedIndex = Type.Items.Count - 1;
            }
        }

        private bool InvNumChecker(string invnum)
        {
            if (Convert.ToUInt32(sqlcc.ReqRef($"SELECT COUNT(*) FROM {sqlTable} WHERE InvNum LIKE '{invnum}' AND Deleted = 1")) > 0)
            {
                sqlcc.ReqNonRef($"DELETE FROM {sqlTable} WHERE InvNum LIKE '{invnum}'");
                sqlcc.Loging(CurrentUser, "Пересоздание", "Мебель", $"{invnum}", $"{FurnitureName.Text}");
                return true;
            }
            else if (Convert.ToUInt32(sqlcc.ReqRef($"SELECT COUNT(*) FROM {sqlTable} WHERE InvNum LIKE '{invnum}' AND Deleted = 0")) > 0) 
                return false;
            else
                return true;
        }
    }
}
