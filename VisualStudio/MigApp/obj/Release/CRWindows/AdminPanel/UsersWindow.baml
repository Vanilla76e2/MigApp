   M S B A M L   `   `   `  ���� A  =MigApp, Version=1.6.1.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:MigApp.CRWindows.AdminPanelMigApp.CRWindows.AdminPanel  -    'MigApp.CRWindows.AdminPanel.UsersWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ;local)clr-namespace:MigApp.CRWindows.AdminPanel       � Title$+  %Пользователь [режим]��$	��350��$	��400�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � Icon$ /images/Icon.png��.��� ���#323232.+��� $	��Top=�$��0 30 0 0q�.+���  �� Orientation$ 
Horizontal=�$��Center=�$��0 0 0 20q�.+��� $��
Логин��   TextBlock_20#���   $	��100��$��0 0 10 0q��� -   $��Login��  MaterialDesignTextBox#���  
�������$	��250��$��16(��� $ 
Horizontal=�$��Center=�$��0 0 0 15q�.+��� $��Сотрудник��#���   $	��100��$��0 0 10 0q��� -   $��Employee��  MaterialDesignComboBox#���  
�������$��16(�$	��250�� �� 
IsEditable .  �� MaxDropDownHeight$	 200�� �� IsTextSearchEnabled .  �� $ 
Horizontal=�$��Center=�$��0 0 0 25q�.+��� $��Роль��#���   $	��100��$��0 0 10 0q��� -   $
��Role��#���  
�������$��16(�$	��250��$	 200���� $ 
Horizontal=�$��Center=�.+��� $��Пароль��#���   $	��100��$��0 0 10 0q��� -   $��ResetPasswordButton��$��Сбросить��$	��250���� -   $��
DoneButton��$��Сохранить��$��Bottom=�$��Right=�$��20q�$��18(��� -   $��DeleteButton��$��Удалить��   MaterialDesignOutlinedButton#���  $��Bottom=�$
��Left=�$��20q�$��18(�