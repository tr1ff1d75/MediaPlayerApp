   M S B A M L   `   `   `  ����I  EMediaPlayerApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:MediaPlayerAppMediaPlayerApp  5   	       MediaPlayerApp.History   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:MediaPlayerApp     5   	   -   5          � Title$  MediaPlayerApp��5   	    � WindowStyle$
 None=�6     � 
ResizeMode$ CanResizeWithGrip=�63   $
��1200��6R    � WindowStartupLocation$ CenterScreen=�6_    � AllowsTransparency . $	��525��6�   $	��350��6�   ���	#FF131B26 � 	Resources 5	         MediaButtonKey �� Property �� Value �� 
TargetType	 �� Triggers
 P� Property P� Value �� 
TargetType��%O  &	        '�� �    �� 5
   
   .�5      ��  �@�OverridesDefaultStyle . 6=   �� 5       �@�Cursor$
 Hand��6.   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $��0q�6!   #��z��65   ._�5      �� $��Center=�6/   $��Center=�6L   5      	 5      P� 5      
 �@J�IsMouseOver . . �5   "   ��  �@J�Opacity$	 0.8Q�6<   5      5      5      5      �� 5   
   " ��6   .�5      ��  � ��$ 	20 8 20 8q�6'   �� 5       � ��$	 100��6&   �� 5        � ��$ 30��6'   5!      5#      .��5$      �  � RowDefinitions 5%   
   �� 5&      $��* �6   �� 5'      $
��Auto �6   .+�5)   
   &   System.Windows.Controls.DataGrid  -   5          Name$ HistoryGrid��5)   /      AutoGenerateColumns .  $��
40,30,40,0q�6D      Columns 5*      0  *System.Windows.Controls.DataGridTextColumn  5+      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6>   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *Name+6>     5,      $ Duration��6B    6%   �� *
Duration+6B     5-      $
 Date��6>    6%   �� *Date+6>     5.      $ Favorite��6B    6%   �� *
Favorite+6B     5/      $ 	Full path��6>    6%   �� *Path+6>   51      ]� 53   
   -   5        $��media_panel��53      ��.  $��0,4,0,46q�6=   $��2��6O   .+�54      � $	��Top=�6   $��10q�6(   .+�55      �� -   5        $��buttom_close��55   �   $��17��6   $	��Top=�6%    � ToolTip$ Close��6=   $��Right=�6M   #��[  6i   $��	488,0,0,0q�6�   ��6�   Z� .��56      �� $��png/close.png��6   $��Uniform=�63   57      �� 58      -   5        $��buttom_menu��58   �   $��17��6   $��17��6$   $ 	Open menu��60   $
��Left=�6D   #��[  6_   ��6�   Z� .��59      �� $��png/menu.png��6   $��Uniform=�62   5:      5;      5<      ]� 5?   
   -   5        $��
menu_panel��5?       J� Opacity$ 0Q�6&   $
��Left=�62   $	��Top=�6M   $��40��6e   $��0,70,0,0q�6p   .+�5@      �� -   5        $��buttom_menu_fileOpen��5@   �   #��[  6   $ Open the file��6=   $	��Top=�6U   $	��Top=�6j   $��30��6�   $��0,0,0,0q�6�   $��30��6�   ��6�   Z� .��5A      �� $��png/file_open.png��6   $��Uniform=�63   5B      �� 5C      -   5        $��buttom_menu_history��5C   �   #��[  6   $	��Top=�6=   $ History��6R   $	��Top=�6d   $��30��6|   $��0,15,0,0q�6�   $��30��6�   ��6�   Z� .��5D      �� $��png/history.png��6   $��Uniform=�61   5E      �� 5F      -	   5        $��buttom_menu_likes��5F   �   #��[  6   $	��Top=�6=   $ Favorite��6R   $	��Top=�6e   $��30��6}   $��0,15,0,0q�6�   $��30��6�   ��6�   Z� .��5G      �� $��png/likes.png��6   $��Uniform=�6/   5H      �� 5I      -
   5        $��buttom_menu_home��5I   �   #��[  6   $	��Top=�6=   $
 Home��6R   $	��Top=�6a   $��30��6y   $��0,40,0,0q�6�   $��30��6�   ��6�   Z� .��5J      �� $��png/home.png��6   $��Uniform=�6.   5K      5L      �� 5N   
   $��Center=�6   $��1��63    �� Orientation$ 
Horizontal=�6@   $��10q�6Y   .+�5O      �� -   5         ��Name$ updateButton��5O   l   #��[  6   $ Refresh��6=   $��0,0,30,0q�6O   $��30��6a   $��30��6�   ��6�   Z� .��5P      �� $��png/refresh.png��6   $��Uniform=�61   5Q      �� 5R      -   5        $ openFileButton��5R   j   #��[  6   $
 Open��6=   $��	30,0,30,0q�6L   $��30��6_   $��30��6�   ��6�   Z� .��5S      �� $��png/play-history.png��6   $��Uniform=�66   5T      �� 5U      -   5        $ deleteButton��5U   k   #��[  6   $ Delete��6=   $��30,0,0,0q�6N   $��30��6`   $��30��6�   ��6�   Z� .��5V      �� $��png/delete.png��6   $��Uniform=�60   5W      5X      5Y      5Z      