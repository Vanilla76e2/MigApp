   M S B A M L   `   `   `  ����B  >MigApp, Version=2.0.0.12, Culture=neutral, PublicKeyToken=nullG(clr-namespace:MigApp.MVVM.View.CRWindowsMigApp.MVVM.View.CRWindows  5   	   =#clr-namespace:MigApp.CustomElementsMigApp.CustomElements  5   	   0    *MigApp.MVVM.View.CRWindows.ComputersWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:MigApp.MVVM.View.CRWindows     5   	   >customElements#clr-namespace:MigApp.CustomElements     5   	   c _MaterialDesignThemes.MahApps, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a_ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5   	     � Title$!  MigApp - Компьютер��5
   	   $	��620��6$   $	��950��61    � WindowStyle$
 None=�6=    � 
ResizeMode$ CanMinimize=�6P    � WindowStartupLocation$ CenterScreen=�6i   '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
      CaptionHeight$ 45Q�5   	      UseAeroCaptionButtons$
 True��5   	      CornerRadius$ 20��5   	      ResizeBorderThickness$ 10q�5   	   	   GlassFrameThickness$	 0q�5   	   .��5      � ���#252525.+�5   
   �� ._�5      � 
 � RowDefinitions
 5      �� 5      $��50 �6$   �� 5      $��1* �6$   .+�5      �� $��1��6    �� TabStripPlacement$
 Left=�6*   .@�5      �� $'��!Основные свойства��6   $��0 10 0 0q�69   .��5      ��    MainPanelBorder#���   6!   $��0 0 0 30q�6K   ._�5       �� $��20 10q�6)   .+�5!   "   ��  �� Orientation$ 
Horizontal=�6-   $	��0 5q�6F   .+�5"   &   �� $!��Имя компьютера��60     ComputersCRTextBlock#���  6F   �� 5#   &     ComputersCRTextBox#���  6.   ��6[   �� *ComputerName+5$   #   �� 5%   "   $ 
Horizontal=�6-   $	��0 5q�6F   .+�5&   &   �� $'��!Инвентарный номер��60   #���  6I   �� 5'   &   #���  6.   ��6[   �� *InventoryNumber+5(   #   �� 5)   "   $ 
Horizontal=�6-   $	��0 5q�6F   .+�5*   &   �� $��Сетевой адрес��60   #���  6E   ,   &MigApp.CustomElements.IPAddressControl  5+   &   -   5          Name$
 Test��5+   F   $	��200��6T   
�������$��20(�6s   	   IP 6�   �� *IP+5,   #   �� 5-   "   $ 
Horizontal=�6-   $	��0 5q�6F   .+�5.   &   �� $��Пользователь��60   #���  6D   �� 5/   &   $	��470��6/   
�������$��20(�6N    �� 
IsEditable$
 True��6\    �� DisplayMemberPath$	 fio��6�    �� SelectedValuePath$ id��6�   ��6n   �� *
UserList+ �� SelectedValue 6�   �� *SelectedUser+6�   50   #   �� 51   "   $ 
Horizontal=�6-   $	��0 5q�6F   .+�52   &   �� $-��'Операционная система��60   #���  6L   �� 53   &   #���  6.   ��6[   �� *OS+54   #   �� 55   "   $ Vertical=�6-   $	��0 5q�6D   .+�56   &   �� $��Комментарий��60     TextBlock_20#���  6C   $��0 0 0 5q�6j   �� 57   &   $��20(�6.    ! MaterialDesignOutlinedTextBox#���  6<   
�������$	��220��6�    �� TextWrapping$
 Wrap=�6�    �� AcceptsReturn$
 True��6�   ��6t   �� *	Comment+6�   58   #   59      5:      5;      �� 5=      $��Комплектация��6   $��0 10 0 0q�64   .��5>      �� #���   6!   $��0 0 0 30q�6K   ._�5?      � $��0 5 0 10q�6#   
 5@   "   �� 5A   &   $��1* �64   �� 5B   &   $��5 �64   �� 5C   &   $��1* �64   .+�5E   "   � $��0��6'   $
��10 0q�64   
 5F   &   �� 5G   *   $��50 �68   �� 5H   *   $��1* �68   .+�5J   &   &   System.Windows.Controls.DataGrid  $	��210��6/     ManualDataGrid#���  6<   $��Bottom=�6�      CanUserAddRows$ False��6�   $��1��6�   ��6e   �� *
Components+   Columns 5K   *   0  *System.Windows.Controls.DataGridTextColumn  5L   .   ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6A      
IsReadOnly$
 True��6M   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6_   �� *id+  5M   .   $ Наименование��6A    6W   �� *name+  5N   .   $' !Инвентарный номер��6A    6\   �� *inventory_number+   EditingElementStyle 5O   2   �� 5P   6    �� 
TargetType" ��6<    0 ,MaterialDesignDataGridTextColumnEditingStyle �� BasedOn# [ 6Z   .�5Q   :   ��  �� Property �@��	MaxLength �� Value$ 15��6V   �� 5R   :   .  (MaterialDesignThemes.Wpf.TextFieldAssist" �@ CharacterCounterVisibility$ 	Collapsed=�6�   5S   7   5U   /     5V   .   $ Спецификация��6A    6W   �� *	specifies+5X   '   � 5Y   &   $��0��6+    � ColumnDefinitions 5Z   *   �� 5[   .   �� 5\   .   .+�5^   *   �� $1��+Аппаратное обеспечение��64   #���  6R   $��Center=�6y   o� 5_   *   $��1��6/   $��Right=�6?   .@�5`   .   m�   MaterialDesignMenuItem#���  67   ��5a   2   �� 5b   6   $ 
Horizontal=�6A   .+�5c   :   �� $��Добавить��6D   
�������$	��5 0q�6g   '  !MaterialDesignThemes.Wpf.PackIcon  5d   :       Kind$   ArrowDownDropCircleOutline=�6R   $��20��6t   $��20��6�   5e   7   .@�5g   2   m� $'��!Материнская плата��6;   ! m� CommandParameter$! Motherboard��6~   " m� Command" 6V   �� *AddComponentCommand+6~   m� 5h   2   $
��ЦП��6;   # � ToolTip$/# )Центральный процессор��6G   $	! CPU��6�   " 6g   �� *AddComponentCommand+6�   m� 5i   2   $��ОЗУ��6;   $)# #Оперативная память��6H   $	! RAM��6�   " 6e   �� *AddComponentCommand+6�   m� 5j   2   $��Блок питания��6;   $! PS��6y   " 6Q   �� *AddComponentCommand+6y   m� 5k   2   $
��ГП��6;   $/# )Графический процессор��6G   $	! GPU��6�   " 6g   �� *AddComponentCommand+6�   �� 5l   2   m� 5m   2   $��Другое��6;   $! Other��6s   " 6K   �� *AddComponentCommand+6s   5n   /   5o   +   5p   '   5q   #   � 5s   "   $��2��6'   $
��10 0q�65   
 5t   &   �� 5u   *   $��50 �68   �� 5v   *   $��1* �68   .+�5x   &     $	��210��6/   #���  6<   $ False��6�   $��Bottom=�6�   $��1��6�   ��6e   �� *	Devices+ 5y   *     5z   .   $ ID��6A   $
 True��6M    6_   �� *id+  5{   .   $ Наименование��6A    6W   �� *name+  5|   .   $' !Инвентарный номер��6A    6\   �� *inventory_number+ 5}   2   �� 5~   6   " ��6<    1 -MaterialDesignDataGridComboBoxEditableTextBox# [ 6Z   .�5   :   ��  �@��	MaxLength$ 15��6V   �� 5�   :   " �@ CharacterCounterVisibility$ 	Collapsed=�6�   5�   7   5�   /     5�   .   $ Модель��6A    6Q   �� *model+  5�   .   $ Спецификация��6A    6W   �� *model+  5�   .   $ Комментарий��6A    6V   �� *	comment+5�   '   � 5�   &   $��0��6+    5�   *   �� 5�   .   �� 5�   .   .+�5�   *   �� $3��-Периферийные устройства��64   #���  6S   $��Center=�6z   o� 5�   *   $��1��6/   $��Right=�6?   .@�5�   .   m� ��5�   2   �� 5�   6   $ 
Horizontal=�6A   .+�5�   :   �� $��Добавить��6D   
�������$	��5 0q�6g     5�   :   $   ArrowDownDropCircleOutline=�6R   $��20��6t   $��20��6�   5�   7   .@�5�   2   m� $��Монитор��6;   " 6L   �� *
AddMonitor+m� 5�   2   $��ИБП��6;   " 6H   �� *AddUPS+�� 5�   2   m� 5�   2   $��Другое��6;   " 6K   �� *AddOtherDevice+5�   /   5�   +   5�   '   5�   #   5�      5�      5�      �� 5�      $+��%Журнал обслуживания��6   $��0 10 0 0q�6;   .��5�      �� #���   6!   $��0 0 0 30q�6K   ._�5�      � $��
10 0 10 10q�6#   
 5�   "   �� 5�   &   $��60 �64   �� 5�   &    5�   "   �� 5�   &   $��70 �67   �� 5�   &   .+�5�   "   ��  	 	AddButton#��� 	 6)   $## Добавить запись��6M   ��6g   �� *
AddService+��5�   &     5�   *   $	  Add=�6B   $��30��6M   $��30��6X   $��-15 -5q�6d   ���#FF97006t   5�   #   �� 5�   "   $+��%Журнал обслуживания��6,    

 Header#��� 
 6G   $��1��6h     5�   "   $��1��6+   $��2��68   #���  6L   $ False��6�   ��6u   �� *ServiceHistory+ 5�   &     5�   *   $ ID��6=   $
 True��6I   $   MinWidth$$ 50Q�6r   %   MaxWidth$	% 100Q�6�    6[   �� *id+6�   4	  .System.Windows.Controls.DataGridTemplateColumn	  5�   *   $ Дата��6A   &   Width5
  /System.Windows.Controls.DataGridLengthConverter$	& 100
 6O   '   CanUserResize$' False��6[   ( 	  CellTemplate( 5�   .   �� 5�   2   .R�5�   6   �� ��6@   �� *date+5�   3   ) 	  CellEditingTemplate) 5�   .   �� 5�   2   .R�5�   6   (  "System.Windows.Controls.DatePicker  * � Language$* Ru�6�   +   SelectedDate+ 6A   �� *date+, �� UpdateSourceTrigger$, PropertyChanged=�6�   5�   3   5�   +   	  5�   *   $ Сотрудник��6A   $	$ 200Q�6T   ( 5�   .   �� 5�   2   .R�5�   6   �� $	 fio��6�   $	 fio��6�    " MaterialDesignDataGridComboBox#���  61  ��6?   �� *DataContext.UserList+- �� RelativeSource- �� . �� AncestorType0   *MigApp.MVVM.View.CRWindows.ComputersWindow".   6�   �� *
employee+$, PropertyChanged=�61  5�   3   5�   +     5�   *   $! Описание работ��6=   $	% 485Q�6u   $	$ 200Q�6�    6U   �� *description+ 5�   .   �� 5�   2   " ��68   # [ 6V   .�5�   6   ��  �@��TextWrapping$
 Wrap=�6U   5�   3   /   ElementStyle/ 5�   .   �� 5�   2   " ��68    ) %MaterialDesignDataGridTextColumnStyle# [ 6X   .�5�   6   ��  � ��$
 Wrap=�6U   5�   3   5�   +   5�   #   5�      5�      5�      5�      � 5�      $��15 30q�6   $��Bottom=�6&   $
��Left=�6A   $��1��6\   .+�5�      �� $	��150��6!   .+�5�      �� $��Сохранить��6!   $
��0 10q�65   $��18(�6C   �� 5�      $��Удалить��6!   $��18(�63      MaterialDesignOutlinedButton#���  6A   5�      5�      � 5�        MainPanelHeader#���  6   .+�5�      �� $��Компьютер��6      HeaderPanelText#���  61   $
��15 0q�6[   �� 5�      $ 
Horizontal=�6!   $��Right=�6:   .+�5�      �� -   5        $��Custom_MinimizeButton��5�   !     WindowControlButtons#���  6>   .��5�      �� .+�5�   "     $  Minimize=�6:   $
��-5 0q�6J   5�      5�      �� 5�      -   5        $��Custom_CloseButton��5�   !   #���  6;   .��5�      �� .+�5�   "     $  Close=�6:   $
��-5 0q�6G   5�      5�      5�      5�      5�      5�      5�      5�      