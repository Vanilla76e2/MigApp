   M S B A M L   `   `   `  ���� B  >MigApp, Version=2.0.0.12, Culture=neutral, PublicKeyToken=nullG(clr-namespace:MigApp.MVVM.View.CRWindowsMigApp.MVVM.View.CRWindows  =#clr-namespace:MigApp.CustomElementsMigApp.CustomElements  0    *MigApp.MVVM.View.CRWindows.ComputersWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :local(clr-namespace:MigApp.MVVM.View.CRWindows     >customElements#clr-namespace:MigApp.CustomElements     _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � Title$!  MigApp - Компьютер��$	��620��$	��950�� � WindowStyle$
 None=� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=�'  !System.Windows.Shell.WindowChrome   WindowChrome      CaptionHeight$ 45Q�   UseAeroCaptionButtons$
 True��   CornerRadius$ 20��   ResizeBorderThickness$ 10q�	   GlassFrameThickness$	 0q�.��� ���#252525.+��� ._�� 
 � RowDefinitions
 �� $��50 ��� $��1* �.+��� $��1�� �� TabStripPlacement$
 Left=�.@��� $'��!Основные свойства��$��0 10 0 0q�.����    MainPanelBorder#���   $��0 0 0 30q�._��� $��20 10q�.+���  �� Orientation$ 
Horizontal=�$	��0 5q�.+��� $!��Имя компьютера��  ComputersCRTextBlock#���  ��   ComputersCRTextBox#���  ���� *ComputerName+�� $ 
Horizontal=�$	��0 5q�.+��� $'��!Инвентарный номер��#���  �� #���  ���� *InventoryNumber+�� $ 
Horizontal=�$	��0 5q�.+��� $��Сетевой адрес��#���  ,   &MigApp.CustomElements.IPAddressControl  -     Name$
 Test��$	��200��
�������$��20(�	   IP �� *IP+�� $ 
Horizontal=�$	��0 5q�.+��� $��Пользователь��#���  �� $	��470��
�������$��20(� �� 
IsEditable$
 True�� �� DisplayMemberPath$	 fio�� �� SelectedValuePath$ id������ *
UserList+ �� SelectedValue �� *SelectedUser+�� $ 
Horizontal=�$	��0 5q�.+��� $-��'Операционная система��#���  �� #���  ���� *OS+�� $ Vertical=�$	��0 5q�.+��� $��Комментарий��  TextBlock_20#���  $��0 0 0 5q��� $��20(� ! MaterialDesignOutlinedTextBox#���  
�������$	��220�� �� TextWrapping$
 Wrap=� �� AcceptsReturn$
 True������ *	Comment+�� $��Комплектация��$��0 10 0 0q�.���� #���   $��0 0 0 30q�._�� $��0 5 0 10q�
 �� $��1* ��� $��5 ��� $��1* �.+�� $��0��$
��10 0q�
 �� $��50 ��� $��1* �.+�&   System.Windows.Controls.DataGrid  $	��210��  ManualDataGrid#���  $��Bottom=�   CanUserAddRows$ False��$��1������ *
Components+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��   
IsReadOnly$
 True��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *id+  $ Наименование�� �� *name+  $' !Инвентарный номер�� �� *inventory_number+   EditingElementStyle ��  �� 
TargetType" �� 0 ,MaterialDesignDataGridTextColumnEditingStyle �� BasedOn# [ .���  �� Property �@��	MaxLength �� Value$ 15���� .  (MaterialDesignThemes.Wpf.TextFieldAssist" �@ CharacterCounterVisibility$ 	Collapsed=�  $ Спецификация�� �� *	specifies+� $��0�� � ColumnDefinitions �� �� .+��� $1��+Аппаратное обеспечение��#���  $��Center=�o� $��1��$��Right=�.@�m�   MaterialDesignMenuItem#���  ���� $ 
Horizontal=�.+��� $��Добавить��
�������$	��5 0q�'  !MaterialDesignThemes.Wpf.PackIcon      Kind$   ArrowDownDropCircleOutline=�$��20��$��20��.@�m� $'��!Материнская плата��! m� CommandParameter$! Motherboard��" m� Command" �� *AddComponentCommand+m� $
��ЦП��# � ToolTip$/# )Центральный процессор��$	! CPU��" �� *AddComponentCommand+m� $��ОЗУ��$)# #Оперативная память��$	! RAM��" �� *AddComponentCommand+m� $��Блок питания��$! PS��" �� *AddComponentCommand+m� $
��ГП��$/# )Графический процессор��$	! GPU��" �� *AddComponentCommand+�� m� $��Другое��$! Other��" �� *AddComponentCommand+� $��2��$
��10 0q�
 �� $��50 ��� $��1* �.+�  $	��210��#���  $ False��$��Bottom=�$��1������ *	Devices+   $ ID��$
 True�� �� *id+  $ Наименование�� �� *name+  $' !Инвентарный номер�� �� *inventory_number+ �� " �� 1 -MaterialDesignDataGridComboBoxEditableTextBox# [ .���  �@��	MaxLength$ 15���� " �@ CharacterCounterVisibility$ 	Collapsed=�  $ Модель�� �� *model+  $ Спецификация�� �� *model+  $ Комментарий�� �� *	comment+� $��0�� �� �� .+��� $3��-Периферийные устройства��#���  $��Center=�o� $��1��$��Right=�.@�m� ���� $ 
Horizontal=�.+��� $��Добавить��
�������$	��5 0q�  $   ArrowDownDropCircleOutline=�$��20��$��20��.@�m� $��Монитор��" �� *
AddMonitor+m� $��ИБП��" �� *AddUPS+�� m� $��Другое��" �� *AddOtherDevice+�� $+��%Журнал обслуживания��$��0 10 0 0q�.���� #���   $��0 0 0 30q�._�� $��
10 0 10 10q�
 �� $��60 ���  �� $��70 ��� .+���  	 	AddButton#��� 	 $## Добавить запись������ *
AddService+��  $	  Add=�$��30��$��30��$��-15 -5q����#FF9700�� $+��%Журнал обслуживания�� 

 Header#��� 
 $��1��  $��1��$��2��#���  $ False������ *ServiceHistory+   $ ID��$
 True��$   MinWidth$$ 50Q�%   MaxWidth$	% 100Q� �� *id+4	  .System.Windows.Controls.DataGridTemplateColumn	  $ Дата��&   Width5
  /System.Windows.Controls.DataGridLengthConverter$	& 100
 '   CanUserResize$' False��( 	  CellTemplate( �� .R��� ���� *date+) 	  CellEditingTemplate) �� .R�(  "System.Windows.Controls.DatePicker  * � Language$* Ru�+   SelectedDate+ �� *date+, �� UpdateSourceTrigger$, PropertyChanged=�	  $ Сотрудник��$	$ 200Q�( �� .R��� $	 fio��$	 fio�� " MaterialDesignDataGridComboBox#���  ���� *DataContext.UserList+- �� RelativeSource- �� . �� AncestorType0   *MigApp.MVVM.View.CRWindows.ComputersWindow".   �� *
employee+$, PropertyChanged=�  $! Описание работ��$	% 485Q�$	$ 200Q� �� *description+ �� " ��# [ .���  �@��TextWrapping$
 Wrap=�/   ElementStyle/ �� " �� ) %MaterialDesignDataGridTextColumnStyle# [ .���  � ��$
 Wrap=�� $��15 30q�$��Bottom=�$
��Left=�$��1��.+��� $	��150��.+��� $��Сохранить��$
��0 10q�$��18(��� $��Удалить��$��18(�   MaterialDesignOutlinedButton#���  �   MainPanelHeader#���  .+��� $��Компьютер��  HeaderPanelText#���  $
��15 0q��� $ 
Horizontal=�$��Right=�.+��� -   $��Custom_MinimizeButton��  WindowControlButtons#���  .���� .+�  $  Minimize=�$
��-5 0q��� -   $��Custom_CloseButton��#���  .���� .+�  $  Close=�$
��-5 0q�