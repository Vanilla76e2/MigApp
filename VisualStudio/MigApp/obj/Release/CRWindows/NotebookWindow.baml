   M S B A M L   `   `   `  ���� A  =MigApp, Version=1.6.1.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MigApp.CRWindowsMigApp.CRWindows  %    MigApp.CRWindows.NotebookWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:MigApp.CRWindows     X TXceed.Wpf.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � Title$!  Ноутбук [режим]��$	��660��$	��600�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � Icon$ /images/Icon.png��.��� ���#323232.+��� $	��Top=�$��0 20 0 0q�.+���  �� Orientation$ 
Horizontal=�$��Center=�$��	0 10 0 20q�.+��� $(��"Инвентарный номер*��   TextBlock_20#���   $	��270��$��0 0 10 0q��� -   $��InvNum��  MaterialDesignTextBox#���  $��18(�
�������$	��280�� �� 	MaxLength$ 15��.  (MaterialDesignThemes.Wpf.TextFieldAssist!   CharacterCounterVisibility$ 	Collapsed=��� 
�������$��1q� �� CornerRadius$ 5��$��	5 10 5 60q�$	��470��._��� $��0 0 0 0q�.@��� $��Основное��.���� .��� .+��� .+��� $ 
Horizontal=�$��Center=�$��	0 30 0 30q�$	��550��.+��� $��Модель*��#���   $��Bottom=�$	��210��$��0 0 10 0q��� -   $��Model��#���  $��16(�$ 50��$ 	Collapsed=�
�������$	��330���� $ 
Horizontal=�$��Center=�$��0 0 0 30q�$	��550��.+��� $"��Серийный номер*��#���   $��Bottom=�$	��210��$��0 0 10 0q��� -   $��SeriaNum��#���  $��16(�
�������$ 25��$ 	Collapsed=�$	��330���� $ 
Horizontal=�$��Center=�$��0 0 0 30q�$	��550��.+��� $��Пользователь��#���   $��Bottom=�$	��210��$��0 0 10 0q��� -   $
��User��  MaterialDesignComboBox#���  
�������$��16(�$	��278�� �� 
IsEditable . 	 �� MaxDropDownHeight$		 200��
 �� IsTextSearchEnabled
 .  �� -   $��EmployeeAdd��$��+��$��10 0 0 0q��� $ 
Horizontal=�$��Center=�$��0 0 0 30q�$	��550��.+��� $
��ОС��#���   $��Bottom=�$	��210��$��0 0 10 0q��� -   $��OS��#���  $��16(�
�������$ 25��$ 	Collapsed=�$	��330���� $ 
Horizontal=�$��Center=�$��0 0 0 10q�$	��550��.+��� $��Комментарий��#���   $��Bottom=�$	��550��$��0 0 5 0q��� $ 
Horizontal=�$��Center=�$��0 0 0 0q�$	��550��.+��� -   $��Comment�� ! MaterialDesignOutlinedTextBox#���  $��16(�
�������$	��550�� �� TextWrapping$
 Wrap=�$	 250��$	��100�� �� AcceptsReturn . �� $ ��Комплектующие��.��� .+��� .���� .+��� $ 
Horizontal=�$��Center=�$��	0 10 0 10q�$	��530��.+��� $��Диагональ*��#���   $��Bottom=�$	��210��$��0 0 10 0q��� -   $��ScreenDiagonal��#���  $��16(�
�������$	��310��$
 Wrap=�$ 5��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��0 0 0 10q�$	��530��.+��� $(��"Разрешение экрана*��#���   $��Bottom=�$	��210��$��0 0 10 0q�%  Xceed.Wpf.Toolkit.MaskedTextBox  -	   $��ScreenResolution��   Mask$ 	9999x9999��#���  $��16(�
�������$	��310��$
 Wrap=��� $ 
Horizontal=�$��Center=�$��0 0 0 10q�$	��530��.+��� $��Процессор*��#���   $��Bottom=�$	��210��$��0 0 10 0q��� -
   $��	Processor��#���  $��16(�
�������$	��310��$
 Wrap=��� $ 
Horizontal=�$��Center=�$��0 0 0 10q�$	��530��.+��� $��ОЗУ*��#���   $��Bottom=�$	��210��$��0 0 10 0q��� -   $	��RAM��#���  $��16(�
�������$	��310��$
 Wrap=��� $ 
Horizontal=�$
��Left=�$��10 0 0 0q�$	��530��.+��� $��Накопители*��#���   $��Bottom=�$��0 0 0 0q��� $��5 0 0 10q�$	��530��.+��� -   $��Drive��#���  $��16(�
�������$	��530��$	 250��$
 Wrap=�$	��100��" �� VerticalScrollBarVisibility$ Visible=� . �� $ 
Horizontal=�$
��Left=�$��10 0 0 0q�$	��530��.+��� $��Иное��#���   $��Bottom=�$��0 0 10 0q��� $��5 0 0 20q�$	��530��.+��� -   $��Other��#���  $��16(�
�������$	��530��$
 Wrap=�$	 250��$	��100��$ Visible=� . �� -   $��
DoneButton��$��Сохранить��$��Bottom=�$��Right=�$��18(�$��	0 0 20 10q��� -   $��RecoveryButton��$��Восстановить��$��Bottom=�$��Right=�$��18(�$��	0 0 20 10q�$y�	Collapsed=��� -   $��DeleteButton��$��Удалить��   MaterialDesignOutlinedButton#���  $��18(�$��Bottom=�$
��Left=�$��	20 0 0 10q�