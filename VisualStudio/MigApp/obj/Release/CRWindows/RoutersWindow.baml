   M S B A M L   `   `   `  ���� A  =MigApp, Version=1.6.1.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MigApp.CRWindowsMigApp.CRWindows  $    MigApp.CRWindows.RoutersWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:MigApp.CRWindows     _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � Title$  Роутер [режим]��$	��670��$	��670�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � Icon$ /images/Icon.png��.��� ���#323232.+��� $	��Top=�$��0 20 0 0q�.+���  �� Orientation$ 
Horizontal=�$��Center=�$��	0 10 0 20q�$	��640��.+��� $(��"Инвентарный номер*��   TextBlock_20#���   $��Center=�$	��270��$��0 0 20 0q��� -   $��InvNum��  MaterialDesignTextBox#���  $��18(�
�������$	��350�� �� 	MaxLength$ 15��.  (MaterialDesignThemes.Wpf.TextFieldAssist!   CharacterCounterVisibility$ 	Collapsed=��� 
�������$��1q� �� CornerRadius$ 5��$��5q�$	��490��._��� $��0 0 0 0q�.@��� $��Основное��.��� .+��� .���� .+��� $ 
Horizontal=�$��Center=�$��	0 30 0 30q�$	��610��.+��� $��Имя��#���   $��Bottom=�$	��270��$��0 0 10 0q��� -   $��
RouterName��#���  
�������$	��330��$��16(�$ 50��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��0 0 0 30q�$	��610��.+��� $��Модель*��#���   $��Bottom=�$	��270��$��0 0 10 0q��� -   $��RouterModel��#���  
�������$	��330��$��16(�$ 50��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��0 0 0 30q�$	��610��.+��� $��IP��#���   $��Bottom=�$	��270��$��0 0 10 0q��� -   $��IPBorder��$	��330��._��� $ 
Horizontal=�.+��� -   $	��ip1��$	��192��$��75��
�������
������ $��Bottom=�$��16(� �� TextAlignment$ Center=��� $��10��$��.��
�������$��Bottom=�	 �� TextAlignment$	 Center=��� -   $	��ip2��$	��168��$��75��
�������
������ $��Bottom=�$��16(�$ Center=��� $��10��$��.��
�������$��Bottom=�$	 Center=��� -   $	��ip3��$��75��
�������
������ $��Bottom=�$��16(�$ Center=��� $��10��$��.��
�������$��Bottom=�$	 Center=��� -   $	��ip4��$��75��
�������
������ $��Bottom=�$��16(�$ Center=��� $ 
Horizontal=�$��Center=�$��0 0 0 30q�$	��610��.+��� $
��DHCP��#���   $��Bottom=�$	��270��$��0 0 10 0q��� -	   $��
DHCPBorder��$	��330��._��� $ 
Horizontal=�.+��� -
   $��dhcp1��$	��192��$��57��
�������
������ $��Bottom=�$��16(�$ Center=��� $��10��$��.��
�������$��Bottom=�$	 Center=��� -   $��dhcp2��$	��168��$��57��
�������
������ $��Bottom=�$��16(�$ Center=��� $��10��$��.��
�������$��Bottom=�$	 Center=��� -   $��dhcp3��$��57��
�������
������ $��Bottom=�$��16(�$ Center=��� $��10��$��.��
�������$��Bottom=�$	 Center=��� -   $��dhcp4��$��57��
�������
������ $��Bottom=�$��16(�$ Center=��� $��15��$��25��$	��—��
�������$��Bottom=�$	 Center=��� -   $��dhcp5��$��57��
�������
������ $��Bottom=�$��16(�$ Center=��� $ 
Horizontal=�$��Center=�$��0 0 0 10q�$	��610��.+��� $��Комментарий��#���   $��Bottom=�$	��610��$��0 0 5 0q��� $ 
Horizontal=�$��Center=�$��0 0 0 20q�$	��610��.+��� -   $��Comment�� ! MaterialDesignOutlinedTextBox#���  $��16(�
�������$	��610��
 �� TextWrapping$

 Wrap=�$	 250��$	��100�� �� AcceptsReturn . �� $��Пароли��.��� .+��� .+��� $ 
Horizontal=�$��Center=�$��	0 30 0 30q�$	��620��.+��� $��
Логин��#���   $��Bottom=�$	��270��$��0 0 20 0q�G� -   $��
AdminLogin�� # MaterialDesignRevealPasswordBox#���  $��16(�
�������$	��330�� G� 	MaxLength$ 25��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��0 0 0 30q�$	��620��.+��� $��Пароль��#���   $��Bottom=�$	��270��$��0 0 20 0q�G� -   $��	AdminPass��#���  $��16(�
�������$	��330��$ 25��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��0 0 0 30q�$	��620��.+��� $��Логин сети��#���   $��Bottom=�$	��270��$��0 0 20 0q��� -   $��	WiFiLogin��#���  $��16(�
�������$	��330��$ 25��$ 	Collapsed=��� $ 
Horizontal=�$��Center=�$��0 0 0 30q�$	��620��.+��� $��Пароль сети��#���   $��Bottom=�$	��270��$��0 0 20 0q��� -   $��WiFiPass��#���  $��16(�
�������$	��330��$ 25��$ 	Collapsed=��� -   $��
DoneButton��$��Сохранить��$��18(�$��Bottom=�$��Right=�$��	0 0 20 10q��� -   $��RecoveryButton��$��Восстановить��$��18(�$��Bottom=�$��Right=�$��	0 0 20 10q�$y�	Collapsed=��� -   $��DeleteButton��$��Удалить��$��18(�   MaterialDesignOutlinedButton#���  $��Bottom=�$
��Left=�$��	20 0 0 10q�