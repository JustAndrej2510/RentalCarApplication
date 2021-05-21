using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RentalCarApplication.Commands;
using RentalCarApplication.Base;
using RentalCarApplication.EntityFramework;
using RentalCarApplication.Core.Model;
using System.Windows;
using RentalCarApplication.Infrastructure;
using RentalCarApplication.View.CustomMessageBox;

namespace RentalCarApplication.ViewModel
{
    public class RegisterWindowViewModel : ViewModelBase
    {
        private UnitOfWork unitOfWork;
        public RegisterWindowViewModel(Navigator navigator)
        {
            NavigateToLoginCommand = new NavigationCommand<LoginWindowViewModel>(navigator, () => new LoginWindowViewModel(navigator));
            RegistrationUserCommand = new RelayCommand(OnRegisterUser, CanRegisterUser);
            unitOfWork = new UnitOfWork();
        }

        #region Navigation commands
        public ICommand NavigateToLoginCommand { get; }
        #endregion

        #region User registration

        private string _name;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _surname;
        public string Surname
        {
            get => _surname;
            set => Set(ref _surname, value);
        }

        private string _passport;
        public string Passport
        {
            get => _passport;
            set => Set(ref _passport, value);
        }

        private string _driverLicense;
        public string DriverLicense
        {
            get => _driverLicense;
            set => Set(ref _driverLicense, value);
        }

        private string _telNumber;
        public string TelNumber
        {
            get => _telNumber;
            set => Set(ref _telNumber, value);
        }

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

        private string _passwordCheck;
        public string PasswordCheck
        {
            get => _passwordCheck;
            set => Set(ref _passwordCheck, value);
        }

        public ICommand RegistrationUserCommand { get; }

        private bool CanRegisterUser(object o)
        {
            return true;
        }

        private void OnRegisterUser(object o)
        {
            try
            {
                if (PasswordCheck != Password)
                {
                    throw new Exception("Пароли не совпадают");
                }
                if(unitOfWork.UserRepository.Find(Email)!=null)
                {
                    throw new Exception("Пользователь с такой почтой уже существует");
                }
                if (unitOfWork.UserRepository.CheckPassportAndLicense(Passport,DriverLicense)!=true)
                {
                    throw new Exception("Пользователь с такими паспортными данными уже существует");
                }

                User user = new User()
                {
                    Email = this.Email,
                    Password = this.Password,
                    Name = this.Name,
                    Surname = this.Surname,
                    Passport = this.Passport,
                    DriverLicense = this.DriverLicense,
                    TelNumber = this.TelNumber,
                    IsAdmin = false
                };

                if(Validation.CheckValid(user))
                {
                    user.Password = Encryption.Encrypt(user.Password);
                    unitOfWork.UserRepository.Create(user);
                    unitOfWork.Save();
                    var result = new CustomMessageBox("Регистрация пройдена успешно",
                                    MessageType.Success,
                                    MessageButtons.Ok).ShowDialog();
                    NavigateToLoginCommand.Execute(user);
                }
               
            }
            catch (Exception ex)
            {
                var result = new CustomMessageBox(ex.Message,
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
            }


        }
        #endregion


    }
}
