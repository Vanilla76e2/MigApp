   M S B A M L   `   `   `  ���� A  =MigApp, Version=1.6.1.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MigApp.CRWindowsMigApp.CRWindows  $    MigApp.CRWindows.TabletsWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:MigApp.CRWindows     _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � Title$!  Планшет [режим]��$	��660��$	��600�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � Icon$ /images/Icon.png��.��� ���#323232.+��� $	��Top=�$��0 20 0 0q�.+���  �� Orientation$ 
Horizontal=�$��Center=�$��	0 10 0 20q�.+��� $(��"Инвентарный номер*��   TextBlock_20#���   $	��210��$��0 0 10 0q�$��Bottom=��� -   $��InvNum��  MaterialDesignTextBox#���  
�������$	��330��$��18(� �� 	MaxLength$ 15��.  (MaterialDesignThemes.Wpf.TextFieldAssist!   CharacterCounterVisibility$ 	Collapsed=��� 
�������$��1q� �� CornerRadius$ 5��$��5q�$	��480��._��� $��0 0 0 0q�.@��� $��Основное��.���� .��� .+��� .+��� $ 
Horizontal=�$��Center=�$��	0 30 0 30q�$	��550��.+��� $��Модель*��#���   $	��210��$��0 0 10 0q�$��Bottom=��� -   $��Model��#���  
�������$	��330��$��16(�$ 50��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��0 0 0 30q�$	��550��.+��� $"��Серийный номер*��#���   $	��210��$��0 0 10 0q�$��Bottom=��� -   $��SeriaNum��#���  
�������$	��330��$��16(�$ 25��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��0 0 0 30q�$	��550��.+��� $��Пользователь��#���   $	��210��$��0 0 10 0q�$��Bottom=��� -   $
��User��  MaterialDesignComboBox#���  
�������$��16(�$	��278�� �� 
IsEditable . 	 �� MaxDropDownHeight$		 200��
 �� IsTextSearchEnabled
 .  �� -   $��EmployeeAdd��$��+��$��10 0 0 0q��� $ 
Horizontal=�$��Center=�$��0 0 0 30q�$	��550��.+��� $
��ОС��#���   $	��210��$��0 0 10 0q�$��Bottom=��� -   $��OS��#���  
�������$	��330��$��16(�$ 25��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��0 0 0 10q�$	��550��.+��� $��Комментарий��#���   $��Bottom=�$	��550��$��0 0 5 0q��� $ 
Horizontal=�$��Center=�$��0 0 0 20q�$	��550��.+��� -   $��Comment�� ! MaterialDesignOutlinedTextBox#���  $��16(�
�������$	��550�� �� TextWrapping$
 Wrap=�$	 250��$	��100�� �� AcceptsReturn . �� $ ��Комплектующие��.��� .+��� .���� .+��� $ 
Horizontal=�$��Center=�$��	0 10 0 10q�.+��� $��Диагональ*��#���   $	��200��$��0 0 10 0q��� -   $��ScreenDiagonal��#���  
�������$	��250��$��16(�$ 5��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��0 0 0 10q�.+��� $��Процессор*��#���   $	��200��$��0 0 10 0q��� -	   $��	Processor��#���  
�������$	��250��$��16(�$
 Wrap=��� $ 
Horizontal=�$��Center=�$��0 0 0 10q�.+��� $��ОЗУ*��#���   $	��200��$��0 0 10 0q��� -
   $	��RAM��#���  
�������$	��250��$��16(��� $��0 0 0 17q�.+��� $��Накопители*��#���   $	��200��$��20 0 0 0q�$��Bottom=��� -   $��Drive��#���  
�������$	��450��$	 250��$
 Wrap=�$	��100��" �� VerticalScrollBarVisibility$ Visible=�$��16(� . �� $��Иное��#���   $	��200��$��	20 10 0 0q�$��Bottom=��� -   $��Other��#���  
�������$	��450��$
 Wrap=�$	 250��$	��100��$ Visible=�$��16(� . �� -   $��
DoneButton��$��Сохранить��$��Bottom=�$��Right=�$��	0 0 20 10q�$��18(��� -   $��RecoveryButton��$��Восстановить��$��Bottom=�$��Right=�$��	0 0 20 10q�$��18(�$y�	Collapsed=��� -   $��DeleteButton��$��Удалить��   MaterialDesignOutlinedButton#���  $��Bottom=�$
��Left=�$��	20 0 0 10q�$��18(�