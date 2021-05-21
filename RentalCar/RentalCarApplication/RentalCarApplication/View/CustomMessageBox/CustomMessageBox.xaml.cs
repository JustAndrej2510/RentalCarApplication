using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RentalCarApplication.View.CustomMessageBox
{
    /// <summary>
    /// Логика взаимодействия для CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox(string Message, MessageType Type, MessageButtons Buttons)
        {
            InitializeComponent();
            txtMessage.Text = Message;
            switch (Type)
            {

                case MessageType.Info:
                    {
                        txtTitle.Text = "Информация";
                        InfoIcon.Visibility = Visibility.Visible;
                        break;
                    }
                   
                case MessageType.Confirmation:
                    {
                        txtTitle.Text = "Подтверждение";
                        ConfirmationIcon.Visibility = Visibility.Visible;
                        break;
                    }
                  
                case MessageType.Success:
                    {
                        txtTitle.Text = "Успешно";
                        SuccessIcon.Visibility = Visibility.Visible;
                        break;
                    }
                case MessageType.Warning:
                    {
                        txtTitle.Text = "Предупреждение";
                        ErrorIcon.Visibility = Visibility.Visible;
                        break;
                    }
                case MessageType.Error:
                    {
                        txtTitle.Text = "Ошибка";
                        ErrorIcon.Visibility = Visibility.Visible;
                        break;
                    }
                   
            }
            switch (Buttons)
            {
                case MessageButtons.OkCancel:
                    btnYes.Visibility = Visibility.Collapsed; btnNo.Visibility = Visibility.Collapsed;
                    break;
                case MessageButtons.YesNo:
                    btnOk.Visibility = Visibility.Collapsed; btnCancel.Visibility = Visibility.Collapsed;
                    break;
                case MessageButtons.Ok:
                    btnOk.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Collapsed;
                    btnYes.Visibility = Visibility.Collapsed; btnNo.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
    public enum MessageType
    {
        Info,
        Confirmation,
        Success,
        Warning,
        Error,
    }
    public enum MessageButtons
    {
        OkCancel,
        YesNo,
        Ok,
    }

}
