   M S B A M L   `   `   `  ���� B  >MigApp, Version=2.0.0.12, Culture=neutral, PublicKeyToken=null3clr-namespace:MigApp.MVVM.ViewMigApp.MVVM.View  "    MigApp.MVVM.View.LoginWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:MigApp.MVVM.View     _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -     � Title$  	LoginView��$	��300��$	��450�� � 
ResizeMode$ NoResize=� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=�'  !System.Windows.Shell.WindowChrome   WindowChrome      CaptionHeight$ 40Q�   UseAeroCaptionButtons$
 True��   CornerRadius$ 20��   ResizeBorderThickness$ 10q�	   GlassFrameThickness$	 0q�.��� ���#323232
 � RowDefinitions
 �� $��30 ��� $��1* ��� $��60 �.+�� ���#252525$��0��.+��� -   $��WindowTitle��$��MigApp v2.0.0��   HeaderPanelText#���   $��20(�$
��15 0q��� -   $��Custom_CloseButton��  WindowControlButtons#���  $	��5 0q�$��30��$��Right=�.���� .+�'  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Close=�$
��-6 0q�$��20��$��20��� -   $��	LoginGrid��$��1��$y�Visible=�.+��� $��Center=�$��Center=�.+���  �� Orientation$ 
Horizontal=�$
��0 10q�.+��� $��
Логин��  LoginTextBlock#���  �� -   $��loginBox��  LoginTextbox#���  ���� *	UserLogin+�� $ 
Horizontal=�$
��0 10q�.+��� $��Пароль��#���  G� -   $��passwordBox��  LoginPasswordbox#���  �� $%��Запомнить пароль��  LoginCheckBox#���   d� 	IsChecked �� *IsPasswordRemembered+� -   $��SettignsGrid��$��1�����#323232y��� *IsSettingsOn+   BooleanToVisibilityConverter �� 	Converter# [ .+��� $��Center=�$��Center=�.+��� $ 
Horizontal=�$	��0 5q�.+��� $ 
Horizontal=�.+��� $��Сервер��  TextBlock_20#���  $��0 0 10 0q�$��80���� -   $��DBServer_Textbox��$	��210��  SettingsTextbox#���   �� 	MaxLength$ 19������ *
DBServer+�� $ 
Horizontal=�.+��� $��Порт��#���  $
��10 0q�$��50���� -	   $��DBPort_Textbox��$��50��#���  $ 4������ *DBPort+�� $ 
Horizontal=�$	��0 5q�.+��� $��База данных��#���  $	��130��$��0 0 10 0q��� -
   $��DBName_Textbox��$	��280��#���  $ 25������ *DBName+�� $ 
Horizontal=�$	��0 5q�.+��� $��
Логин��#���  $	��130��$��0 0 10 0q��� -   $��DBUser_Textbox��$	��280��#���  $ 25������ *DBUser+�� $ 
Horizontal=�$	��0 5q�.+��� $��Пароль��#���  $	��130��$��0 0 10 0q�G� -   $��DBPassword_Passwordbox��$	��280�� 	 SettingsPasswordbox#��� 	  G� 	MaxLength$ 23��� $��2��$��Bottom=�.+��� $��Вход�� 
 LoginEnterButton#��� 
 ���� *LoginCommand+{��� *IsConnectionCorrect+�� $��Подтвердить��  SettingsCommitButton#���  ���� *SettingsCommitCommand+y��� *IsSettingsOn+# [ d�   LoginSettingsButton#���   �� *IsSettingsOn+��   LoginInfoButton#���  � -   $��LoadingBorder��$��1��$��2��y��� *	IsLoading+# [ .+��� 
���   � J� Opacity$	 0.8Q��� $��Center=�$��Center=�$��0 0 0 50q�.+��   ProgresRing#���  �� $-��'Проверка подключения��
�������$��20(�