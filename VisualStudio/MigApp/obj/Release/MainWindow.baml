   M S B A M L   `   `   `  ���� A  =MigApp, Version=1.6.1.0, Culture=neutral, PublicKeyToken=nullclr-namespace:MigAppMigApp      MigApp.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     &localclr-namespace:MigApp     _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -     � Title$  MigApp��$	��650��$	��800�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ 	CanResize=� � Icon$ images\Icon.png��.��� ���#323232.+���  �� TabStripPlacement$
 Left=�.@��� $��50��$	��100��$��3q�$��0 10 0 0q����� $��images/FavImage.png��.��� .+��� $	��Top=����#323232$��50��._�� .+��� $��Избранное�� 
  Header#���   $��10 8 0 8q�&   System.Windows.Controls.DataGrid  -   $��FavTable�����#525252
�������$��	0 50 5 20q�   
IsReadOnly .  � ContextMenu �� .@�m� -   $ ��Редактировать��m� -   $,��&Убрать из избранного���� -   $��FavSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� -   $��EmployeesGroup��$��50��$	��100��$��3q�$��0 10 0 0q�$y�	Collapsed=����� $��images/Employees.png��.��� ���#404040.+��� $
 Left=�.@��� -   $��PersonsGroup��$��Сотрудники��$��50��$	��100��$��3q�$��0 10 0 0q�$y�Visible=�.��� ���#404040.+��� $	��Top=����#404040$	��100��._�� .+��� .+�� .+��� -   $	��➕��$
��Left=�$��50��$��10 0 0 0q�   MaterialDesignOutlinedButton#���   � ToolTip$! Создать запись���� $��Сотрудники��#���   $��70 8 0 8q��� -	   $	��⌕��$��20(�  MaterialDesignFlatButton#���  $��0 0 5 0q�$��Right=�$ 
Поиск���� -
   $��
Filter_Emp��$��	10 5 10 5q�$��1q�
��� ��� �� CornerRadius$ 3��._�� .+��� -   $��FilterEmpText��$��18(�$��Center=�
�������$��5 0 55 0q��� -   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -   $��EmployeeTable��$��	0 50 5 20q����#525252
�������	   CanUserReorderColumns	 .   .  �� .@�m� $��Копировать��
 m� Command$

 Copy���� m� -   $��В избранное��m� -   $ ��Редактировать��m� -   $��Удалить���� -   $��EmpSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� -   $��GroupsGroup��$��Отделы��$��50��$	��100��$��3q�$��0 10 0 0q�$y�	Collapsed=�.��� ���#404040.+��� $	��Top=����#404040$	��100��._�� .+��� .+�� .+��� -   $	��➕��$
��Left=�$��50��$��10 0 0 0q�#���  $! Создать запись���� $��Отделы��#���   $��70 8 0 8q�  -   $��GroupsTable��$��	0 50 5 20q����#525252
�������	 .   .  �� .@�m� $��Копировать��$

 Copy���� m� -   $ ��Редактировать��m� -   $��Удалить���� -   $��GrSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� -   $��	TechGroup��$��50��$	��100��$��5q�$��0 10 0 0q�$y�	Collapsed=����� $��images/PC.png��.��� ���#404040.+��� $
 Left=�.@��� -   $��ComputersGroup��$��Компьютеры��$��50��$	��100��$��3q�$��0 10 0 0q�$y�	Collapsed=�.��� ���#404040.+��� $	��Top=����#404040$	��100��._��� .+�� .+��� -   $	��➕��$
��Left=�$��50��$��10 0 0 0q�#���  $! Создать запись���� $��Компьютеры��#���   $��70 8 0 8q��� -   $	��⌕��$��20(�#���  $��Right=�$��0 0 5 0q�$ 
Поиск���� -   $��	Filter_PC��$��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -   $��FilterPCText��$��18(�$��Center=�
�������$��5 0 55 0q��� -   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -   $��PCTable��$��	0 50 5 20q����#525252
�������	 .   .  �� .@�m� $��Копировать��$

 Copy���� m� -    $��В избранное��m� -!   $ ��Редактировать��m� -"   $��Удалить���� -#   $��PCSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� -$   $��NotebookGroup��$��Ноутбуки��$��50��$	��100��$��3q�$��0 10 0 0q�$y�	Collapsed=�.��� ���#404040.+��� $	��Top=����#404040$	��100��._��� .+�� .+��� -%   $	��➕��$
��Left=�$��50��$��10 0 0 0q�#���  $! Создать запись���� $��Ноутбуки��#���   $��70 8 0 8q��� -&   $	��⌕��$��20(�#���  $��0 0 5 0q�$��Right=�$ 
Поиск���� -'   $��Filter_Notebook��$��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -(   $��FilterNotebookText��$��18(�$��Center=�
�������$��5 0 55 0q��� -)   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -*   $��NotebookTable��$��	0 50 5 20q����#525252
�������	 .   .  �� .@�m� $��Копировать��$

 Copy���� m� -+   $��В избранное��m� -,   $ ��Редактировать��m� --   $��Удалить���� -.   $��NBSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� -/   $��TabletGroup��$��Планшеты��$��50��$	��100��$��0q�$��0 10 0 0q�$y�	Collapsed=�.��� ���#404040.+��� $	��Top=����#404040$	��100��._��� .+�� .+��� -0   $	��➕��$
��Left=�$��50��$��10 0 0 0q�#���  $! Создать запись���� $��Планшеты��#���   $��70 8 0 8q��� -1   $	��⌕��$��20(�#���  $��0 0 5 0q�$��Right=�$ 
Поиск���� -2   $��Filter_Tablets��$��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -3   $��FilterTabletsText��$��18(�$��Center=�
�������$��5 0 55 0q��� -4   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -5   $��TabletsTable��$��	0 50 5 20q����#525252
�������	 .   .  �� .@�m� $��Копировать��$

 Copy���� m� -6   $��В избранное��m� -7   $ ��Редактировать��m� -8   $��Удалить���� -9   $��TabSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� -:   $��OTGroup��$��Оргтехника��$��50��$	��100��$��3q�$��0 10 0 0q�$y�	Collapsed=�.��� ���#404040.+��� $	��Top=����#404040$	��100��._��� .+�� .+��� -;   $	��➕��$
��Left=�$��50��$��10 0 0 0q�#���  $! Создать запись���� $��Оргтехника��#���   $��70 8 0 8q��� -<   $	��⌕��$��20(�#���  $��0 0 5 0q�$��Right=�$ 
Поиск���� -=   $��Filter_OrgTech��$��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� ->   $��FilterOrgTechText��$��18(�$��Center=�
�������$��5 0 55 0q��� -?   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -@   $��PrintersTable��$��	0 50 5 20q����#525252
�������	 .   .  �� .@�m� $��Копировать��$

 Copy���� m� -A   $��В избранное��m� -B   $ ��Редактировать��m� -C   $��Удалить���� -D   $��OTSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� -E   $��MonitorGroup��$��Мониторы��$��50��$	��100��$��3q�$��0 10 0 0q�$y�	Collapsed=�.��� ���#404040.+��� $	��Top=����#404040$	��100��._��� .+�� .+��� -F   $	��➕��$
��Left=�$��50��$��10 0 0 0q�#���  $! Создать запись���� $��Мониторы��#���   $��70 8 0 8q��� -G   $	��⌕��$��20(�#���  $��0 0 5 0q�$��Right=�$ 
Поиск���� -H   $��Filter_Monitors��$��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -I   $��FilterMonitorsText��$��18(�$��Center=�
�������$��5 0 55 0q��� -J   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -K   $��MonitorsTable��$��	0 50 5 20q����#525252
�������	 .   .  �� .@�m� $��Копировать��$

 Copy���� m� -L   $��В избранное��m� -M   $ ��Редактировать��m� -N   $��Удалить���� -O   $��MonSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� -P   $��RouterGroup��$��Роутеры��$��50��$	��100��$��3q�$��0 10 0 0q�$y�	Collapsed=�.��� ���#404040.+��� $	��Top=����#404040$	��100��._��� .+�� .+��� -Q   $	��➕��$
��Left=�$��50��$��10 0 0 0q�#���  $! Создать запись���� $��Роутеры��#���   $��70 8 0 8q��� -R   $	��⌕��$��20(�#���  $��0 0 5 0q�$��Right=�$ 
Поиск���� -S   $��Filter_Routers��$��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -T   $��FilterRoutersText��$��18(�$��Center=�
�������$��5 0 55 0q��� -U   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -V   $��RoutersTable��$��	0 50 5 20q����#525252
�������	 .   .  �� .@�m� $��Копировать��$

 Copy���� m� -W   $��В избранное��m� -X   $ ��Редактировать��m� -Y   $��Удалить���� -Z   $��RoutSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� -[   $��SwitchesGroup��$��Свитчи��$��50��$	��100��$��3q�$��0 10 0 0q�$y�	Collapsed=�.��� ���#404040.+��� $	��Top=����#404040$	��100��._��� .+�� .+��� -\   $	��➕��$
��Left=�$��50��$��10 0 0 0q�#���  $! Создать запись���� $��Свитчи��#���   $��70 8 0 8q��� -]   $	��⌕��$��20(�#���  $��0 0 5 0q�$��Right=�$ 
Поиск���� -^   $��Filter_Switches��$��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -_   $��FilterSwitchesText��$��18(�$��Center=�
�������$��5 0 55 0q��� -`   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -a   $��SwitchesTable��$��	0 50 5 20q����#525252
�������	 .   .  �� .@�m� $��Копировать��$

 Copy���� m� -b   $��В избранное��m� -c   $ ��Редактировать��m� -d   $��Удалить���� -e   $��SwitchSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� -f   $��FurnitureGroup��$��50��$	��100��$��5q�$��0 10 0 0q�$y�	Collapsed=����� $��images/Furniture.png��.��� ���#404040.+��� $
 Left=�.@��� $��Мебель��$��50��$	��100��$��3q�$��0 10 0 0q�.��� ���#404040.+��� $	��Top=����#404040$	��100��._��� .+�� .+��� -g   $	��➕��$
��Left=�$��50��$��10 0 0 0q�#���  $! Создать запись���� $��Мебель��#���   $��70 8 0 8q��� -h   $	��⌕��$��20(�#���  $��Right=�$��0 0 5 0q�$ 
Поиск���� -i   $��Filter_Furn��$��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -j   $��FilterFurnText��$��18(�$��Center=�
�������$��5 0 55 0q��� -k   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -l   $��	FurnTable��$��	0 50 5 20q����#525252
�������	 .   .  �� .@�m� $��Копировать��$

 Copy���� m� -m   $��В избранное��m� -n   $ ��Редактировать��m� -o   $��Удалить���� -p   $��FurnSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� $��Типы мебели��$��50��$	��100��$��3q�$��0 10 0 0q�.��� .+��� $	��Top=����#404040$	��100��._��� .+�� .+��� -q   $	��➕��$
��Left=�$��50��$��10 0 0 0q�#���  $! Создать запись���� $��Типы мебели��#���   $��70 8 0 8q�  -r   $��FurnTypeTable��$��	0 50 5 20q����#525252
�������	 .   .  �� .@�m� $��Копировать��$

 Copy���� m� -s   $ ��Редактировать��m� -t   $��Удалить���� -u   $��FurnTypeSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� -v   $��ReportsGroup��$��50��$	��100��$��5q�$��0 10 0 0q�$y�	Collapsed=����� $��images/Document.png��.��� ���#404040.+��� $
 Left=�.@��� -w   $��PCReport��$��Компьютеры��$��50��$	��100��$��3q�$��0 10 0 0q�$y�	Collapsed=�.��� .+��� $	��Top=����#404040$	��100��._��� .+�� .+��� $&�� Отчёт: Компьютеры��#���   $��10 8 0 8q��� -x   $��50��$��30��$��Right=�$��0 1 60 0q�.���� $��images/Exel.png��$��20��$ Экспорт���� -y   $	��⌕��$��20(�#���  $��0 0 5 0q�$��Right=�$ 
Поиск���� -z   $��Filter_ReportPC��$��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -{   $��FilterReportPCText��$��18(�$��Center=�
�������$��5 0 55 0q��� -|   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -}   $��ReportPC��$��	0 50 5 20q����#525252
�������	 .   .  �� .@�m� $��Копировать��$

 Copy���� -~   $��PCRepSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� -   $��NBReport��$��Ноутбуки��$��50��$	��100��$��3q�$��0 10 0 0q�$y�	Collapsed=�.��� .+��� $	��Top=����#404040$	��100��._��� .+�� .+��� $"��Отчёт: Ноутбуки��#���   $��10 8 0 8q��� -�   $��50��$��30��$��Right=�$��0 1 60 0q�.���� $��images/Exel.png��$��20��$ Экспорт���� -�   $	��⌕��$��20(�#���  $��0 0 5 0q�$��Right=�$ 
Поиск���� -�   $��Filter_ReportNB��$��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -�   $��FilterReportNBText��$��18(�$��Center=�
�������$��5 0 55 0q��� -�   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -�   $��ReportNB��$��	0 50 5 20q����#525252
�������	 .   .  �� .@�m� $��Копировать��$

 Copy���� -�   $��NBRepSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� -�   $��	TabReport��$��Планшеты��$��50��$	��100��$��3q�$��0 10 0 0q�$y�	Collapsed=�.��� .+��� $	��Top=����#404040$	��100��._��� .+�� .+��� $"��Отчёт: Планшеты��#���   $��10 8 0 8q��� -�   $��50��$��30��$��Right=�$��0 1 60 0q�.���� $��images/Exel.png��$��20��$ Экспорт���� -�   $	��⌕��$��20(�#���  $��0 0 5 0q�$��Right=�$ 
Поиск���� -�   $��Filter_ReportTab��$��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -�   $��FilterReportTabText��$��18(�$��Center=�
�������$��5 0 55 0q��� -�   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -�   $��	ReportTab��$��	0 50 5 20q����#525252
�������	 .   .  �� .@�m� $��Копировать��$

 Copy���� -�   $��TabRepSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� -�   $��IPReport��$��IP��$��50��$	��100��$��3q�$��0 10 0 0q�.��� .+��� $	��Top=����#404040$	��100��._��� .+�� .+��� $��Отчёт: IP��#���   $��10 8 0 8q��� -�   $��50��$��30��$��Right=�$��0 1 60 0q�.���� $��images/Exel.png��$��20��$ Экспорт���� -�   $��Filter_ReportIP��$��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -�   $��FilterReportIPText��$��18(�$��Center=�
�������$��5 0 55 0q��� -�   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -�   $��ReportIP��$��	0 50 5 20q����#525252
�������	 .   .  �� .@�m� $��Копировать��$

 Copy���� m� -�   $ ��Редактировать���� -�   $��IPRepSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� -�   $��
AdminGroup��$��50��$	��100��$��5q�$��0 10 0 0q�$y�	Collapsed=����� $��images/Users.png��.��� ���#404040.+��� $
 Left=�.@��� $��Пользователи��.��� .+��� $	��Top=����#404040$	��100��._��� .+�� .+��� -�   $	��➕��$
��Left=�$��50��$��10 0 0 0q�#���  $! Создать запись���� $��Пользователи��#���   $��70 8 0 8q��� -�   $	��⌕��$��20(�#���  $��0 0 5 0q�$��Right=�$ 
Поиск���� $��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -�   $��FilterUsersText��$��18(�$��Center=�
�������$��5 0 55 0q��� -�   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -�   $��
UsersTable��$��	0 50 5 20q����#525252
������� .  �� .@�m� $��Копировать��$

 Copy���� m� -�   $ ��Редактировать��m� -�   $��Удалить���� -�   $��UserSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� $��Роли��.��� .+��� $	��Top=����#404040$��50��._�� .+��� -�   $	��➕��$
��Left=�$��50��$��10 0 0 0q�#���  $! Создать запись���� $��Роли��#���   $��70 8 0 8q��� $��Right=� �� Orientation$ 
Horizontal=�  -�   $��
RolesTable��$��0 50 5 5q����#525252
������� .  �� .@�m� -�   $ ��Редактировать��m� -�   $��Удалить���� -�   $��RoleSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� $��Журнал��.��� .+��� $	��Top=����#404040$	��100��._��� .+�� .+��� $��Журнал��#���   $��10 8 0 8q��� -�   $	��⌕��$��20(�#���  $��Right=�$��0 0 5 0q�$ 
Поиск���� $��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -�   $��FilterLogsText��$��18(�$��Center=�
�������$��5 0 55 0q��� -�   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -�   $��	LogsTable��$��	0 50 5 20q����#525252
������� .  �� .@�m� $��Копировать��$

 Copy��m� $��Копировать��$

 Copy���� -�   $��LogSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� -�   $��DeletedGroup��$��50��$	��100��$��5q�$��0 10 0 0q�$y�	Collapsed=����� $��images/TrashBin.png��.��� ���#404040.+��� $
 Left=���. $��Visible=�.@��� $��Сотрудники��$��50��$	��100��$��3q�$��0 10 0 0q�.��� .+��� $	��Top=����#404040$	��100��._��� .+�� .+��� $&�� Архив: Сотрудники��#���   $��10 8 0 8q��� -�   $	��⌕��$��20(�#���  $��0 0 5 0q�$��Right=�$ 
Поиск���� $��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -�   $��FilterEmployeesText_Del��$��18(�$��Center=�
�������$��5 0 55 0q��� -�   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -�   $��EmployeesDeleted��$��	0 50 5 20q����#525252
������� .  �� .@�m� $��Копировать��$

 Copy���� m� -�   $��Открыть��m� -�   $��Восстановить��m� -�   $��Удалить���� -�   $��EmpDelSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� $��Компьютеры��$��50��$	��100��$��3q�$��0 10 0 0q�.��� .+��� $	��Top=����#404040$	��100��._��� .+�� .+��� $&�� Архив: Компьютеры��#���   $��10 8 0 8q��� -�   $	��⌕��$��20(�#���  $��0 0 5 0q�$��Right=�$ 
Поиск���� $��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -�   $��FilterPCText_Del��$��18(�$��Center=�
�������$��5 0 55 0q��� -�   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -�   $��	PCDeleted��$��	0 50 5 20q����#525252
������� .  �� .@�m� $��Копировать��$

 Copy���� m� -�   $��Открыть��m� -�   $��Восстановить��m� -�   $��Удалить���� -�   $��PCDelSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� $��Ноутбуки��$��50��$	��100��$��3q�$��0 10 0 0q�.��� .+��� $	��Top=����#404040$	��100��._��� .+�� .+��� $"��Архив: Ноутбуки��#���   $��10 8 0 8q��� -�   $	��⌕��$��20(�#���  $��0 0 5 0q�$��Right=�$ 
Поиск���� $��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -�   $��FilterNotebooksText_Del��$��18(�$��Center=�
�������$��5 0 55 0q��� -�   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -�   $��NotebookDeleted��$��	0 50 5 20q����#525252
������� .  �� .@�m� $��Копировать��$

 Copy���� m� -�   $��Открыть��m� -�   $��Восстановить��m� -�   $��Удалить���� -�   $��NBDelSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� $��Планшеты��$��50��$	��100��$��3q�$��0 10 0 0q�.��� .+��� $	��Top=����#404040$	��100��._��� .+�� .+��� $"��Архив: Планшеты��#���   $��10 8 0 8q��� -�   $	��⌕��$��20(�#���  $��0 0 5 0q�$��Right=�$ 
Поиск���� $��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -�   $��FilterTabletsText_Del��$��18(�$��Center=�
�������$��5 0 55 0q��� -�   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -�   $��TabletsDeleted��$��	0 50 5 20q����#525252
������� .  �� .@�m� $��Копировать��$

 Copy���� m� -�   $��Открыть��m� -�   $��Восстановить��m� -�   $��Удалить���� -�   $��TabDelSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� $��Оргтехника��$��50��$	��100��$��3q�$��0 10 0 0q�.��� .+��� $	��Top=����#404040$	��100��._��� .+�� .+��� $&�� Архив: Оргтехника��#���   $��10 8 0 8q��� -�   $	��⌕��$��20(�#���  $��0 0 5 0q�$��Right=�$ 
Поиск���� $��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -�   $��FilterOrgTechText_Del��$��18(�$��Center=�
�������$��5 0 55 0q��� -�   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -�   $��OrgTechDeleted��$��	0 50 5 20q����#525252
������� .  �� .@�m� $��Копировать��$

 Copy���� m� -�   $��Открыть��m� -�   $��Восстановить��m� -�   $��Удалить���� -�   $��OTDelSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� $��Мониторы��$��50��$	��100��$��3q�$��0 10 0 0q�.��� .+��� $	��Top=����#404040$	��100��._��� .+�� .+��� $"��Архив: Мониторы��#���   $��10 8 0 8q��� -�   $	��⌕��$��20(�#���  $��0 0 5 0q�$��Right=�$ 
Поиск���� $��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -�   $��FilterMonitorsText_Del��$��18(�$��Center=�
�������$��5 0 55 0q��� -�   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -�   $��MonitorsDeleted��$��	0 50 5 20q����#525252
������� .  �� .@�m� $��Копировать��$

 Copy���� m� -�   $��Открыть��m� -�   $��Восстановить��m� -�   $��Удалить���� -�   $��MonDelSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� $��Роутеры��$��50��$	��100��$��3q�$��0 10 0 0q�.��� .+��� $	��Top=����#404040$	��100��._��� .+�� .+��� $ ��Архив: Роутеры��#���   $��10 8 0 8q��� -�   $	��⌕��$��20(�#���  $��0 0 5 0q�$��Right=�$ 
Поиск���� $��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -�   $��FilterRoutersText_Del��$��18(�$��Center=�
�������$��5 0 55 0q��� -�   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -�   $��RoutersDeleted��$��	0 50 5 20q����#525252
������� .  �� .@�m� $��Копировать��$

 Copy���� m� -�   $��Открыть��m� -�   $��Восстановить��m� -�   $��Удалить���� -�   $��RoutDelSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� $��Свитчи��$��50��$	��100��$��3q�$��0 10 0 0q�.��� .+��� $	��Top=����#404040$	��100��._��� .+�� .+��� $��Архив: Свитчи��#���   $��10 8 0 8q��� -�   $	��⌕��$��20(�#���  $��0 0 5 0q�$��Right=�$ 
Поиск���� -�   $��Filter_Switch_Del��$��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -�   $��FilterSwitchesText_Del��$��18(�$��Center=�
�������$��5 0 55 0q��� -�   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -�   $��SwitchesDeleted��$��	0 50 5 20q����#525252
������� .  �� .@�m� $��Копировать��$

 Copy���� m� -�   $��Открыть��m� -�   $��Восстановить��m� -�   $��Удалить���� -�   $��SwitchDelSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� $��Мебель��$��50��$	��100��$��3q�$��0 10 0 0q�.��� ���#404040.+��� $	��Top=����#404040$	��100��._��� .+�� .+��� $��Архив: Мебель��#���   $��10 8 0 8q��� -�   $	��⌕��$��20(�#���  $��Right=�$��0 0 5 0q�$ 
Поиск���� -�   $��Filter_Furn_Del��$��	10 5 10 5q�$��1q�
��� ���$ 3��._�� .+��� -�   $��FilterFurnText_Del��$��18(�$��Center=�
�������$��5 0 55 0q��� -�   $	��✖��#���  $��Right=�$# Сбросить фильтр��  -�   $��FurnDeleted��$��	0 50 5 20q����#525252
�������	 .   .  �� .@�m� $��Копировать��$

 Copy���� m� -�   $��Открыть��m� -�   $��Восстановить��m� -�   $��Удалить���� -�   $��FurnDelSelectedCount��$��Выбрано: 0��$��Bottom=�$��Right=�
�������$��0 0 50 3q��� -�   $��Bottom=�$
��Left=�$��	30 0 0 65q� & "MaterialDesignFloatingActionButton#���  $��40��$��40��.���� $��images/Manual.png��$��30��$! Руководство | F1���� -�   $��Bottom=�$
��Left=�$��	30 0 0 10q�#���  $��40��$��40��.���� $��images/Exit.png��$��30��$ 
Выход���� -�   $��ReloadButton��$��Bottom=�$
��Left=�$��
30 0 0 120q�#���  $��40��$��40��.���� $��images/Reload.png��$��30��$* $Обновить таблицы | F5���� -�    ��Name$ 	Blindfall��
���   � J� Opacity$
 0.35Q�$y�	Collapsed=��� -�   $��UserLoginText��$��[Login]��$��24(�$��SemiBold"�
�������$	��Top=�$��Right=�$��	0 5 130 0q�