   M S B A M L   `   `   `  ���� D  @FlameCage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:GrayHills.FlameCage.Client.CoreGrayHills.FlameCage.Client.Core  +    %GrayHills.FlameCage.Client.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  h dMicrosoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  8core-clr-namespace:GrayHills.FlameCage.Client.Core  \ XHardcodet.Wpf.TaskbarNotification, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null)tb http://www.hardcodet.net/taskbar  4   .GrayHills.FlameCage.Client.Core.ShadowedWindow    StateChanged$#  ShadowedWindow_OnStateChanged��$	��700��$	��500��$	��527��$
��1014�� � Icon$G A/FlameCage;component/Resources/Icons/CampfireChatBubble-55196.ico�� � WindowState �� *CurrentWindowState+ �� Mode$ TwoWay=� � 	Resources  �� MergedDictionaries �� Source   ResizeRectangleStyle �� 
TargetType �� Property	 �� Value��  �� $ Resources/General.xaml?��� $ Resources/Geometry.xaml?��� $  Resources/Dictionary2.xaml?��� $ Resources/Buttons.xaml?�%P   &	        �� " ��.���  � ��
	 ���� ��  �@K�ZIndex$	 5��-   .��� -   
 �Name$
 
LayoutRoot��'  !System.Windows.VisualStateManager   VisualStateGroups %  System.Windows.VisualStateGroup  -   Main��   States.    System.Windows.VisualState  -   RoomView��   
Storyboard. �� .��T�  �� TargetProperty$ (UIElement.Visibility)� �� 
TargetName$ SettingsPanel��..�i�  R� KeyTime$ 0��  System.Windows.Visibility   	Collapsed R� Value# Z   -   SettingsView��. �� .��T� $ (UIElement.Visibility)�$ SettingsPanel��..�i� $ 0��   Visible# Z .+�3  -Hardcodet.Wpf.TaskbarNotification.TaskbarIcon  $y�Visible=�   ToolTipText$! FlameCage Technical Preview��   
IconSource$3 -/Resources/Icons/CampfireChatBubble-55196.ico���  � RowDefinitions �� $��50 ��� .+�]� $��3��.+�� -   $
 
HeaderArea��  TopBarActiveBrush#��[ $��Stretch=�$��Stretch=�.+��� $��5q�
������� J� Opacity$	 .75Q�$!��FLAMECAGE TECHNICAL PREVIEW��$!��./resources/fonts/#Segoe WP)�$��10(��� $��Settings��$	��100��$��Right=�$y�	Collapsed=�$��0, 0, 100, 0q�.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ Click��.	  (System.Windows.Interactivity.TriggerBase 	  Actions. =
  7Microsoft.Expression.Interactivity.Core.GoToStateAction
   
  	StateName$ SettingsView���� $��
0, 0, 5, 0q� �� Orientation$ 
Horizontal=�$��Right=�$	��Top=�.+��� -   $��MinimizeButton��  MetroPathButton#���  $��19��$��23�� � ToolTip$ Minimize������ *MinimizeCommand+$ OneTime=�.���� $��7��$��2��
�������$��Bottom=�$��0,6,0,4q��� -   $��MaximizeButton��#���  $��19��$��23��$ Maximize������ *MaximizeCommand+$ OneTime=�.��F� 
�������  PathMaximize#��[ $��0,6,0,0q�$��9��$��9�� �� Stretch$
 Fill=��� -	   $��RestoreButton��#���  $��19��$��23��$ Restore��$y�	Collapsed=����� *RestoreCommand+$ OneTime=�.��F� 
�������  PathRestore#��[ $��0,6,0,0q�$��9��$��9��$
 Fill=��� -
    ��Name$ CloseButton��$��25��$��23��$��0,0,-6,0q�#��[ $ Close������ *CloseCommand+$ OneTime=�.��F� 
�������  	PathClose#��[ $��0,6,6,0q�$
 Fill=�$��9��$��9��� $��1��  � ColumnDefinitions  �� $
��172* ��� $
��331* �.+�]� -   ! ]�Name$! LeftDockPanel��$��0���� $��0��" K� ZIndex$" 1��$��1��#��[ #��[ ]� -   $! RightDockPanel��$��2��]� -   $! SettingsPanel��
�������$��1��$y�	Collapsed=��� -   # ��Name$# TopResizeRectangle��#��[  $��5��$ � Cursor$$ SizeNS��$��Stretch=�$	��Top=��� -   $# LeftResizeRectangle��#��[  $��5��$$ SizeWE��$
��Left=�$��Stretch=�$��2���� -   $# BottomResizeRectangle��#��[  $��5��$$ SizeNS��$��Stretch=�$��Bottom=�$��1���� -   $# RightResizeRectangle��#��[  $��5��$$ SizeWE��$��Right=�$��Stretch=�$��1��$��2���� -   $# TopLeftResizeRectangle��#��[  $��5��$��5��$$ SizeNWSE��$
��Left=�$	��Top=��� -   $# TopRightResizeRectangle��#��[  $��5��$��5��$$ SizeNESW��$��Right=�$	��Top=��� -   $ # BottomRightResizeRectangle��#��[  $��5��$��5��$$ SizeNWSE��$��Right=�$��Bottom=�$��1���� -   $# BottomLeftResizeRectangle��#��[  $��5��$��5��$$ SizeNESW��$
��Left=�$��Bottom=�$��1���� -   % ��Name$% FormResizeGrip��$��1��$��Right=�$��Bottom=��� -   & ��Name$& OverlayCanvas��
�������$ .8Q�$��Stretch=�$��Stretch=�$y�	Collapsed=�