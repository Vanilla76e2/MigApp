﻿using MigApp.CRWindows.AdminPanel;
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
    /// Логика взаимодействия для TabletsWindow.xaml
    /// </summary>
    public partial class TabletsWindow : Window
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

        public TabletsWindow(bool mode, string invnum, bool deleted)
        {
            InitializeComponent();
            Mode = mode;
            InvNum.Focus();
            InventoryNum = invnum;
            Deleted = deleted;
            Start(invnum);
        }

        // Нажатие кнопки "Сохранить"
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            // Проверка заполнения
            if (InvNum.Text.Length > 0 && Model.Text.Length > 0 && SeriaNum.Text.Length > 0 && ScreenDiagonal.Text.Length > 0 && Processor.Text.Length > 0 && RAM.Text.Length > 0 && Drive.Text.Length > 0)
            {
                if (sqlcc.InvNumChecker(InvNum.Text) || !Mode)
                {
                    // Если создание
                    if (Mode == true)
                    {
                        sqlcc.ReqNonRef($"INSERT INTO Tablets (InvNum, Model, SNum, [User], OS, Diagonal, Processor, RAM, Drive, Other) Values ('{InvNum.Text}', '{Model.Text}', '{SeriaNum.Text}', (SELECT ID FROM Employees WHERE FIO LIKE '{User.Text}'), '{OS.Text}', '{ScreenDiagonal.Text}', '{Processor.Text}', '{RAM.Text}', '{Drive.Text}', '{Other.Text}')");
                        sqlcc.Loging(CurrentUser, "Создание", "Планшеты", InvNum.Text, "");
                    }
                    // Если редактирование
                    else
                    {
                        sqlcc.ReqNonRef($"UPDATE Tablets SET Model = '{Model.Text}', SNum = '{SeriaNum.Text}', [User] = (SELECT ID FROM Employees WHERE FIO LIKE '{User.Text}'), OS = '{OS.Text}', Diagonal = '{ScreenDiagonal.Text}', Processor = '{Processor.Text}', RAM = '{RAM.Text}', Drive = '{Drive.Text}', Other = '{Other.Text}' Where InvNum LIKE '{InvNum.Text}'");
                        sqlcc.Loging(CurrentUser, "Редактирование", "Планшеты", InvNum.Text, "");
                    }
                    DialogResult = true; Close();
                }
                else
                {
                    InvNum.Focus();
                    MessageBox.Show("Инвентарный номер уже занят.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            // Если не заполнены обязательные поля
            else
            {
                if (InvNum.Text.Length < 1) InvNum.BorderBrush = Brushes.Red;
                if (Model.Text.Length < 1) Model.BorderBrush = Brushes.Red;
                if (SeriaNum.Text.Length < 1) SeriaNum.BorderBrush = Brushes.Red;
                if (ScreenDiagonal.Text.Length < 1) ScreenDiagonal.BorderBrush = Brushes.Red;
                if (Processor.Text.Length < 1) Processor.BorderBrush = Brushes.Red;
                if (RAM.Text.Length < 1) RAM.BorderBrush = Brushes.Red;
                if (Drive.Text.Length < 1) Drive.BorderBrush = Brushes.Red;
                MessageBox.Show("Не все обязательные поля заполнены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Нажатие кнопки "Удалить"
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите удалить запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (!Deleted)
                {
                    sqlcc.ReqDel($"UPDATE Tablets SET Deleted = 1 WHERE InvNum LIKE '{InventoryNum}'");
                    sqlcc.Loging(CurrentUser, "Удаление", "Планшеты", InvNum.Text, "");
                    DialogResult = true; Close();
                }
                else
                {
                    sqlcc.ReqDel($"DELETE FROM Tablets WHERE InvNum LIKE '{InventoryNum}'");
                    sqlcc.Loging(CurrentUser, "Стирание", "Планшеты", InventoryNum, "");
                    DialogResult = true; Close();
                }
            }
        }

        // Нажатие кнопки "Восстановить"
        private void RecoveryClick(object sender, RoutedEventArgs e)
        {
            sqlcc.Recovery("Tablets", "InvNum", InventoryNum);
            sqlcc.Loging(CurrentUser, "Восстановление", "Планшеты", InventoryNum, "");
            DialogResult = true; Close();
        }

        #region User ComboBox

        private void ListFill()
        {
            DataTable table = new DataTable();
            table = sqlcc.DataGridUpdate("FIO", "Employees", "WHERE Deleted = 0");
            foreach (DataRow row in table.Rows)
            {
                userList.Add(row["FIO"].ToString());
            }
            User.ItemsSource = userList;
        }

        private List<string> userList = new List<string>();

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
            ListFill();
            if (Mode)
            {
                Title = "Планшет (Создание)";
                DeleteButton.Visibility = Visibility.Collapsed;
            }
            else if (!Mode && !Deleted)
            {
                try
                {
                    InvNum.IsReadOnly = true;
                    Title = "Планшет (Редактирование)";
                    InvNum.Text = Invnum;
                    table = sqlcc.DataGridUpdate("*", "Tablet_View", $"WHERE [Инвентарный номер] Like '{Invnum}'");
                    DataRow row = table.Rows[0];
                    Model.Text = row["Модель"].ToString();
                    SeriaNum.Text = row["Серийный номер"].ToString();
                    User.Text = row["Пользователь"].ToString();
                    OS.Text = row["ОС"].ToString();
                    ScreenDiagonal.Text = row["Диагональ экрана"].ToString();
                    Processor.Text = row["Процессор"].ToString();
                    RAM.Text = row["ОЗУ"].ToString();
                    Drive.Text = row["Накопители"].ToString();
                    Other.Text = row["Другое"].ToString();
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
                    Title = "Планшет (Архив)";
                    InvNum.Text = Invnum;
                    table = sqlcc.DataGridUpdate("*", "Tablet_Deleted", $"WHERE [Инвентарный номер] Like '{Invnum}'");
                    DataRow row = table.Rows[0];
                    Model.Text = row["Модель"].ToString();
                    SeriaNum.Text = row["Серийный номер"].ToString();
                    User.Text = row["Пользователь"].ToString();
                    OS.Text = row["ОС"].ToString();
                    ScreenDiagonal.Text = row["Диагональ экрана"].ToString();
                    Processor.Text = row["Процессор"].ToString();
                    RAM.Text = row["ОЗУ"].ToString();
                    Drive.Text = row["Накопители"].ToString();
                    Other.Text = row["Другое"].ToString();
                }
                catch
                {
                    MessageBox.Show("Запись не найдена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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

        private void NumOnly(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void LockAll()
        {
            Model.IsReadOnly = true;
            SeriaNum.IsReadOnly = true;
            User.IsEnabled = false;
            OS.IsReadOnly = true;
            ScreenDiagonal.IsReadOnly = true;
            Processor.IsReadOnly = true;
            RAM.IsReadOnly = true;
            Drive.IsReadOnly = true;
            Other.IsReadOnly = true;
        }
    }
}