   M S B A M L   `   `   `  ����B  >MigApp, Version=2.0.0.12, Culture=neutral, PublicKeyToken=null3clr-namespace:MigApp.MVVM.ViewMigApp.MVVM.View  5   	   !    MigApp.MVVM.View.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:MigApp.MVVM.View     5   	   c _MaterialDesignThemes.MahApps, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a_ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5   	   $	��650��5
   	   $	��920��6     � WindowStartupLocation$  CenterScreen=�5   	    � 
ResizeMode$ 	CanResize=�5   	    � AllowsTransparency$ False��5   	    � WindowStyle$
 None=�5   	      CustomWindowStyle#���   5   	    � Title 5	   	   ��  �� Path$ WindowTitle�.��5      � 
���   �.+�5   
   �  � ColumnDefinitions 5      �� 5      $��60 �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $��50 �6    �� 5      $��* �6    �� 5      $��15 �6    .+�5      � $��0��6   $��2��6      DarkBackgroundBrush#���  64   .+�5      ��   HeaderPanelText#���  5       $��Center=�5!      $
��16,0q�5"      ��5      �� $ WindowTitle�5"      �� 5#       �� Orientation$ 
Horizontal=�6   $��Right=�5$      $��Center=�5%      $��0,0,16,0q�5&      .+�5'      ��   MinimizeWindowButton#���  6   $*��$SystemCommands.MinimizeWindowCommand��5(      ��5)      �� 	 �� RelativeSource	 �� 
 �� AncestorType

 Window�� 5*        MaximizeWindowButton#���  6   $*��$SystemCommands.MaximizeWindowCommand��5+      ��5,      �� 	 �� 

 Window�� 5-        CloseWindowButton#���  6   $'��!SystemCommands.CloseWindowCommand��5.      ��5/      �� 	 �� 

 Window50      51      �� 54      -   5        $��1��54      $��1��55      $��2��56      ��57      �� *Navigation.CurrentView+58      � 5;      -   5        $��	LeftPanel��5;      $��1��6$   $��0��61     LeftNavigationPanel#���  6A   .+�5>      �� -   5        $��LeftPanel_TopButtons��5>        LeftPanelTopButtons#���  69   .+�5?      d� -   5        $��
MenuButton��5?   #     MenuToggleButton#���  65    � ToolTip$ Меню��6`   �� 5@      -   5        $��FavouriteButton��5@       	 FavoriteButton#��� 	 64   $ Избранное��6]   ��6q   �� *NavigateToFavouriteCommand+.��5A      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Star=�62   5B      d� 5C      -   5        $��AdminButton��5C   #    
 AdminToggleButton#��� 
 66   $( "Администрирование��6b   d� 5D      -   5        $��ArchiveButton��5D   #     ArchiveToggleButton#���  68   $ 
Архив��6f   5E      �� 5H      -   5        $��LeftPanel_BottomButtons��5H      $��Bottom=�6<     LeftPanelBottomButtons#���  6W   .+�5I      ��   
HelpButton#���  6   $! Руководство | F1��6B   .��5J        $ 
HelpCircle=�62   5K      �� 5L        RefreshButton#���  6   $ Обновить | F5��6E   .��5M        $ Reload=�62   5N      �� 5O        LogoutButton#���  6   $ 
Выход��6D   ��6T   �� *LogOutCommand+.��5P        $ Logout=�62   5Q      5R      5S      5U      �� 5W   
   -	   5        $��	Blindfall��5W        BlindfallOverlayStyle#���  6"   5X      5Y      