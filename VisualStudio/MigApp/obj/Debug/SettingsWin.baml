   M S B A M L   `   `   `  ����A  =MigApp, Version=1.6.2.0, Culture=neutral, PublicKeyToken=nullclr-namespace:MigAppMigApp  5   	       MigApp.SettingsWin   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:MigApp     5   	     � Title$  Настройки��5   	   $	��350��6   $	��450��6(    � 
ResizeMode$ NoResize=�64    � WindowStyle$
 None=�6J    � WindowStartupLocation$ CenterScreen=�6]    � Icon$ images\Icon.png��6�   .��5	      � ���#323232.+�5
   
   �� $	��Top=�6   $��0 55 0 0q�6-   .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��Center=�6   $��Сервер��67   $	��120��6E      TextBlock_20#���   6Q   $��	30 0 10 0q�6x   �� 5      -   5        $��
ServerName��5      $��16(�6,   $	��250��6:    % !MaterialDesignFloatingHintTextBox#���  6F   
�������6�   5      �� 5      $ 
Horizontal=�6   $��0 35 0 0q�62   .+�5      �� $��Center=�6   $��База данных��67   $	��120��6J   #���   6V   $��	30 0 10 0q�6}   �� 5      -   5        $��DBName��5      $	��250��6(   $��16(�64   #���  6B   
�������6~   5      �� 5      $ 
Horizontal=�6   $��0 35 0 0q�62   .+�5      �� $��Center=�6   $��Пароль��67   $	��120��6E   #���   6Q   $��	30 0 10 0q�6x   G� 5      -   5        $��
DBPassword��5      $	��250��60   $��16(�6<    ) %MaterialDesignFloatingHintPasswordBox#���  6J   
�������6�   5      5      �� 5   
   -   5        $��Enter��5      $��Подтвердить��6   $��Bottom=�64   $��Center=�6O   $��20q�6l    �� 	IsDefault . 6�   �� 5   
   -   5        $��Bottom=�5   %   $
��Left=�6@   $��	15 0 0 20q�6[    & "MaterialDesignFloatingActionButton#���  6n   $��40��6�   $��40��6�   .��5      �� $��images/Manual.png��6   $��30��6/    � ToolTip$ Руководство��6;   5      �� 5   
   -   5        $��border1��5      $	��Top=�6    ���#292929._�5      � -   5        .+�5      �� $��   Настройки��6    	 Title#���  60   �� 5      -   5        $	��✖��5        CloseButton#���  6%   $	��Top=�6K   $��Right=�6c   6   5!      5"      5#      5$      