using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для FurnitureTypeWindow.xaml
    /// </summary>
    public partial class FurnitureTypeWindow : Window
    {
        SQLConnectionClass sqlcc = SQLConnectionClass.getinstance();
        string sqlTable = "FurnitureType", logname = "Типы мебели";
        string ID;
        string CurrentUser = MigApp.Properties.Settings.Default.userLogin;

        bool Mode = false;

        public FurnitureTypeWindow(bool mode, string id)
        {
            InitializeComponent();
            ID = id;
            Mode = mode;
            Start();
        }
        
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (FurnTypeName.Text.Length > 0)
            {
                if (Convert.ToInt32(sqlcc.ReqRef($"SELECT COUNT(*) FROM {sqlTable} Where Name LIKE '{FurnTypeName.Text}'")) < 1)
                {   
                    if (Mode)
                    {
                        sqlcc.ReqNonRef($"INSERT INTO {sqlTable} (Name) Values ('{FurnTypeName.Text}')");
                        sqlcc.Loging(CurrentUser, "Создание", logname, FurnTypeName.Text, "");
                    }
                    else
                    {
                        sqlcc.ReqNonRef($"UPDATE {sqlTable} SET Name = '{FurnTypeName.Text}' WHERE ID LIKE {ID}");
                        sqlcc.Loging(CurrentUser, "Редактирование", logname, FurnTypeName.Text, "");
                    }
                    DialogResult = true; Close();
                }
                else
                {
                    FurnTypeName.Focus();
                    MessageBox.Show("Такой тип мебели уже существует.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            sqlcc.ReqDel($"Delete From {sqlTable} Where ID Like '{ID}'");
            sqlcc.Loging(CurrentUser, "Стирание", logname, FurnTypeName.Text, "");
            DialogResult = true; Close();
        }

        private void Start()
        {
            if (Mode)
            {
                Title = "Тип мебели (Создание)";
                DeleteButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                Title = "Тип мебели (Редактирование)";
                FurnTypeName.Text = sqlcc.ReqRef($"Select Name From {sqlTable} Where ID Like {ID}");
            }
        }
    }
}
