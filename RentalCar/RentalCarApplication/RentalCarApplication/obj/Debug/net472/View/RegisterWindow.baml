   M S B A M L   `   `   `  ����O  KRentalCarApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:RentalCarApplication.ViewModelRentalCarApplication.ViewModel  5   	   E'clr-namespace:RentalCarApplication.ViewRentalCarApplication.View  5   	   .    (RentalCarApplication.View.RegisterWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;vm,clr-namespace:RentalCarApplication.ViewModel     5   	   5fa5%http://schemas.fontawesome.com/icons/     5   	   5l'clr-namespace:RentalCarApplication.View     5   	   b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   $	��700��5   	   $
��1100��6   
����� �.��5      � $��Center=�6   .+�5   
   �� $	��500��6   $��15q�6    $��Center=�6,   $��Center=�6G     �� Padding$  25q�6d    �� CornerRadius$ 20��6q   
������� J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 20Q�6#      Color$
 Teal��63      ShadowDepth$ 5Q�6@   ._�5      �� .+�5      � .+�5      �� $��Регистрация��6    $��24(�63   $��Medium"�6A   $��0q�6U   $��Center=�6`   �� 5      $��Right=�6      MaterialDesignFlatButton#��[  69   $��3q�6�   $��0 5 0 0q�6�   ��6k   �� *NavigateToLoginCommand+.��5      ��  �� Orientation$ 
Horizontal=�6%   .+�5      �� $��
Войти��6(   $��16(�65   '  !MaterialDesignThemes.Wpf.PackIcon  5         Kind$ ArrowRightBold=�66   $��3 5 0 0q�6L   5      5      5      � 5       � ColumnDefinitions 5      �� 5      $
��0.1* �6+   �� 5      $��2* �6+   	 � RowDefinitions	 5!      �� 5"      �� 5#      �� 5$      �� 5%      �� 5&      �� 5'      �� 5(      �� 5)      �� 5*      .+�5-        $��0��6.   $��0��6>   $ 	PersonAdd=�6K   $��20��6\   $��30��6g   $��0 20 5 0q�6s     5.      $��0��6.   $��1��6>   $ 	PersonAdd=�6K   $��20��6\   $��30��6g   $��0 20 5 0q�6s     5/      $��0��6.   $��2��6>   $ Passport=�6K   $��20��6[   $��30��6f   $��0 20 5 0q�6r     50      $��0��6.   $��3��6>   $ PassportElectronic=�6K   $��20��6e   $��30��6p   $��0 20 5 0q�6|     51      $��0��6.   $��4��6>   $ 	Telephone=�6K   $��20��6\   $��30��6g   $��0 20 5 0q�6s     52      $��0��6.   $��5��6>   $ Email=�6K   $��20��6X   $��30��6c   $��0 20 5 0q�6o     53      $��0��6.   $��6��6>   $	 Key=�6K   $��20��6V   $��30��6a   $��0 20 5 0q�6m     54      $��0��6.   $��7��6>   $ 	KeyChange=�6K   $��20��6\   $��30��6g   $��0 20 5 0q�6s   �� 56      -   5        
 ��Name$
 regName��56      $��1��6/   $��0��6?   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Введите Имя��6L   .  (MaterialDesignThemes.Wpf.TextFieldAssist   HasClearButton$
 True��6y    % !MaterialDesignFloatingHintTextBox#��[ 6�   $��18(�6�   �� 57      -   5        $
 
regSurname��57      $��1��62   $��1��6B   $# Введите Фамилию��6O   $
 True��6�   #��[ 6�   $��18(�6�   �� 58      -   5        $
 regPassport��58      $��1��63   $��2��6C   $2 ,Введите данные паспорта��6P   $
 True��6�      
HelperText$ 	ABXXXXXXX��6�   $��0 0 0 15q�6�   #��[ 6  $��18(�6<  �� 59      -   5        $
 regDriverLicense��59      $��1��68   $��3��6H   $U OВведите номер водительского удостоверения��6U   $
 True��6�   $ 	AАXXXXXX��6�   $��0 0 0 15q�6  #��[ 6  $��18(�6R  �� 5:      -   5        $
 regTelNumber��5:      $��1��64   $��4��6D   $
��+375��6Q   $E ?Введите номер мобильного телефона��6]   $
 True��6�   $ +375XXXXXXXXX��6�   $��0 0 0 15q�6
  #��[ 6  $��18(�6W  �� 5;      -   5        $
 regEmail��5;      $��1��60   $��5��6@   $' !Введите логин (Email)��6M   $
 True��6�   #��[ 6�   $��18(�6�   G� 5<      -   5         G�Name$ regPassword1��5<   "   $��1��68   $��6��6H   $! Введите пароль��6U   $
 True��6�    ) %MaterialDesignFloatingHintPasswordBox#��[ 6�   $��18(�6�   G� 5=      -   5        $ regPassword2��5=   "   $��1��68   $��7��6H   $) #Подтвердите пароль��6U   $
 True��6�   #��[ 6�   $��18(�6�   �� 5>      $��0��6   $��2��6-   $��8��6A    " MaterialDesignRaisedDarkButton#��[ 6N   $*��$Зарегистрироваться��6�   $��18(�6�   $��
15 15 0 10q�6�   5?      5@      5A      5B      5C      