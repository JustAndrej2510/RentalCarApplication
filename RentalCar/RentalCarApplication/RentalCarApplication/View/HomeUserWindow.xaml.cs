using RentalCarApplication.Infrastructure;
using RentalCarApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RentalCarApplication.View
{
    /// <summary>
    /// Логика взаимодействия для HomeUserWindow.xaml
    /// </summary>
    public partial class HomeUserWindow : UserControl
    {
        public HomeUserWindow()
        {
            InitializeComponent();
        }

        private void userNewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).UserShownNewPassword = ((PasswordBox)sender).Password;
                ((dynamic)this.DataContext).UserNewPassword = ((PasswordBox)sender).Password;

            }
        }

        private void userConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).UserConfirmPassword = ((PasswordBox)sender).Password;

            }
        }
    }
}
