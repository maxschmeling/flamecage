   M S B A M L   `   `   `  ����D  @FlameCage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:GrayHills.FlameCage.Client.CoreGrayHills.FlameCage.Client.Core  5      +    %GrayHills.FlameCage.Client.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  6M   h dMicrosoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  5      8core-clr-namespace:GrayHills.FlameCage.Client.Core  5      \ XHardcodet.Wpf.TaskbarNotification, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null)tb http://www.hardcodet.net/taskbar  5      4   .GrayHills.FlameCage.Client.Core.ShadowedWindow    StateChanged$#  ShadowedWindow_OnStateChanged��5      $	��700��5	      $	��500��5
      $	��527��5      $
��1014��5       � Icon$G A/FlameCage;component/Resources/Icons/CampfireChatBubble-55196.ico��5       � WindowState 5      �� *CurrentWindowState+ �� Mode$ TwoWay=� � 	Resources 5       �� MergedDictionaries �� Source   ResizeRectangleStyle �� 
TargetType �� Property	 �� Value  TrayContextMenu�� 5       5      �� 5   
   $ Resources/General.xaml?�6   �� 5   
   $ Resources/Geometry.xaml?�6   �� 5   
   $  Resources/Dictionary2.xaml?�6   �� 5   
   $ Resources/Buttons.xaml?�6   %  &	        &	 �     �� 5      " ��6+   .�5   
   ��  � ��
	 ���� 6!   �� 5   
    �@K�ZIndex$	 5��6)   -   5        5   	   �� 5      .@�5   
   m� $��Leave all rooms...��6   �� 5   
   m� 5    
   $��Exit FlameCage��6   5!   	   5"      .��5$      � -   5        
 �Name$
 
LayoutRoot��5$   	   '  !System.Windows.VisualStateManager   VisualStateGroups 5%      %  System.Windows.VisualStateGroup  5&      -   5        Main��   States. 5'   
      System.Windows.VisualState  -   5        RoomView��   
Storyboard. 5(      �� .��5)      T�  �� TargetProperty$ (UIElement.Visibility)�6,    �� 
TargetName$ SettingsPanel��6_   ..�5*      i�  R� KeyTime$ 0��6'     System.Windows.Visibility   	Collapsed R� Value# Z 63   5+      5,      5-        5.   
   -   5        SettingsView��. 5/      �� .��50      T� $ (UIElement.Visibility)�6,   $ SettingsPanel��6_   ..�51      i� $ 0��6'      Visible# Z 63   52      53      54      55   	   .+�58      3  -Hardcodet.Wpf.TaskbarNotification.TaskbarIcon  -   5          Name$ 
NotifyIcon��58      $y�Visible=�59       � ContextMenu# [ 5:         MenuActivation$ 
RightClick=�5;         ToolTipText$! FlameCage Technical Preview��5<         
IconSource$3 -/Resources/Icons/CampfireChatBubble-55196.ico��5=      � 5>       � RowDefinitions 5?      �� 5@   
   $��50 �6   �� 5A   
   .+�5D      ]� $��3��6   .+�5E   
   � -   5        $
 
HeaderArea��5E        TopBarActiveBrush#��[ 5G      $��Stretch=�5H      $��Stretch=�5I      .+�5J      �� $��5q�6   
������� J� Opacity$	 .75Q�64   $!��FLAMECAGE TECHNICAL PREVIEW��6B   $!��./resources/fonts/#Segoe WP)�6e   $��10(�6�   �� 5K      $��Settings��6   $	��100��5L      $��Right=�5M      $y�	Collapsed=�5N      $��0, 0, 100, 0q�5O      .  (System.Windows.Interactivity.Interaction   Triggers 5P      /  )System.Windows.Interactivity.EventTrigger  5Q         	EventName$ Click��6   .	  (System.Windows.Interactivity.TriggerBase 	  Actions. 5R      =
  7Microsoft.Expression.Interactivity.Core.GoToStateAction
   
  	StateName$ SettingsView��6%   5S      5U      �� 5V      $��
0, 0, 5, 0q�6    �� Orientation$ 
Horizontal=�6+   $��Right=�6D   $	��Top=�6`   .+�5W      �� -   5        $��MinimizeButton��5W        MetroPathButton#���  6]   $��19��6�   $��23��6�     � ToolTip$  Minimize��6�   ��6+   �� *MinimizeCommand+$ OneTime=�.��5X      �� $��7��6   $��2��6$   
�������$��Bottom=�6<   $��0,6,0,4q�6W   5Y      �� 5Z      -	   5        $��MaximizeButton��5Z      #���  6]   $��19��6�   $��23��6�   $  Maximize��6�   ��6+   �� *MaximizeCommand+$ OneTime=�.��5[      F� 
�������  PathMaximize#��[ 6"   $��0,6,0,0q�6G   $��9��6X   $��9��6b   ! �� Stretch$
! Fill=�6m   5\      �� 5]      -
   5        $��RestoreButton��5]      #���  6[   $��19��6�   $��23��6�   $  Restore��6�   $y�	Collapsed=�6�   ��6*   �� *RestoreCommand+$ OneTime=�.��5^      F� 
�������  PathRestore#��[ 6"   $��0,6,0,0q�6F   $��9��6W   $��9��6a   $
! Fill=�6l   5_      �� 5`      -   5        " ��Name$" CloseButton��5`      $��25��6Y   $��23��6d   $��0,0,-6,0q�6p   #��[ 6�   $  Close��6�   ��6*   �� *CloseCommand+$ OneTime=�.��5a      F� 
�������  	PathClose#��[ 6"   $��0,6,6,0q�6D   $
! Fill=�6U   $��9��6d   $��9��6n   5b      5c      5d      5e   	   � 5g      $��1��6   # � ColumnDefinitions# 5h   
   �� 5i      $
��172* �6   �� 5j      $
��331* �6   .+�5m   
   ]� -   5        $ ]�Name$$ LeftDockPanel��5m      $��0��6+   �� 5n   
   $��0��6   % K� ZIndex$% 1��6'   $��1��68   #��[ 6B   #��[ 6r   ]� 5o   
   -   5        $$ RightDockPanel��5o      $��2��6,   5p   	   ]� 5r      -   5        $$ SettingsPanel��5r      
�������$��1��6<   $y�	Collapsed=�6I   �� 5t      -   5        & ��Name$& TopResizeRectangle��5t   @   #��[  6   $��5��6_   ' � Cursor$' SizeNS��6j   $��Stretch=�6z   $	��Top=�6�   �� 5u      -   5        $& LeftResizeRectangle��5u   @   #��[  6   $��5��6_   $' SizeWE��6j   $
��Left=�6z   $��Stretch=�6�   $��2��6�   �� 5v      -   5        $& BottomResizeRectangle��5v   @   #��[  6   $��5��6_   $' SizeNS��6j   $��Stretch=�6z   $��Bottom=�6�   $��1��6�   �� 5w      -   5        $& RightResizeRectangle��5w   @   #��[  6   $��5��6_   $' SizeWE��6j   $��Right=�6z   $��Stretch=�6�   $��1��6�   $��2��6�   �� 5y      -   5        $& TopLeftResizeRectangle��5y   @   #��[  6   $��5��6d   $��5��6n   $' SizeNWSE��6y   $
��Left=�6�   $	��Top=�6�   �� 5z      -   5        $& TopRightResizeRectangle��5z   @   #��[  6   $��5��6d   $��5��6n   $' SizeNESW��6y   $��Right=�6�   $	��Top=�6�   �� 5{      -   5        $ & BottomRightResizeRectangle��5{   @   #��[  6   $��5��6d   $��5��6n   $' SizeNWSE��6y   $��Right=�6�   $��Bottom=�6�   $��1��6�   �� 5|      -   5        $& BottomLeftResizeRectangle��5|   @   #��[  6   $��5��6d   $��5��6n   $' SizeNESW��6y   $
��Left=�6�   $��Bottom=�6�   $��1��6�   �� 5~      -   5        ( ��Name$( FormResizeGrip��5~      $��1��6+   $��Right=�68   $��Bottom=�6T   5      �� 5�      -   5        ) ��Name$) OverlayCanvas��5�      
�������$ .8Q�67   $��Stretch=�6D   $��Stretch=�6b   $y�	Collapsed=�6~   5�      5�      