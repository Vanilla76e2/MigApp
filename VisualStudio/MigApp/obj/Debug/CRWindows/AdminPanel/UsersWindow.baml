   M S B A M L   `   `   `  ����A  =MigApp, Version=1.6.2.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:MigApp.CRWindows.AdminPanelMigApp.CRWindows.AdminPanel  5   	   -    'MigApp.CRWindows.AdminPanel.UsersWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:MigApp.CRWindows.AdminPanel     5   	     � Title$+  %Пользователь [режим]��5   	   $	��350��6&   $	��400��63    � 
ResizeMode$ NoResize=�6?    � WindowStartupLocation$ CenterScreen=�6U    � Icon$ /images/Icon.png��6z   .��5	      � ���#323232.+�5
   
   �� $	��Top=�6   $��0 30 0 0q�6-   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   $��0 0 0 20q�6O   .+�5      �� $��
Логин��6      TextBlock_20#���   6)   $	��100��6P   $��0 0 10 0q�6_   �� 5      -   5        $��Login��5        MaterialDesignTextBox#���  6'   
�������$	��250��6j   $��16(�6v   5      �� 5      $ 
Horizontal=�6   $��Center=�62   $��0 0 0 15q�6O   .+�5      �� $��Сотрудник��6   #���   6-   $	��100��6T   $��0 0 10 0q�6c   �� 5      -   5        $��Employee��5        MaterialDesignComboBox#���  6+   
�������$��16(�6o   $	��250��6}    �� 
IsEditable .  �� MaxDropDownHeight$	 200��6�    �� IsTextSearchEnabled .  6�   5      �� 5      $ 
Horizontal=�6   $��Center=�62   $��0 0 0 25q�6O   .+�5      �� $��Роль��6   #���   6(   $	��100��6O   $��0 0 10 0q�6^   �� 5      -   5        $
��Role��5      #���  6'   
�������$��16(�6k   $	��250��6y   $	 200��6�   5      �� 5      $ 
Horizontal=�6   $��Center=�62   .+�5      �� $��Пароль��6   #���   6*   $	��100��6Q   $��0 0 10 0q�6`   �� 5      -   5        $��ResetPasswordButton��5      $��Сбросить��64   $	��250��6]   5      5      �� 5   
   -   5        $��
DoneButton��5   #   $��Сохранить��65   $��Bottom=�6I   $��Right=�6d   $��20q�6�   $��18(�6�   �� 5   
   -   5        $��DeleteButton��5   %   $��Удалить��69      MaterialDesignOutlinedButton#���  6K   $��Bottom=�6�   $
��Left=�6�   $��20q�6�   $��18(�6�   5      5      