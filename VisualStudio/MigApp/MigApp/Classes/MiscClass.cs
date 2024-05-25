using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MigApp
{
    class MiscClass
    {
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
    }
}
