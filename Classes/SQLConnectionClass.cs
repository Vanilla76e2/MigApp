using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace MigApp
{
    internal class SQLConnectionClass
    {
        SqlConnection sqlConnection = new SqlConnection($@"Data Source = {MigApp.Properties.Settings.Default.Server}; Initial Catalog = {MigApp.Properties.Settings.Default.Database}; Integrated Security = True");
        DataTable Table = new DataTable("");

        public bool SQLtest()
        {
            sqlConnection.ConnectionString = $@"Data Source = {MigApp.Properties.Settings.Default.Server}; Initial Catalog = {MigApp.Properties.Settings.Default.Database}; Integrated Security = True";
            try
            {
                sqlConnection.Open();
                sqlConnection.Close();
                return true;
            }
            catch { return false; }
        }

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
                MessageBox.Show("Error ReqRef", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return "";
            }
            finally { sqlConnection.Close(); }
        }

        public void ReqNonRef (string text)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand com = new SqlCommand (text, sqlConnection);
                com.ExecuteNonQuery();
            }
            catch { MessageBox.Show("Error ReqNonRef\nНе удалось выполнить запрос.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { sqlConnection.Close(); }
        }

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

        public DataTable DataGridUpdate(string entities, string table, string command)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand com = new SqlCommand($"SELECT {entities} FROM {table} {command}", sqlConnection);
                com.ExecuteNonQuery ();
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                Table = new DataTable($"{table}");
                adapter.Fill(Table);
                return Table;
            }
            catch
            {
                MessageBox.Show($"Error DataGridUpdate\nНе удалось найти таблицу {table}.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            finally { sqlConnection.Close(); }
        }
    }
}
