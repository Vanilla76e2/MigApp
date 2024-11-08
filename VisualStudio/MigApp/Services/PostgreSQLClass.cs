using Npgsql;
using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace MigApp.Services
{
    internal class PostgreSQLClass
    {
        string connectionString = "Server=localhost; port=5432; user id=MigApp; password=migPass2024; database=MigDataBase;"; // Заменить на динамические параметры
        NpgsqlConnection pgCon;

        private PostgreSQLClass()
        {

        }

        private static PostgreSQLClass instance;

        public static PostgreSQLClass getinstance()
        {
            if (instance == null)
            {
                instance = new PostgreSQLClass();
            }
            return instance;
        }

        private async Task connection()
        {
            try
            {
                pgCon = new NpgsqlConnection(connectionString);
                if (pgCon.State == ConnectionState.Closed)
                {
                    await pgCon.OpenAsync();
                    Console.WriteLine($"connection: Статус подключения = {pgCon.State.ToString()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"connection: Ошибка при подключении к PostgreSQL: {ex.Message}");
            }
        }

        // Проверка подключения
        public async Task<bool> ConnectionTest()
        {
            Console.WriteLine("\nConnectionTest: Начата проверка подключения к PostgreSQL");
            connectionString = "Server=localhost; port=5432; user id=MigApp; password=migPass2024; database=MigDataBase;"; // Заменить на динамические параметры
            try
            {
                pgCon = new NpgsqlConnection(connectionString);
                await pgCon.OpenAsync();
                Console.WriteLine($"ConnectionTest: Статус подключения = {pgCon.State.ToString()}");
                pgCon.Dispose();
                Console.WriteLine($"ConnectionTest: Результат проверки: успех\nConnectionTest: Статус подключения = {pgCon.State.ToString()}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ConnectionTest: Результат проверки: {ex.Message}");
                return false; 
            }
        }

        // Запрос с возвратом
        public async Task<string> ReqRef(string text)
        {
            try
            {
                Console.WriteLine("\nReqRef: Отправлен запрос с возвратом");
                await connection();
                Console.WriteLine($"ReqRef: Отправлен запрос '{text}'");
                NpgsqlCommand com = new NpgsqlCommand(text, pgCon);
                var test = await com.ExecuteScalarAsync();
                if (test != null)
                {
                    return Convert.ToString(test);
                }
                else
                {
                    Console.WriteLine("ReqRef: Результат выполнения пустой");
                    return "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ReqRef:Ошибка при выполнении запроса: {ex.Message}");
                MessageBox.Show("Error ReqRef\nНе удалось выполнить запрос.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return "";
            }
            finally 
            { 
                pgCon.Dispose();
                Console.WriteLine($"ReqRef: Статус подключения = {pgCon.State.ToString()}");
            }
        }

        // Запрос без возврата
        public async Task ReqNonRef(string text)
        {
            try
            {
                Console.WriteLine("\nReqNonRef: Отправлен запрос без возврата");
                await connection();
                NpgsqlCommand com = new NpgsqlCommand(text, pgCon);
                await com.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"ReqNonRef: Ошибка при выполнении запроса: {ex.Message}");
                MessageBox.Show("Error ReqNonRef\nНе удалось выполнить запрос.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); 
            }
            finally 
            { 
                pgCon.Dispose();
                Console.WriteLine($"ReqNonRef: Статус подключения = {pgCon.State.ToString()}");
            }
        }

        // Запрос на удаление
        public async Task ReqDel(string text)
        {
            try
            {
                Console.WriteLine("\nReqNonRef: Отправлен запрос на удаление");
                await connection();
                NpgsqlCommand com = new NpgsqlCommand(text, PostgreSQLClass.getinstance().pgCon);
                await com.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ReqNonRef: Ошибка при выполнении запроса: {ex.Message}");
                MessageBox.Show("Не удалось удалить поле!\nВозможно оно используется как внешний ключ.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            { 
                pgCon.Dispose();
                Console.WriteLine($"ReqDel: Статус подключения = {pgCon.State.ToString()}");
            }
        }

        // Получение таблицы
        public async Task<DataTable> GetTable(string items, string table, string command)
        {
            try
            {
                Console.WriteLine($"\nReqNonRef: Отправлен запрос на получение таблицы {table}");
                await connection();
                NpgsqlCommand com = new NpgsqlCommand($"SELECT {items} FROM {table} {command}", pgCon);
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(com);
                DataTable Table = new DataTable($"{table}");
                adapter.Fill(Table);
                return Table;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ReqNonRef: Ошибка при выполнении запроса: {ex.Message}");
                MessageBox.Show($"Error DataGridUpdate\nНе удалось найти таблицу.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DataTable Table = new DataTable($"{table}");
                return Table;
            }
            finally 
            { 
                pgCon.Dispose();
                Console.WriteLine($"GetTable: Статус подключения = {pgCon.State.ToString()}");
            }
        }

        // Логирование
        public async Task Loging(string CurrentUser, string Action, string Table, string Row, string Specifies)
        {
            Console.WriteLine("\nLoging: Отправлен запрос на логирование");
            await ReqNonRef($"INSERT INTO Logs (ActionDate, [User], Action, [Table], Row, Specifies) Values ('{DateTime.Now:yyyy-MM-dd HH:mm:ss}', '{CurrentUser}', '{Action}', '{Table}', '{Row}', '{Specifies}')");
        }

        // Восстановление
        public async Task Recovery(string table, string id_variant, string id)
        {
            Console.WriteLine("\nRecovery: Отправлен запрос на восстановление");
            await ReqNonRef($"UPDATE {table} SET Deleted = 0, DelDate = NULL WHERE id LIKE '{id}'");
        }

        // Проверка логина
        public async Task<bool> CheckLogin(string username, string password)
        {
            Console.WriteLine("\nCheckLogin: Отправлен запрос на проверку пароля");
            string result = await ReqRef($"SELECT user_password FROM \"Misc\".users_profiles WHERE username = '{username}'");
            if (result == password)
                return true;
            else
                return false;
        }

        // IP Table
        public DataTable Report_IP(string subnet)
        {
            DataTable result = new DataTable();
            //result.Columns.Add("IP", typeof(String));
            //result.Columns.Add("Устройство", typeof(String));
            //result.Columns.Add("Инвентарный номер", typeof(String));
            //result.Columns.Add("Имя", typeof(String));
            //result.Columns.Add("Комментарий", typeof(String));

            //for (int i = 1; i < 255; i++)
            //{
            //    DataRow ip = result.NewRow();
            //    ip["IP"] = $"192.168.{subnet}.{i}";
            //    result.Rows.Add(ip);
            //}

            //DataTable PC = GetTable("*", "PC_View", "Where LEN(IP) > 0");
            //foreach (DataRow row in PC.Rows)
            //{
            //    foreach (DataRow ip in result.Rows)
            //    {
            //        if (row["IP"].ToString() == ip["IP"].ToString())
            //        {
            //            ip["Устройство"] = "Компьютер";
            //            ip["Инвентарный номер"] = row["Инвентарный номер"].ToString();
            //            ip["Имя"] = row["Имя"].ToString();
            //            ip["Комментарий"] = row["Комментарий"];
            //        }
            //    }
            //}

            //DataTable OrgTech = GetTable("*", "OrgTech_View", "Where LEN(IP) > 0");
            //foreach (DataRow row in OrgTech.Rows)
            //{
            //    foreach (DataRow ip in result.Rows)
            //    {
            //        if (row["IP"].ToString() == ip["IP"].ToString())
            //        {
            //            ip["Устройство"] = row["Тип"].ToString();
            //            ip["Инвентарный номер"] = row["Инвентарный номер"].ToString();
            //            ip["Имя"] = row["Модель"].ToString();
            //            ip["Комментарий"] = row["Комментарий"];
            //        }
            //    }
            //}

            //DataTable Routers = GetTable("*", "Routers_View", "Where LEN(IP) > 0");
            //foreach (DataRow row in Routers.Rows)
            //{
            //    foreach (DataRow ip in result.Rows)
            //    {
            //        if (row["IP"].ToString() == ip["IP"].ToString())
            //        {
            //            ip["Устройство"] = "Роутер";
            //            ip["Инвентарный номер"] = row["Инвентарный номер"].ToString();
            //            ip["Имя"] = row["Модель"].ToString();
            //            ip["Комментарий"] = row["Комментарий"];
            //        }
            //    }
            //}

            //DataTable Switches = GetTable("*", "Switches_View", "Where LEN(IP) > 0");
            //foreach (DataRow row in Switches.Rows)
            //{
            //    foreach (DataRow ip in result.Rows)
            //    {
            //        if (row["IP"].ToString() == ip["IP"].ToString())
            //        {
            //            ip["Устройство"] = "Свитч";
            //            ip["Инвентарный номер"] = row["Инвентарный номер"].ToString();
            //            ip["Имя"] = row["Модель"].ToString();
            //            ip["Комментарий"] = row["Комментарий"];
            //        }
            //    }
            //}

            return result;
        }
    }
}
