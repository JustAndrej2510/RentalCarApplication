   M S B A M L   `   `   `  ����  RentalCarApplicationA    ;RentalCarApplication.View.CustomMessageBox.CustomMessageBox   5   
   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
   	     � Title$  MessageBoxWindow��5   	   $	��220��6"   $	��500��6/    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�6.   
������� � 
ResizeMode$ NoResize=�5	   	    � Topmost$
 True��6    � ShowInTaskbar$ False��6.      MaterialDesignBody#���   5   	   $��Regular"�5   	   $��10(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignFont#���  5   	   .��5      � +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth5=�6   	 � ColumnDefinitions	 5   
   �� 5      $
��197* �6   �� 5      $
��295* �6   
 � RowDefinitions
 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   #  MaterialDesignThemes.Wpf.Card  -   5          Name$ 
cardHeader��5      $��0��62   $��10q�6?   $��0q�6L   $ Depth2=�6W     PrimaryHueMidBrush#���  6�    ! PrimaryHueDarkForegroundBrush#���  6�   $y�Visible=�6�   $��2��6  .��5      �� .+�5      �� -   5         ��Name$ txtTitle��5      $��Center=�6.   $��Stretch=�6K      MaterialDesignTitleTextBlock#���  6g   $��20(�6�   Message Title6�   5      5      �� 5    
   $��Right=�6   $��0,5,5,0q�62   $	��Top=�6D   $��1��6\   .+�5!      �� -   5         ��Name$ btnClose��5!      $��35��6>   $��35��6I   
�������  PrimaryHueDarkBrush#���  6i    * &MaterialDesignFloatingActionDarkButton#��[ 6�   .��5"      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Close=�6*   5#      5$      � 5%   
   $��1��6   $��2��6   
�������.+�5&      � $��20q�6   
 5'      �� 5(      $
��auto �6$   �� 5)      $
��auto �6$   �� 5*      $��* �6$   .+�5-      � $��0��6   
 5.      �� 5/      $
��auto �6(   �� 50      $��* �6(   .+�52      ��  �� Orientation$ 
Horizontal=�6!   $��Center=�6:   .+�53        -   5          Name$ ConfirmationIcon��53   2   $ HelpCircleOutline=�6L   $��40��6e   $��40��6p   
���  ��$y�	Collapsed=�6�     54      -   5        $ InfoIcon��54   2   $ InformationOutline=�6D   $��40��6^   $��40��6i   
��� ���$y�	Collapsed=�6�     55      -   5        $ SuccessIcon��55   2   $ CheckCircleOutline=�6G   $��40��6a   $��40��6l   
��� � �$y�	Collapsed=�6�     56      -   5        $ 	ErrorIcon��56   2   $ CloseCircleOutline=�6E   $��40��6_   $��40��6j   
���  ��$y�	Collapsed=�6�   �� 57      -   5         ��Name$ 
txtMessage��57   "   $	��400��66   $-��'dsdf sdf dfgdf dfg dfg dfgdffdgfgdfg sd��6B   $��Center=�6q    �� 
IsReadOnly$
 True��6�   $��0��6�   $��5q�6�   $��18(�6�    1 -MaterialDesignDataGridComboBoxEditableTextBox#��[ 6�    �� TextWrapping$
 Wrap=�6  58      5A      � 5B      $��1��6   $��0,20,0,5q�6$   	 5C      �� 5D      $��* �6+   �� 5E      $
��auto �6+   �� 5F      $
��auto �6+   �� 5G      $��* �6+   .+�5J      �� -	   5        $ btnOk��5J      $��1��6@     MaterialDesignRaisedButton#��[ 6P   $��5q�6�   $	��100��6�   $��OK��6�    � ToolTip$ Ok��6�   �� 5K      -
   5        $ 	btnCancel��5K      $��2��6H   #��[ 6X   $��5q�6�   $	��100��6�   $��Cancel��6�   $ Cancel��6�   �� 5L      -   5        $ btnYes��5L      $��1��6B   #��[ 6R   $��5q�6�   $	��100��6�   $
��Да��6�   $
 Да��6�   �� 5M      -   5        $ btnNo��5M      $��2��6@   #��[ 6P   $��5q�6�   $	��100��6�   $��Нет��6�   $ Нет��6�   5N      5P      5Q      5R      5S      