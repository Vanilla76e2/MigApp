   M S B A M L   `   `   `  ���� 
  MigApp&     MigApp.CRWindows.FurnitureWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � Title$  Мебель [режим]��$	��550��$	��620�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � Icon$ /images/Icon.png��.��� ���#323232.+��� $	��Top=�$��0 20 0 0q�.+���  �� Orientation$ 
Horizontal=�$��Center=�$��	0 10 0 20q�.+��� $(��"Инвентарный номер*��   TextBlock_20#���   $	��270��$��0 0 10 0q�$��Bottom=��� -   $��InvNum��  MaterialDesignTextBox#���  
�������$	��270��$��18(� �� 	MaxLength$ 15��.  (MaterialDesignThemes.Wpf.TextFieldAssist!   CharacterCounterVisibility$ 	Collapsed=��� 
�������$��1q� �� CornerRadius$ 5��$��	10 0 10 0q�._��� .+��� $ 
Horizontal=�$��Center=�$��	0 20 0 20q�.+��� $��Тип мебели*��#���   $	��200��$��0 0 10 0q�$��Bottom=��� -   $
��Type��  MaterialDesignComboBox#���  
�������$	��270��$��16(��� -   $��TypeAdd��$��+��$��28 0 0 0q� � ToolTip$' !Созать сотрудника���� $ 
Horizontal=�$��Center=�$��	0 10 0 20q�.+��� $��Наименование*��#���   $	��200��$��0 0 10 0q�$��Bottom=��� -   $��FurnitureName��#���  
�������$	��340��$��18(��� $ 
Horizontal=�$��Center=�$��	0 10 0 20q�.+��� $��Расположение*��#���   $	��200��$��0 0 10 0q�$��Bottom=��� -   $
��Room��#���  
�������$	��340��$��18(��� $ 
Horizontal=�$��Center=�$��	0 10 0 10q�$	��550��.+��� $��Комментарий��#���   $��Bottom=�$	��560��$��0 0 5 0q��� $ 
Horizontal=�$��Center=�$��0 0 0 20q�$	��565��.+��� -   $��Comment�� ! MaterialDesignOutlinedTextBox#���  $��16(�
�������	 �� TextWrapping$
	 Wrap=�$	��565��$	 250��"
 �� VerticalScrollBarVisibility$
 Visible=�$	��100�� �� AcceptsReturn . �� -   $��
DoneButton��$��Сохранить��$��Bottom=�$��Right=�$��	0 0 20 10q��� -   $��RecoveryButton��$��Восстановить��$��Bottom=�$��Right=�$��	0 0 20 10q�$y�	Collapsed=��� -	   $��DeleteButton��$��Удалить��   MaterialDesignOutlinedButton#���  $��Bottom=�$
��Left=�$��	20 0 0 10q�