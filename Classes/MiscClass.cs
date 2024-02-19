using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
    }
}
