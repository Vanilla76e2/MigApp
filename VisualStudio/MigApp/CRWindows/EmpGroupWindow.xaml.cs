using System;
using System.Windows;
namespace MigApp.CRWindows
{
    /// <summary>
    /// Логика взаимодействия для EmpGroupWindow.xaml
    /// </summary>
    public partial class EmpGroupWindow : Window
    {
        SQLConnectionClass sqlcc = SQLConnectionClass.getinstance();
        string sqlTable = "[Group]", logname = "Отделы";
        string NAME;
        string CurrentUser = MigApp.Properties.Settings.Default.UserLogin;

        bool Mode = false;
        public EmpGroupWindow(bool mode, string name)
        {
            InitializeComponent();
            NAME = name;
            Mode = mode;
            Start();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (GroupName.Text.Length > 0)
            {
                if (Convert.ToInt32(sqlcc.ReqRef($"SELECT COUNT(*) FROM {sqlTable} Where Name LIKE '{GroupName.Text}'")) < 1)
                {   
                    if (Mode)
                    {
                        sqlcc.ReqNonRef($"INSERT INTO {sqlTable} (Name) Values ('{GroupName.Text}')");
                        sqlcc.Loging(CurrentUser, "Создание", logname, GroupName.Text, "");
                    }
                    else
                    {
                        sqlcc.ReqNonRef($"UPDATE {sqlTable} SET Name = '{GroupName.Text}' WHERE Name Like '{NAME}'");
                        sqlcc.Loging(CurrentUser, "Редактирование", logname, GroupName.Text, "");
                    }
                    DialogResult = true; Close();
                }
                else
                {
                    GroupName.Focus();
                    MessageBox.Show("Такой отдел уже существует.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            sqlcc.ReqDel($"Delete From {sqlTable} Where Name Like '{NAME}'");
            sqlcc.Loging(CurrentUser, "Стирание", logname, GroupName.Text, "");
            DialogResult = true; Close();
        }

        private void Start()
        {
            if (Mode)
            {
                Title = "Отдел (Создание)";
                DeleteButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                Title = "Отдел (Редактирование)";
                GroupName.Text = NAME;
            }
        }
    }
}
