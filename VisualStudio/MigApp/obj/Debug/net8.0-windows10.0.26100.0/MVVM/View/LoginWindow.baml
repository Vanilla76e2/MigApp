   M S B A M L   `   `   `  ����B  >MigApp, Version=2.0.0.12, Culture=neutral, PublicKeyToken=null3clr-namespace:MigApp.MVVM.ViewMigApp.MVVM.View  5   	   Q-clr-namespace:MigApp.CustomElements.BehaviorsMigApp.CustomElements.Behaviors  5   	   "    MigApp.MVVM.View.LoginWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:MigApp.MVVM.View     5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5   	   =	behaviors-clr-namespace:MigApp.CustomElements.Behaviors  5   	   c _MaterialDesignThemes.MahApps, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a_ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5	   	   $	��300��5   '   $	��450��64     � 
ResizeMode$  NoResize=�6@    � WindowStartupLocation$ CenterScreen=�6V      LoginWindowStyle#���   5   	    � Title 5   	   �� *WindowTitle+.��5      � -   5         �Name$ RootGrid��5      ���#323232 � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��60 �6   .+�5   
   � -   5        $��	LoginGrid��5      $��0��6    .+�5      �� $��Center=�6   $��Center=�64   .+�5      ��  �� Orientation$ 
Horizontal=�6   $
��0 10q�66   .+�5      �� $��
Логин��6     
 Text20#���  6-   �� 5      -   5        $��loginBox��5        LoginTextBox#���  6.   ��6U   �� *
Username+5      �� 5      $ 
Horizontal=�6   $
��0 10q�66   .+�5      �� $��Пароль��6      LoginTextBlock#���  6.   G� 5      -   5         G�Name$ passwordBox��5   "     LoginPasswordBox#���  67   *  $Microsoft.Xaml.Behaviors.Interaction   	Behaviors 5      9   3MigApp.CustomElements.Behaviors.PasswordBoxBehavior  5         Password 6<   �� *UserPassword+	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=�5!      5"      �� 5#      $%��Запомнить пароль��6     LoginCheckBox#���  66    d� 	IsChecked 6^   �� *IsPasswordRemembered+5$      5%      � 5'   
   -   5        $��SettignsGrid��5'      $��0��6#   ���#323232y�6E   �� *IsSettingsOn+   BooleanToVisibilityConverter �� 	Converter# [ .+�5)      ��   SettingsStackPanel#���  6   .+�5+      ��   FormRowPanel#���  6   .+�5-      �� $ 
Horizontal=�6!   .+�5.      �� $��Сервер��6$    	 	FormLabel#��� 	 62   $��0 0 10 0q�6V   $��80��6h   �� 5/      -   5        $��DBServer_Textbox��5/   "   $	��210��6:    
 SettingsTextbox#��� 
 6F    �� 	MaxLength$ 19��6�   ��6p   �� *
DBServer+6�   50      �� 52      $ 
Horizontal=�6!   .+�53      �� $��Порт��6$   #��� 	 60   $
��10 0q�6T   $��50��6b   �� 54      -   5        $��DBPort_Textbox��54   "   $��50��68   #��� 
 6C   $ 4��6�   ��6m   �� *DBPort+6�   55      57      �� 59      #���  6   .+�5:      �� $��База данных��6    #��� 	 63   $	��130��6W   $��0 0 10 0q�6c   �� 5;      -   5        $��DBName_Textbox��5;      $	��280��64   #��� 
 6@   $ 25��6�   ��6j   �� *DBName+6�   5<      �� 5>      #���  6   .+�5?      �� $��
Логин��6    #��� 	 6-   $	��130��6Q   $��0 0 10 0q�6]   �� 5@      -	   5        $��DBUser_Textbox��5@      $	��280��64   #��� 
 6@   $ 25��6�   ��6j   �� *DBUser+6�   5A      �� 5C      #���  6   .+�5D      �� $��Пароль��6    #��� 	 6.   $	��130��6R   $��0 0 10 0q�6^   G� 5E      -
   5        $��DBPassword_Passwordbox��5E   "   $	��280��6@     SettingsPasswordbox#���  6L    G� 	MaxLength$ 23��6z    5F        5G       6<   �� *
DBPassword+$	 TwoWay=�$
 PropertyChanged=�5I      5J      5L      5O      � 5R   
   $��1��6   $��Bottom=�6   .+�5S      �� -   5         ��Name$ LoginButton��5S      $��Вход��6*     LoginEnterButton#���  69    �� 	IsDefault$
 True��6�   ��6d   �� *LoginCommand+{�6�   �� *IsConnectionCorrect+6�   �� 5T      -   5        $ CommitSettingsButton��5T      $��Подтвердить��63     SettingsCommitButton#���  6I   ��6x   �� *CommitSettingsCommand+y�6�   �� *IsSettingsOn+# [ d� 5U      -   5         d�Name$ SettingsToggle��5U        LoginSettingsButton#���  63    6a   �� *IsSettingsOn+�� 5V      -   5          LoginInfoButton#���  5V      6?   5W      � 5[   
   -   5        $��LoadingBorder��5[      $��0��6$   $��2��61   $y�	Collapsed=�6B   .+�5\      �� 
���   � J� Opacity$	 0.8Q�6(   �� 5]      $��Center=�6   $��Center=�64   $��0 0 0 50q�6Q   .+�5^      �   ProgresRing#���  6   �� 5_      $-��'Проверка подключения��6   
�������$��20(�6K   5`      5a      '  !MaterialDesignThemes.Wpf.Snackbar  5d   
   -   5          Name$ MainSnackbar��5d   "   $��Center=�5f   #   $	��Top=�5g   #   $��14(�5h   #    K� ZIndex$ 99��5i   #      MessageQueue 5e   #   �� *SnackbarMessageQueue+��5j      �� 5k       �� 
TargetType"  6    �� BasedOn# [B 6=   .�5l      ��  �� Property � �� �� Value �	#FF42424263   �� 5m       � ��
 �����63   �� 5n       � ��$
 12 6q�60   �� 5o       �@J�Effect 5p      3  -System.Windows.Media.Effects.DropShadowEffect  5q         
BlurRadius$ 10Q�6/      ShadowDepth$ 2Q�6?      Opacity$	 0.3Q�6O   5s      5t      5v      5w      5y      