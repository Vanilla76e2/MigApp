using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MigApp
{
    /// <summary>
    /// Логика взаимодействия для SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        SQLConnectionClass sqlcc = SQLConnectionClass.getinstance();
        MiscClass mc = new MiscClass();
        string Mode;

        public SearchWindow(string mode)
        {
            InitializeComponent();
            Mode = mode;
            Setup(mode);
        }

        // Преднастройка окна поиска
        private void Setup(string mode)
        {
            #region Пользовательские таблицы
            // Сотрудники
            if (mode == "Emp" || mode == "EmpDel")
            {
                SearchPanel_Emp.Visibility = Visibility.Visible;
                this.Height = 330;
                this.Width = 550;
                FillEmpGroup();
                string[] Parametrs = null;

                if (mode == "Emp" && MigApp.Properties.Settings.Default.ParamsEmp != null)
                        Parametrs = MigApp.Properties.Settings.Default.ParamsEmp.Split('|');
                else if (mode == "EmpDel" && MigApp.Properties.Settings.Default.ParamsEmpDel != null)
                        Parametrs = MigApp.Properties.Settings.Default.ParamsEmpDel.Split('|');

                if (Parametrs != null)
                {
                    ID_Emp.Text = Parametrs[0];
                    FIO_Emp.Text = Parametrs[1];
                    Room_Emp.Text = Parametrs[2];
                    Group_Emp.Text = Parametrs[3];
                }
            }

            // ПК
            else if (mode == "PC" || mode == "PCDel")
            {
                SearchPanel_PC.Visibility = Visibility.Visible;
                this.Height = 500;
                string[] Parametrs = null;

                if (mode == "PC" && MigApp.Properties.Settings.Default.ParamsPC != null)
                       Parametrs = MigApp.Properties.Settings.Default.ParamsPC.Split('|');
                else if (mode == "PCDel" && MigApp.Properties.Settings.Default.ParamsPCDel != null)
                    Parametrs = MigApp.Properties.Settings.Default.ParamsPCDel.Split('|');

                if (Parametrs != null)
                {
                    InvNum_PC.Text = Parametrs[0];
                    Name_PC.Text = Parametrs[1];
                    User_PC.Text = Parametrs[2];
                    OS_PC.Text = Parametrs[3];
                    Motherboard_PC.Text = Parametrs[4];
                    Processor_PC.Text = Parametrs[5];
                    RAM_PC.Text = Parametrs[6];
                    Drive_PC.Text = Parametrs[7];
                    Other_PC.Text = Parametrs[8];
                    try
                    {
                        string[] ip = Parametrs[9].Split('.');
                        if (ip[0] != "%")
                            ip1_PC.Text = ip[0];
                        if (ip[1] != "%")
                            ip2_PC.Text = ip[1];
                        if (ip[2] != "%")
                            ip3_PC.Text = ip[2];
                        if (ip[3] != "%")
                            ip4_PC.Text = ip[3];
                    }
                    catch { }
                }
            }

            // Ноутбуки
            else if (mode == "NB" || mode == "NBDel")
            {
                SearchPanel_NB.Visibility = Visibility.Visible;
                this.Height = 550;
                string[] Parametrs = null;

                if (mode == "NB" && MigApp.Properties.Settings.Default.ParamsNB != null)
                    Parametrs = MigApp.Properties.Settings.Default.ParamsNB.Split('|');
                else if (mode == "NBDel" && MigApp.Properties.Settings.Default.ParamsNBDel != null)
                    Parametrs = MigApp.Properties.Settings.Default.ParamsNBDel.Split('|');

                if (Parametrs != null)
                {
                    InvNum_NB.Text = Parametrs[0];
                    Model_NB.Text = Parametrs[1];
                    SeriaNum_NB.Text = Parametrs[2];
                    User_NB.Text = Parametrs[3];
                    ScreenDiagonal_NB.Text = Parametrs[4];
                    ScreenResolution_NB.Text = Parametrs[5];
                    OS_NB.Text = Parametrs[6];
                    Processor_NB.Text = Parametrs[7];
                    RAM_NB.Text = Parametrs[8];
                    Drive_NB.Text = Parametrs[9];
                    Other_NB.Text = Parametrs[10];
                }
            }

            // Планшеты
            else if (mode == "Tab" || mode == "TabDel")
            {
                SearchPanel_Tab.Visibility = Visibility.Visible;
                this.Height = 450;
                string[] Parametrs = null;

                if (mode == "Tab" && MigApp.Properties.Settings.Default.ParamsTab != null)
                    Parametrs = MigApp.Properties.Settings.Default.ParamsTab.Split('|');
                else if (mode == "TabDel" && MigApp.Properties.Settings.Default.ParamsTabDel != null)
                    Parametrs = MigApp.Properties.Settings.Default.ParamsTabDel.Split('|');

                if (Parametrs != null)
                {
                    InvNum_Tab.Text = Parametrs[0];
                    Model_Tab.Text = Parametrs[1];
                    SeriaNum_Tab.Text = Parametrs[2];
                    User_Tab.Text = Parametrs[3];
                    ScreenDiagonal_Tab.Text = Parametrs[4];
                    Processor_Tab.Text = Parametrs[5];
                    RAM_Tab.Text = Parametrs[6];
                    Drive_Tab.Text = Parametrs[7];
                    Other_Tab.Text = Parametrs[8];
                }
            }

            // Оргтехника
            else if (mode == "OT" || mode == "OTDel")
            {
                SearchPanel_OT.Visibility = Visibility.Visible;
                this.Height = 480;
                Type_OT.SelectedIndex = 0;
                string[] Parametrs = null;

                if (mode == "OT" && MigApp.Properties.Settings.Default.ParamsOT != null)
                    Parametrs = MigApp.Properties.Settings.Default.ParamsOT.Split('|');
                else if (mode == "OTDel" && MigApp.Properties.Settings.Default.ParamsOTDel != null)
                    Parametrs = MigApp.Properties.Settings.Default.ParamsOTDel.Split('|');

                if (Parametrs != null)
                {
                    Type_OT.Text = Parametrs[0];
                    InvNum_OT.Text = Parametrs[1];
                    Model_OT.Text = Parametrs[2];
                    SeriaNum_OT.Text = Parametrs[3];
                    User_OT.Text = Parametrs[4];
                    Name_OT.Text = Parametrs[5];
                    Cartridge_OT.Text = Parametrs[6];
                    Room_OT.Text = Parametrs[7];
                    try
                    {
                        string[] ip = Parametrs[8].Split('.');
                        if (ip[0] != "%")
                            ip1_OT.Text = ip[0];
                        if (ip[1] != "%")
                            ip2_OT.Text = ip[1];
                        if (ip[2] != "%")
                            ip3_OT.Text = ip[2];
                        if (ip[3] != "%")
                            ip4_OT.Text = ip[3];
                    }
                    catch { }
                }
            }

            // Мониторы
            else if (mode == "Mon" || mode == "MonDel")
            {
                SearchPanel_Mon.Visibility = Visibility.Visible;
                this.Height = 400;
                string[] Parametrs = null;

                if (mode == "Mon" && MigApp.Properties.Settings.Default.ParamsMon != null)
                    Parametrs = MigApp.Properties.Settings.Default.ParamsMon.Split('|');
                else if (mode == "MonDel" && MigApp.Properties.Settings.Default.ParamsMonDel != null)
                    Parametrs = MigApp.Properties.Settings.Default.ParamsMonDel.Split('|');

                if (Parametrs != null)
                {
                    InvNum_Mon.Text = Parametrs[0];
                    Firm_Mon.Text = Parametrs[1];
                    Model_Mon.Text = Parametrs[2];
                    SeriaNum_Mon.Text = Parametrs[3];
                    ScreenDiagonal_Mon.Text = Parametrs[4];
                    ScreenResolution_Mon.Text = Parametrs[5];
                    ScreenType_Mon.Text = Parametrs[6];
                    User_Mon.Text = Parametrs[7];
                }
            }

            // Роутеры
            else if (mode == "Rout" || mode == "RoutDel")
            {
                SearchPanel_Rout.Visibility = Visibility.Visible;
                this.Height = 320;
                string[] Parametrs = null;

                if (mode == "Rout" && MigApp.Properties.Settings.Default.ParamsRout != null)
                    Parametrs = MigApp.Properties.Settings.Default.ParamsRout.Split('|');
                else if (mode == "RoutDel" && MigApp.Properties.Settings.Default.ParamsRoutDel != null)
                    Parametrs = MigApp.Properties.Settings.Default.ParamsRoutDel.Split('|');
                        
                if (Parametrs != null)
                {
                    InvNum_Rout.Text = Parametrs[0];
                    Name_Rout.Text = Parametrs[1];
                    Model_Rout.Text = Parametrs[2];
                    WiFiName_Rout.Text = Parametrs[3];
                    try
                    {
                        string[] ip = Parametrs[4].Split('.');
                        if (ip[0] != "%")
                            ip1_Rout.Text = ip[0];
                        if (ip[1] != "%")
                            ip2_Rout.Text = ip[1];
                        if (ip[2] != "%")
                            ip3_Rout.Text = ip[2];
                        if (ip[3] != "%")
                            ip4_Rout.Text = ip[3];
                    }
                    catch { } // IP
                    try
                    {
                        string[] dhcp = Parametrs[5].Split('.');
                        if (dhcp[0] != "%")
                            dhcp1_Rout.Text = dhcp[0];
                        if (dhcp[1] != "%")
                            dhcp2_Rout.Text = dhcp[1];
                        if (dhcp[2] != "%")
                            dhcp3_Rout.Text = dhcp[2];
                        string[] dhcp45 = dhcp[3].Split('-');
                        if (dhcp[3] != "%")
                            dhcp4_Rout.Text = dhcp45[0];
                        if (dhcp[4] != "%")
                            dhcp5_Rout.Text = dhcp45[1];
                    }
                    catch { } // DHCP
                }
            }

            // Свитчи
            else if (mode == "Switch" || mode == "SwitchDel")
            {
                SearchPanel_Switch.Visibility = Visibility.Visible;
                this.Height = 250;
                string[] Parametrs = null;

                if (mode == "Switch" && MigApp.Properties.Settings.Default.ParamsSwitch != null)
                    Parametrs = MigApp.Properties.Settings.Default.ParamsSwitch.Split('|');
                else if (mode == "SwitchDel" && MigApp.Properties.Settings.Default.ParamsSwitchDel != null)
                    Parametrs = MigApp.Properties.Settings.Default.ParamsSwitchDel.Split('|');

                if (Parametrs != null)
                {
                    InvNum_Switch.Text = Parametrs[0];
                    Name_Switch.Text = Parametrs[1];
                    Model_Switch.Text = Parametrs[2];
                    try
                    {
                        string[] ip = Parametrs[4].Split('.');
                        if (ip[0] != "%")
                            ip1_Switch.Text = ip[0];
                        if (ip[1] != "%")
                            ip2_Switch.Text = ip[1];
                        if (ip[2] != "%")
                            ip3_Switch.Text = ip[2];
                        if (ip[3] != "%")
                            ip4_Switch.Text = ip[3];
                    }
                    catch { }
                }
            }
            
            // Мебель
            else if (mode == "Furniture" || mode == "FurnitureDel")
            {
                SearchPanel_Furniture.Visibility = Visibility.Visible;
                this.Height = 250;
                FillFurnitureTypes();
                string[] Parametrs = null;

                if (mode == "Furniture" && MigApp.Properties.Settings.Default.ParamsFurn != null)
                    Parametrs = MigApp.Properties.Settings.Default.ParamsFurn.Split('|');
                else if (mode == "FurnitureDel" && MigApp.Properties.Settings.Default.ParamsFurnDel != null)
                    Parametrs = MigApp.Properties.Settings.Default.ParamsFurnDel.Split('|');

                if (Parametrs != null)
                {
                    InvNum_Furniture.Text = Parametrs[0];
                    Type_Furniture.Text = Parametrs[1];
                    Name_Furniture.Text = Parametrs[2];
                    Room_Furniture.Text = Parametrs[3];
                }
            }
            #endregion

            #region Админпанель
            // Пользователи
            else if (mode == "Users")
            {
                SearchPanel_Users.Visibility = Visibility.Visible;
                this.Height = 350;
                if (MigApp.Properties.Settings.Default.ParamsUsers != null)
                {
                    try
                    {
                        string[] Parametrs = MigApp.Properties.Settings.Default.ParamsUsers.Split('|');
                        ID_User.Text = Parametrs[0];
                        Login_User.Text = Parametrs[1];
                        Employee_User.Text = Parametrs[2];
                        Role_User.Text = Parametrs[3];
                        if (Parametrs[4] == "Без пароля")
                            NoPassword_User.IsChecked = true;
                    }
                    catch { }
                }
            }

            // Логи
            else if (mode == "Logs")
            {
                SearchPanel_Logs.Visibility = Visibility.Visible;
                this.Height = 500;
                if (MigApp.Properties.Settings.Default.ParamsLogs != null)
                {
                    try
                    {
                        string[] Parametrs = MigApp.Properties.Settings.Default.ParamsLogs.Split('|');
                        ID_Logs.Text = Parametrs[0];
                        Date_LogsStart.Text = Parametrs[1];
                        Date_LogsEnd.Text = Parametrs[2];
                        User_Logs.Text = Parametrs[3];
                        if (Parametrs[4].Length > 0)
                            Action_Logs.Text = Parametrs[4];
                        else Action_Logs.Text = "Не выбрано";
                        if (Parametrs[5].Length > 0)
                            Table_Logs.Text = Parametrs[5];
                        else Table_Logs.Text = "Не выбрано";
                        Row_Logs.Text = Parametrs[6];
                    }
                    catch { }
                }
            }
            #endregion

            #region Отчёты
            // ПК
            else if (Mode == "PCRep")
            {
                SearchPanel_PCRep.Visibility = Visibility.Visible;
                this.Height = 500;
                OTType_Report1.Text = "Не выбрано";
                if (MigApp.Properties.Settings.Default.ParamsPCRep != null)
                {
                    try
                    {
                        string[] Parametrs = MigApp.Properties.Settings.Default.ParamsPCRep.Split('|');
                        Employee_Report1.Text = Parametrs[0];
                        Group_Report1.Text = Parametrs[1];
                        Room_Report1.Text = Parametrs[2];
                        PCInvNum_Report1.Text = Parametrs[3];
                        PCName_Report1.Text = Parametrs[4];
                        MonitorInvNum_Report1.Text = Parametrs[5];
                        MonitorModel_Report1.Text = Parametrs[6];
                        OTInvNum_Report1.Text = Parametrs[7];
                        if (Parametrs[8].Length > 0)
                            OTType_Report1.Text = Parametrs[8];
                        else OTType_Report1.Text = "Не выбрано";
                        OTModel_Report1.Text = Parametrs[9];
                    }
                    catch { }
                }
            }

            // Ноутбуки
            else if (Mode == "NBRep")
            {
                SearchPanel_NBRep.Visibility = Visibility.Visible;
                this.Height = 300;
                if (MigApp.Properties.Settings.Default.ParamsNBRep != null)
                {
                    try
                    {
                        string[] Parametrs = MigApp.Properties.Settings.Default.ParamsNBRep.Split('|');
                        Employee_Report2.Text = Parametrs[0];
                        Group_Report2.Text = Parametrs[1];
                        Room_Report2.Text = Parametrs[2];
                        NotebookInvNum_Report2.Text = Parametrs[3];
                        NotebookName_Report2.Text = Parametrs[4];
                    }
                    catch { }
                }
            }

            // Планшеты
            else if (Mode == "TabRep")
            {
                SearchPanel_TabRep.Visibility = Visibility.Visible;
                this.Height = 300;
                if (MigApp.Properties.Settings.Default.ParamsTabRep != null)
                {
                    try
                    {
                        string[] Parametrs = MigApp.Properties.Settings.Default.ParamsTabRep.Split('|');
                        Employee_Report3.Text = Parametrs[0];
                        Group_Report3.Text = Parametrs[1];
                        Room_Report3.Text = Parametrs[2];
                        TabletInvNum_Report3.Text = Parametrs[3];
                        TabletName_Report3.Text = Parametrs[4];
                    }
                    catch { }
                }
            }
            #endregion
        }

        // Применение фильтров
        private void AcceptClick(object sender, RoutedEventArgs e)
        {
            string command = "Where ";

            #region Пользовательские таблицы
            // Сотрудники
            if (Mode == "Emp" || Mode == "EmpDel")
            {
                if (ID_Emp.Text.Length > 0)
                    command += $"ID Like '{ID_Emp.Text}' AND ";
                if (FIO_Emp.Text.Length > 0)
                    command += $"[ФИО] Like '%{FIO_Emp.Text}%' AND ";
                if (Room_Emp.Text.Length > 0)
                    command += $"[Кабинет] Like '{Room_Emp.Text}' AND ";
                if (Group_Emp.Text != "Не выбрано")
                    command += $"[Отдел] Like '%{Group_Emp.Text}%' AND ";
                else command += $" ";
                command += "ID NOT Like 'NULL'";

                if (command != "Where ID NOT Like 'NULL'" && Mode == "Emp")
                {
                    MigApp.Properties.Settings.Default.ParamsEmp = $"{ID_Emp.Text}|{FIO_Emp.Text}|{Room_Emp.Text}|{Group_Emp.Text}";
                    MigApp.Properties.Settings.Default.comEmp = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                else if (command != "Where ID NOT Like 'NULL'" && Mode == "EmpDel")
                {
                    MigApp.Properties.Settings.Default.ParamsEmpDel = $"{ID_Emp.Text}|{FIO_Emp.Text}|{Room_Emp.Text}|{Group_Emp.Text}";
                    MigApp.Properties.Settings.Default.comEmpDel = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }

            //ПК
            else if (Mode == "PC" || Mode == "PCDel")
            {
                if (InvNum_PC.Text.Length > 0)
                    command += $"[Инвентарный номер] Like '{InvNum_PC.Text}' AND ";
                if (Name_PC.Text.Length > 0)
                    command += $"Имя Like '%{Name_PC.Text}%' AND ";
                if (ip1_PC.Text.Length > 0 || ip2_PC.Text.Length > 0 || ip3_PC.Text.Length > 0 || ip4_PC.Text.Length > 0)
                    command += $"IP Like '{mc.IPSearcher(ip1_PC.Text, ip2_PC.Text, ip3_PC.Text, ip4_PC.Text)}' AND ";
                if (User_PC.Text.Length > 0)
                    command += $"Пользователь Like '%{User_PC.Text}%' AND ";
                if (OS_PC.Text.Length > 0)
                    command += $"ОС Like '%{OS_PC.Text}%' AND ";
                if (Motherboard_PC.Text.Length > 0)
                    command += $"[Материнская плата] Like '%{Motherboard_PC.Text}%' AND ";
                if (Processor_PC.Text.Length > 0)
                    command += $"Процессор Like '%{Processor_PC.Text}%' AND ";
                if (RAM_PC.Text.Length > 0)
                    command += $"ОЗУ Like '%{RAM_PC.Text}%' AND ";
                if (Drive_PC.Text.Length > 0)
                    command += $"Накопители Like '%{Drive_PC.Text}%' AND ";
                if (Other_PC.Text.Length > 0)
                    command += $"Другое Like '%{Other_PC.Text}%' AND ";
                command += $"[Инвентарный номер] NOT Like 'NULL'";
                if (command != "Where [Инвентарный номер] NOT Like 'NULL'")
                {
                    string Parametrs = $"{InvNum_PC.Text}|{Name_PC.Text}|{User_PC.Text}|{OS_PC.Text}|{Motherboard_PC.Text}|{Processor_PC.Text}|{RAM_PC.Text}|{Drive_PC.Text}|{Other_PC.Text}";
                    if (ip1_PC.Text.Length > 0 || ip2_PC.Text.Length > 0 || ip3_PC.Text.Length > 0 || ip4_PC.Text.Length > 0)
                        Parametrs += $"|{mc.IPSearcher(ip1_PC.Text, ip2_PC.Text, ip3_PC.Text, ip4_PC.Text)}";
                    else Parametrs += "|";

                    if (Mode == "PC")
                    {
                        MigApp.Properties.Settings.Default.ParamsPC = Parametrs;
                        MigApp.Properties.Settings.Default.comPC = command;
                        MigApp.Properties.Settings.Default.Save();
                    }
                    else if (Mode == "PCDel")
                    {
                        MigApp.Properties.Settings.Default.ParamsPCDel = Parametrs;
                        MigApp.Properties.Settings.Default.comPCDel = command;
                        MigApp.Properties.Settings.Default.Save();
                    }
                    DialogResult = true;
                }
                this.Close();

            }

            // Ноутбуки
            else if (Mode == "NB" || Mode == "NBDel")
            {
                if (InvNum_NB.Text.Length > 0)
                    command += $"[Инвентарный номер] Like '{InvNum_NB.Text}' AND ";
                if (Model_NB.Text.Length > 0)
                    command += $"Модель Like '%{Model_NB.Text}%' AND ";
                if (SeriaNum_NB.Text.Length > 0)
                    command += $"[Серийный номер] Like '%{SeriaNum_NB.Text}%' AND ";
                if (User_NB.Text.Length > 0)
                    command += $"Пользователь Like '%{User_NB.Text}%' AND ";
                if (ScreenDiagonal_NB.Text.Length > 0)
                    command += $"Диагональ Like '%{ScreenDiagonal_NB.Text}%' AND ";
                if (ScreenResolution_NB.Text != "____x____")
                    command += $"Разрешение Like '%{ScreenResolution_NB.Text.Replace("_", "%")}%' AND ";
                if (OS_NB.Text.Length > 0)
                    command += $"ОС Like '%{OS_NB.Text}%' AND ";
                if (Processor_NB.Text.Length > 0)
                    command += $"Процессор Like '%{Processor_NB.Text}%' AND ";
                if (RAM_NB.Text.Length > 0)
                    command += $"ОЗУ Like '%{RAM_NB.Text}%' AND ";
                if (Drive_NB.Text.Length > 0)
                    command += $"Накопители Like '%{Drive_NB.Text}%' AND ";
                if (Other_NB.Text.Length > 0)
                    command += $"Другое Like '%{Other_NB.Text}%' AND ";
                command += $"[Инвентарный номер] NOT Like 'Placeholder'";
                if (command != "Where [Инвентарный номер] NOT Like 'Placeholder'")
                {
                    string Param = $"{InvNum_NB.Text}|{Model_NB.Text}|{SeriaNum_NB.Text}|{User_NB.Text}|{ScreenDiagonal_NB.Text}";
                    if (ScreenResolution_NB.Text != "____x____")
                        Param += $"|{ScreenResolution_NB.Text.Replace("_", "")}";
                    else Param += "|";
                    Param += $"|{OS_NB.Text}|{Processor_NB.Text}|{RAM_NB.Text}|{Drive_NB.Text}|{Other_NB.Text}";

                    if (Mode == "NB")
                    {
                        MigApp.Properties.Settings.Default.ParamsNB = Param;
                        MigApp.Properties.Settings.Default.comNB = command;
                        MigApp.Properties.Settings.Default.Save();
                    }
                    else if (Mode == "NBDel")
                    {
                        MigApp.Properties.Settings.Default.ParamsNBDel = Param;
                        MigApp.Properties.Settings.Default.comNBDel = command;
                        MigApp.Properties.Settings.Default.Save();
                    }
                    DialogResult = true;
                }
                this.Close();
            }

            // Планшеты
            else if (Mode == "Tab" || Mode == "TabDel")
            {
                if (InvNum_Tab.Text.Length > 0)
                    command += $"[Инвентарный номер] Like '{InvNum_Tab.Text}' AND ";
                if (Model_Tab.Text.Length > 0)
                    command += $"Модель Like '%{Model_Tab.Text}%' AND ";
                if (SeriaNum_Tab.Text.Length > 0)
                    command += $"[Серийный номер] Like '%{SeriaNum_Tab.Text}%' AND ";
                if (User_Tab.Text.Length > 0)
                    command += $"Пользователь Like '%{User_Tab.Text}%' AND ";
                if (ScreenDiagonal_Tab.Text.Length > 0)
                    command += $"Диагональ Like '%{ScreenDiagonal_Tab.Text}%' AND ";
                if (Processor_Tab.Text.Length > 0)
                    command += $"Процессор Like '%{Processor_Tab.Text}%' AND ";
                if (RAM_Tab.Text.Length > 0)
                    command += $"ОЗУ Like '%{RAM_Tab.Text}%' AND ";
                if (Drive_Tab.Text.Length > 0)
                    command += $"Накопители Like '%{Drive_Tab.Text}%' AND ";
                if (Other_Tab.Text.Length > 0)
                    command += $"Другое Like '%{Other_Tab.Text}%' AND ";
                command += $"[Инвентарный номер] NOT Like 'Placeholder'";
                if (command != "Where [Инвентарный номер] NOT Like 'Placeholder'")
                {
                    string Param = $"{InvNum_Tab.Text}|{Model_Tab.Text}|{SeriaNum_Tab.Text}|{User_Tab.Text}|{ScreenDiagonal_Tab.Text}|{Processor_Tab.Text}|{RAM_Tab.Text}|{Drive_Tab.Text}|{Other_Tab.Text}";
                    
                    if (Mode == "Tab")
                    {
                        MigApp.Properties.Settings.Default.ParamsTab = Param;
                        MigApp.Properties.Settings.Default.comTab = command;
                        MigApp.Properties.Settings.Default.Save();
                    }
                    else if (Mode == "TabDel")
                    {
                        MigApp.Properties.Settings.Default.ParamsTabDel = Param;
                        MigApp.Properties.Settings.Default.comTabDel = command;
                        MigApp.Properties.Settings.Default.Save();
                    }
                    DialogResult = true;
                }
                this.Close();
            }

            // Орг.техника
            else if (Mode == "OT" || Mode == "OTDel")
            {
                if (InvNum_OT.Text.Length > 0)
                    command += $"[Инвентарный номер] Like '{InvNum_OT.Text}' AND ";
                if (Type_OT.Text != "Не выбрано")
                    command += $"Тип Like '{Type_OT.Text}' AND ";
                else command += " ";
                if (Model_OT.Text.Length > 0)
                    command += $"Модель Like '%{Model_OT.Text}%' AND ";
                if (SeriaNum_OT.Text.Length > 0)
                    command += $"[Серийный номер] Like '%{SeriaNum_OT.Text}%' AND ";
                if (Name_OT.Text.Length > 0)
                    command += $"Имя Like '%{Name_OT.Text}%' AND ";
                if (ip1_OT.Text.Length > 0 || ip2_OT.Text.Length > 0 || ip3_OT.Text.Length > 0 || ip4_OT.Text.Length > 0)
                    command += $"IP Like '{mc.IPSearcher(ip1_OT.Text, ip2_OT.Text, ip3_OT.Text, ip4_OT.Text)}' AND ";
                if (Cartridge_OT.Text.Length > 0)
                    command += $"Картридж Like '%{Cartridge_OT.Text}%' AND ";
                if (User_OT.Text.Length > 0)
                    command += $"Пользователь Like '%{User_OT.Text}%' AND ";
                if (Room_OT.Text.Length > 0)
                    command += $"Кабинет Like '%{Room_OT.Text}%' AND ";
                command += $"[Инвентарный номер] NOT Like 'Placeholder'";
                if (command != "Where [Инвентарный номер] NOT Like 'Placeholder'")
                {
                    string Parametrs = "";
                    Parametrs += $"{Type_OT.Text}|{InvNum_OT.Text}|{Model_OT.Text}|{SeriaNum_OT.Text}|{User_OT.Text}|{Name_OT.Text}|{Cartridge_OT.Text}|{Room_OT.Text}";
                    if (ip1_OT.Text.Length > 0 || ip2_OT.Text.Length > 0 || ip3_OT.Text.Length > 0 || ip4_OT.Text.Length > 0)
                        Parametrs += $"|{mc.IPSearcher(ip1_OT.Text, ip2_OT.Text, ip3_OT.Text, ip4_OT.Text)}";
                    else Parametrs += "|";

                    if (Mode == "OT")
                    {
                        MigApp.Properties.Settings.Default.ParamsOT = Parametrs;
                        MigApp.Properties.Settings.Default.comOT = command;
                        MigApp.Properties.Settings.Default.Save();
                    }
                    else if (Mode == "OTDel")
                    {
                        MigApp.Properties.Settings.Default.ParamsOTDel = Parametrs;
                        MigApp.Properties.Settings.Default.comOTDel = command;
                        MigApp.Properties.Settings.Default.Save();
                    }
                    DialogResult = true;
                }
                this.Close();
            }

            // Мониторы
            else if (Mode == "Mon" || Mode == "MonDel")
            {
                if (InvNum_Mon.Text.Length > 0)
                    command += $"[Инвентарный номер] Like '{InvNum_Mon.Text}' AND ";
                if (Firm_Mon.Text.Length > 0)
                    command += $"Производитель Like '%{Firm_Mon.Text}%' AND ";
                if (Model_Mon.Text.Length > 0)
                    command += $"Модель Like '%{Model_Mon.Text}%' AND ";
                if (SeriaNum_Mon.Text.Length > 0)
                    command += $"[Серийный номер] Like '%{SeriaNum_Mon.Text}%' AND ";
                if (ScreenDiagonal_Mon.Text.Length > 0)
                    command += $"[Диагональ экрана] Like '%{ScreenDiagonal_Mon.Text}%' AND ";
                if (ScreenResolution_Mon.Text != "____x____")
                    command += $"Разрешение Like '{ScreenResolution_Mon.Text}' AND ";
                if (ScreenType_Mon.Text.Length > 0)
                    command += $"[Тип экрана] Like '{ScreenType_Mon.Text}' AND ";
                if (User_Mon.Text.Length > 0)
                    command += $"Пользователь Like '%{User_Mon.Text}%' AND ";
                command += "[Инвентарный номер] NOT Like 'Placeholder'";
                if (command != "Where [Инвентарный номер] NOT Like 'Placeholder'")
                {
                    string Param = $"{InvNum_Mon.Text}|{Firm_Mon.Text}|{Model_Mon.Text}|{SeriaNum_Mon.Text}|{ScreenDiagonal_Mon.Text}";
                    if (ScreenResolution_Mon.Text != "____x____")
                        Param += $"|{ScreenResolution_Mon.Text.Replace("_", "%")}";
                    else Param += "|";
                    Param += $"|{ScreenType_Mon.Text}|{User_Mon.Text}";

                    if (Mode == "Mon")
                    {
                        MigApp.Properties.Settings.Default.ParamsMon = Param;
                        MigApp.Properties.Settings.Default.comMon = command;
                        MigApp.Properties.Settings.Default.Save();
                    }
                    else if (Mode == "MonDel")
                    {
                        MigApp.Properties.Settings.Default.ParamsMonDel = Param;
                        MigApp.Properties.Settings.Default.comMonDel = command;
                        MigApp.Properties.Settings.Default.Save();
                    }
                    DialogResult = true;
                }
                this.Close();
            }

            // Роутеры
            else if (Mode == "Rout" || Mode == "RoutDel")
            {
                if (InvNum_Rout.Text.Length > 0)
                    command += $"[Инвентарный номер] LIKE '{InvNum_Rout.Text}' AND ";
                if (Name_Rout.Text.Length > 0)
                    command += $"Имя LIKE '%{Name_Rout.Text}%' AND ";
                if (Model_Rout.Text.Length > 0)
                    command += $"Модель LIKE '%{Model_Rout.Text}%' AND ";
                if (WiFiName_Rout.Text.Length > 0)
                    command += $"[Имя сети] LIKE '%{WiFiName_Rout.Text}%' AND ";
                if (ip1_Rout.Text.Length > 0 || ip2_Rout.Text.Length > 0 || ip3_Rout.Text.Length > 0 || ip4_Rout.Text.Length > 0)
                    command += $"IP Like '{mc.IPSearcher(ip1_Rout.Text, ip2_Rout.Text, ip3_Rout.Text, ip4_Rout.Text)}' AND ";
                if (dhcp1_Rout.Text.Length > 0 || dhcp2_Rout.Text.Length > 0 || dhcp3_Rout.Text.Length > 0 || dhcp4_Rout.Text.Length > 0 || dhcp5_Rout.Text.Length > 0)
                    command += $"DHCP Like '{mc.DHCPSearcher(dhcp1_Rout.Text, dhcp2_Rout.Text, dhcp3_Rout.Text, dhcp4_Rout.Text, dhcp5_Rout.Text)}' AND ";
                command += "[Инвентарный номер] NOT Like 'Placeholder'";
                if (command != "Where [Инвентарный номер] NOT Like 'Placeholder'")
                {
                    string Param = $"{InvNum_Rout.Text}|{Name_Rout.Text}|{Model_Rout.Text}|{WiFiName_Rout.Text}|";
                    if (ip1_Rout.Text.Length > 0 || ip2_Rout.Text.Length > 0 || ip3_Rout.Text.Length > 0 || ip4_Rout.Text.Length > 0)
                        Param += $"{mc.IPSearcher(ip1_Rout.Text, ip2_Rout.Text, ip3_Rout.Text, ip4_Rout.Text)}|";
                    else Param += "|";
                    if (dhcp1_Rout.Text.Length > 0 || dhcp2_Rout.Text.Length > 0 || dhcp3_Rout.Text.Length > 0 || dhcp4_Rout.Text.Length > 0 || dhcp5_Rout.Text.Length > 0)
                        Param += $"{mc.DHCPSearcher(dhcp1_Rout.Text, dhcp2_Rout.Text, dhcp3_Rout.Text, dhcp4_Rout.Text, dhcp5_Rout.Text)}";

                    if (Mode == "Route")
                    {
                        MigApp.Properties.Settings.Default.ParamsRout = Param;
                        MigApp.Properties.Settings.Default.comRout = command;
                        MigApp.Properties.Settings.Default.Save();
                    }
                    else if (Mode == "RouteDel")
                    {
                        MigApp.Properties.Settings.Default.ParamsRoutDel = Param;
                        MigApp.Properties.Settings.Default.comRoutDel = command;
                        MigApp.Properties.Settings.Default.Save();
                    }
                    DialogResult = true;
                }
                this.Close();
            }

            // Свитчи
            else if (Mode == "Switch" || Mode == "SwitchDel")
            {
                if (InvNum_Switch.Text.Length > 0)
                    command += $"[Инвентарный номер] LIKE '{InvNum_Switch.Text}' AND ";
                if (Name_Switch.Text.Length > 0)
                    command += $"Имя LIKE '%{Name_Switch.Text}%' AND ";
                if (Model_Switch.Text.Length > 0)
                    command += $"Модель LIKE '%{Model_Switch.Text}%' AND ";
                if (ip1_Switch.Text.Length > 0 || ip2_Switch.Text.Length > 0 || ip3_Switch.Text.Length > 0 || ip4_Switch.Text.Length > 0)
                    command += $"IP Like '{mc.IPSearcher(ip1_Switch.Text, ip2_Switch.Text, ip3_Switch.Text, ip4_Switch.Text)}' AND ";
                command += "[Инвентарный номер] NOT Like 'Placeholder'";
                if (command != "Where [Инвентарный номер] NOT Like 'Placeholder'")
                {
                    string Param = $"{InvNum_Switch.Text}|{Name_Switch.Text}|{Model_Switch.Text}|";
                    if (ip1_Switch.Text.Length > 0 || ip2_Switch.Text.Length > 0 || ip3_Switch.Text.Length > 0 || ip4_Switch.Text.Length > 0)
                        Param += $"{mc.IPSearcher(ip1_Switch.Text, ip2_Switch.Text, ip3_Switch.Text, ip4_Switch.Text)}";

                    if (Mode == "Switch")
                    {
                        MigApp.Properties.Settings.Default.ParamsSwitch = Param;
                        MigApp.Properties.Settings.Default.comSwitch = command;
                        MigApp.Properties.Settings.Default.Save();
                    }
                    else if (Mode == "SwitchDel")
                    {
                        MigApp.Properties.Settings.Default.ParamsSwitchDel = Param;
                        MigApp.Properties.Settings.Default.comSwitchDel = command;
                        MigApp.Properties.Settings.Default.Save();
                    }
                    DialogResult = true;
                }
                this.Close();
            }

            // Мебель
            else if (Mode == "Furniture" || Mode == "FurnitureDel")
            {
                if (InvNum_Furniture.Text.Length > 0)
                    command += $"[Инвентарный номер] LIKE '{InvNum_Furniture.Text}' AND ";
                if (Type_Furniture.Text != "Не выбрано")
                    command += $"Тип LIKE '{Type_Furniture.Text}' AND ";
                else command += " ";
                if (Name_Furniture.Text.Length > 0)
                    command += $"Имя LIKE '%{Name_Furniture.Text}%' AND ";
                if (Room_Furniture.Text.Length > 0)
                    command += $"Расположение LIKE '%{Room_Furniture.Text}%' AND ";
                command += "[Инвентарный номер] NOT Like 'Placeholder'";
                if (command != "Where [Инвентарный номер] NOT Like 'Placeholder'")
                {
                    string Param = $"{InvNum_Furniture.Text}|{Type_Furniture.Text}|{Name_Furniture.Text}|{Room_Furniture.Text}";
                    
                    if (Mode == "Furniture")
                    {
                        MigApp.Properties.Settings.Default.ParamsFurn = Param;
                        MigApp.Properties.Settings.Default.comFurn = command;
                        MigApp.Properties.Settings.Default.Save();
                    }
                    else if (Mode == "FurnitureDel")
                    {
                        MigApp.Properties.Settings.Default.ParamsFurnDel = Param;
                        MigApp.Properties.Settings.Default.comFurnDel = command;
                        MigApp.Properties.Settings.Default.Save();
                    }
                    DialogResult = true;
                }
                this.Close();
            }
            #endregion

            #region Админпанель
            // Пользователи
            else if (Mode == "Users")
            {
                if (ID_User.Text.Length > 0)
                    command += $"ID Like '{ID_User.Text}' AND ";
                if (Login_User.Text.Length > 0)
                    command += $"Логин Like '%{Login_User.Text}%' AND ";
                if (Employee_User.Text.Length > 0)
                    command += $"Сотрудник Like '%{Employee_User.Text}%' AND ";
                if (Role_User.Text.Length > 0)
                    command += $"Роль Like '%{Role_User.Text}%' AND ";
                if (NoPassword_User.IsChecked == true)
                    command += $"Пароль Like 'Сброшен' AND ";
                command += "ID NOT Like 'NULL'";
                if (command != "Where ID NOT Like 'NULL'")
                {
                    MigApp.Properties.Settings.Default.ParamsUsers = $"{ID_User.Text}|{Login_User.Text}|{Employee_User.Text}|{Role_User.Text}";
                    if (NoPassword_User.IsChecked == true)
                        MigApp.Properties.Settings.Default.ParamsUsers += "|Без пароля";
                    else MigApp.Properties.Settings.Default.ParamsUsers += "|";
                    MigApp.Properties.Settings.Default.comUsers = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                Close();
            }

            // Логи
            else if (Mode == "Logs")
            {
                if (ID_Logs.Text.Length > 0)
                    command += $"ID Like '{ID_Logs.Text}' AND ";
                if (Date_LogsStart.Text.Length > 0 && Date_LogsEnd.Text.Length > 0)
                    command += $"ActionDate >= '{Date_LogsStart.Text}' AND ActionDate <= '{Date_LogsEnd.Text + " 23:59"}' AND ";
                else if (Date_LogsStart.Text.Length > 0 && Date_LogsEnd.Text.Length == 0)
                    command += $"ActionDate >= '{Date_LogsStart.Text}' AND ";
                else if (Date_LogsStart.Text.Length == 0 && Date_LogsEnd.Text.Length > 0)
                    command += $"ActionDate <='{Date_LogsEnd.Text + " 23:59"}' AND ";
                if (User_Logs.Text.Length > 0)
                    command += $"Пользователь Like '%{User_Logs.Text}%' AND ";
                if (Action_Logs.Text.Length > 0 && Action_Logs.Text != "Не выбрано")
                    command += $"Действие Like '{Action_Logs.Text}' AND ";
                if (Table_Logs.Text.Length > 0 && Table_Logs.Text != "Не выбрано")
                    command += $"Таблица Like '{Table_Logs.Text}' AND ";
                if (Row_Logs.Text.Length > 0)
                    command += $"Запись Like '%{Row_Logs.Text}%' AND ";
                command += $"ID NOT LIKE 'NULL'";
                if (command != "Where ID NOT LIKE 'NULL'")
                {
                    string param = $"{ID_Logs.Text}|{Date_LogsStart.Text}|{Date_LogsEnd.Text}|{User_Logs.Text}";
                    if (Action_Logs.Text != "Не выбрано")
                        param += $"|{Action_Logs.Text}";
                    else param += "|";
                    if (Table_Logs.Text != "Не выбрано")
                        param += $"|{Table_Logs.Text}";
                    else param += "|";
                    param += $"|{Row_Logs.Text}";
                    MigApp.Properties.Settings.Default.ParamsLogs = param;
                    MigApp.Properties.Settings.Default.comLogs = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                Close();
            }
            #endregion

            #region Отчёты

            // Компьютеры
            else if (Mode == "PCRep")
            {
                if (Employee_Report1.Text.Length > 0)
                    command += $"Сотрудник Like '%{Employee_Report1.Text}%' AND ";
                if (Group_Report1.Text.Length > 0)
                    command += $"Отдел Like '%{Group_Report1.Text}%' AND ";
                if (Room_Report1.Text.Length > 0)
                    command += $"Кабинет Like '{Room_Report1.Text}' AND ";
                if (PCInvNum_Report1.Text.Length > 0)
                    command += $"Компьютер Like '{PCInvNum_Report1.Text}' AND ";
                if (PCName_Report1.Text.Length > 0)
                    command += $"Имя Like '%{PCName_Report1.Text}%' AND ";
                if (MonitorInvNum_Report1.Text.Length > 0)
                    command += $"Монитор Like '{MonitorInvNum_Report1.Text}' AND ";
                if (MonitorModel_Report1.Text.Length > 0)
                    command += $"[Модель монитора] Like '%{MonitorModel_Report1.Text}%' AND ";
                if (OTInvNum_Report1.Text.Length > 0)
                    command += $"[Орг техника] Like '{OTInvNum_Report1.Text}' AND ";
                if (OTType_Report1.Text != "Не выбрано" && OTType_Report1.Text.Length > 0)
                    command += $"[Тип техники] Like '{OTType_Report1.Text}' AND ";
                if (OTModel_Report1.Text.Length > 0)
                    command += $"[Модель техники] Like '%{OTModel_Report1.Text}%' AND ";
                command += $"Сотрудник NOT Like 'NULL'";
                if (command != "Where Сотрудник NOT Like 'NULL'")
                {
                    string Params = $"{Employee_Report1.Text}|{Group_Report1.Text}|{Room_Report1.Text}|{PCInvNum_Report1.Text}|{PCName_Report1.Text}|{MonitorInvNum_Report1.Text}|{MonitorModel_Report1.Text}|{OTInvNum_Report1.Text}";
                    if (OTType_Report1.Text != "Не выбрано")
                        Params += $"|{OTType_Report1.Text}";
                    else Params += "|";
                    Params += $"|{OTModel_Report1.Text}";
                    MigApp.Properties.Settings.Default.ParamsPCRep = Params;
                    MigApp.Properties.Settings.Default.comPCRep = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                Close();
            }

            // Ноутбуки
            else if (Mode == "NBRep")
            {
                if (Employee_Report2.Text.Length > 0)
                    command += $"Сотрудник Like '%{Employee_Report2.Text}%' AND ";
                if (Group_Report2.Text.Length > 0)
                    command += $"Отдел Like '%{Group_Report2.Text}%' AND ";
                if (Room_Report2.Text.Length > 0)
                    command += $"Кабинет Like '{Room_Report2.Text}' AND ";
                if (NotebookInvNum_Report2.Text.Length > 0)
                    command += $"Ноутбук Like '{NotebookInvNum_Report2.Text}' AND ";
                if (NotebookName_Report2.Text.Length > 0)
                    command += $"[Модель ноутбука] Like '%{NotebookName_Report2.Text}%' AND ";
                command += $"Сотрудник NOT Like 'NULL'";
                if (command != "Where Сотрудник NOT Like 'NULL'")
                {
                    string Params = $"{Employee_Report2.Text}|{Group_Report2.Text}|{Room_Report2.Text}|{NotebookInvNum_Report2.Text}|{NotebookName_Report2.Text}";
                    MigApp.Properties.Settings.Default.ParamsNBRep = Params;
                    MigApp.Properties.Settings.Default.comNBRep = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                Close();
            }

            // Планшеты
            else if (Mode == "TabRep")
            {
                if (Employee_Report3.Text.Length > 0)
                    command += $"Сотрудник Like '%{Employee_Report3.Text}%' AND ";
                if (Group_Report3.Text.Length > 0)
                    command += $"Отдел Like '%{Group_Report3.Text}%' AND ";
                if (Room_Report3.Text.Length > 0)
                    command += $"Кабинет Like '{Room_Report3.Text}' AND ";
                if (TabletInvNum_Report3.Text.Length > 0)
                    command += $"Планшет Like '{TabletInvNum_Report3.Text}' AND ";
                if (TabletName_Report3.Text.Length > 0)
                    command += $"[Модель планшета] Like '%{TabletName_Report3.Text}%' AND ";
                command += $"Сотрудник NOT Like 'NULL'";
                if (command != "Where Сотрудник NOT Like 'NULL'")
                {
                    string Params = $"{Employee_Report3.Text}|{Group_Report3.Text}|{Room_Report3.Text}|{TabletInvNum_Report3.Text}|{TabletName_Report3.Text}";
                    MigApp.Properties.Settings.Default.ParamsTabRep = Params;
                    MigApp.Properties.Settings.Default.comTabRep = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                Close();
            }
            #endregion
        }

        // Проверка на цифры
        private void NumOnly(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        #region IP Box для компьтера

        // Проверка на цифры
        private void NumOnlyIP(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        // Проверка до 255 и переключение на следующий
        private void IPcheck1(object sender, TextChangedEventArgs e)
        {
            if (ip1_PC.Text.Length == 3)
                ip2_PC.Focus();
            try
            {
                if (Convert.ToInt32(ip1_PC.Text) > 255)
                    ip1_PC.Text = "255";
            }
            catch { }
        }

        private void IPcheck2(object sender, TextChangedEventArgs e)
        {
            if (ip2_PC.Text.Length == 3)
                ip3_PC.Focus();
            try
            {
                if (Convert.ToInt32(ip2_PC.Text) > 255)
                    ip2_PC.Text = "255";
            }
            catch { }
        }

        private void IPcheck3(object sender, TextChangedEventArgs e)
        {
            if (ip3_PC.Text.Length == 3)
                ip4_PC.Focus();
            try
            {
                if (Convert.ToInt32(ip3_PC.Text) > 255)
                    ip3_PC.Text = "255";
            }
            catch { }
        }

        private void IPcheck4(object sender, TextChangedEventArgs e)
        {
            if (ip4_PC.Text.Length == 3)
                e.Handled = false;
            try
            {
                if (Convert.ToInt32(ip4_PC.Text) > 255)
                    ip4_PC.Text = "255";
            }
            catch { }
        }

        private void IPfocus(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            e.Handled = true;
            if (textBox != null)
                textBox.SelectAll();
        }

        // Запрет на пробелы
        private void NextIP1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip2_PC.Focus();
            }
        }

        private void NextIP2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip3_PC.Focus();
            }
            if (e.Key == Key.Back && ip2_PC.Text.Length == 0)
            {
                e.Handled = true;
                ip1_PC.Focus();
            }

        }

        private void NextIP3(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip4_PC.Focus();
            }
            if (e.Key == Key.Back && ip3_PC.Text.Length == 0)
            {
                e.Handled = true;
                ip2_PC.Focus();
            }
        }

        private void NextIP4(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Back && ip4_PC.Text.Length == 0)
            {
                e.Handled = true;
                ip3_PC.Focus();
            }
        }
        #endregion

        #region IP Box для Орг.техники

        // Проверка до 255 и переключение на следующий
        private void IPcheck5(object sender, TextChangedEventArgs e)
        {
            if (ip1_OT.Text.Length == 3)
                ip2_OT.Focus();
            try
            {
                if (Convert.ToInt32(ip1_OT.Text) > 255)
                    ip1_OT.Text = "255";
            }
            catch { }
        }

        private void IPcheck6(object sender, TextChangedEventArgs e)
        {
            if (ip2_OT.Text.Length == 3)
                ip3_OT.Focus();
            try
            {
                if (Convert.ToInt32(ip2_OT.Text) > 255)
                    ip2_OT.Text = "255";
            }
            catch { }
        }

        private void IPcheck7(object sender, TextChangedEventArgs e)
        {
            if (ip3_OT.Text.Length == 3)
                ip4_OT.Focus();
            try
            {
                if (Convert.ToInt32(ip3_OT.Text) > 255)
                    ip3_OT.Text = "255";
            }
            catch { }
        }

        private void IPcheck8(object sender, TextChangedEventArgs e)
        {
            if (ip4_OT.Text.Length == 3)
                e.Handled = false;
            try
            {
                if (Convert.ToInt32(ip4_OT.Text) > 255)
                    ip4_OT.Text = "255";
            }
            catch { }
        }

        private void IPfocus1(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            e.Handled = true;
            if (textBox != null)
                textBox.SelectAll();
        }

        // Запрет на пробелы
        private void NextIP5(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip2_OT.Focus();
            }
        }

        private void NextIP6(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip3_OT.Focus();
            }
            if (e.Key == Key.Back && ip2_OT.Text.Length == 0)
            {
                e.Handled = true;
                ip1_OT.Focus();
            }

        }

        private void NextIP7(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip4_OT.Focus();
            }
            if (e.Key == Key.Back && ip3_OT.Text.Length == 0)
            {
                e.Handled = true;
                ip2_OT.Focus();
            }
        }

        private void NextIP8(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Back && ip4_OT.Text.Length == 0)
            {
                e.Handled = true;
                ip3_OT.Focus();
            }
        }
        #endregion

        #region IP Box для Роутера

        // Проверка до 255 и переключение на следующий
        private void Rout_IPcheck1(object sender, TextChangedEventArgs e)
        {
            if (ip1_Rout.Text.Length == 3)
                ip2_Rout.Focus();
            try
            {
                if (Convert.ToInt32(ip1_Rout.Text) > 255)
                    ip1_Rout.Text = "255";
            }
            catch { }
        }

        private void Rout_IPcheck2(object sender, TextChangedEventArgs e)
        {
            if (ip2_Rout.Text.Length == 3)
                ip3_Rout.Focus();
            try
            {
                if (Convert.ToInt32(ip2_Rout.Text) > 255)
                    ip2_Rout.Text = "255";
            }
            catch { }
        }

        private void Rout_IPcheck3(object sender, TextChangedEventArgs e)
        {
            if (ip3_Rout.Text.Length == 3)
                ip4_Rout.Focus();
            try
            {
                if (Convert.ToInt32(ip3_Rout.Text) > 255)
                    ip3_Rout.Text = "255";
            }
            catch { }
        }

        private void Rout_IPcheck4(object sender, TextChangedEventArgs e)
        {
            if (ip4_Rout.Text.Length == 3)
                e.Handled = false;
            try
            {
                if (Convert.ToInt32(ip4_Rout.Text) > 255)
                    ip4_Rout.Text = "255";
            }
            catch { }
        }

        private void Rout_IPfocus(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            e.Handled = true;
            if (textBox != null)
                textBox.SelectAll();
        }

        // Запрет на пробелы
        private void Rout_NextIP1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip2_Rout.Focus();
            }
        }

        private void Rout_NextIP2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip3_Rout.Focus();
            }
            if (e.Key == Key.Back && ip2_Rout.Text.Length == 0)
            {
                e.Handled = true;
                ip1_Rout.Focus();
            }

        }

        private void Rout_NextIP3(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip4_Rout.Focus();
            }
            if (e.Key == Key.Back && ip3_Rout.Text.Length == 0)
            {
                e.Handled = true;
                ip2_Rout.Focus();
            }
        }

        private void Rout_NextIP4(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Back && ip4_Rout.Text.Length == 0)
            {
                e.Handled = true;
                ip3_Rout.Focus();
            }
        }
        #endregion

        #region DHCP Box для Роутера

        // Проверка до 255 и переключение на следующий
        private void Rout_DHCPcheck1(object sender, TextChangedEventArgs e)
        {
            if (dhcp1_Rout.Text.Length == 3)
                dhcp2_Rout.Focus();
            try
            {
                if (Convert.ToInt32(dhcp1_Rout.Text) > 255)
                    dhcp1_Rout.Text = "255";
            }
            catch { }
        }

        private void Rout_DHCPcheck2(object sender, TextChangedEventArgs e)
        {
            if (dhcp2_Rout.Text.Length == 3)
                dhcp3_Rout.Focus();
            try
            {
                if (Convert.ToInt32(dhcp2_Rout.Text) > 255)
                    dhcp2_Rout.Text = "255";
            }
            catch { }
        }

        private void Rout_DHCPcheck3(object sender, TextChangedEventArgs e)
        {
            if (dhcp3_Rout.Text.Length == 3)
                dhcp4_Rout.Focus();
            try
            {
                if (Convert.ToInt32(dhcp3_Rout.Text) > 255)
                    dhcp3_Rout.Text = "255";
            }
            catch { }
        }

        private void Rout_DHCPcheck4(object sender, TextChangedEventArgs e)
        {
            if (dhcp4_Rout.Text.Length == 3)
                dhcp4_Rout.Focus();
            try
            {
                if (Convert.ToInt32(dhcp4_Rout.Text) > 255)
                    dhcp4_Rout.Text = "255";
            }
            catch { }
        }

        private void Rout_DHCPcheck5(object sender, TextChangedEventArgs e)
        {
            if (dhcp5_Rout.Text.Length == 3)
                e.Handled = false;
            try
            {
                if (Convert.ToInt32(dhcp5_Rout.Text) > 255)
                    dhcp5_Rout.Text = "255";
            }
            catch { }
        }

        private void Rout_DHCPfocus(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            e.Handled = true;
            if (textBox != null)
                textBox.SelectAll();
        }

        // Запрет на пробелы
        private void Rout_NextDHCP1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                dhcp2_Rout.Focus();
            }
        }

        private void Rout_NextDHCP2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                dhcp3_Rout.Focus();
            }
            if (e.Key == Key.Back && dhcp2_Rout.Text.Length == 0)
            {
                e.Handled = true;
                dhcp1_Rout.Focus();
            }

        }

        private void Rout_NextDHCP3(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                dhcp4_Rout.Focus();
            }
            if (e.Key == Key.Back && dhcp3_Rout.Text.Length == 0)
            {
                e.Handled = true;
                dhcp2_Rout.Focus();
            }
        }

        private void Rout_NextDHCP4(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                dhcp5_Rout.Focus();
            }
            if (e.Key == Key.Back && dhcp4_Rout.Text.Length == 0)
            {
                e.Handled = true;
                dhcp3_Rout.Focus();
            }
        }

        private void Rout_NextDHCP5(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Back && dhcp5_Rout.Text.Length == 0)
            {
                e.Handled = true;
                dhcp4_Rout.Focus();
            }
        }
        #endregion

        #region IP Box для Свитча

        //Проверка до 255 и переключение на следующий
        private void Switch_IPcheck1(object sender, TextChangedEventArgs e)
        {
            if (ip1_Switch.Text.Length == 3)
                ip2_Switch.Focus();
            try
            {
                if (Convert.ToInt32(ip1_Switch.Text) > 255)
                    ip1_Switch.Text = "255";
            }
            catch { }
        }

        private void Switch_IPcheck2(object sender, TextChangedEventArgs e)
        {
            if (ip2_Switch.Text.Length == 3)
                ip3_Switch.Focus();
            try
            {
                if (Convert.ToInt32(ip2_Switch.Text) > 255)
                    ip2_Switch.Text = "255";
            }
            catch { }
        }

        private void Switch_IPcheck3(object sender, TextChangedEventArgs e)
        {
            if (ip3_Switch.Text.Length == 3)
                ip4_Switch.Focus();
            try
            {
                if (Convert.ToInt32(ip3_Switch.Text) > 255)
                    ip3_Switch.Text = "255";
            }
            catch { }
        }

        private void Switch_IPcheck4(object sender, TextChangedEventArgs e)
        {
            if (ip4_Switch.Text.Length == 3)
                e.Handled = false;
            try
            {
                if (Convert.ToInt32(ip4_Switch.Text) > 255)
                    ip4_Switch.Text = "255";
            }
            catch { }
        }

        private void Switch_IPfocus(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            e.Handled = true;
            if (textBox != null)
                textBox.SelectAll();
        }

        //Запрет на пробелы
        private void Switch_NextIP1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip2_Switch.Focus();
            }
        }

        private void Switch_NextIP2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip3_Switch.Focus();
            }
            if (e.Key == Key.Back && ip2_Switch.Text.Length == 0)
            {
                e.Handled = true;
                ip1_Switch.Focus();
            }

        }

        private void Switch_NextIP3(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip4_Switch.Focus();
            }
            if (e.Key == Key.Back && ip3_Switch.Text.Length == 0)
            {
                e.Handled = true;
                ip2_Switch.Focus();
            }
        }

        private void Switch_NextIP4(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Back && ip4_Switch.Text.Length == 0)
            {
                e.Handled = true;
                ip3_Switch.Focus();
            }
        }
        #endregion

        private void FillEmpGroup()
        {
            List.Clear();
            DataTable table = new DataTable();
            table = sqlcc.DataGridUpdate("*", "Group_View", "");
            List.Add("Не выбрано");
            foreach (DataRow row in table.Rows)
            {
                List.Add(row["Name"].ToString());
            }
            Group_Emp.ItemsSource = List;
            Group_Emp.SelectedIndex = 0;
        }

        private void FillFurnitureTypes()
        {
            List.Clear();
            DataTable table = new DataTable();
            table = sqlcc.DataGridUpdate("*", "FurnitureType", "");
            List.Add("Не выбрано");
            foreach (DataRow row in table.Rows)
            {
                List.Add(row["Name"].ToString());
            }
            Type_Furniture.ItemsSource = List;
            Type_Furniture.SelectedIndex = 0;
        }

        private List<string> List = new List<string>();

        private void HotKeys(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
