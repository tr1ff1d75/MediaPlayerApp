   M S B A M L   `   `   `  ���� I  EMediaPlayerApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:MediaPlayerAppMediaPlayerApp      MediaPlayerApp.History   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:MediaPlayerApp     -     � Title$  MediaPlayerApp�� � WindowStyle$
 None=� � 
ResizeMode$ CanResizeWithGrip=�$
��1200�� � WindowStartupLocation$ CenterScreen=� � AllowsTransparency . $	��525��$	��350�����	#FF131B26 � 	Resources    MediaButtonKey �� Property �� Value �� 
TargetType	 �� Triggers
 P� Property P� Value �� 
TargetType��%V  &	        '�� �     �� .���  �@�OverridesDefaultStyle . ��  �@�Cursor$
 Hand����  � �� �� " ��.R��� $��0q�#��z��._��� $��Center=�$��Center=�	 P� 
 �@J�IsMouseOver . . ���  �@J�Opacity$	 0.8Q��� " ��.���  � ��$ 	20 8 20 8q���  � ��$	 100����  � ��$ 30��.���  � RowDefinitions �� $��* ��� $
��Auto �.+�&   System.Windows.Controls.DataGrid  -     Name$ HistoryGrid��   AutoGenerateColumns .  $��
40,30,40,0q�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Name+  $ Duration�� �� *
Duration+  $
 Date�� �� *Date+  $ Favorite�� �� *
Favorite+  $ 	Full path�� �� *Path+]� -   $��media_panel����.  $��0,4,0,46q�$��2��.+�� $	��Top=�$��10q�.+��� -   $��buttom_close��$��17��$	��Top=� � ToolTip$ Close��$��Right=�#��[  $��	488,0,0,0q���Z� .���� $��png/close.png��$��Uniform=��� -   $��buttom_menu��$��17��$��17��$ 	Open menu��$
��Left=�#��[  ��Z� .���� $��png/menu.png��$��Uniform=�]� -   $��
menu_panel�� J� Opacity$ 0Q�$
��Left=�$	��Top=�$��40��$��0,70,0,0q�.+��� -   $��buttom_menu_fileOpen��#��[  $ Open the file��$	��Top=�$	��Top=�$��30��$��0,0,0,0q�$��30����Z� .���� $��png/file_open.png��$��Uniform=��� -   $��buttom_menu_history��#��[  $	��Top=�$ History��$	��Top=�$��30��$��0,15,0,0q�$��30����Z� .���� $��png/history.png��$��Uniform=��� -	   $��buttom_menu_likes��#��[  $	��Top=�$ Favorite��$	��Top=�$��30��$��0,15,0,0q�$��30����Z� .���� $��png/likes.png��$��Uniform=��� -
   $��buttom_menu_home��#��[  $	��Top=�$
 Home��$	��Top=�$��30��$��0,40,0,0q�$��30����Z� .���� $��png/home.png��$��Uniform=��� $��Center=�$��1�� �� Orientation$ 
Horizontal=�$��10q�.+��� -    ��Name$ updateButton��#��[  $ Refresh��$��0,0,30,0q�$��30��$��30����Z� .���� $��png/refresh.png��$��Uniform=��� -   $ openFileButton��#��[  $
 Open��$��	30,0,30,0q�$��30��$��30����Z� .���� $��png/play-history.png��$��Uniform=��� -   $ deleteButton��#��[  $ Delete��$��30,0,0,0q�$��30��$��30����Z� .���� $��png/delete.png��$��Uniform=�