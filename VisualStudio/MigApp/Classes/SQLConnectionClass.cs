using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace MigApp
{
    internal class SQLConnectionClass
    {
        //SqlConnection sqlConnection = new SqlConnection($@"Data Source = {MigApp.Properties.Settings.Default.Server}; Initial Catalog = {MigApp.Properties.Settings.Default.Database}; Integrated Security = True");

        SqlConnection sqlConnection = new SqlConnection($@"Data Source = {MigApp.Properties.Settings.Default.Server}; Initial Catalog = {MigApp.Properties.Settings.Default.Database}; Integrated Security = false; User id = sa; Password = {MigApp.Properties.Settings.Default.DBPassword}");
        DataTable Table = new DataTable("");

        private static SQLConnectionClass instance;

        // Должен быть пустым
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
                //sqlConnection.Close();
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
        public DataTable GetTable(string items, string table, string command)
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

        public DataTable DataGridUpdate(string items, string table, string command) { DataTable tmp = null;  return tmp; }

        // Проверка занятости инвентарного номера
        public bool InvNumChecker (string InvNum)
        {
            //if (Convert.ToUInt32(ReqRef($"SELECT COUNT(*) FROM Computers WHERE InvNum LIKE '{InvNum}'")) < 1 
            //    && Convert.ToUInt32(ReqRef($"SELECT COUNT(*) FROM Notebooks WHERE InvNum LIKE '{InvNum}'")) < 1 
            //    && Convert.ToUInt32(ReqRef($"SELECT COUNT(*) FROM Tablets WHERE InvNum LIKE '{InvNum}'")) < 1 
            //    && Convert.ToUInt32(ReqRef($"SELECT COUNT(*) FROM OrgTech WHERE InvNum LIKE '{InvNum}'")) < 1 
            //    && Convert.ToUInt32(ReqRef($"SELECT COUNT(*) FROM Monitor WHERE InvNum LIKE '{InvNum}'")) < 1)
            //    return true;
            //else return false;
            return true;
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
                ReqDel($"UPDATE Computers SET User = NULL WHERE User LIKE '{ID}'");
            if (ReqRef($"SELECT COUNT(*) FROM Notebooks WHERE User LIKE '{ID}'") != "0")
                ReqDel($"UPDATE Notebooks SET User = NULL WHERE User LIKE '{ID}'");
            if (ReqRef($"SELECT COUNT(*) FROM Tablets WHERE User LIKE '{ID}'") != "0")
                ReqDel($"UPDATE Tablets SET User = NULL WHERE User LIKE '{ID}'");
            ReqNonRef($"DELETE FROM Employees WHERE ID LIKE '{ID}'");
        }

        // Удаление ПК из архива
        public void Delete_DeletedPC(string invnum)
        {
            ReqDel($"UPDATE OrgTech SET PC = NULL WHERE PC LIKE '{invnum}' " +
                    $"UPDATE Monitor SET PC = NULL WHERE PC LIKE '{invnum}' " +
                    $"DELETE FROM Computers WHERE InvNum LIKE '{invnum}'");
        }

        // Отчёт IP
        public DataTable Report_IP()
        {
            DataTable result = new DataTable();
            result.Columns.Add("IP", typeof(String));
            result.Columns.Add("Устройство", typeof(String));
            result.Columns.Add("Инвентарный номер", typeof(String));
            result.Columns.Add("Имя", typeof(String));
            result.Columns.Add("Комментарий", typeof(String));
            for (int i=1; i<255; i++)
            {
                DataRow ip = result.NewRow();
                ip["IP"] = $"192.168.0.{i}";
                result.Rows.Add(ip);
            }

            DataTable PC = GetTable("*", "PC_View", "Where LEN(IP) > 0");
            foreach (DataRow row in PC.Rows)
            {
                foreach (DataRow ip in result.Rows)
                {
                    if (row["IP"].ToString() == ip["IP"].ToString())
                    {
                        ip["Устройство"] = "Компьютер";
                        ip["Инвентарный номер"] = row["Инвентарный номер"].ToString();
                        ip["Имя"] = row["Имя"].ToString();
                        ip["Комментарий"] = row["Комментарий"];
                    }
                }
            }

            DataTable OrgTech = GetTable("*", "OrgTech_View", "Where LEN(IP) > 0");
            foreach (DataRow row in OrgTech.Rows)
            {
                foreach (DataRow ip in result.Rows)
                {
                    if (row["IP"].ToString() == ip["IP"].ToString())
                    {
                        ip["Устройство"] = row["Тип"].ToString();
                        ip["Инвентарный номер"] = row["Инвентарный номер"].ToString();
                        ip["Имя"] = row["Модель"].ToString();
                        ip["Комментарий"] = row["Комментарий"];
                    }
                }
            }

            DataTable Routers = GetTable("*", "Routers_View", "Where LEN(IP) > 0");
            foreach (DataRow row in Routers.Rows)
            {
                foreach (DataRow ip in result.Rows)
                {
                    if (row["IP"].ToString() == ip["IP"].ToString())
                    {
                        ip["Устройство"] = "Роутер";
                        ip["Инвентарный номер"] = row["Инвентарный номер"].ToString();
                        ip["Имя"] = row["Модель"].ToString();
                        ip["Комментарий"] = row["Комментарий"];
                    }
                }
            }

            DataTable Switches = GetTable("*", "Switches_View", "Where LEN(IP) > 0");
            foreach (DataRow row in Switches.Rows)
            {
                foreach (DataRow ip in result.Rows)
                {
                    if (row["IP"].ToString() == ip["IP"].ToString())
                    {
                        ip["Устройство"] = "Свитч";
                        ip["Инвентарный номер"] = row["Инвентарный номер"].ToString();
                        ip["Имя"] = row["Модель"].ToString();
                        ip["Комментарий"] = row["Комментарий"];
                    }
                }
            }

            return result;
        }

        // Отчёт ПК
        public DataTable Report_PC()
        {
            DataTable table = GetTable("*","Report_PC","");
            foreach(DataRow row in table.Rows)
            {
                string monInvNum = "", monModel = "";
                string otInvNum = "", otModel = "", otType = "";
                DataTable mons = GetTable("*", "Monitor", $"Where PC Like '{row["ИН компьютера"]}'");
                foreach(DataRow mon in mons.Rows)
                {
                    monInvNum += mon["InvNum"].ToString() + "/";
                    monModel += mon["Firm"].ToString() + " " + mon["Model"].ToString() + "/";
                }
                monInvNum = monInvNum.TrimEnd('/'); monModel = monModel.TrimEnd('/');
                DataTable ots = GetTable("*", "OrgTech", $"Where PC Like '{row["ИН компьютера"]}'");
                foreach (DataRow ot in ots.Rows)
                {
                    otType += ot["Type"].ToString() + "/";
                    otInvNum += ot["InvNum"].ToString() + "/";
                    otModel += ot["Model"].ToString() + "/";
                }
                otType = otType.TrimEnd('/'); otInvNum = otInvNum.TrimEnd('/'); otModel = otModel.TrimEnd('/');
                row["ИН монитора"] = monInvNum;
                row["Модель монитора"] = monModel;
                row["ИН оргтехники"] = otInvNum;
                row["Тип оргтехники"] = otType;
                row["Модель оргтехники"] = otModel;
            }
            return table;
        }

        // Отчёт ПК фильтр
        public DataTable Report_PC_Filtered()
        {
            DataTable result = new DataTable();
            //result.Columns.Add("Сотрудник");
            //result.Columns.Add("Отдел");
            //result.Columns.Add("Кабинет");
            //result.Columns.Add("ИН компьютера");
            //result.Columns.Add("Имя компьютера");
            //result.Columns.Add("ИН монитора");
            //result.Columns.Add("Модель монитора");
            //result.Columns.Add("ИН оргтехники");
            //result.Columns.Add("Тип оргтехники");
            //result.Columns.Add("Модель оргтехники");
            //DataTable table = new DataTable();
            //int i;
            //table = Report_PC();
            //string[] Params = MigApp.Properties.Settings.Default.ParamsPCRep.Split('|');
            //foreach (DataRow row in table.Rows)
            //{
            //    i = 0;
            //    if (Params[0].Length > 0)
            //        if (row[0].ToString().IndexOf(Params[0]) != -1)
            //            i++;
            //        else i--;
            //    if (Params[1].Length > 0)
            //        if (row[1].ToString().IndexOf(Params[1]) != -1)
            //            i++;
            //        else i--;
            //    if (Params[2].Length > 0)
            //        if (row[2].ToString() == Params[2])
            //            i++;
            //        else i--;
            //    if(Params[3].Length > 0)
            //        if(row[3].ToString() == Params[3])
            //            i++;
            //        else i--;
            //    if (Params[4].Length > 0)
            //        if (row[4].ToString().IndexOf(Params[4]) != -1)
            //            i++;
            //        else i--;
            //    if (Params[5].Length > 0)
            //        if (row[5].ToString() == Params[5])
            //            i++;
            //        else i--;
            //    if (Params[6].Length > 0)
            //        if (row[6].ToString().IndexOf(Params[6]) != -1)
            //            i++;
            //        else i--;
            //    if (Params[7].Length > 0)
            //        if (row[7].ToString() == Params[7])
            //            i++;
            //        else i--;
            //    if (Params[8].Length > 0)
            //        if (row[8].ToString().IndexOf(Params[8]) != -1)
            //            i++;
            //        else i--;
            //    if (Params[9].Length > 0)
            //        if (row[9].ToString().IndexOf(Params[9]) != -1)
            //            i++;
            //        else i--;
            //    if (i > 0)
            //    {
            //        DataRow dr = result.NewRow();
            //        dr["Сотрудник"] = row["Сотрудник"].ToString();
            //        dr["Отдел"] = row["Отдел"].ToString();
            //        dr["Кабинет"] = row["Кабинет"].ToString();
            //        dr["ИН компьютера"] = row["ИН компьютера"].ToString();
            //        dr["Имя компьютера"] = row["Имя компьютера"].ToString();
            //        dr["ИН монитора"] = row["ИН монитора"].ToString();
            //        dr["Модель монитора"] = row["Модель монитора"].ToString();
            //        dr["ИН оргтехники"] = row["ИН оргтехники"].ToString();
            //        dr["Тип оргтехники"] = row["Тип оргтехники"].ToString();
            //        dr["Модель оргтехники"] = row["Модель оргтехники"].ToString();
            //        result.Rows.Add(dr);
            //    }
            //}
            return result;
        }

        // Провести изменения в избранное
        public void FavoriteUpdate(string oldRow, string oldSpec, string newRow, string newSpec, string CurUser, bool Delete)
        {
            if ((oldRow != newRow || oldSpec != newSpec) && !Delete)
            {
                ReqNonRef($"UPDATE Favourite SET Row = '{oldRow}', Comment = {newSpec} WHERE Row LIKE '{newRow}' AND Comment LIKE '{oldSpec}'");
            }
            else if (Delete)
            {
                ReqNonRef($"DELETE FROM Favourite WHERE Row LIKE '{oldRow}' AND Comment LIKE '{oldSpec}'");
            }
        }

    }
}
