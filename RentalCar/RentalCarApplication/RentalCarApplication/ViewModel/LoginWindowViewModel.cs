using System;
using System.Windows;
using System.Windows.Input;
using RentalCarApplication.Base;
using RentalCarApplication.Commands;
using RentalCarApplication.Core.Model;
using RentalCarApplication.EntityFramework;
using RentalCarApplication.Infrastructure;
using RentalCarApplication.View.CustomMessageBox;

namespace RentalCarApplication.ViewModel
{
    /// <summary>
    ///   ViewModel Login window
    /// </summary>
    public class LoginWindowViewModel : ViewModelBase
    {
        UnitOfWork unitOfWork;
        public static User CurrentUser;
        public LoginWindowViewModel(Navigator navigator)
        {
            unitOfWork = new UnitOfWork();
            AdminSignInCommand = new RelayCommand(OnAdminSignIn, CanAdminSignIn);
            UserSignInCommand = new RelayCommand(OnUserSignIn, CanUserSignIn);
            NavigateToHomeAdminCommand = new NavigationCommand<HomeAdminViewModel>(navigator, () => new HomeAdminViewModel(navigator));
            NavigateToRegisterCommand = new NavigationCommand<RegisterWindowViewModel>(navigator, () => new RegisterWindowViewModel(navigator));
            NavigateToHomeUserCommand = new NavigationCommand<HomeUserViewModel>(navigator, () => new HomeUserViewModel(navigator));
        }

        #region Navigation
        public ICommand NavigateToRegisterCommand { get; }
        public ICommand NavigateToHomeAdminCommand { get; }
        public ICommand NavigateToHomeUserCommand { get; }

        #endregion

        #region SignIn
        private string _email;
        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }
        private string _password;
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        #region AdminSignIn
        public ICommand AdminSignInCommand { get; }

        private bool CanAdminSignIn(object o) => true;
        private void OnAdminSignIn(object o)
        {
            try
            {
                User user = new User();
                user = unitOfWork.UserRepository.Find(Email);

                if(user == null)
                {
                    throw new Exception("Неверный логин");
                }

                if (Encryption.Dencrypt(user.Password) != Password)
                {
                    throw new Exception("Неверный пароль");
                }
                if(user.IsAdmin!=true)
                {
                    throw new Exception("Вы не являетесь администратором");
                }
                CurrentUser = user;
                NavigateToHomeAdminCommand.Execute(user);

            }
            catch(Exception ex)
            {
                var result = new CustomMessageBox(ex.Message,
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
            }
           
        }

        #endregion

        #region UserSignIn
        public ICommand UserSignInCommand { get; }

        private bool CanUserSignIn(object o) => true;
        private void OnUserSignIn(object o)
        {
            try
            {
                User user = new User();
                user = unitOfWork.UserRepository.Find(Email);

                if (user == null)
                {
                    throw new Exception("Неверный логин");
                }

                if (Encryption.Dencrypt(user.Password) != Password)
                {
                    throw new Exception("Неверный пароль");
                }
                
                CurrentUser = user;
                NavigateToHomeUserCommand.Execute(user);

            }
            catch (Exception ex)
            {
                var result = new CustomMessageBox(ex.Message,
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
            }

        }
        #endregion
        #endregion











        //private static LoginWindowViewModel _instance;
        //public static LoginWindowViewModel Instance => _instance??(_instance = new LoginWindowViewModel())






    }
}
