   M S B A M L   `   `   `  ����A  =MigApp, Version=1.6.2.0, Culture=neutral, PublicKeyToken=nullclr-namespace:MigAppMigApp  5   	       MigApp.LoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:MigApp     5   	   2gif"http://wpfanimatedgif.codeplex.com     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � Title$  MigApp��5
   	   $	��280��6   $	��450��6%    � 
ResizeMode$ NoResize=�61    � WindowStyle$
 None=�6G    � WindowStartupLocation$ CenterScreen=�6Z    � Icon$ images\Icon.png��6   .��5      � ���#323232.+�5   
   �� $	��Top=�6   $��0 55 0 0q�6-   .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��
Логин��6   $��Center=�6)   $	��100��6D      TextBlock_20#���   6P   $��	30 0 10 0q�6w   �� 5      -   5        $��	UserLogin��5      $	��250��6+    % !MaterialDesignFloatingHintTextBox#���  67   
�������$��16(�6�    �� 	MaxLength$ 30��6�   .  (MaterialDesignThemes.Wpf.TextFieldAssist!   CharacterCounterVisibility$ 	Collapsed=�6�   5      �� 5      $ 
Horizontal=�6   $��0 20 0 0q�62   .+�5      �� $��Пароль��6   $��Center=�6*   $	��100��6E   #���   6Q   $��	30 0 10 0q�6x   G� 5      -   5        $��UserPassword1��5      $	��250��63    # MaterialDesignRevealPasswordBox#���  6?   
�������$y�	Collapsed=�6�    G� 	MaxLength$ 20��6�   $��16(�6�   G� 5      -   5        $��UserPassword2��5      $	��250��63    ) %MaterialDesignFloatingHintPasswordBox#���  6?   
�������$y�	Collapsed=�6�   $ 20��6�   $��16(�6�   5      �� 5      -   5        $��RemPass��5      $%��Запомнить пароль��6&   $��	25 20 0 0q�6A   
�������$	��130��6g   $
��Left=�6s   5      �� 5   
   -   5        $��Enter1��5      $��Вход��6   $��Bottom=�6.   $��Center=�6I   $��20q�6f   $y�	Collapsed=�6�   	 �� 	IsDefault	 . 6�   �� 5   
   -   5        $��Enter2��5      $��Вход��6   $��Bottom=�6.   $��Center=�6I   $��20q�6f   $y�	Collapsed=�6�   	 . 6�   �� 5   
   -   5        $��⚙️��5      $��Bottom=�64   $��Right=�6O     MaterialDesignFlatButton#���  6k   $��10q�6�   �� 5   
   -	   5        $��Refresh��5   9   $	��↺��6   $��Bottom=�6H   $��Right=�6c   #���  6   $��	0 0 70 10q�6�   $��18(�6�   $y�	Collapsed=�6�   �� 5   
   -
   5        $��border1��5      $	��Top=�6    ���#292929._�5      � -   5        .+�5      �� -   5        $��CustomTitle��5      $��  MigApp [v.]��60    	 Title#���  6E   �� 5       -   5        $	��✖��5         CloseButton#���  6%   $	��Top=�6K   $��Right=�6c   6   5"      5#      5$      5%      