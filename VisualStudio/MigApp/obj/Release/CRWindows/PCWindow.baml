   M S B A M L   `   `   `  ���� A  =MigApp, Version=1.6.1.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MigApp.CRWindowsMigApp.CRWindows      MigApp.CRWindows.PCWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:MigApp.CRWindows     X TXceed.Wpf.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � Title$%  Компьютер [режим]��$	��680��$	��680�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � Icon$ /images/Icon.png��.��� ���#323232.+��� $	��Top=�$��0 20 0 0q�.+���  �� Orientation$ 
Horizontal=�$��Center=�$��	0 10 0 20q�$	��640��.+��� $(��"Инвентарный номер*��   TextBlock_20#���   $��Center=�$	��270��$��0 0 20 0q��� -   $��InvNum��  MaterialDesignTextBox#���  $��18(�
�������$	��350�� �� 	MaxLength$ 15��.  (MaterialDesignThemes.Wpf.TextFieldAssist!   CharacterCounterVisibility$ 	Collapsed=��� 
�������$��1q� �� CornerRadius$ 5��$��5q�$	��500��._��� $��0 0 0 0q�.@��� $��Основное��.���� .��� .+��� .+��� $ 
Horizontal=�$��Center=�$��	5 30 5 30q�$	��620��.+��� $!��Имя компьютера��#���   $��Bottom=�$	��270��$��0 0 20 0q��� -   $��PCName��#���  
�������$	��330��$��16(�$ 50��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��5 0 5 30q�$	��620��.+��� $��IP��#���   $��Bottom=�$	��270��$��0 0 20 0q��� -   $��IPBorder��$	��330��._��� $ 
Horizontal=�.+��� -   $	��ip1��$	��192��$��75��
�������
������ $��Bottom=�$��16(� �� TextAlignment$ Center=��� $��10��$��.��
�������$��Bottom=�	 �� TextAlignment$	 Center=��� -   $	��ip2��$	��168��$��75��
�������
������ $��Bottom=�$��16(�$ Center=��� $��10��$��.��
�������$��Bottom=�$	 Center=��� -   $	��ip3��$��75��
�������
������ $��Bottom=�$��16(�$ Center=��� $��10��$��.��
�������$��Bottom=�$	 Center=��� -   $	��ip4��$��75��
�������
������ $��Bottom=�$��16(�$ Center=��� $ 
Horizontal=�$��Center=�$��5 0 5 30q�$	��620��.+��� $%��ФИО пользователя��#���   $��Bottom=�$	��270��$��0 0 20 0q��� -   $
��User��  MaterialDesignComboBox#���  
�������$��16(�$	��260��
 �� 
IsEditable
 .  �� MaxDropDownHeight$	 200�� �� IsTextSearchEnabled .  �� -	   $��EmployeeAdd��$��+��$��28 0 0 0q� � ToolTip$' !Созать сотрудника���� $ 
Horizontal=�$��Center=�$��5 0 5 30q�$	��620��.+��� $3��-Логин локального админ-а��#���   $��Bottom=�$	��270��$��0 0 20 0q�G� -
   $��
AdminLogin�� # MaterialDesignRevealPasswordBox#���  $��16(�
�������$	��330�� G� 	MaxLength$ 25��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��5 0 5 30q�$	��620��.+��� $5��/Пароль локального админ-а��#���   $��Bottom=�$	��270��$��0 0 20 0q�G� -   $��	AdminPass��#���  $��16(�
�������$	��330��$ 25��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��5 0 5 30q�$	��620��.+��� $
��ОС��#���   $��Bottom=�$	��270��$��0 0 20 0q��� -   $��OS��#���  
�������$��16(�$	��330��$ 20��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��5 0 5 10q�$	��620��.+��� $��Комментарий��#���   $��Bottom=�$	��620��$��0 0 5 0q��� $ 
Horizontal=�$��Center=�$��5 0 5 20q�$	��620��.+��� -   $��Comment�� ! MaterialDesignOutlinedTextBox#���  $��16(�
�������$	��620�� �� TextWrapping$
 Wrap=�$	 250��" �� VerticalScrollBarVisibility$ Visible=�$	��100�� �� AcceptsReturn . �� $ ��Комплектующие��.��� .+��� .���� .+��� $ 
Horizontal=�$��Center=�$��	5 10 5 10q�$	��620��.+��� $(��"Материнская плата*��#���   $	��200��$��0 0 10 0q��� -   $��Motherboard��#���  $��16(�
�������$	��410��$
 Wrap=�$	 150��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��5 0 5 10q�$	��620��.+��� $��Процессор*��#���   $	��200��$��0 0 10 0q��� -   $��	Processor��#���  $��16(�
�������$	��410��$
 Wrap=�$	 150��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��5 0 5 10q�$	��620��.+��� $��ОЗУ*��#���   $	��200��$��0 0 10 0q��� -   $	��RAM��#���  $��16(�
�������$	��410��$
 Wrap=�$	 150��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��5 0 5 5q�$	��620��.+��� $��Накопители*��#���   $	��460��$��0 0 10 0q��� $ 
Horizontal=�$��Center=�$��5 0 5 10q�$	��620��.+��� -   $��Drive��#���  $��16(�
�������$	��620��$
 Wrap=�$	 250��$ Visible=�$	��100�� . �� $ 
Horizontal=�$��Center=�$��5 0 5 10q�$	��620��.+��� $��Иное��#���   $��Bottom=�$	��620��$��0 0 5 0q��� $ 
Horizontal=�$��Center=�$��5 0 5 20q�$	��620��.+��� -   $��Other��#���  $��16(�
�������$	��620��$
 Wrap=�$	 250��$ Visible=�$	��100�� . �� -   $��
DoneButton��$��Сохранить��$��18(�$��Bottom=�$��Right=�$��	0 0 20 10q��� -   $��RecoveryButton��$��Восстановить��$��18(�$��Bottom=�$��Right=�$��	0 0 20 10q�$y�	Collapsed=��� -   $��DeleteButton��$��Удалить��$��18(�   MaterialDesignOutlinedButton#���  $��Bottom=�$
��Left=�$��	20 0 0 10q�