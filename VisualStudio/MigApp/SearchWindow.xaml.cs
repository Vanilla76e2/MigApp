﻿using System;
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
        SQLConnectionClass sqlcc = new SQLConnectionClass();
        MiscClass mc = new MiscClass();
        int Mode;

        public SearchWindow(int mode)
        {
            InitializeComponent();
            Mode = mode;
            Setup(mode);
        }

        // Преднастройка окна поиска
        private void Setup(int mode)
        {
            #region Пользовательские таблицы
            // Сотрудники
            if (mode == 0) 
            { 
                SearchPanel_0.Visibility = Visibility.Visible;
                this.Height = 330;
                if (MigApp.Properties.Settings.Default.Params0 != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.Params0.Split('|');
                    ID_Emp.Text = Parametrs[0];
                    FIO_Emp.Text = Parametrs[1];
                    Room_Emp.Text = Parametrs[2];
                    Group_Emp.Text = Parametrs[3];
                    Birthdate_Emp.Text = Parametrs[4];
                }
            }

            // ПК
            else if (mode == 1)
            {
                SearchPanel_1.Visibility = Visibility.Visible;
                this.Height = 500;
                if (MigApp.Properties.Settings.Default.Params1 != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.Params1.Split('|');
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
            else if (mode == 2)
            {
                SearchPanel_2.Visibility = Visibility.Visible;
                this.Height = 550;
                if (MigApp.Properties.Settings.Default.Params2 != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.Params2.Split('|');
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
            else if (mode == 3)
            {
                SearchPanel_3.Visibility = Visibility.Visible;
                this.Height = 450;
                if (MigApp.Properties.Settings.Default.Params3 != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.Params3.Split('|');
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
            else if (mode == 4)
            {
                SearchPanel_4.Visibility = Visibility.Visible;
                this.Height = 420;
                Type_OT.Text = "Не выбрано";
                if (MigApp.Properties.Settings.Default.Params4 != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.Params4.Split('|');
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
            else if (mode == 5)
            {
                SearchPanel_5.Visibility = Visibility.Visible;
                this.Height = 400;
                if (MigApp.Properties.Settings.Default.Params5 != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.Params5.Split('|');
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
            #endregion

            #region Админпанель
            // Пользователи
            else if (mode == 6)
            {
                SearchPanel_6.Visibility = Visibility.Visible;
                this.Height = 350;
                if (MigApp.Properties.Settings.Default.Params6 != null)
                {
                    try
                    {
                        string[] Parametrs = MigApp.Properties.Settings.Default.Params6.Split('|');
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
            else if (mode == 7)
            {
                SearchPanel_7.Visibility = Visibility.Visible;
                this.Height = 500;
                if (MigApp.Properties.Settings.Default.Params7 != null)
                {
                    try
                    {
                        string[] Parametrs = MigApp.Properties.Settings.Default.Params7.Split('|');
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
            if (mode == 8)
            {
                SearchPanel_8.Visibility = Visibility.Visible;
                this.Height = 330;
                if (MigApp.Properties.Settings.Default.Params8 != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.Params8.Split('|');
                    ID_Emp_Del.Text = Parametrs[0];
                    FIO_Emp_Del.Text = Parametrs[1];
                    Room_Emp_Del.Text = Parametrs[2];
                    Group_Emp_Del.Text = Parametrs[3];
                    Birthdate_Emp_Del.Text = Parametrs[4];
                }
            }

            // ПК
            else if (mode == 9)
            {
                SearchPanel_9.Visibility = Visibility.Visible;
                this.Height = 500;
                if (MigApp.Properties.Settings.Default.Params9 != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.Params9.Split('|');
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
            else if (mode == 10)
            {
                SearchPanel_10.Visibility = Visibility.Visible;
                this.Height = 550;
                if (MigApp.Properties.Settings.Default.Params10 != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.Params10.Split('|');
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
            else if (mode == 11)
            {
                SearchPanel_11.Visibility = Visibility.Visible;
                this.Height = 450;
                if (MigApp.Properties.Settings.Default.Params11 != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.Params11.Split('|');
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
            else if (mode == 12)
            {
                SearchPanel_12.Visibility = Visibility.Visible;
                this.Height = 400;
                Type_OT_Del.Text = "Не выбрано";
                if (MigApp.Properties.Settings.Default.Params12 != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.Params12.Split('|');
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
            else if (mode == 13)
            {
                SearchPanel_13.Visibility = Visibility.Visible;
                this.Height = 400;
                if (MigApp.Properties.Settings.Default.Params13 != null)
                {
                    string[] Parametrs = MigApp.Properties.Settings.Default.Params13.Split('|');
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
            #endregion

            #region Отчёты
            // ПК
            else if (Mode == 14)
            {
                SearchPanel_14.Visibility = Visibility.Visible;
                this.Height = 500;
                OTType_Report1.Text = "Не выбрано";
                if (MigApp.Properties.Settings.Default.Params14 != null)
                {
                    try
                    {
                        string[] Parametrs = MigApp.Properties.Settings.Default.Params14.Split('|');
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
            else if (Mode == 15)
            {
                SearchPanel_15.Visibility = Visibility.Visible;
                this.Height = 300;
                if (MigApp.Properties.Settings.Default.Params15 != null)
                {
                    try
                    {
                        string[] Parametrs = MigApp.Properties.Settings.Default.Params15.Split('|');
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
            else if (Mode == 16)
            {
                SearchPanel_16.Visibility = Visibility.Visible;
                this.Height = 300;
                if (MigApp.Properties.Settings.Default.Params16 != null)
                {
                    try
                    {
                        string[] Parametrs = MigApp.Properties.Settings.Default.Params16.Split('|');
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
            if (Mode == 0)
            {
                if (ID_Emp.Text.Length > 0)
                    command += $"ID Like '{ID_Emp.Text}' AND ";
                if (FIO_Emp.Text.Length > 0)
                    command += $"[ФИО] Like '%{FIO_Emp.Text}%' AND ";
                if (Room_Emp.Text.Length > 0)
                    command += $"[Кабинет] Like '{Room_Emp.Text}' AND ";
                if (Group_Emp.Text.Length > 0)
                    command += $"[Отдел] Like '%{Group_Emp.Text}%' AND ";
                if (Birthdate_Emp.Text.Length > 0)
                    command += $"[Дата рождения] Like '%{Birthdate_Emp.Text}%' AND ";
                command += "ID NOT Like 'NULL'";
                if (command != "Where ID NOT Like 'NULL'")
                {
                    MigApp.Properties.Settings.Default.Params0 = $"{ID_Emp.Text}|{FIO_Emp.Text}|{Room_Emp.Text}|{Group_Emp.Text}|{Birthdate_Emp.Text}";
                    MigApp.Properties.Settings.Default.com0 = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }

            //ПК
            else if (Mode == 1)
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
                    MigApp.Properties.Settings.Default.Params1 = Parametrs;
                    MigApp.Properties.Settings.Default.com1 = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
                
            }

            // Ноутбуки
            else if (Mode == 2)
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
                    MigApp.Properties.Settings.Default.Params2 = Param;
                    MigApp.Properties.Settings.Default.com2 = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }

            // Планшеты
            else if (Mode == 3)
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
                    MigApp.Properties.Settings.Default.Params3 = $"{InvNum_Tab.Text}|{Model_Tab.Text}|{SeriaNum_Tab.Text}|{User_Tab.Text}|{ScreenDiagonal_Tab.Text}|{Processor_Tab.Text}|{RAM_Tab.Text}|{Drive_Tab.Text}|{Other_Tab.Text}";
                    MigApp.Properties.Settings.Default.com3 = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }

            // Орг.техника
            else if (Mode == 4)
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
                    MigApp.Properties.Settings.Default.Params4 = Parametrs;
                    MigApp.Properties.Settings.Default.com4 = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }

            // Мониторы
            else if (Mode == 5)
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
                    MigApp.Properties.Settings.Default.Params5 = Param;
                    MigApp.Properties.Settings.Default.com5 = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }
            #endregion

            #region Админпанель
            // Пользователи
            else if (Mode == 6)
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
                    MigApp.Properties.Settings.Default.Params6 = $"{ID_User.Text}|{Login_User.Text}|{Employee_User.Text}|{Role_User.Text}";
                    if (NoPassword_User.IsChecked == true)
                        MigApp.Properties.Settings.Default.Params6 += "|Без пароля";
                    else MigApp.Properties.Settings.Default.Params6 += "|";
                    MigApp.Properties.Settings.Default.com6 = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                Close();
            }

            // Логи
            else if (Mode == 7)
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
                    MigApp.Properties.Settings.Default.Params7 = param;
                    MigApp.Properties.Settings.Default.com7 = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                Close();
            }
            #endregion

            #region Архив
            // Сотрудники архив
            else if (Mode == 8)
            {
                if (ID_Emp_Del.Text.Length > 0)
                    command += $"ID Like '{ID_Emp_Del.Text}' AND ";
                if (FIO_Emp_Del.Text.Length > 0)
                    command += $"[ФИО] Like '%{FIO_Emp_Del.Text}%' AND ";
                if (Room_Emp_Del.Text.Length > 0)
                    command += $"[Кабинет] Like '{Room_Emp_Del.Text}' AND ";
                if (Group_Emp_Del.Text.Length > 0)
                    command += $"[Отдел] Like '%{Group_Emp_Del.Text}%' AND ";
                if (Birthdate_Emp_Del.Text.Length > 0)
                    command += $"[Дата рождения] Like '%{Birthdate_Emp_Del.Text}%' AND ";
                command += "ID NOT Like 'NULL'";
                if (command != "Where ID NOT Like 'NULL'")
                {
                    MigApp.Properties.Settings.Default.Params8 = $"{ID_Emp_Del.Text}|{FIO_Emp_Del.Text}|{Room_Emp_Del.Text}|{Group_Emp_Del.Text}|{Birthdate_Emp_Del.Text}";
                    MigApp.Properties.Settings.Default.com8 = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();

            }
                
            // ПК Архив
            else if (Mode == 9)
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
                    MigApp.Properties.Settings.Default.Params9 = Parametrs;
                    MigApp.Properties.Settings.Default.com9 = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }

            // Ноутбуки Архив
            else if (Mode == 10)
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
                    MigApp.Properties.Settings.Default.Params10 = Param;
                    MigApp.Properties.Settings.Default.com10 = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }

            // Планшеты Архив
            else if (Mode == 11)
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
                    MigApp.Properties.Settings.Default.Params11 = $"{InvNum_Tab_Del.Text}|{Model_Tab_Del.Text}|{SeriaNum_Tab_Del.Text}|{User_Tab_Del.Text}|{ScreenDiagonal_Tab_Del.Text}|{Processor_Tab_Del.Text}|{RAM_Tab_Del.Text}|{Drive_Tab_Del.Text}|{Other_Tab_Del.Text}";
                    MigApp.Properties.Settings.Default.com11 = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }

            // Орг.техника Архив
            else if (Mode == 12)
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
                    MigApp.Properties.Settings.Default.Params12 = Params;
                    MigApp.Properties.Settings.Default.com4 = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }

            // Мониторы Архив
            else if (Mode == 13)
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
                    MigApp.Properties.Settings.Default.Params13 = Param;
                    MigApp.Properties.Settings.Default.com13 = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                this.Close();
            }
            #endregion

            #region Отчёты

            // Компьютеры
            else if (Mode == 14)
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
                    MigApp.Properties.Settings.Default.Params14 = Params;
                    MigApp.Properties.Settings.Default.com14 = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                Close();
            }

            // Ноутбуки
            else if (Mode == 15)
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
                    MigApp.Properties.Settings.Default.Params15 = Params;
                    MigApp.Properties.Settings.Default.com15 = command;
                    MigApp.Properties.Settings.Default.Save();
                    DialogResult = true;
                }
                Close();
            }

            // Планшеты
            else if (Mode == 16)
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
                    MigApp.Properties.Settings.Default.Params16 = Params;
                    MigApp.Properties.Settings.Default.com16 = command;
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
    }
}