using MigApp.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MigApp
{
    class MiscClass
    {
        HttpClient hc = new HttpClient();
        WebClient wc = new WebClient();
        string curver = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
        //string ver, prerel, url = null;

        public string Splitter(string txt)
        {
            string result = "";
            try
            {
                string[] mass = txt.Split('|');
                foreach(string s in mass) 
                {
                    if(s.Length > 0)
                        result += $"{s}, ";
                }
                result = result.Remove(result.Length - 2, 2);
                return result;
            }
            catch
            {
                return "";
            }
        }

        public string[] IPSplitter(string ip)
        {
            try
            {
                string[] mass = ip.Split('.');
                return mass;
            }
            catch
            {
                return null;
            }
        }

        public string[] DHCPSplitter(string ip4)
        {
            try
            {
                string[] mass = ip4.Split('-');
                return mass;
            }
            catch
            {
                return null;
            }
        }

        public string ReverceStr(string str)
        {
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            string result = new string(chars);
            return result;
        }

        public string IPSearcher(string ip1, string ip2, string ip3, string ip4)
        {
            string result = "";
            if (ip1.Length > 0)
                result = ip1 + ".";
            else result = "%.";
            if (ip2.Length > 0)
                result += ip2 + ".";
            else result += "%.";
            if (ip3.Length > 0)
                result += ip3 + ".";
            else result += "%.";
            if (ip4.Length > 0)
                result += ip4;
            else result += "%";
            return result;
        }

        public string DHCPSearcher(string dhcp1, string dhcp2, string dhcp3, string dhcp4, string dhcp5)
        {
            string result = "";
            if (dhcp1.Length > 0)
                result = dhcp1 + ".";
            else result = "%.";
            if (dhcp2.Length > 0)
                result += dhcp2 + ".";
            else result += "%.";
            if (dhcp3.Length > 0)
                result += dhcp3 + ".";
            else result += "%.";
            if (dhcp4.Length > 0)
                result += dhcp4 + "-";
            else result += "%-";
            if (dhcp5.Length > 0)
                result += dhcp5;
            else result += "%";
            return result;
        }

        public string BoolToString (bool? Bool)
        {
            if (Bool == true) { return "1"; }
            else { return "0"; }
        }

        public string NormalizeDateTime(string datetime)
        {
            try
            {
                return datetime.Replace('.','/');
            }
            catch { return ""; }
        }

        public void ExcelExport(DataGrid Report)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = null;
                Microsoft.Office.Interop.Excel.Workbook wb = null;
                object missing = Type.Missing;
                Microsoft.Office.Interop.Excel.Worksheet ws = null;

                // Сбор таблицы
                DataTable dt = new DataTable();
                dt = ((DataView)Report.ItemsSource).ToTable();

                excel = new Microsoft.Office.Interop.Excel.Application();
                wb = excel.Workbooks.Add();
                ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                ws.Columns.AutoFit();
                ws.Columns.EntireColumn.ColumnWidth = 25;

                // Строка заголовков
                for (int Idx = 0; Idx < dt.Columns.Count; Idx++)
                {
                    ws.Range["A1"].Offset[0, Idx].Value = dt.Columns[Idx].ColumnName;
                }

                // Строки данных
                ws.Cells.NumberFormat = "@";
                for (int Idx = 0; Idx < dt.Rows.Count; Idx++)
                {
                    ws.Range["A2"].Offset[Idx].Resize[1, dt.Columns.Count].Value = dt.Rows[Idx].ItemArray;
                }

                excel.Visible = true;
                wb.Activate();
            }
            catch
            { MessageBox.Show("На устройстве отсутствует Excel.\nУстановите Excel и попробуйте ещё раз.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        public static bool InternetChecker()
        {
            try
            {
                Dns.GetHostEntry("8.8.8.8");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void CheckVersion()
        {
            //if (InternetChecker())
            //{
            //    // Получение API
            //    hc.BaseAddress = new Uri("https://api.github.com/repos/Vanilla76e2/MigApp/releases");
            //    hc.DefaultRequestHeaders.Add("User-Agent", "MigApp");
            //    HttpResponseMessage response = await hc.GetAsync("https://api.github.com/repos/Vanilla76e2/MigApp/releases");
            //    string json = await response.Content.ReadAsStringAsync();

            //    try
            //    {
            //        // Парсинг
            //        string[] strings = json.Split(',');
            //        foreach (string s in strings)
            //        {
            //            if (s.IndexOf("tag_name") != -1)
            //            {
            //                string[] temp = s.Split(':');
            //                ver = temp[1].Substring(2, 5);
            //            }
            //            else if (s.IndexOf("prerelease") != -1)
            //            {
            //                string[] temp = s.Split(':');
            //                prerel = temp[1].Trim();
            //            }
            //            else if (s.IndexOf("browser_download_url") != -1 && prerel == "false")
            //            {
            //                string[] temp = s.Split(':');
            //                string temp2 = temp[1] + ":" + temp[2];
            //                url = temp2.Substring(temp2.IndexOf("https"), temp2.IndexOf(".msi") + 3);
            //            }
            //            if (ver != null && prerel != null && url != null)
            //                break;
            //        }
            //        ver = ver.Replace(".","");
            //        curver = curver.Replace(".", "");
            //        if (Convert.ToUInt32(curver) < Convert.ToInt32(ver))
            //        {
            //            if (MessageBox.Show("Доступна новая версия. Обновить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            //            {
            //                string tmpserv = Path.GetTempPath() + "MigAppConnectionParametrs.txt";
            //                StreamWriter sw = File.CreateText(tmpserv);
            //                sw.WriteLine(MigApp.Properties.Settings.Default.Server + "|" + MigApp.Properties.Settings.Default.Database +  "|" + MigApp.Properties.Settings.Default.DBPassword);
            //                sw.Close();
            //                string tmpmsi = Path.GetTempPath() + "MigAppInstaller.msi";
            //                Uri uri = new Uri(url);
            //                wc.DownloadFile(uri, tmpmsi);
            //                Installer(tmpmsi);
            //            }
            //        }
            //    }
            //    catch 
            //    {
            //        Console.WriteLine(ver);
            //        Console.WriteLine(prerel);
            //        Console.WriteLine(url);
            //        MessageBox.Show("Ошибка при попытке обновления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Нет доступа к сети", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
        }

        private void Installer(string filepath)
        {
            Process p = new Process();
            p.StartInfo.FileName = "msiexec";
            p.StartInfo.Arguments = $"/i {filepath} /qr";
            p.Start();
            Application.Current.Shutdown();
        }

        // Горячие клавиши
        public void HotKeys(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.B)
            {
                e.Handled = true;
            }
        }

        // Перезапуск приложения
        public void RestartApplication()
        {
            // Закрытие  текущего  приложения
            Application.Current.Shutdown();

            // Запуск  нового  экземпляра  приложения
            Process.Start(Application.ResourceAssembly.Location);
        }


        public DataTable SortTableByIP(string sortDirection, DataTable table)
        {
            // 1.  Преобразовать строки IP в IPAddress
            var ipAddressesWithRows = table.AsEnumerable()
                .Select(row => new
                {
                    IpAddr = IPAddress.Parse(row.Field<string>("IP")),
                    Row = row
                });

            // 2.  Сортировка  с  помощью  LINQ  и  CompareTo
            IEnumerable<dynamic> sortedRows;
            if (sortDirection == "ASC")
            {
                sortedRows = ipAddressesWithRows.OrderBy(x => x.IpAddr.GetAddressBytes(), new ByteArrayComparer());
            }
            else // sortDirection == "DESC"
            {
                sortedRows = ipAddressesWithRows.OrderByDescending(x => x.IpAddr.GetAddressBytes(), new ByteArrayComparer());
            }

            // 3.  Создать новый DataTable с отсортированными данными
            DataTable sortedTable = table.Clone(); // Создаем пустую копию структуры Table
            foreach (var item in sortedRows)
            {
                sortedTable.ImportRow(item.Row);
            }

            // 4.  Заменить старый DataTable новым
            return sortedTable;
        }
    }

    public class ByteArrayComparer : IComparer<byte[]>
    {
        public int Compare(byte[] x, byte[] y)
        {
            if (x.Length != y.Length)
            {
                return x.Length - y.Length; // Сравнение  по  длине
            }

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i])
                {
                    return x[i] - y[i]; // Сравнение  по  элементам
                }
            }

            return 0; // Равны
        }
    }

}
