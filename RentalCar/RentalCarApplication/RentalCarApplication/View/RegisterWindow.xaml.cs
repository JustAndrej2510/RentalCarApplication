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
using RentalCarApplication.ViewModel;
using RentalCarApplication.Base;

namespace RentalCarApplication.View
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : UserControl
    {
        public RegisterWindow()
        {
            InitializeComponent();
            //DataContext = new RegisterWindowViewModel();
        }

        private void regPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; }
        }

        private void regPasswordCheck_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).PasswordCheck = ((PasswordBox)sender).Password; }
        }
    }
}
