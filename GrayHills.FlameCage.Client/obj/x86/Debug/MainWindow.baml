   M S B A M L   `   `   `  ����D  @FlameCage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:GrayHills.FlameCage.Client.CoreGrayHills.FlameCage.Client.Core  5      +    %GrayHills.FlameCage.Client.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  6M   h dMicrosoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  5      8core-clr-namespace:GrayHills.FlameCage.Client.Core  5      4   .GrayHills.FlameCage.Client.Core.ShadowedWindow    StateChanged$#  ShadowedWindow_OnStateChanged��5      $	��700��5      $	��500��5	      $	��527��5
      $
��1014��5       � Icon$G A/FlameCage;component/Resources/Icons/CampfireChatBubble-55196.ico��5       � WindowState 5      �� *CurrentWindowState+ �� Mode$ TwoWay=� � 	Resources 5       �� MergedDictionaries �� Source   ResizeRectangleStyle �� 
TargetType �� Property	 �� Value�� 5       5      �� 5   
   $ Resources/General.xaml?�6   �� 5   
   $ Resources/Geometry.xaml?�6   �� 5   
   $  Resources/Dictionary2.xaml?�6   �� 5   
   $ Resources/Buttons.xaml?�6   %�   &	        �� 5      " ��6+   .�5   
   ��  � ��
	 ���� 6!   �� 5   
    �@K�ZIndex$	 5��6)   -   5        5   	   5      .��5      � -   5        
 �Name$
 
LayoutRoot��5   	   '  !System.Windows.VisualStateManager   VisualStateGroups 5      %  System.Windows.VisualStateGroup  5      -   5        Main��   States. 5    
      System.Windows.VisualState  -   5        RoomView��   
Storyboard. 5!      �� .��5"      T�  �� TargetProperty$ (UIElement.Visibility)�6,    �� 
TargetName$ SettingsPanel��6_   ..�5#      i�  R� KeyTime$ 0��6'     System.Windows.Visibility   	Collapsed R� Value# Z 63   5$      5%      5&        5'   
   -   5        SettingsView��. 5(      �� .��5)      T� $ (UIElement.Visibility)�6,   $ SettingsPanel��6_   ..�5*      i� $ 0��6'      Visible# Z 63   5+      5,      5-      5.   	   .+�50      �  � RowDefinitions 51      �� 52   
   $��50 �6   �� 53   
   .+�56      ]� $��3��6   .+�57   
   � -   5        $
 
HeaderArea��57        TopBarActiveBrush#��[ 59      $��Stretch=�5:      $��Stretch=�5;      .+�5<      �� $��5q�6   
������� J� Opacity$	 .75Q�64   $!��FLAMECAGE TECHNICAL PREVIEW��6B   $!��./resources/fonts/#Segoe WP)�6e   $��10(�6�   �� 5=      $��Settings��6   $	��100��5>      $��Right=�5?      $y�	Collapsed=�5@      $��0, 0, 100, 0q�5A      .  (System.Windows.Interactivity.Interaction   Triggers 5B      /  )System.Windows.Interactivity.EventTrigger  5C         	EventName$ Click��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5D      =	  7Microsoft.Expression.Interactivity.Core.GoToStateAction	   	  	StateName$ SettingsView��6%   5E      5G      �� 5H      $��
0, 0, 5, 0q�6    �� Orientation$ 
Horizontal=�6+   $��Right=�6D   $	��Top=�6`   .+�5I      �� -   5        $��MinimizeButton��5I        MetroPathButton#���  6]   $��19��6�   $��23��6�    � ToolTip$ Minimize��6�   ��6+   �� *MinimizeCommand+$ OneTime=�.��5J      �� $��7��6   $��2��6$   
�������$��Bottom=�6<   $��0,6,0,4q�6W   5K      �� 5L      -   5        $��MaximizeButton��5L      #���  6]   $��19��6�   $��23��6�   $ Maximize��6�   ��6+   �� *MaximizeCommand+$ OneTime=�.��5M      F� 
�������  PathMaximize#��[ 6"   $��0,6,0,0q�6G   $��9��6X   $��9��6b    �� Stretch$
 Fill=�6m   5N      �� 5O      -	   5        $��RestoreButton��5O      #���  6[   $��19��6�   $��23��6�   $ Restore��6�   $y�	Collapsed=�6�   ��6*   �� *RestoreCommand+$ OneTime=�.��5P      F� 
�������  PathRestore#��[ 6"   $��0,6,0,0q�6F   $��9��6W   $��9��6a   $
 Fill=�6l   5Q      �� 5R      -
   5         ��Name$ CloseButton��5R      $��25��6Y   $��23��6d   $��0,0,-6,0q�6p   #��[ 6�   $ Close��6�   ��6*   �� *CloseCommand+$ OneTime=�.��5S      F� 
�������  	PathClose#��[ 6"   $��0,6,6,0q�6D   $
 Fill=�6U   $��9��6d   $��9��6n   5T      5U      5V      5W   	   � 5Y      $��1��6    � ColumnDefinitions 5Z   
   �� 5[      $
��172* �6   �� 5\      $
��331* �6   .+�5_   
   ]� -   5         ]�Name$ LeftDockPanel��5_      $��0��6+   �� 5`   
   $��0��6     K� ZIndex$  1��6'   $��1��68   #��[ 6B   #��[ 6r   ]� 5a   
   -   5        $ RightDockPanel��5a      $��2��6,   5b   	   ]� 5d      -   5        $ SettingsPanel��5d      
�������$��1��6<   $y�	Collapsed=�6I   �� 5f      -   5        ! ��Name$! TopResizeRectangle��5f   @   #��[  6   $��5��6_   " � Cursor$" SizeNS��6j   $��Stretch=�6z   $	��Top=�6�   �� 5g      -   5        $! LeftResizeRectangle��5g   @   #��[  6   $��5��6_   $" SizeWE��6j   $
��Left=�6z   $��Stretch=�6�   $��2��6�   �� 5h      -   5        $! BottomResizeRectangle��5h   @   #��[  6   $��5��6_   $" SizeNS��6j   $��Stretch=�6z   $��Bottom=�6�   $��1��6�   �� 5i      -   5        $! RightResizeRectangle��5i   @   #��[  6   $��5��6_   $" SizeWE��6j   $��Right=�6z   $��Stretch=�6�   $��1��6�   $��2��6�   �� 5k      -   5        $! TopLeftResizeRectangle��5k   @   #��[  6   $��5��6d   $��5��6n   $" SizeNWSE��6y   $
��Left=�6�   $	��Top=�6�   �� 5l      -   5        $! TopRightResizeRectangle��5l   @   #��[  6   $��5��6d   $��5��6n   $" SizeNESW��6y   $��Right=�6�   $	��Top=�6�   �� 5m      -   5        $ ! BottomRightResizeRectangle��5m   @   #��[  6   $��5��6d   $��5��6n   $" SizeNWSE��6y   $��Right=�6�   $��Bottom=�6�   $��1��6�   �� 5n      -   5        $! BottomLeftResizeRectangle��5n   @   #��[  6   $��5��6d   $��5��6n   $" SizeNESW��6y   $
��Left=�6�   $��Bottom=�6�   $��1��6�   �� 5p      -   5        # ��Name$# FormResizeGrip��5p      $��1��6+   $��Right=�68   $��Bottom=�6T   5q      �� 5r      -   5        $ ��Name$$ OverlayCanvas��5r      
�������$ .8Q�67   $��Stretch=�6D   $��Stretch=�6b   $y�	Collapsed=�6~   5s      5t      