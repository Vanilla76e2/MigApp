   M S B A M L   `   `   `  ���� A  =MigApp, Version=1.6.1.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MigApp.CRWindowsMigApp.CRWindows  $    MigApp.CRWindows.MonitorWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:MigApp.CRWindows     X TXceed.Wpf.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � Title$!  Монитор [режим]��$	��590��$	��600�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � Icon$ /images/Icon.png��.��� ���#323232.+��� $	��Top=�$��0 20 0 0q�.+���  �� Orientation$ 
Horizontal=�$��Center=�$��	0 10 0 20q�.+��� $(��"Инвентарный номер*��   TextBlock_20#���   $	��270��$��0 0 20 0q�$��Center=��� -   $��InvNum��  MaterialDesignTextBox#���  
�������$	��270��$��18(� �� 	MaxLength$ 15��.  (MaterialDesignThemes.Wpf.TextFieldAssist!   CharacterCounterVisibility$ 	Collapsed=��� 
�������$��1q� �� CornerRadius$ 5��$��	5 10 5 65q�$	��400��._��� $��0 0 0 0q�.@��� $��Основное��.��� .+��� .���� .+��� $ 
Horizontal=�$��Center=�$��	0 20 0 20q�$	��550��.+��� $��Фирма*��#���   $	��210��$��0 0 10 0q�$��Center=��� -   $
��Firm��#���  
�������$	��330��$��16(�$ 50��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��0 0 0 20q�$	��550��.+��� $��Модель*��#���   $	��210��$��0 0 10 0q�$��Center=��� -   $��Model��#���  
�������$	��330��$��16(�$ 50��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��0 0 0 20q�$	��550��.+��� $"��Серийный номер*��#���   $	��210��$��0 0 10 0q�$��Center=��� -   $��SeriaNum��#���  
�������$	��330��$��16(�$ 25��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��0 0 0 20q�$	��550��.+��� $��Пользователь��#���   $	��210��$��0 0 10 0q�$��Center=��� -   $
��User��  MaterialDesignComboBox#���  
�������$��16(�$	��278�� �� 
IsEditable . 	 �� MaxDropDownHeight$		 200��
 �� IsTextSearchEnabled
 .  �� -   $��EmployeeAdd��$��+��$��10 0 0 0q��� $ 
Horizontal=�$��Center=�$��0 0 0 20q�$	��550��.+��� $��Компьютер��#���   $	��210��$��0 0 10 0q�$��Center=��� -   $��PC��#���  
�������$��16(�$	��278��$		 200��{�.   � ContextMenu �� .@�m� -   $��Очистить���� -	   $��PCAdd��$��+��$��10 0 0 0q��� $ 
Horizontal=�$��Center=�$��0 0 0 10q�$	��550��.+��� $��Комментарий��#���   $��Bottom=�$	��550��$��0 0 5 0q��� $ 
Horizontal=�$��Center=�$��0 0 0 20q�$	��550��.+��� -
   $��Comment�� ! MaterialDesignOutlinedTextBox#���  $��16(�
�������$	��550�� �� TextWrapping$
 Wrap=�$	 250��" �� VerticalScrollBarVisibility$ Visible=�$	��100�� �� AcceptsReturn . �� $1��+Техническая информация��.��� .+��� $��2��.+��� $ 
Horizontal=�$��Center=�$��	0 20 0 20q�$	��550��.+��� $��Диагональ*��#���   $	��200��$��0 0 10 0q�$��Center=��� -   $��ScreenDiagonal��#���  
�������$	��250��$��16(�$ 5��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��0 0 0 20q�$	��550��.+��� $��Разрешение*��#���   $	��200��$��0 0 10 0q�$��Center=�%  Xceed.Wpf.Toolkit.MaskedTextBox  -   $��ScreenResolution��   Mask$ 	9999x9999��#���  
�������$	��250��$��16(��� $ 
Horizontal=�$��Center=�$��0 0 0 20q�$	��550��.+��� $��Тип матрицы��#���   $	��200��$��0 0 10 0q�$��Center=��� -   $��
ScreenType��#���  
�������$	��250��$��16(�$ 10��$ 	Collapsed=��� -   $��
DoneButton��$��Сохранить��$��Bottom=�$��Right=�$��	0 0 20 10q�$��18(��� -   $��RecoveryButton��$��Восстановить��$��Bottom=�$��Right=�$��	0 0 20 10q�$��18(�$y�	Collapsed=��� -   $��DeleteButton��$��Удалить��   MaterialDesignOutlinedButton#���  $��Bottom=�$
��Left=�$��	20 0 0 10q�$��18(�