   M S B A M L   `   `   `  ���� B  >MigApp, Version=2.0.0.12, Culture=neutral, PublicKeyToken=null3clr-namespace:MigApp.MVVM.ViewMigApp.MVVM.View  $    MigApp.MVVM.View.EmployeesView   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:MigApp.MVVM.View     _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  ���#252525.��� ���#252525  � RowDefinitions  �� $��1* ��� $��15 �.+���    MainPanelBorder#��[  $��0��._�� .+��   MainPanelHeader#��[ .+��� -   $��EmployeesAddButton��  	AddButton#��[ ���� *
AddCommand+�� $��Сотрудники��  HeaderWithAddButton#��[ &   System.Windows.Controls.DataGrid  -   $��Table��  MainDataGrid#���  ���� *Table+ � ContextMenu �� .@�m� $ ��Редактировать�� m� Icon 0  *MaterialDesignThemes.Wpf.PackIconExtension     Kind$ Pencil=� m� Command �� *RedactCommand+m� $��Копировать�� m� InputGestureText$ Ctrl+C��   $ ContentCopy=� �� *MyCopyCommand+m� $��Удалить��$	 Del��   $ Delete=� �� *MyDeleteCommand+�� m� $��
Отчёт��   $
 File=� �� *ReportCommand+� -   $��FilterPanel��$��Right=�$��70��.+���   
FilterBody#��[ ._�� .+��� $��Center=�$��	10 0 0 50q�.+���  �� Orientation$ 
Horizontal=�$��0 20 0 0q�.+��� $��ID��  FilterTextBlock#���  �� -   $��ID��  FilterTextBox#���  �� $ 
Horizontal=�$��0 20 0 0q�.+��� $��ФИО��#���  �� -   $	��FIO��#���  �� $ 
Horizontal=�$��0 20 0 0q�.+��� $��
Отдел��#���  �� -   $��Group��  FilterComboBox#���   �� 
IsEditable$
 True�� �� IsTextSearchEnabled$
 True���� $ 
Horizontal=�$��0 20 0 0q�.+��� $��Кабинет��#���  �� -   $
��Room��#���  � $��60 15q�$��Bottom=�.+��� $��Применить��$
��Left=�$	��300������ *ApplyFilterCommand+�� $��Сбросить��  	 MaterialDesignOutlinedButton#��� 	 $��Right=����� *ClearFilterCommand+��  
 FilterLatch#��� 
 ._�d� -   $��Filter��  FilterButton#��[ 	 � Triggers	 :� 
 :� RoutedEvent$
 ToggleButton.Checked��.[��� .���� .��W�  �� 
TargetName$ FilterPanel�� �� TargetProperty$ (Width)� W� From 70	 W� To 600 g� Duration$ 0:0:0.2D� g� AutoReverse$ False��:� $
 ToggleButton.Unchecked��.[��� .���� .��W� $ FilterPanel��$ (Width)� 600 70$ 0:0:0.2D�$ False����  
 Footer#��[ ._���   FooterSelectedRowsCounter#��[ ����  �� ElementName$ Table�� �� Path$ SelectedItems.Count� �� StringFormat$ Выбрано: {0}��