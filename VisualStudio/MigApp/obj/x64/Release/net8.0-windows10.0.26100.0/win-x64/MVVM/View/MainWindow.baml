   M S B A M L   `   `   `  ���� B  >MigApp, Version=2.0.0.12, Culture=neutral, PublicKeyToken=null3clr-namespace:MigApp.MVVM.ViewMigApp.MVVM.View      MigApp.MVVM.View.MainView   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:MigApp.MVVM.View     _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  $	��650��$	��920��  � WindowStartupLocation$  CenterScreen=� � 
ResizeMode$ 	CanResize=� � AllowsTransparency$
 True�� � WindowStyle$
 None=� � Title ��  �� Path$ WindowTitle�'  !System.Windows.Shell.WindowChrome   WindowChrome      CaptionHeight$ 50Q�   UseAeroCaptionButtons$
 True��	   CornerRadius$	 20��
   ResizeBorderThickness$
 10q�   GlassFrameThickness$ 0q�.��� 
���   �.+��� ._��  � ColumnDefinitions �� $��60 ��� $��1* � � RowDefinitions �� $��50 ��� $��1* ��� $��15 �.+��� -   $��1��$��1��$��2������ *Navigation.CurrentView+� -   $��NavigationPanel��
������ $��0��$��1��$��1��$
��Left=�.+�� -   $��MainMenu��$��0��$
��Left=�.+���    NavigationPanel#���   ._���  �� TabStripPlacement$
 Left=�.@��� ��'  !MaterialDesignThemes.Wpf.PackIcon     Kind$ AccountGroup=�  LeftPanelICons#���   � ToolTip$ Сотрудники��.��� ���#404040.+��� .+��� -   $��Navigation_Employees��  NavigationButton#��[ ���� *NavigateToEmployeesCommand+.����  �� Orientation$ 
Horizontal=�.+�  $ 
AccountTie=�  NavigationIcons#���  �� $��Сотрудники��  TextBlock_20#���  $��30 0 0 0q�$��Center=��� -   $��Navigation_Department��#���  ���� *NavigateToDepartmentCommand+.���� $ 
Horizontal=�.+�  $
 Work=�#���  �� $��Отделы��#���  $��30 0 0 0q�$��Center=��� $��0 5 0 0q���  $ MonitorCellphone=�#���  $ Техника��.��� ���#404040.+��� .+��� #���  ���� *NavigateToComputersCommand+.���� $ 
Horizontal=�.+�  $ DesktopTower=�#���  �� $��Компьютеры��  NavigationTextBlock#���  �� #���  ���� *NavigateToLaptopsCommand+.���� $ 
Horizontal=�.+�  $ Computer=�#���  �� $��Ноутбуки��#���  �� #���  ���� *NavigateToTabletsCommand+.���� $ 
Horizontal=�.+�  $ Tablet=�#���  �� $��Планшеты��#���  �� #���  ���� *NavigateToOrgtechCommand+.���� $ 
Horizontal=�.+�  $ Printer=�#���  �� $��Оргтехника��#���  �� #���  $y�	Collapsed=����� *NavigateToMonitorsCommand+.���� $ 
Horizontal=�.+�  $ Monitor=�#���  �� $��Мониторы��#���  �� #���  ���� *NavigateToRoutersCommand+.���� $ 
Horizontal=�.+�  $ RouterWireless=�#���  �� $��Роутеры��#���  �� #���  ���� *NavigateToSwitchesCommand+.���� $ 
Horizontal=�.+�  $ RouterNetwork=�#���  �� $��Коммутаторы��#���  �� #���  ���� *NavigateToCCTVCommand+.���� $ 
Horizontal=�.+�  $
 Cctv=�#���  �� $$��Видеонаблюдение��#���  �� $��0 5 0 0q���  $ Wardrobe=�#���  $ Мебель��.��� ���#404040.+��� .+��� #���  ���� *NavigateToFurnitureCommand+.���� $ 
Horizontal=�.+�  $ Dresser=�#���  �� $��Мебель��#���  �� #���  ���� * NavigateToFurnitureTypeCommand+.���� $ 
Horizontal=�.+�  $ TagMultiple=�#���  �� $��Виды мебели��#���  � -   $��	AdminMenu��$��0��$
��Left=�.+��� #���   ._��� $��Disabled=�.+��� #���  ���� *NavigateToUsersCommand+.���� $ 
Horizontal=�.+�  $ AccountEdit=�$��40��$��40��$��10 0 0 0q����#FF9700�� $��Пользователи��#���  $��30 0 0 0q�$��Center=��� #���  ���� *NavigateToRolesCommand+.���� $ 
Horizontal=�.+�  $ AccountWrench=�$��10 0 0 0q�$��40��$��40�����#FF9700�� $��Роли��#���  $��30 0 0 0q�$��Center=��� #���  ���� *NavigateToLogsCommand+.���� $ 
Horizontal=�.+�  $ History=�$��10 0 0 0q�$��40��$��40�����#FF9700�� $!��Журнал событий��#���  $��30 0 0 0q�$��Center=��� #���  ���� *NavigateToIPCommand+.���� $ 
Horizontal=�.+�  $ 	IpNetwork=�$��10 0 0 0q�$��40��$��40�����#FF9700�� $��IP адреса��#���  $��30 0 0 0q�$��Center=�� -   $��ArchiveMenu��$��0��$
��Left=�.+��� #���   � -   $��	LeftPanel��$��0��$��1��$��2�����#252525.+��� -	   $��LeftPanel_TopButtons��$	��Top=�$��Center=�.+�d� -
   $��
MenuButton��  LeftPanel_MainMenu#��[ $ Меню�� � Triggers :�  :� RoutedEvent$ ToggleButton.Checked��.[��� .���� .��W�  �� 
TargetName$ NavigationPanel�� �� TargetProperty$ (Grid.Width)� W� From 0	 W� To 500 g� Duration$ 0:0:0.2D� g� AutoReverse$ False���� .���� .��W� $ MainMenu��$ (Grid.Width)� 0 500$ 0:0:0.2D�$ False��:� $ ToggleButton.Unchecked��.[��� .���� .��W� $ NavigationPanel��$ (Grid.Width)� 500 0$ 0:0:0.2D�$ False���� .���� .��W� $ MainMenu��$ (Grid.Width)� 500 0$ 0:0:0.2D�$ False���� -   $��FavouriteButton�� & "MaterialDesignFloatingActionButton#��[ 
������ 
������ ���#FF9700$��50��$��50��$ Избранное������ *NavigateToFavouriteCommand+.���� .+�  $
 Star=�$��45��$��45��d� -   $��AdminButton��  LeftPanel_AdminMenu#��[ $( "Администрирование�� :� $ ToggleButton.Checked��.[��� .���� .��W� $ NavigationPanel��$ (Grid.Width)� 0 500$ 0:0:0.2D�$ False���� .���� .��W� $ 	AdminMenu��$ (Grid.Width)� 0 500$ 0:0:0.2D�$ False��:� $ ToggleButton.Unchecked��.[��� .���� .��W� $ NavigationPanel��$ (Grid.Width)� 500 0$ 0:0:0.2D�$ False���� .���� .��W� $ 	AdminMenu��$ (Grid.Width)� 500 0$ 0:0:0.2D�$ False��d� -   $��ArchiveButton�� 	 LeftPanel_ArchiveMenu#��[	 $ 
Архив�� :� $ ToggleButton.Checked��.[��� .���� .��W� $ NavigationPanel��$ (Grid.Width)� 0 500$ 0:0:0.2D�$ False���� .���� .��W� $ ArchiveMenu��$ (Grid.Width)� 0 500$ 0:0:0.2D�$ False��:� $ ToggleButton.Unchecked��.[��� .���� .��W� $ NavigationPanel��$ (Grid.Width)� 500 0$ 0:0:0.2D�$ False���� .���� .��W� $ ArchiveMenu��$ (Grid.Width)� 500 0$ 0:0:0.2D�$ False���� -   $��LeftPanel_BottomButtons��$��Bottom=�$��0 0 0 15q�.+��� $��Center=� 
 LeftPanelBottomButtons#��� 
 $! Руководство | F1��.���� .+�  $ AboutOutline=�$��40��$��40��$��-20 -20q��� $��Center=�#��� 
 $* $Обновить таблицы | F5��.���� .+�  $ Reload=�$��40��$��40��$��0 -20q��� $��Center=�#��� 
 $ 
Выход������ *LogOutCommand+.��  $ Logout=�$��40��$��40��$��1 -10q�� $��3��$��0�����#252525.+��� -   $��WindowTitle��  HeaderPanelText#���  $
��15 0q����� $ WindowTitle��� -   $��UserLoginText��#���  $��Right=�$��	0 0 130 0q����� $ CurrentUser��� $ 
Horizontal=�$��Right=�.+��� -   $��Custom_MinimizeButton��  WindowControlButtons#���  .���� .+�  $ Minimize=�$
��-5 0q��� -   $��Custom_MaximizeButton��#���  .���� .+�  $ Maximize=�$
��-5 0q��� -   $��Custom_CloseButton��#���  .���� .+�  $ Close=�$
��-5 0q��� -   $��	Blindfall��
���   � J� Opacity$
 0.35Q�$y�	Collapsed=�