   M S B A M L   `   `   `  ����B  >MigApp, Version=2.0.0.12, Culture=neutral, PublicKeyToken=null3clr-namespace:MigApp.MVVM.ViewMigApp.MVVM.View  5   	   "    MigApp.MVVM.View.LoginWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:MigApp.MVVM.View     5   	   _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � Title$  	LoginView��5	   	   $	��300��6   $	��450��6(    � 
ResizeMode$ NoResize=�64    � WindowStyle$
 None=�6J    � WindowStartupLocation$ CenterScreen=�6]   '  !System.Windows.Shell.WindowChrome   WindowChrome 5
        5   
      CaptionHeight$ 40Q�5   	      UseAeroCaptionButtons$
 True��5   	      CornerRadius$ 20��5   	      ResizeBorderThickness$ 10q�5   	   	   GlassFrameThickness$	 0q�5   	   .��5      � ���#323232
 � RowDefinitions
 5   
   �� 5      $��30 �6   �� 5      $��1* �6   �� 5      $��60 �6   .+�5   
   � ���#252525$��0��6$   .+�5      �� -   5        $��WindowTitle��5      $��MigApp v2.0.0��6+      HeaderPanelText#���   6@   $��20(�6j   $
��15 0q�6x   �� 5      -   5        $��Custom_CloseButton��5        WindowControlButtons#���  6/   $	��5 0q�6^   $��30��6k   $��Right=�6w   .��5      �� .+�5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Close=�6.   $
��-6 0q�6;   $��20��6I   $��20��6U   5       5!      5"      � 5%   
   -   5        $��	LoginGrid��5%      $��1��6    $y�Visible=�6-   .+�5'      �� $��Center=�6   $��Center=�64   .+�5(      ��  �� Orientation$ 
Horizontal=�6   $
��0 10q�66   .+�5)      �� $��
Логин��6      LoginTextBlock#���  6-   �� 5*      -   5        $��loginBox��5*        LoginTextbox#���  6.   ��6U   �� *	UserLogin+5+      �� 5,      $ 
Horizontal=�6   $
��0 10q�66   .+�5-      �� $��Пароль��6    #���  6.   G� 5.      -   5        $��passwordBox��5.   "     LoginPasswordbox#���  65   6`   5/      �� 50      $%��Запомнить пароль��6     LoginCheckBox#���  66    d� 	IsChecked 6^   �� *IsPasswordRemembered+51      53      � 55   
   -   5        $��SettignsGrid��55      $��1��6#   ���#323232y�6E   �� *IsSettingsOn+   BooleanToVisibilityConverter �� 	Converter# [ .+�57      �� $��Center=�6   $��Center=�64   .+�59      �� $ 
Horizontal=�6   $	��0 5q�66   .+�5;      �� $ 
Horizontal=�6!   .+�5<      �� $��Сервер��6$     TextBlock_20#���  62   $��0 0 10 0q�6Y   $��80��6k   �� 5=      -   5        $��DBServer_Textbox��5=   "   $	��210��6:     SettingsTextbox#���  6F    �� 	MaxLength$ 19��6�   ��6p   �� *
DBServer+6�   5>      �� 5@      $ 
Horizontal=�6!   .+�5A      �� $��Порт��6$   #���  60   $
��10 0q�6W   $��50��6e   �� 5B      -	   5        $��DBPort_Textbox��5B   "   $��50��68   #���  6C   $ 4��6�   ��6m   �� *DBPort+6�   5C      5E      �� 5G      $ 
Horizontal=�6   $	��0 5q�66   .+�5H      �� $��База данных��6    #���  63   $	��130��6Z   $��0 0 10 0q�6f   �� 5I      -
   5        $��DBName_Textbox��5I      $	��280��64   #���  6@   $ 25��6�   ��6j   �� *DBName+6�   5J      �� 5L      $ 
Horizontal=�6   $	��0 5q�66   .+�5M      �� $��
Логин��6    #���  6-   $	��130��6T   $��0 0 10 0q�6`   �� 5N      -   5        $��DBUser_Textbox��5N      $	��280��64   #���  6@   $ 25��6�   ��6j   �� *DBUser+6�   5O      �� 5Q      $ 
Horizontal=�6   $	��0 5q�66   .+�5R      �� $��Пароль��6    #���  6.   $	��130��6U   $��0 0 10 0q�6a   G� 5S      -   5        $��DBPassword_Passwordbox��5S   "   $	��280��6@    	 SettingsPasswordbox#��� 	 6L    G� 	MaxLength$ 23��6�   5T      5V      5Y      � 5\   
   $��2��6   $��Bottom=�6   .+�5]      �� $��Вход��6    
 LoginEnterButton#��� 
 6$   ��6O   �� *LoginCommand+{�6p   �� *IsConnectionCorrect+�� 5^      $��Подтвердить��6     SettingsCommitButton#���  6+   ��6Z   �� *SettingsCommitCommand+y�6�   �� *IsSettingsOn+# [ d� 5_        LoginSettingsButton#���  6    6I   �� *IsSettingsOn+�� 5`        LoginInfoButton#���  6   5a      � 5e   
   -   5        $��LoadingBorder��5e      $��1��6$   $��2��61   y�6B   �� *	IsLoading+# [ .+�5f      �� 
���   � J� Opacity$	 0.8Q�6(   �� 5g      $��Center=�6   $��Center=�64   $��0 0 0 50q�6Q   .+�5h      �   ProgresRing#���  6   �� 5i      $-��'Проверка подключения��6   
�������$��20(�6K   5j      5k      5m      5n      