using System;
using System.Collections.Generic;
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
            if (mode == "Emp") 
            { 
                SearchPanel_Emp.Visibility = Visibility.Visible;
                this.Height = 330;
                this.Width = 550;
                if (MigApp.Properties.Settings.Default.ParamsEmp != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.ParamsEmp.Split('|');
                    ID_Emp.Text = Parametrs[0];
                    FIO_Emp.Text = Parametrs[1];
                    Room_Emp.Text = Parametrs[2];
                    Group_Emp.Text = Parametrs[3];
                }
            }

            // ПК
            else if (mode == "PC")
            {
                SearchPanel_PC.Visibility = Visibility.Visible;
                this.Height = 500;
                if (MigApp.Properties.Settings.Default.ParamsPC != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.ParamsPC.Split('|');
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
            else if (mode == "NB")
            {
                SearchPanel_NB.Visibility = Visibility.Visible;
                this.Height = 550;
                if (MigApp.Properties.Settings.Default.ParamsNB != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.ParamsNB.Split('|');
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
            else if (mode == "Tab")
            {
                SearchPanel_Tab.Visibility = Visibility.Visible;
                this.Height = 450;
                if (MigApp.Properties.Settings.Default.ParamsTab != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.ParamsTab.Split('|');
                    InvNum_Tab.Text = Parametrs[0];
                    Model_Tab.Text = Parametrs[1];
                    SeriaNum_Tab.Text = Parametrs[2];
                    User_Tab.Text = Parametrs[3];
                    ScreenDiagonal_Tab.Text = Parametrs[4];
                    Processor_Tab.Text = Parametrs[5];
                    RAM_Tab.Text= Parametrs[6];
                    Drive_Tab.Text = Parametrs[7];
                    Other_Tab.Text = Parametrs[8];
                }
            }

            // Орг.техника
            else if (mode == "OT")
            {
                SearchPanel_OT.Visibility = Visibility.Visible;
                this.Height = 420;
                Type_OT.Text = "Не выбрано";
                if (MigApp.Properties.Settings.Default.ParamsOT != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.ParamsOT.Split('|');
                    if (Parametrs[0].Length > 0 && Parametrs[0] != "Не выбрано")
                        Type_OT.Text = Parametrs[0];
                    else Type_OT.Text = "Не выбрано";
                    InvNum_OT.Text = Parametrs[1];
                    Model_OT.Text = Parametrs[2];
                    SeriaNum_OT.Text = Parametrs[3];
                    User_OT.Text = Parametrs[4];
                    Name_OT.Text = Parametrs[5];
                    Cartridge_OT.Text = Parametrs[6];
                    try
                    {
                        string[] ip = Parametrs[7].Split('.');
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
            else if (mode == "Mon")
            {
                SearchPanel_Mon.Visibility = Visibility.Visible;
                this.Height = 400;
                if (MigApp.Properties.Settings.Default.ParamsMon != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.ParamsMon.Split('|');
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
            else if (mode == "Rout")
            {
                SearchPanel_Rout.Visibility = Visibility.Visible;
                this.Height = 320;
                if (MigApp.Properties.Settings.Default.ParamsRout != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.ParamsRout.Split('|');
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
                    catch { }
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
                    catch { }
                }
            }

            // Свитчи
            else if (mode == "Switch")
            {
                SearchPanel_Switch.Visibility = Visibility.Visible;
                this.Height = 250;
                if (MigApp.Properties.Settings.Default.ParamsSwitch != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.ParamsSwitch.Split('|');
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

            #region Архив
            // Сотрудники
            else if (mode == "EmpDel")
            {
                SearchPanel_EmpDel.Visibility = Visibility.Visible;
                this.Height = 330;
                this.Width = 550;
                if (MigApp.Properties.Settings.Default.ParamsEmpDel != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.ParamsEmpDel.Split('|');
                    ID_Emp_Del.Text = Parametrs[0];
                    FIO_Emp_Del.Text = Parametrs[1];
                    Room_Emp_Del.Text = Parametrs[2];
                    Group_Emp_Del.Text = Parametrs[3];
                }
            }

            // ПК
            else if (mode == "PCDel")
            {
                SearchPanel_PCDel.Visibility = Visibility.Visible;
                this.Height = 500;
                if (MigApp.Properties.Settings.Default.ParamsPCDel != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.ParamsPCDel.Split('|');
                    InvNum_PC_Del.Text = Parametrs[0];
                    Name_PC_Del.Text = Parametrs[1];
                    User_PC_Del.Text = Parametrs[2];
                    OS_PC_Del.Text = Parametrs[3];
                    Motherboard_PC_Del.Text = Parametrs[4];
                    Processor_PC_Del.Text = Parametrs[5];
                    RAM_PC_Del.Text = Parametrs[6];
                    Drive_PC_Del.Text = Parametrs[7];
                    Other_PC_Del.Text = Parametrs[8];
                    try
                    {
                        string[] ip = Parametrs[9].Split('.');
                        if (ip[0] != "%")
                            ip1_PC_Del.Text = ip[0];
                        if (ip[1] != "%")
                            ip2_PC_Del.Text = ip[1];
                        if (ip[2] != "%")
                            ip3_PC_Del.Text = ip[2];
                        if (ip[3] != "%")
                            ip4_PC_Del.Text = ip[3];
                    }
                    catch { }
                }
            }

            // Ноутбуки
            else if (mode == "NBDel")
            {
                SearchPanel_NBDel.Visibility = Visibility.Visible;
                this.Height = 550;
                if (MigApp.Properties.Settings.Default.ParamsNBDel != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.ParamsNBDel.Split('|');
                    InvNum_NB_Del.Text = Parametrs[0];
                    Model_NB_Del.Text = Parametrs[1];
                    SeriaNum_NB_Del.Text = Parametrs[2];
                    User_NB_Del.Text = Parametrs[3];
                    ScreenDiagonal_NB_Del.Text = Parametrs[4];
                    ScreenResolution_NB_Del.Text = Parametrs[5];
                    OS_NB_Del.Text = Parametrs[6];
                    Processor_NB_Del.Text = Parametrs[7];
                    RAM_NB_Del.Text = Parametrs[8];
                    Drive_NB_Del.Text = Parametrs[9];
                    Other_NB_Del.Text = Parametrs[10];
                }
            }

            // Планшеты
            else if (mode == "TabDel")
            {
                SearchPanel_TabDel.Visibility = Visibility.Visible;
                this.Height = 450;
                if (MigApp.Properties.Settings.Default.ParamsTabDel != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.ParamsTabDel.Split('|');
                    InvNum_Tab_Del.Text = Parametrs[0];
                    Model_Tab_Del.Text = Parametrs[1];
                    SeriaNum_Tab_Del.Text = Parametrs[2];
                    User_Tab_Del.Text = Parametrs[3];
                    ScreenDiagonal_Tab_Del.Text = Parametrs[4];
                    Processor_Tab_Del.Text = Parametrs[5];
                    RAM_Tab_Del.Text = Parametrs[6];
                    Drive_Tab_Del.Text = Parametrs[7];
                    Other_Tab_Del.Text = Parametrs[8];
                }
            }

            // Орг.техника
            else if (mode == "OTDel")
            {
                SearchPanel_OTDel.Visibility = Visibility.Visible;
                this.Height = 400;
                Type_OT_Del.Text = "Не выбрано";
                if (MigApp.Properties.Settings.Default.ParamsOTDel != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.ParamsOTDel.Split('|');
                    if (Parametrs[0].Length > 0 && Parametrs[0] != "Не выбрано")
                        Type_OT_Del.Text = Parametrs[0];
                    else Type_OT_Del.Text = "Не выбрано";
                    InvNum_OT_Del.Text = Parametrs[1];
                    Model_OT_Del.Text = Parametrs[2];
                    SeriaNum_OT_Del.Text = Parametrs[3];
                    User_OT_Del.Text = Parametrs[4];
                    Name_OT_Del.Text = Parametrs[5];
                    Cartridge_OT_Del.Text = Parametrs[6];
                    try
                    {
                        string[] ip = Parametrs[7].Split('.');
                        if (ip[0] != "%")
                            ip1_OT_Del.Text = ip[0];
                        if (ip[1] != "%")
                            ip2_OT_Del.Text = ip[1];
                        if (ip[2] != "%")
                            ip3_OT_Del.Text = ip[2];
                        if (ip[3] != "%")
                            ip4_OT_Del.Text = ip[3];
                    }
                    catch { }
                }
            }

            // Мониторы
            else if (mode == "MonDel")
            {
                SearchPanel_MonDel.Visibility = Visibility.Visible;
                this.Height = 400;
                if (MigApp.Properties.Settings.Default.ParamsMonDel != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.ParamsMonDel.Split('|');
                    InvNum_Mon_Del.Text = Parametrs[0];
                    Firm_Mon_Del.Text = Parametrs[1];
                    Model_Mon_Del.Text = Parametrs[2];
                    SeriaNum_Mon_Del.Text = Parametrs[3];
                    ScreenDiagonal_Mon_Del.Text = Parametrs[4];
                    ScreenResolution_Mon_Del.Text = Parametrs[5];
                    ScreenType_Mon_Del.Text = Parametrs[6];
                    User_Mon_Del.Text = Parametrs[7];
                }
            }

            // Роутеры
            else if (mode == "RoutDel")
            {
                SearchPanel_RoutDel.Visibility = Visibility.Visible;
                this.Height = 320;
                if (MigApp.Properties.Settings.Default.ParamsRoutDel != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.ParamsRoutDel.Split('|');
                    InvNum_RoutDel.Text = Parametrs[0];
                    Name_RoutDel.Text = Parametrs[1];
                    Model_RoutDel.Text = Parametrs[2];
                    WiFiName_RoutDel.Text = Parametrs[3];
                    try
                    {
                        string[] ip = Parametrs[4].Split('.');
                        if (ip[0] != "%")
                            ip1_RoutDel.Text = ip[0];
                        if (ip[1] != "%")
                            ip2_RoutDel.Text = ip[1];
                        if (ip[2] != "%")
                            ip3_RoutDel.Text = ip[2];
                        if (ip[3] != "%")
                            ip4_RoutDel.Text = ip[3];
                    }
                    catch { }
                    try
                    {
                        string[] dhcp = Parametrs[5].Split('.');
                        if (dhcp[0] != "%")
                            dhcp1_RoutDel.Text = dhcp[0];
                        if (dhcp[1] != "%")
                            dhcp2_RoutDel.Text = dhcp[1];
                        if (dhcp[2] != "%")
                            dhcp3_RoutDel.Text = dhcp[2];
                        string[] dhcp45 = dhcp[3].Split('-');
                        if (dhcp[3] != "%")
                            dhcp4_RoutDel.Text = dhcp45[0];
                        if (dhcp[4] != "%")
                            dhcp5_RoutDel.Text = dhcp45[1];
                    }
                    catch { }
                }
            }

            // Свитчи
            else if (mode == "SwitchDel")
            {
                SearchPanel_SwitchDel.Visibility = Visibility.Visible;
                this.Height = 250;
                if (MigApp.Properties.Settings.Default.ParamsSwitchDel != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.ParamsSwitchDel.Split('|');
                    InvNum_SwitchDel.Text = Parametrs[0];
                    Name_SwitchDel.Text = Parametrs[1];
                    Model_SwitchDel.Text = Parametrs[2];
                    try
                    {
                        string[] ip = Parametrs[4].Split('.');
                        if (ip[0] != "%")
                            ip1_SwitchDel.Text = ip[0];
                        if (ip[1] != "%")
                            ip2_SwitchDel.Text = ip[1];
                        if (ip[2] != "%")
                            ip3_SwitchDel.Text = ip[2];
                        if (ip[3] != "%")
                            ip4_SwitchDel.Text = ip[3];
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
            if (Mode == "Emp")
            {
                if (ID_Emp.Text.Length > 0)
                    command += $"ID Like '{ID_Emp.Text}' AND ";
                if (FIO_Emp.Text.Length > 0)
                    command += $"[ФИО] Like '%{FIO_Emp.Text}%' AND ";
                if (Room_Emp.Text.Length > 0)
                    command += $"[Кабинет] Like '{Room_Emp.Text}' AND ";
                if (Group_Emp.Text.Length > 0)
                    command += $"[Отдел] Like '%{Group_Emp.Text}%' AND ";
                command += "ID NOT Like 'NULL'";
                if (command != "Where ID NOT Like 'NULL'")
                {
                    MigApp.Properties.Settings.Default.ParamsEmp = $"{ID_Emp.Text}|{FIO_Emp.Text}|{Room_Emp.Text}|{Group_Emp.Text}";
                    MigApp.Properties.Settings.Default.comEmp = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }

            //ПК
            else if (Mode == "PC")
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
                    MigApp.Properties.Settings.Default.ParamsPC = Parametrs;
                    MigApp.Properties.Settings.Default.comPC = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
                
            }

            // Ноутбуки
            else if (Mode == "NB")
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
                    MigApp.Properties.Settings.Default.ParamsNB = Param;
                    MigApp.Properties.Settings.Default.comNB = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }

            // Планшеты
            else if (Mode == "Tab")
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
                    MigApp.Properties.Settings.Default.ParamsTab = $"{InvNum_Tab.Text}|{Model_Tab.Text}|{SeriaNum_Tab.Text}|{User_Tab.Text}|{ScreenDiagonal_Tab.Text}|{Processor_Tab.Text}|{RAM_Tab.Text}|{Drive_Tab.Text}|{Other_Tab.Text}";
                    MigApp.Properties.Settings.Default.comTab = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }

            // Орг.техника
            else if (Mode == "OT")
            {
                if (InvNum_OT.Text.Length > 0)
                    command += $"[Инвентарный номер] Like '{InvNum_OT.Text}' AND ";
                if (Type_OT.Text.Length > 0 && Type_OT.Text != "Не выбрано")
                    command += $"Тип Like '{Type_OT.Text}' AND ";
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
                command += $"[Инвентарный номер] NOT Like 'Placeholder'";
                if (command != "Where [Инвентарный номер] NOT Like 'Placeholder'")
                {
                    string Parametrs = "";
                    if (Type_OT.Text != "Не выбрано")
                        Parametrs = $"{Type_OT.Text}|";
                    else Parametrs = "|";
                    Parametrs += $"{InvNum_OT.Text}|{Model_OT.Text}|{SeriaNum_OT.Text}|{User_OT.Text}|{Name_OT.Text}|{Cartridge_OT.Text}";
                    if (ip1_OT.Text.Length > 0 || ip2_OT.Text.Length > 0 || ip3_OT.Text.Length > 0 || ip4_OT.Text.Length > 0)
                        Parametrs += $"|{mc.IPSearcher(ip1_OT.Text, ip2_OT.Text, ip3_OT.Text, ip4_OT.Text)}";
                    else Parametrs += "|";
                    MigApp.Properties.Settings.Default.ParamsOT = Parametrs;
                    MigApp.Properties.Settings.Default.comOT = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }

            // Мониторы
            else if (Mode == "Mon")
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
                    MigApp.Properties.Settings.Default.ParamsMon = Param;
                    MigApp.Properties.Settings.Default.comMon = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }

            // Роутеры
            else if (Mode == "Rout")
            {
                if (InvNum_Rout.Text.Length > 0)
                    command += $"[Инвентарный номер] LIKE '{InvNum_Rout.Text}' AND ";
                if (Name_Rout.Text.Length > 0)
                    command += $"Имя LIKE '{Name_Rout.Text}' AND ";
                if (Model_Rout.Text.Length > 0)
                    command += $"Модель LIKE '{Model_Rout.Text}' AND ";
                if (WiFiName_Rout.Text.Length > 0)
                    command += $"[Имя сети] LIKE '{WiFiName_Rout.Text}' AND ";
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

                    MigApp.Properties.Settings.Default.ParamsRout = Param;
                    MigApp.Properties.Settings.Default.comRout = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }

            // Свитчи
            else if (Mode == "Switch")
            {
                if (InvNum_Switch.Text.Length > 0)
                    command += $"[Инвентарный номер] LIKE '{InvNum_Switch.Text}' AND ";
                if (Name_Switch.Text.Length > 0)
                    command += $"Имя LIKE '{Name_Switch.Text}' AND ";
                if (Model_Switch.Text.Length > 0)
                    command += $"Модель LIKE '{Model_Switch.Text}' AND ";
                if (ip1_Switch.Text.Length > 0 || ip2_Switch.Text.Length > 0 || ip3_Switch.Text.Length > 0 || ip4_Switch.Text.Length > 0)
                    command += $"IP Like '{mc.IPSearcher(ip1_Switch.Text, ip2_Switch.Text, ip3_Switch.Text, ip4_Switch.Text)}' AND ";
                command += "[Инвентарный номер] NOT Like 'Placeholder'";
                if (command != "Where [Инвентарный номер] NOT Like 'Placeholder'")
                {
                    string Param = $"{InvNum_Switch.Text}|{Name_Switch.Text}|{Model_Switch.Text}|";
                    if (ip1_Switch.Text.Length > 0 || ip2_Switch.Text.Length > 0 || ip3_Switch.Text.Length > 0 || ip4_Switch.Text.Length > 0)
                        Param += $"{mc.IPSearcher(ip1_Switch.Text, ip2_Switch.Text, ip3_Switch.Text, ip4_Switch.Text)}";

                    MigApp.Properties.Settings.Default.ParamsSwitch = Param;
                    MigApp.Properties.Settings.Default.comSwitch = command;
                    MigApp.Properties.Settings.Default.Save();
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

            #region Архив
            // Сотрудники Архив
            else if (Mode == "EmpDel")
            {
                if (ID_Emp_Del.Text.Length > 0)
                    command += $"ID Like '{ID_Emp_Del.Text}' AND ";
                if (FIO_Emp_Del.Text.Length > 0)
                    command += $"[ФИО] Like '%{FIO_Emp_Del.Text}%' AND ";
                if (Room_Emp_Del.Text.Length > 0)
                    command += $"[Кабинет] Like '{Room_Emp_Del.Text}' AND ";
                if (Group_Emp_Del.Text.Length > 0)
                    command += $"[Отдел] Like '%{Group_Emp_Del.Text}%' AND ";
                command += "ID NOT Like 'NULL'";
                if (command != "Where ID NOT Like 'NULL'")
                {
                    MigApp.Properties.Settings.Default.ParamsEmpDel = $"{ID_Emp_Del.Text}|{FIO_Emp_Del.Text}|{Room_Emp_Del.Text}|{Group_Emp_Del.Text}";
                    MigApp.Properties.Settings.Default.comEmpDel = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();

            }
                
            // ПК Архив
            else if (Mode == "PCDel")
            {
                if (InvNum_PC_Del.Text.Length > 0)
                    command += $"[Инвентарный номер] Like '{InvNum_PC_Del.Text}' AND ";
                if (Name_PC_Del.Text.Length > 0)
                    command += $"Имя Like '%{Name_PC_Del.Text}%' AND ";
                if (ip1_PC_Del.Text.Length > 0 || ip2_PC_Del.Text.Length > 0 || ip3_PC_Del.Text.Length > 0 || ip4_PC_Del.Text.Length > 0)
                    command += $"IP Like '{mc.IPSearcher(ip1_PC_Del.Text, ip2_PC_Del.Text, ip3_PC_Del.Text, ip4_PC_Del.Text)}' AND ";
                if (User_PC_Del.Text.Length > 0)
                    command += $"Пользователь Like '%{User_PC_Del.Text}%' AND ";
                if (OS_PC_Del.Text.Length > 0)
                    command += $"ОС Like '%{OS_PC_Del.Text}%' AND ";
                if (Motherboard_PC_Del.Text.Length > 0)
                    command += $"[Материнская плата] Like '%{Motherboard_PC_Del.Text}%' AND ";
                if (Processor_PC_Del.Text.Length > 0)
                    command += $"Процессор Like '%{Processor_PC_Del.Text}%' AND ";
                if (RAM_PC_Del.Text.Length > 0)
                    command += $"ОЗУ Like '%{RAM_PC_Del.Text}%' AND ";
                if (Drive_PC_Del.Text.Length > 0)
                    command += $"Накопители Like '%{Drive_PC_Del.Text}%' AND ";
                if (Other_PC_Del.Text.Length > 0)
                    command += $"Другое Like '%{Other_PC_Del.Text}%' AND ";
                command += $"[Инвентарный номер] NOT Like NULL";
                if (command != "Where [Инвентарный номер] NOT Like NULL")
                {
                    string Parametrs = $"{InvNum_PC_Del.Text}|{Name_PC_Del.Text}|{User_PC_Del.Text}|{OS_PC_Del.Text}|{Motherboard_PC_Del.Text}|{Processor_PC_Del.Text}|{RAM_PC_Del.Text}|{Drive_PC_Del.Text}|{Other_PC_Del.Text}";
                    if (ip1_PC_Del.Text.Length > 0 || ip2_PC_Del.Text.Length > 0 || ip3_PC_Del.Text.Length > 0 || ip4_PC_Del.Text.Length > 0)
                        Parametrs += $"|{mc.IPSearcher(ip1_PC_Del.Text, ip2_PC_Del.Text, ip3_PC_Del.Text, ip4_PC_Del.Text)}";
                    else command += "|";
                    MigApp.Properties.Settings.Default.ParamsPCDel = Parametrs;
                    MigApp.Properties.Settings.Default.comPCDel = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }

            // Ноутбуки Архив
            else if (Mode == "NBDel")
            {
                if (InvNum_NB_Del.Text.Length > 0)
                    command += $"[Инвентарный номер] Like '{InvNum_NB_Del.Text}' AND ";
                if (Model_NB_Del.Text.Length > 0)
                    command += $"Модель Like '%{Model_NB_Del.Text}%' AND ";
                if (SeriaNum_NB_Del.Text.Length > 0)
                    command += $"[Серийный номер] Like '%{SeriaNum_NB_Del.Text}%' AND ";
                if (User_NB_Del.Text.Length > 0)
                    command += $"Пользователь Like '%{User_NB_Del.Text}%' AND ";
                if (ScreenDiagonal_NB_Del.Text.Length > 0)
                    command += $"Диагональ Like '%{ScreenDiagonal_NB_Del.Text}%' AND ";
                if (ScreenResolution_NB_Del.Text != "____x____")
                    command += $"Разрешение Like '%{ScreenResolution_NB_Del.Text}%' AND ";
                if (OS_NB.Text.Length > 0)
                    command += $"ОС Like '%{OS_NB_Del.Text}%' AND ";
                if (Processor_NB_Del.Text.Length > 0)
                    command += $"Процессор Like '%{Processor_NB_Del.Text}%' AND ";
                if (RAM_NB_Del.Text.Length > 0)
                    command += $"ОЗУ Like '%{RAM_NB_Del.Text}%' AND ";
                if (Drive_NB_Del.Text.Length > 0)
                    command += $"Накопители Like '%{Drive_NB_Del.Text}%' AND ";
                if (Other_NB_Del.Text.Length > 0)
                    command += $"Другое Like '%{Other_NB_Del.Text}%' AND ";
                command += $"[Инвентарный номер] NOT Like 'Placeholder'";
                if (command != "Where [Инвентарный номер] NOT Like 'Placeholder'")
                {
                    string Param = $"{InvNum_NB_Del.Text}|{Model_NB_Del.Text}|{SeriaNum_NB_Del.Text}|{User_NB_Del.Text}|{ScreenDiagonal_NB_Del.Text}";
                    if (ScreenResolution_NB_Del.Text != "____x____")
                        Param += $"|{ScreenResolution_NB_Del.Text.Replace("_", "%")}";
                    else Param += "|";
                    Param += $"|{OS_NB_Del.Text}|{Processor_NB_Del.Text}|{RAM_NB_Del.Text}|{Drive_NB_Del.Text}|{Other_NB_Del.Text}";
                    MigApp.Properties.Settings.Default.ParamsNBDel = Param;
                    MigApp.Properties.Settings.Default.comNBDel = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }

            // Планшеты Архив
            else if (Mode == "TabDel")
            {
                if (InvNum_Tab_Del.Text.Length > 0)
                    command += $"[Инвентарный номер] Like '{InvNum_Tab_Del.Text}' AND ";
                if (Model_Tab_Del.Text.Length > 0)
                    command += $"Модель Like '%{Model_Tab_Del.Text}%' AND ";
                if (SeriaNum_Tab_Del.Text.Length > 0)
                    command += $"[Серийный номер] Like '%{SeriaNum_Tab_Del.Text}%' AND ";
                if (User_Tab_Del.Text.Length > 0)
                    command += $"Пользователь Like '%{User_Tab_Del.Text}%' AND ";
                if (ScreenDiagonal_Tab_Del.Text.Length > 0)
                    command += $"Диагональ Like '%{ScreenDiagonal_Tab_Del.Text}%' AND ";
                if (Processor_Tab_Del.Text.Length > 0)
                    command += $"Процессор Like '%{Processor_Tab_Del.Text}%' AND ";
                if (RAM_Tab_Del.Text.Length > 0)
                    command += $"ОЗУ Like '%{RAM_Tab_Del.Text}%' AND ";
                if (Drive_Tab_Del.Text.Length > 0)
                    command += $"Накопители Like '%{Drive_Tab_Del.Text}%' AND ";
                if (Other_Tab_Del.Text.Length > 0)
                    command += $"Другое Like '{Other_Tab_Del.Text}' AND ";
                command += $"[Инвентарный номер] NOT Like 'Placeholder'";
                if (command != "Where [Инвентарный номер] NOT Like 'Placeholder'")
                {
                    MigApp.Properties.Settings.Default.ParamsTabDel = $"{InvNum_Tab_Del.Text}|{Model_Tab_Del.Text}|{SeriaNum_Tab_Del.Text}|{User_Tab_Del.Text}|{ScreenDiagonal_Tab_Del.Text}|{Processor_Tab_Del.Text}|{RAM_Tab_Del.Text}|{Drive_Tab_Del.Text}|{Other_Tab_Del.Text}";
                    MigApp.Properties.Settings.Default.comTabDel = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }

            // Орг.техника Архив
            else if (Mode == "OTDel")
            {
                if (InvNum_OT_Del.Text.Length > 0)
                    command += $"[Инвентарный номер] Like '{InvNum_OT_Del.Text}' AND ";
                if (Type_OT_Del.Text != "Не выбрано")
                    command += $"Тип Like '{Type_OT_Del.Text}' AND ";
                if (Model_OT_Del.Text.Length > 0)
                    command += $"Модель Like '%{Model_OT_Del.Text}%' AND ";
                if (SeriaNum_OT_Del.Text.Length > 0)
                    command += $"[Серийный номер] Like '{SeriaNum_OT_Del.Text}' AND ";
                if (Name_OT_Del.Text.Length > 0)
                    command += $"Имя Like '%{Name_OT_Del.Text}%' AND ";
                if (ip1_OT_Del.Text.Length > 0 || ip2_OT_Del.Text.Length > 0 || ip3_OT_Del.Text.Length > 0 || ip4_OT_Del.Text.Length > 0)
                    command += $"IP Like '{mc.IPSearcher(ip1_OT_Del.Text, ip2_OT_Del.Text, ip3_OT_Del.Text, ip4_OT_Del.Text)}' AND ";
                if (Cartridge_OT_Del.Text.Length > 0)
                    command += $"Картридж Like '%{Cartridge_OT_Del.Text}%' AND ";
                if (User_OT_Del.Text.Length > 0)
                    command += $"Пользователь Like '%{User_OT_Del.Text}%' AND ";
                command += $"[Инвентарный номер] NOT Like 'Placeholder'";
                if (command != "Where [Инвентарный номер] NOT Like 'Placeholder'")
                {
                    string Params = "";
                    if (Type_OT_Del.Text.Length > 0 && Type_OT_Del.Text != "Не выбрано")
                        Params = $"{Type_OT_Del.Text}|";
                    else Params = "|";
                    Params += $"{InvNum_OT_Del.Text}|{Model_OT_Del.Text}|{SeriaNum_OT_Del.Text}|{User_OT_Del.Text}|{Name_OT_Del.Text}|{Cartridge_OT_Del.Text}|{User_OT_Del.Text}";
                    if (ip1_OT_Del.Text.Length > 0 || ip2_OT_Del.Text.Length > 0 || ip3_OT_Del.Text.Length > 0 || ip4_OT_Del.Text.Length > 0)
                        Params += $"|{mc.IPSearcher(ip1_OT_Del.Text, ip2_OT_Del.Text, ip3_OT_Del.Text, ip4_OT_Del.Text)}";
                    else command += "|";
                    MigApp.Properties.Settings.Default.ParamsOTDel = Params;
                    MigApp.Properties.Settings.Default.comOT = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }

            // Мониторы Архив
            else if (Mode == "MonDel")
            {
                if (InvNum_Mon_Del.Text.Length > 0)
                    command += $"[Инвентарный номер] Like '{InvNum_Mon_Del.Text}' AND ";
                if (Firm_Mon_Del.Text.Length > 0)
                    command += $"Производитель Like '%{Firm_Mon_Del.Text}%' AND ";
                if (Model_Mon_Del.Text.Length > 0)
                    command += $"Модель Like '%{Model_Mon_Del.Text}%' AND ";
                if (SeriaNum_Mon_Del.Text.Length > 0)
                    command += $"[Серийный номер] Like '%{SeriaNum_Mon_Del.Text}%' AND ";
                if (ScreenDiagonal_Mon_Del.Text.Length > 0)
                    command += $"Диагональ Like '%{ScreenDiagonal_Mon_Del.Text}%' AND ";
                if (ScreenResolution_Mon_Del.Text != "____x____")
                    command += $"Разрешение Like '%{ScreenResolution_Mon_Del.Text}%' AND ";
                if (ScreenType_Mon_Del.Text.Length > 0)
                    command += $"[Тип экрана] Like '%{ScreenType_Mon_Del.Text}%' AND ";
                if (User_Mon_Del.Text.Length > 0)
                    command += $"Пользователь Like '%{User_Mon_Del.Text}%' AND ";
                command += "[Инвентарный номер] NOT Like 'Placeholder'";
                if (command != "Where [Инвентарный номер] NOT Like 'Placeholder'")
                {
                    string Param = $"{InvNum_Mon_Del.Text}|{Firm_Mon_Del.Text}|{Model_Mon_Del.Text}|{SeriaNum_Mon_Del.Text}|{ScreenDiagonal_Mon_Del.Text}";
                    if (ScreenResolution_Mon_Del.Text != "____x____")
                        Param += $"{ScreenResolution_Mon_Del.Text.Replace("_", "%")}";
                    else Param += "|";
                    Param += $"|{ScreenType_Mon_Del.Text}|{User_Mon_Del.Text}";
                    MigApp.Properties.Settings.Default.ParamsMonDel = Param;
                    MigApp.Properties.Settings.Default.comMonDel = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }

            // Роутеры Архив
            else if (Mode == "RoutDel")
            {
                if (InvNum_RoutDel.Text.Length > 0)
                    command += $"[Инвентарный номер] LIKE '{InvNum_RoutDel.Text}' AND ";
                if (Name_RoutDel.Text.Length > 0)
                    command += $"Имя LIKE '{Name_RoutDel.Text}' AND ";
                if (Model_RoutDel.Text.Length > 0)
                    command += $"Модель LIKE '{Model_RoutDel.Text}' AND ";
                if (WiFiName_RoutDel.Text.Length > 0)
                    command += $"[Имя сети] LIKE '{WiFiName_RoutDel.Text}' AND ";
                if (ip1_RoutDel.Text.Length > 0 || ip2_RoutDel.Text.Length > 0 || ip3_RoutDel.Text.Length > 0 || ip4_RoutDel.Text.Length > 0)
                    command += $"IP Like '{mc.IPSearcher(ip1_RoutDel.Text, ip2_RoutDel.Text, ip3_RoutDel.Text, ip4_RoutDel.Text)}' AND ";
                if (dhcp1_RoutDel.Text.Length > 0 || dhcp2_RoutDel.Text.Length > 0 || dhcp3_RoutDel.Text.Length > 0 || dhcp4_RoutDel.Text.Length > 0 || dhcp5_RoutDel.Text.Length > 0)
                    command += $"DHCP Like '{mc.DHCPSearcher(dhcp1_RoutDel.Text, dhcp2_RoutDel.Text, dhcp3_RoutDel.Text, dhcp4_RoutDel.Text, dhcp5_RoutDel.Text)}' AND ";
                command += "[Инвентарный номер] NOT Like 'Placeholder'";
                if (command != "Where [Инвентарный номер] NOT Like 'Placeholder'")
                {
                    string Param = $"{InvNum_RoutDel.Text}|{Name_RoutDel.Text}|{Model_RoutDel.Text}|{WiFiName_RoutDel.Text}|";
                    if (ip1_RoutDel.Text.Length > 0 || ip2_RoutDel.Text.Length > 0 || ip3_RoutDel.Text.Length > 0 || ip4_RoutDel.Text.Length > 0)
                        Param += $"{mc.IPSearcher(ip1_RoutDel.Text, ip2_RoutDel.Text, ip3_RoutDel.Text, ip4_RoutDel.Text)}|";
                    else Param += "|";
                    if (dhcp1_RoutDel.Text.Length > 0 || dhcp2_RoutDel.Text.Length > 0 || dhcp3_RoutDel.Text.Length > 0 || dhcp4_RoutDel.Text.Length > 0 || dhcp5_RoutDel.Text.Length > 0)
                        Param += $"{mc.DHCPSearcher(dhcp1_RoutDel.Text, dhcp2_RoutDel.Text, dhcp3_RoutDel.Text, dhcp4_RoutDel.Text, dhcp5_RoutDel.Text)}";

                    MigApp.Properties.Settings.Default.ParamsRoutDel = Param;
                    MigApp.Properties.Settings.Default.comRoutDel = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }

            // Свитчи Архив
            else if (Mode == "SwitchDel")
            {
                if (InvNum_SwitchDel.Text.Length > 0)
                    command += $"[Инвентарный номер] LIKE '{InvNum_SwitchDel.Text}' AND ";
                if (Name_SwitchDel.Text.Length > 0)
                    command += $"Имя LIKE '{Name_SwitchDel.Text}' AND ";
                if (Model_SwitchDel.Text.Length > 0)
                    command += $"Модель LIKE '{Model_SwitchDel.Text}' AND ";
                if (ip1_SwitchDel.Text.Length > 0 || ip2_SwitchDel.Text.Length > 0 || ip3_SwitchDel.Text.Length > 0 || ip4_SwitchDel.Text.Length > 0)
                    command += $"IP Like '{mc.IPSearcher(ip1_SwitchDel.Text, ip2_SwitchDel.Text, ip3_SwitchDel.Text, ip4_SwitchDel.Text)}' AND ";
                command += "[Инвентарный номер] NOT Like 'Placeholder'";
                if (command != "Where [Инвентарный номер] NOT Like 'Placeholder'")
                {
                    string Param = $"{InvNum_SwitchDel.Text}|{Name_SwitchDel.Text}|{Model_SwitchDel.Text}|";
                    if (ip1_SwitchDel.Text.Length > 0 || ip2_SwitchDel.Text.Length > 0 || ip3_SwitchDel.Text.Length > 0 || ip4_SwitchDel.Text.Length > 0)
                        Param += $"{mc.IPSearcher(ip1_SwitchDel.Text, ip2_SwitchDel.Text, ip3_SwitchDel.Text, ip4_SwitchDel.Text)}";

                    MigApp.Properties.Settings.Default.ParamsSwitchDel = Param;
                    MigApp.Properties.Settings.Default.comSwitchDel = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
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
                Close ();
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

        #region Основной
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

        #region Архив

        // Проверка до 255 и переключение на следующий
        private void IPcheck1_Del(object sender, TextChangedEventArgs e)
        {
            if (ip1_PC_Del.Text.Length == 3)
                ip2_PC_Del.Focus();
            try
            {
                if (Convert.ToInt32(ip1_PC_Del.Text) > 255)
                    ip1_PC_Del.Text = "255";
            }
            catch { }
        }

        private void IPcheck2_Del(object sender, TextChangedEventArgs e)
        {
            if (ip2_PC_Del.Text.Length == 3)
                ip3_PC_Del.Focus();
            try
            {
                if (Convert.ToInt32(ip2_PC_Del.Text) > 255)
                    ip2_PC_Del.Text = "255";
            }
            catch { }
        }

        private void IPcheck3_Del(object sender, TextChangedEventArgs e)
        {
            if (ip3_PC_Del.Text.Length == 3)
                ip4_PC_Del.Focus();
            try
            {
                if (Convert.ToInt32(ip3_PC_Del.Text) > 255)
                    ip3_PC_Del.Text = "255";
            }
            catch { }
        }

        private void IPcheck4_Del(object sender, TextChangedEventArgs e)
        {
            if (ip4_PC_Del.Text.Length == 3)
                e.Handled = false;
            try
            {
                if (Convert.ToInt32(ip4_PC_Del.Text) > 255)
                    ip4_PC_Del.Text = "255";
            }
            catch { }
        }

        private void IPfocus_Del(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            e.Handled = true;
            if (textBox != null)
                textBox.SelectAll();
        }

        // Запрет на пробелы
        private void NextIP1_Del(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip2_PC_Del.Focus();
            }
        }

        private void NextIP2_Del(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip3_PC_Del.Focus();
            }
            if (e.Key == Key.Back && ip2_PC_Del.Text.Length == 0)
            {
                e.Handled = true;
                ip1_PC_Del.Focus();
            }

        }

        private void NextIP3_Del(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip4_PC_Del.Focus();
            }
            if (e.Key == Key.Back && ip3_PC_Del.Text.Length == 0)
            {
                e.Handled = true;
                ip2_PC_Del.Focus();
            }
        }

        private void NextIP4_Del(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Back && ip4_PC_Del.Text.Length == 0)
            {
                e.Handled = true;
                ip3_PC_Del.Focus();
            }
        }
        #endregion

        #endregion

        #region IP Box для Орг.техники

        #region Основной
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

        #region Архив
        // Проверка до 255 и переключение на следующий
        private void IPcheck5_Del(object sender, TextChangedEventArgs e)
        {
            if (ip1_OT_Del.Text.Length == 3)
                ip2_OT_Del.Focus();
            try
            {
                if (Convert.ToInt32(ip1_OT_Del.Text) > 255)
                    ip1_OT_Del.Text = "255";
            }
            catch { }
        }

        private void IPcheck6_Del(object sender, TextChangedEventArgs e)
        {
            if (ip2_OT_Del.Text.Length == 3)
                ip3_OT_Del.Focus();
            try
            {
                if (Convert.ToInt32(ip2_OT_Del.Text) > 255)
                    ip2_OT_Del.Text = "255";
            }
            catch { }
        }

        private void IPcheck7_Del(object sender, TextChangedEventArgs e)
        {
            if (ip3_OT_Del.Text.Length == 3)
                ip4_OT_Del.Focus();
            try
            {
                if (Convert.ToInt32(ip3_OT_Del.Text) > 255)
                    ip3_OT_Del.Text = "255";
            }
            catch { }
        }

        private void IPcheck8_Del(object sender, TextChangedEventArgs e)
        {
            if (ip4_OT_Del.Text.Length == 3)
                e.Handled = false;
            try
            {
                if (Convert.ToInt32(ip4_OT_Del.Text) > 255)
                    ip4_OT_Del.Text = "255";
            }
            catch { }
        }

        private void IPfocus1_Del(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            e.Handled = true;
            if (textBox != null)
                textBox.SelectAll();
        }

        // Запрет на пробелы
        private void NextIP5_Del(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip2_OT_Del.Focus();
            }
        }

        private void NextIP6_Del(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip3_OT_Del.Focus();
            }
            if (e.Key == Key.Back && ip2_OT_Del.Text.Length == 0)
            {
                e.Handled = true;
                ip1_OT_Del.Focus();
            }

        }

        private void NextIP7_Del(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip4_OT_Del.Focus();
            }
            if (e.Key == Key.Back && ip3_OT_Del.Text.Length == 0)
            {
                e.Handled = true;
                ip2_OT_Del.Focus();
            }
        }

        private void NextIP8_Del(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Back && ip4_OT_Del.Text.Length == 0)
            {
                e.Handled = true;
                ip3_OT_Del.Focus();
            }
        }
        #endregion

        #endregion

        #region IP Box для Роутера

        #region Основной
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

        #region Архив
        // Проверка до 255 и переключение на следующий
        private void RoutDel_IPcheck1(object sender, TextChangedEventArgs e)
        {
            if (ip1_RoutDel.Text.Length == 3)
                ip2_RoutDel.Focus();
            try
            {
                if (Convert.ToInt32(ip1_RoutDel.Text) > 255)
                    ip1_RoutDel.Text = "255";
            }
            catch { }
        }

        private void RoutDel_IPcheck2(object sender, TextChangedEventArgs e)
        {
            if (ip2_RoutDel.Text.Length == 3)
                ip3_RoutDel.Focus();
            try
            {
                if (Convert.ToInt32(ip2_RoutDel.Text) > 255)
                    ip2_RoutDel.Text = "255";
            }
            catch { }
        }

        private void RoutDel_IPcheck3(object sender, TextChangedEventArgs e)
        {
            if (ip3_RoutDel.Text.Length == 3)
                ip4_RoutDel.Focus();
            try
            {
                if (Convert.ToInt32(ip3_RoutDel.Text) > 255)
                    ip3_RoutDel.Text = "255";
            }
            catch { }
        }

        private void RoutDel_IPcheck4(object sender, TextChangedEventArgs e)
        {
            if (ip4_RoutDel.Text.Length == 3)
                e.Handled = false;
            try
            {
                if (Convert.ToInt32(ip4_RoutDel.Text) > 255)
                    ip4_RoutDel.Text = "255";
            }
            catch { }
        }

        private void RoutDel_IPfocus(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            e.Handled = true;
            if (textBox != null)
                textBox.SelectAll();
        }

        // Запрет на пробелы
        private void RoutDel_NextIP1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip2_RoutDel.Focus();
            }
        }

        private void RoutDel_NextIP2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip3_RoutDel.Focus();
            }
            if (e.Key == Key.Back && ip2_RoutDel.Text.Length == 0)
            {
                e.Handled = true;
                ip1_RoutDel.Focus();
            }

        }

        private void RoutDel_NextIP3(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                ip4_RoutDel.Focus();
            }
            if (e.Key == Key.Back && ip3_RoutDel.Text.Length == 0)
            {
                e.Handled = true;
                ip2_RoutDel.Focus();
            }
        }

        private void RoutDel_NextIP4(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Back && ip4_RoutDel.Text.Length == 0)
            {
                e.Handled = true;
                ip3_RoutDel.Focus();
            }
        }
        #endregion

        #endregion

        #region DHCP Box для Роутера

        #region Основной
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

        #region Архив
        // Проверка до 255 и переключение на следующий
        private void RoutDel_DHCPcheck1(object sender, TextChangedEventArgs e)
        {
            if (dhcp1_RoutDel.Text.Length == 3)
                dhcp2_RoutDel.Focus();
            try
            {
                if (Convert.ToInt32(dhcp1_RoutDel.Text) > 255)
                    dhcp1_RoutDel.Text = "255";
            }
            catch { }
        }

        private void RoutDel_DHCPcheck2(object sender, TextChangedEventArgs e)
        {
            if (dhcp2_RoutDel.Text.Length == 3)
                dhcp3_RoutDel.Focus();
            try
            {
                if (Convert.ToInt32(dhcp2_RoutDel.Text) > 255)
                    dhcp2_RoutDel.Text = "255";
            }
            catch { }
        }

        private void RoutDel_DHCPcheck3(object sender, TextChangedEventArgs e)
        {
            if (dhcp3_RoutDel.Text.Length == 3)
                dhcp4_RoutDel.Focus();
            try
            {
                if (Convert.ToInt32(dhcp3_RoutDel.Text) > 255)
                    dhcp3_RoutDel.Text = "255";
            }
            catch { }
        }

        private void RoutDel_DHCPcheck4(object sender, TextChangedEventArgs e)
        {
            if (dhcp4_RoutDel.Text.Length == 3)
                dhcp4_RoutDel.Focus();
            try
            {
                if (Convert.ToInt32(dhcp4_RoutDel.Text) > 255)
                    dhcp4_RoutDel.Text = "255";
            }
            catch { }
        }

        private void RoutDel_DHCPcheck5(object sender, TextChangedEventArgs e)
        {
            if (dhcp5_RoutDel.Text.Length == 3)
                e.Handled = false;
            try
            {
                if (Convert.ToInt32(dhcp5_RoutDel.Text) > 255)
                    dhcp5_RoutDel.Text = "255";
            }
            catch { }
        }

        private void RoutDel_DHCPfocus(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            e.Handled = true;
            if (textBox != null)
                textBox.SelectAll();
        }

        // Запрет на пробелы
        private void RoutDel_NextDHCP1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                dhcp2_RoutDel.Focus();
            }
        }

        private void RoutDel_NextDHCP2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                dhcp3_RoutDel.Focus();
            }
            if (e.Key == Key.Back && dhcp2_RoutDel.Text.Length == 0)
            {
                e.Handled = true;
                dhcp1_RoutDel.Focus();
            }

        }

        private void RoutDel_NextDHCP3(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                dhcp4_RoutDel.Focus();
            }
            if (e.Key == Key.Back && dhcp3_RoutDel.Text.Length == 0)
            {
                e.Handled = true;
                dhcp2_RoutDel.Focus();
            }
        }

        private void RoutDel_NextDHCP4(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                dhcp5_RoutDel.Focus();
            }
            if (e.Key == Key.Back && dhcp4_RoutDel.Text.Length == 0)
            {
                e.Handled = true;
                dhcp3_RoutDel.Focus();
            }
        }

        private void RoutDel_NextDHCP5(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Back && dhcp5_RoutDel.Text.Length == 0)
            {
                e.Handled = true;
                dhcp4_RoutDel.Focus();
            }
        }
        #endregion

        #endregion

        //#region IP Box для Свитча

        //#region Основной
        //// Проверка до 255 и переключение на следующий
        //private void Rout_IPcheck1(object sender, TextChangedEventArgs e)
        //{
        //    if (ip1_Rout.Text.Length == 3)
        //        ip2_Rout.Focus();
        //    try
        //    {
        //        if (Convert.ToInt32(ip1_Rout.Text) > 255)
        //            ip1_Rout.Text = "255";
        //    }
        //    catch { }
        //}

        //private void Rout_IPcheck2(object sender, TextChangedEventArgs e)
        //{
        //    if (ip2_Rout.Text.Length == 3)
        //        ip3_Rout.Focus();
        //    try
        //    {
        //        if (Convert.ToInt32(ip2_Rout.Text) > 255)
        //            ip2_Rout.Text = "255";
        //    }
        //    catch { }
        //}

        //private void Rout_IPcheck3(object sender, TextChangedEventArgs e)
        //{
        //    if (ip3_Rout.Text.Length == 3)
        //        ip4_Rout.Focus();
        //    try
        //    {
        //        if (Convert.ToInt32(ip3_Rout.Text) > 255)
        //            ip3_Rout.Text = "255";
        //    }
        //    catch { }
        //}

        //private void Rout_IPcheck4(object sender, TextChangedEventArgs e)
        //{
        //    if (ip4_Rout.Text.Length == 3)
        //        e.Handled = false;
        //    try
        //    {
        //        if (Convert.ToInt32(ip4_Rout.Text) > 255)
        //            ip4_Rout.Text = "255";
        //    }
        //    catch { }
        //}

        //private void Rout_IPfocus(object sender, RoutedEventArgs e)
        //{
        //    var textBox = e.OriginalSource as TextBox;
        //    e.Handled = true;
        //    if (textBox != null)
        //        textBox.SelectAll();
        //}

        //// Запрет на пробелы
        //private void Rout_NextIP1(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Space)
        //    {
        //        e.Handled = true;
        //        ip2_Rout.Focus();
        //    }
        //}

        //private void Rout_NextIP2(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Space)
        //    {
        //        e.Handled = true;
        //        ip3_Rout.Focus();
        //    }
        //    if (e.Key == Key.Back && ip2_Rout.Text.Length == 0)
        //    {
        //        e.Handled = true;
        //        ip1_Rout.Focus();
        //    }

        //}

        //private void Rout_NextIP3(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Space)
        //    {
        //        e.Handled = true;
        //        ip4_Rout.Focus();
        //    }
        //    if (e.Key == Key.Back && ip3_Rout.Text.Length == 0)
        //    {
        //        e.Handled = true;
        //        ip2_Rout.Focus();
        //    }
        //}

        //private void Rout_NextIP4(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Space)
        //    {
        //        e.Handled = true;
        //    }
        //    if (e.Key == Key.Back && ip4_Rout.Text.Length == 0)
        //    {
        //        e.Handled = true;
        //        ip3_Rout.Focus();
        //    }
        //}
        //#endregion

        //#region Архив
        //// Проверка до 255 и переключение на следующий
        ////private void IPcheck5_Del(object sender, TextChangedEventArgs e)
        ////{
        ////    if (ip1_OT_Del.Text.Length == 3)
        ////        ip2_OT_Del.Focus();
        ////    try
        ////    {
        ////        if (Convert.ToInt32(ip1_OT_Del.Text) > 255)
        ////            ip1_OT_Del.Text = "255";
        ////    }
        ////    catch { }
        ////}

        ////private void IPcheck6_Del(object sender, TextChangedEventArgs e)
        ////{
        ////    if (ip2_OT_Del.Text.Length == 3)
        ////        ip3_OT_Del.Focus();
        ////    try
        ////    {
        ////        if (Convert.ToInt32(ip2_OT_Del.Text) > 255)
        ////            ip2_OT_Del.Text = "255";
        ////    }
        ////    catch { }
        ////}

        ////private void IPcheck7_Del(object sender, TextChangedEventArgs e)
        ////{
        ////    if (ip3_OT_Del.Text.Length == 3)
        ////        ip4_OT_Del.Focus();
        ////    try
        ////    {
        ////        if (Convert.ToInt32(ip3_OT_Del.Text) > 255)
        ////            ip3_OT_Del.Text = "255";
        ////    }
        ////    catch { }
        ////}

        ////private void IPcheck8_Del(object sender, TextChangedEventArgs e)
        ////{
        ////    if (ip4_OT_Del.Text.Length == 3)
        ////        e.Handled = false;
        ////    try
        ////    {
        ////        if (Convert.ToInt32(ip4_OT_Del.Text) > 255)
        ////            ip4_OT_Del.Text = "255";
        ////    }
        ////    catch { }
        ////}

        ////private void IPfocus1_Del(object sender, RoutedEventArgs e)
        ////{
        ////    var textBox = e.OriginalSource as TextBox;
        ////    e.Handled = true;
        ////    if (textBox != null)
        ////        textBox.SelectAll();
        ////}

        //// Запрет на пробелы
        ////private void NextIP5_Del(object sender, KeyEventArgs e)
        ////{
        ////    if (e.Key == Key.Space)
        ////    {
        ////        e.Handled = true;
        ////        ip2_OT_Del.Focus();
        ////    }
        ////}

        ////private void NextIP6_Del(object sender, KeyEventArgs e)
        ////{
        ////    if (e.Key == Key.Space)
        ////    {
        ////        e.Handled = true;
        ////        ip3_OT_Del.Focus();
        ////    }
        ////    if (e.Key == Key.Back && ip2_OT_Del.Text.Length == 0)
        ////    {
        ////        e.Handled = true;
        ////        ip1_OT_Del.Focus();
        ////    }

        ////}

        ////private void NextIP7_Del(object sender, KeyEventArgs e)
        ////{
        ////    if (e.Key == Key.Space)
        ////    {
        ////        e.Handled = true;
        ////        ip4_OT_Del.Focus();
        ////    }
        ////    if (e.Key == Key.Back && ip3_OT_Del.Text.Length == 0)
        ////    {
        ////        e.Handled = true;
        ////        ip2_OT_Del.Focus();
        ////    }
        ////}

        ////private void NextIP8_Del(object sender, KeyEventArgs e)
        ////{
        ////    if (e.Key == Key.Space)
        ////    {
        ////        e.Handled = true;
        ////    }
        ////    if (e.Key == Key.Back && ip4_OT_Del.Text.Length == 0)
        ////    {
        ////        e.Handled = true;
        ////        ip3_OT_Del.Focus();
        ////    }
        ////}
        //#endregion

        //#endregion
        
    }
}
