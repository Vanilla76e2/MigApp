   M S B A M L   `   `   `  ����
  MigApp&     MigApp.CRWindows.FurnitureWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Мебель [режим]��5      $	��550��6   $	��620��6)    � 
ResizeMode$ NoResize=�65    � WindowStartupLocation$ CenterScreen=�6K    � Icon$ /images/Icon.png��6p   .��5	      � ���#323232.+�5   
   �� $	��Top=�6   $��0 20 0 0q�6-   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   $��	0 10 0 20q�6O   .+�5      �� $(��"Инвентарный номер*��6      TextBlock_20#���   66   $	��270��6]   $��0 0 10 0q�6l   $��Bottom=�6~   �� 5      -   5        $��InvNum��5        MaterialDesignTextBox#���  6(   
�������$	��270��6k   $��18(�6w    �� 	MaxLength$ 15��6�   .  (MaterialDesignThemes.Wpf.TextFieldAssist!   CharacterCounterVisibility$ 	Collapsed=�6�   6�   5      �� 5      
�������$��1q�6-    �� CornerRadius$ 5��6A   $��	10 0 10 0q�6R   ._�5      �� .+�5      �� $ 
Horizontal=�6!   $��Center=�6:   $��	0 20 0 20q�6W   .+�5      �� $��Тип мебели*��6$   #���   67   $	��200��6^   $��0 0 10 0q�6j   $��Bottom=�6|   �� 5      -   5        $
��Type��5   #     MaterialDesignComboBox#���  6/   
�������$	��270��6s   $��16(�6   �� 5      -   5        $��TypeAdd��5   !   $��+��6F   $��28 0 0 0q�6R    � ToolTip$' !Созать сотрудника��6d   5      �� 5      $ 
Horizontal=�6!   $��Center=�6:   $��	0 10 0 20q�6W   .+�5      �� $��Наименование*��6$   #���   69   $	��200��6`   $��0 0 10 0q�6l   $��Bottom=�6~   �� 5      -   5        $��FurnitureName��5   "   #���  67   
�������$	��340��6z   $��18(�6�   5      �� 5      $ 
Horizontal=�6!   $��Center=�6:   $��	0 10 0 20q�6W   .+�5      �� $��Расположение*��6$   #���   69   $	��200��6`   $��0 0 10 0q�6l   $��Bottom=�6~   �� 5      -   5        $
��Room��5   "   #���  6.   
�������$	��340��6q   $��18(�6}   5       �� 5!      $ 
Horizontal=�6!   $��Center=�6:   $��	0 10 0 10q�6W   $	��550��6j   .+�5"      �� $��Комментарий��6$   #���   67   $��Bottom=�6^   $	��560��6y   $��0 0 5 0q�6�   5#      �� 5$      $ 
Horizontal=�6!   $��Center=�6:   $��0 0 0 20q�6W   $	��565��6i   .+�5%      �� -   5        $��Comment��5%   "    ! MaterialDesignOutlinedTextBox#���  61   $��16(�6i   
�������	 �� TextWrapping$
	 Wrap=�6�   $	��565��6�   $	 250��6�   "
 �� VerticalScrollBarVisibility$
 Visible=�6�   $	��100��6�    �� AcceptsReturn . 6�   5&      5'      5(      5)      �� 5*   
   -   5        $��
DoneButton��5*   #   $��Сохранить��65   $��Bottom=�6I   $��Right=�6d   $��	0 0 20 10q�6�   �� 5+   
   -   5        $��RecoveryButton��5+   '   $��Восстановить��6=   $��Bottom=�6T   $��Right=�6o   $��	0 0 20 10q�6�   $y�	Collapsed=�6�   �� 5,   
   -	   5        $��DeleteButton��5,   %   $��Удалить��69      MaterialDesignOutlinedButton#���  6K   $��Bottom=�6�   $
��Left=�6�   $��	20 0 0 10q�6�   5-      5.      