using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace MigApp
{
    internal class SQLConnectionClass
    {
        SqlConnection sqlConnection = new SqlConnection($@"Data Source = {MigApp.Properties.Settings.Default.Server}; Initial Catalog = {MigApp.Properties.Settings.Default.Database}; Integrated Security = false; User id = sa; Password = {MigApp.Properties.Settings.Default.DBPassword}");
        DataTable Table = new DataTable("");

        private static SQLConnectionClass instance;

        private SQLConnectionClass()
        {

        }

        public static SQLConnectionClass getinstance()
        {
            if (instance == null)
            {
                instance = new SQLConnectionClass();
            }
            return instance;
        }

        // Проверка подключения
        public bool SQLtest()
        {
            //SqlConnection sqlConnection = new SqlConnection($@"Data Source = {MigApp.Properties.Settings.Default.Server}; Initial Catalog = {MigApp.Properties.Settings.Default.Database}; Integrated Security = false; User id = sa; Password = {MigApp.Properties.Settings.Default.DBPassword}");

            sqlConnection.ConnectionString = $@"Data Source = {MigApp.Properties.Settings.Default.Server}; Initial Catalog = {MigApp.Properties.Settings.Default.Database}; Integrated Security = false; User id = sa; Password = {MigApp.Properties.Settings.Default.DBPassword}";
            try
            {
                sqlConnection.Open();
                sqlConnection.Close();
                return true;
            }
            catch { return false; }
        }

        // Запрос с возвратом
        public string ReqRef (string text)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand com = new SqlCommand(text, sqlConnection);
                var test = com.ExecuteScalar();
                if (test != null)
                {
                    return Convert.ToString(test);
                }
                else
                    return "";
            }
            catch 
            { 
                MessageBox.Show("Error ReqRef\nНе удалось выполнить запрос.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return "";
            }
            finally { sqlConnection.Close(); }
        }

        // Запрос без возврата
        public void ReqNonRef (string text)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand com = new SqlCommand(text, sqlConnection);
                com.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch { MessageBox.Show("Error ReqNonRef\nНе удалось выполнить запрос.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { sqlConnection.Close(); }
        }

        // Запрос на удаление
        public void ReqDel (string text)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand com = new SqlCommand(text, sqlConnection);
                com.ExecuteNonQuery();
            }
            catch
            { MessageBox.Show("Не удалось удалить поле!\nВозможно оно используется как вторичный ключ.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning); }
            finally { sqlConnection.Close(); }
        }

        // Запрос с возвратом таблицы
        public DataTable DataGridUpdate(string items, string table, string command)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand com = new SqlCommand($"SELECT {items} FROM {table} {command}", sqlConnection);
                com.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                Table = new DataTable($"{table}");
                adapter.Fill(Table);
                //sqlConnection.Close();
                return Table;
            }
            catch
            {
                MessageBox.Show($"Error DataGridUpdate\nНе удалось найти таблицу {table}.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            finally { sqlConnection.Close(); }
        }

        // Проверка занятости инвентарного номера
        public bool InvNumChecker (string InvNum)
        {
            if (Convert.ToUInt32(ReqRef($"SELECT COUNT(*) FROM Computers WHERE InvNum LIKE '{InvNum}'")) < 1 
                && Convert.ToUInt32(ReqRef($"SELECT COUNT(*) FROM Notebooks WHERE InvNum LIKE '{InvNum}'")) < 1 
                && Convert.ToUInt32(ReqRef($"SELECT COUNT(*) FROM Tablets WHERE InvNum LIKE '{InvNum}'")) < 1 
                && Convert.ToUInt32(ReqRef($"SELECT COUNT(*) FROM OrgTech WHERE InvNum LIKE '{InvNum}'")) < 1 
                && Convert.ToUInt32(ReqRef($"SELECT COUNT(*) FROM Monitor WHERE InvNum LIKE '{InvNum}'")) < 1)
                return true;
            else return false;
        }

        // Логирование 
        public void Loging(string CurrentUser, string Action, string Table, string Row, string Specifies)
        {
            ReqNonRef($"INSERT INTO Logs (ActionDate, [User], Action, [Table], Row, Specifies) Values ('{DateTime.Now}', '{CurrentUser}', '{Action}', '{Table}', '{Row}', '{Specifies}')");
        }

        // Восстановление
        public void Recovery(string table, string id_variant ,string id)
        {
            ReqNonRef($"UPDATE {table} SET Deleted = 0, DelDate = NULL WHERE {id_variant} LIKE '{id}'");
        }

        // Удаление сотрудника из архива
        public void Delete_DeletedEmployee(string ID)
        {
            if (ReqRef($"SELECT COUNT(*) FROM Computers WHERE User LIKE '{ID}'") != "0")
                ReqNonRef($"UPDATE Computers SET User = NULL WHERE User LIKE '{ID}'");
            if (ReqRef($"SELECT COUNT(*) FROM Notebooks WHERE User LIKE '{ID}'") != "0")
                ReqNonRef($"UPDATE Notebooks SET User = NULL WHERE User LIKE '{ID}'");
            if (ReqRef($"SELECT COUNT(*) FROM Tablets WHERE User LIKE '{ID}'") != "0")
                ReqNonRef($"UPDATE Tablets SET User = NULL WHERE User LIKE '{ID}'");
            ReqNonRef($"DELETE FROM Employees WHERE ID LIKE '{ID}'");
        }

        // Удаление ПК из архива
        public void Delete_DeletedPC(string invnum)
        {
            ReqNonRef($"UPDATE OrgTech SET PC = NULL WHERE PC LIKE '{invnum}' " +
                    $"UPDATE Monitor SET PC = NULL WHERE PC LIKE '{invnum}' " +
                    $"DELETE FROM Computers WHERE InvNum LIKE '{invnum}'");
        }
    }
}
