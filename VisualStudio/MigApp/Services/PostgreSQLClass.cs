using Npgsql;
using System;
using System.Data;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace MigApp.Services
{
    internal class PostgreSQLClass
    {
        string connectionString = $"Server={MigApp.Properties.Settings.Default.pgServer}; port={MigApp.Properties.Settings.Default.pgPort}; user id={MigApp.Properties.Settings.Default.pgUser}; password={MigApp.Properties.Settings.Default.pgPassword}; database={MigApp.Properties.Settings.Default.pgDatabase};";
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
            connectionString = $"Server={MigApp.Properties.Settings.Default.pgServer}; port={MigApp.Properties.Settings.Default.pgPort}; user id={MigApp.Properties.Settings.Default.pgUser}; password={MigApp.Properties.Settings.Default.pgPassword}; database={MigApp.Properties.Settings.Default.pgDatabase};"; 
            try
            {
                pgCon = new NpgsqlConnection(connectionString);
                var conTask = pgCon.OpenAsync();
                if (await Task.WhenAny(conTask, Task.Delay(TimeSpan.FromSeconds(10))) == conTask)
                {
                    Console.WriteLine($"ConnectionTest: Статус подключения = {pgCon.State.ToString()}");
                    pgCon.Dispose();
                    Console.WriteLine($"ConnectionTest: Результат проверки: успех\nConnectionTest: Статус подключения = {pgCon.State.ToString()}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"ConnectionTest: При попытке установить соединение было превышено время ожидания");
                    return false;
                }

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
                var result = await com.ExecuteScalarAsync();
                if (result != null)
                {
                    return Convert.ToString(result);
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
                Console.WriteLine("\nReqDel: Отправлен запрос на удаление");
                await connection();
                NpgsqlCommand com = new NpgsqlCommand(text, PostgreSQLClass.getinstance().pgCon);
                await com.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ReqDel: Ошибка при выполнении запроса: {ex.Message}");
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
                Console.WriteLine($"\nGetTable: Отправлен запрос на получение таблицы {table}");
                await connection();
                NpgsqlCommand com = new NpgsqlCommand($"SELECT {items} FROM {table} {command}", pgCon);
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(com);
                DataTable Table = new DataTable($"{table}");
                adapter.Fill(Table);
                return Table;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetTable: Ошибка при выполнении запроса: {ex.Message}");
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
            if (result == "")
            {
                Console.WriteLine($"CheckLogin: Результат запроса null");
                if (MessageBox.Show("Ваш пароль был сброшен\nХотите сохранить как новый пароль?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Console.WriteLine($"CheckLogin: Перезапись пароля");
                    if (password.Length >= 8)
                    {
                        await ReqNonRef($"UPDATE \"Misc\".users_profiles SET user_password = '{password}' WHERE username LIKE '{username}'");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"CheckLogin: Пароль не соответствует требованиям безопасности");
                        MessageBox.Show("Пароль должен содержать минимум 8 символов.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }    
                }
                else return false;

            }
            else if (result == password)
            {
                Console.WriteLine($"CheckLogin: Результат запроса true");
                return true;
            }
            else
            {
                Console.WriteLine($"CheckLogin: Результат запроса false");
                MessageBox.Show("Неверный логин или пароль.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        // Проверка запомненого логина
        public async Task<bool> CheckRememberedLogin(string username, string password)
        {
            Console.WriteLine("\nCheckLogin: Отправлен запрос на проверку пароля");
            string result = await ReqRef($"SELECT user_password FROM \"Misc\".users_profiles WHERE username = '{username}'");
            if (result == password)
            {
                Console.WriteLine($"CheckLogin: Результат запроса true");
                return true;
            }
            else
            {
                Console.WriteLine($"CheckLogin: Результат запроса false");
                MessageBox.Show("Неверный логин или пароль.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        // Получить ID пользователя
        public async Task<string> GetUserID(string username)
        {
            Console.WriteLine($"\nGetUserID: Отправлен запрос на получение ID пользователя");
            string result = await ReqRef($"SELECT id FROM users_profiles WHERE username = '{username}'");
            if (result != null)
                return result;
            else
            {
                Console.WriteLine($"GetUserID: Пользователь с именем {username} не найден");
                return "";
            }
        }

        // IP Table
        public async Task<DataTable> Report_IP(string subnet)
        {
            DataTable result = new DataTable();
            result.Columns.Add("IP", typeof(String));
            result.Columns.Add("Устройство", typeof(String));
            result.Columns.Add("Инвентарный номер", typeof(String));
            result.Columns.Add("Имя", typeof(String));
            result.Columns.Add("Комментарий", typeof(String));

            for (int i = 1; i < 255; i++)
            {
                DataRow ip = result.NewRow();
                ip["IP"] = $"192.168.{subnet}.{i}";
                result.Rows.Add(ip);
            }

            DataTable PC = await GetTable("ip, inventory_number, name, comment", "\"Technic\".computers", "");
            foreach (DataRow row in PC.Rows)
            {
                string ipAddress = row["ip"].ToString();
                DataRow ipRow = result.Select($"IP = '{ipAddress}'")[0];

                if (ipRow != null)
                {
                    if (ipRow["Устройство"].ToString().Length == 0)
                    {
                        ipRow["Устройство"] = "Компьютер";
                        ipRow["Инвентарный номер"] = row["inventory_number"].ToString();
                        ipRow["Имя"] = row["name"].ToString();
                        ipRow["Комментарий"] = row["comment"].ToString();
                    }
                    else
                    {
                        DataRow newRow = result.NewRow();
                        newRow["IP"] = ipAddress;
                        newRow["Устройство"] = "Компьютер";
                        newRow["Инвентарный номер"] = row["inventory_number"].ToString();
                        newRow["Имя"] = row["name"].ToString();
                        newRow["Комментарий"] = row["comment"].ToString();
                        result.Rows.Add(newRow);
                    }
                }
            }

            DataTable OrgTech = await GetTable("ip, type, inventory_number, model, comment", "\"Technic\".orgtechnic", "");
            foreach (DataRow row in OrgTech.Rows)
            {
                string ipAddress = row["ip"].ToString();
                DataRow ipRow = result.Select($"IP = '{ipAddress}'")[0];

                if (ipRow != null)
                {
                    if (ipRow["Устройство"].ToString().Length == 0)
                    {
                        ipRow["Устройство"] = row["type"].ToString();
                        ipRow["Инвентарный номер"] = row["inventory_number"].ToString();
                        ipRow["Имя"] = row["model"].ToString();
                        ipRow["Комментарий"] = row["comment"].ToString();
                    }
                    else
                    {
                        DataRow newRow = result.NewRow();
                        newRow["IP"] = ipAddress;
                        newRow["Устройство"] = row["type"].ToString();
                        newRow["Инвентарный номер"] = row["inventory_number"].ToString();
                        newRow["Имя"] = row["model"].ToString();
                        newRow["Комментарий"] = row["comment"].ToString();
                        result.Rows.Add(newRow);
                    }
                }
            }

            DataTable Routers = await GetTable("ip, inventory_number, model, comment", "\"Technic\".routers", "");
            foreach (DataRow row in Routers.Rows)
            {
                string ipAddress = row["ip"].ToString();
                DataRow ipRow = result.Select($"IP = '{ipAddress}'")[0];

                if (ipRow != null)
                {
                    if (ipRow["Устройство"].ToString().Length == 0)
                    {
                        ipRow["Устройство"] = "Роутер";
                        ipRow["Инвентарный номер"] = row["inventory_number"].ToString();
                        ipRow["Имя"] = row["model"].ToString();
                        ipRow["Комментарий"] = row["comment"].ToString();
                    }
                    else
                    {
                        DataRow newRow = result.NewRow();
                        newRow["IP"] = ipAddress;
                        newRow["Устройство"] = "Роутер";
                        newRow["Инвентарный номер"] = row["inventory_number"].ToString();
                        newRow["Имя"] = row["model"].ToString();
                        newRow["Комментарий"] = row["comment"].ToString();
                        result.Rows.Add(newRow);
                    }
                }
            }

            DataTable Switches = await GetTable("ip, inventory_number, model, comment", "\"Technic\".switches", "");
            foreach (DataRow row in Switches.Rows)
            {
                string ipAddress = row["ip"].ToString();
                DataRow ipRow = result.Select($"IP = '{ipAddress}'")[0];

                if (ipRow != null)
                {
                    if (ipRow["Устройство"].ToString().Length == 0)
                    {
                        ipRow["Устройство"] = "Коммутатор";
                        ipRow["Инвентарный номер"] = row["inventory_number"].ToString();
                        ipRow["Имя"] = row["model"].ToString();
                        ipRow["Комментарий"] = row["comment"].ToString();
                    }
                    else
                    {
                        DataRow newRow = result.NewRow();
                        newRow["IP"] = ipAddress;
                        newRow["Устройство"] = "Коммутатор";
                        newRow["Инвентарный номер"] = row["inventory_number"].ToString();
                        newRow["Имя"] = row["model"].ToString();
                        newRow["Комментарий"] = row["comment"].ToString();
                        result.Rows.Add(newRow);
                    }
                }
            }

            return result;
        }
    }
}
