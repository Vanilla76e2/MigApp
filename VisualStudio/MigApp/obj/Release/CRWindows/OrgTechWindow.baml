   M S B A M L   `   `   `  ���� A  =MigApp, Version=1.6.1.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MigApp.CRWindowsMigApp.CRWindows  $    MigApp.CRWindows.OrgTechWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:MigApp.CRWindows     _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � Title$'  !Оргтехника [режим]��$	��660��$	��620�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � Icon$ /images/Icon.png��.��� ���#323232.+��� $	��Top=�$��0 20 0 0q�.+���  �� Orientation$ 
Horizontal=�$��Center=�$��	0 10 0 20q�.+��� $(��"Инвентарный номер*��   TextBlock_20#���   $	��270��$��0 0 10 0q�$��Bottom=��� -   $��InvNum��  MaterialDesignTextBox#���  
�������$	��270��$��18(� �� 	MaxLength$ 15��.  (MaterialDesignThemes.Wpf.TextFieldAssist!   CharacterCounterVisibility$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��	0 10 0 20q�.+��� $��Тип техники*��#���   $	��270��$��0 0 10 0q�$��Bottom=��� -   $
��Type��  MaterialDesignComboBox#���  
�������$	��270��$��16(�.@��� $��МФУ���� $��Принтер���� $��Сканер���� 
�������$��1q� �� CornerRadius$ 5��$��5q�$	��410��._��� $��0 0 0 0q�.@��� $��Основное��.��� .+��� .+��� $ 
Horizontal=�$��Center=�$��	0 15 0 20q�$	��550��.+��� $��Модель*��#���   $	��210��$��0 0 10 0q�$��Bottom=��� -   $��Model��#���  
�������$	��330��$��16(�$ 50��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��0 0 0 20q�$	��550��.+��� $"��Серийный номер*��#���   $	��210��$��0 0 10 0q�$��Bottom=��� -   $��SeriaNum��#���  
�������$	��330��$��16(�$ 25��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��0 0 0 20q�$	��550��.+��� $��Сетевое имя��#���   $	��210��$��0 0 10 0q�$��Bottom=��� -   $��OTName��#���  
�������$	��330��$��16(�$ 50��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��0 0 0 10q�$	��550��.+��� $��Комментарий��#���   $��Bottom=�$	��560��$��0 0 5 0q��� $ 
Horizontal=�$��Center=�$��0 0 0 20q�$	��565��.+��� -   $��Comment�� ! MaterialDesignOutlinedTextBox#���  $��16(�
������� �� TextWrapping$
 Wrap=�$	��565��$	 250��"	 �� VerticalScrollBarVisibility$	 Visible=�$	��100��
 �� AcceptsReturn
 . �� $1��+Техническая информация��.��� .+��� .+��� 
�������$��1q�$ 5��$��10 10 10 20q�._�� .+��� .+��� $ 
Horizontal=�$��5q�.+��� $��Подключение:��  TextBlock_14#���  $��Center=�	� -   $��Ethernet��$��Ethernet��
�������$��20 0 0 0q�	� -   $	��USB��$	��USB��
�������$��20 0 0 0q��� -	   $��USBPanel��$y�	Collapsed=�.+��� $ 
Horizontal=�$��Center=�$��0 5 0 20q�$	��550��.+��� $��Пользователь��#���   $	��210��$��0 0 10 0q�$��Bottom=��� -
   $
��User��#���  
�������$��16(�$	��278�� �� 
IsEditable .  �� MaxDropDownHeight$	 200�� �� IsTextSearchEnabled .  �� -   $��EmployeeAdd��$��+��$��10 0 0 0q��� $ 
Horizontal=�$��Center=�$��0 0 0 20q�$	��550��.+��� $��Компьютер��#���   $	��210��$��0 0 10 0q�$��Bottom=��� -   $��PC��#���  
�������$��16(�$	��278��$	 200��{�.   � ContextMenu �� .@�m� -   $��Очистить���� -   $��PCAdd��$��+��$��10 0 0 0q��� -   $��EthernetPanel��.+��� $ 
Horizontal=�$��Center=�$��0 5 0 20q�$	��550��.+��� $��Кабинет��#���   $	��210��$��0 0 10 0q�$��Bottom=��� -   $
��Room��#���  
�������$��16(�$	��330��$ 3��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��0 0 0 20q�$	��550��.+��� $��IP��#���   $	��210��$��0 0 10 0q�$��Center=��� -   $��IPBorder��$	��330��._��� $ 
Horizontal=�.+��� -   $	��ip1��$	��192��$��75��
�������
������ $��Bottom=�$��16(� �� TextAlignment$ Center=��� $��10��$��.��
�������$��Bottom=� �� TextAlignment$ Center=��� -   $	��ip2��$	��168��$��75��
�������
������ $��Bottom=�$��16(�$ Center=��� $��10��$��.��
�������$��Bottom=�$ Center=��� -   $	��ip3��$��75��
�������
������ $��Bottom=�$��16(�$ Center=��� $��10��$��.��
�������$��Bottom=�$ Center=��� -   $	��ip4��$��75��
�������
������ $��Bottom=�$��16(�$ Center=��� -   $	��Tip��$��?��$	��Top=�$��Right=�
�������$��0 0 5 0q�#���  �� $ 
Horizontal=�$��Center=�$��0 0 0 20q�$	��550��.+��� $��
Логин��#���   $	��210��$��0 0 10 0q�$��Center=�G� -   $��Login�� # MaterialDesignRevealPasswordBox#���  
�������$	��330��$��16(� G� 	MaxLength$ 25��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��0 0 0 20q�$	��550��.+��� $��Пароль��#���   $	��210��$��0 0 10 0q�$��Center=�G� -   $��Password��#���  
�������$	��330��$��16(�$ 25��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��0 0 0 20q�$	��550��.+��� $&�� Модель картриджа*��#���   $	��210��$��0 0 10 0q�$��Center=��� -   $��Cartrige��#���  
�������$	��330��$��16(�$ 50��$ 	Collapsed=��� -   $��
DoneButton��$��Сохранить��$��Bottom=�$��Right=�$��	0 0 20 10q��� -   $��RecoveryButton��$��Восстановить��$��Bottom=�$��Right=�$��	0 0 20 10q�$y�	Collapsed=��� -   $��DeleteButton��$��Удалить��   MaterialDesignOutlinedButton#���  $��Bottom=�$
��Left=�$��	20 0 0 10q�