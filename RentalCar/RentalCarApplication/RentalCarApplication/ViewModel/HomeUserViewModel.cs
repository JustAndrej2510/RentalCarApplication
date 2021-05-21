using System;
using System.Collections.Generic;
using System.Linq;
using RentalCarApplication.Base;
using RentalCarApplication.ViewModel;
using System.Windows;
using System.Windows.Input;
using RentalCarApplication.Infrastructure;
using RentalCarApplication.EntityFramework;
using RentalCarApplication.Core.Model;
using RentalCarApplication.Commands;
using RentalCarApplication.View.CustomMessageBox;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace RentalCarApplication.ViewModel
{
    public class HomeUserViewModel : ViewModelBase
    {
        UnitOfWork unitOfWork;
        public HomeUserViewModel(Navigator navigator)
        {
            unitOfWork = new UnitOfWork();
            CurrentUser = LoginWindowViewModel.CurrentUser;
            if (CurrentUser != null)
            {
                UserName = CurrentUser.Name;
                UserSurname = CurrentUser.Surname;
                UserEmail = CurrentUser.Email;
                UserDriverLicense = CurrentUser.DriverLicense;
                UserPassport = CurrentUser.Passport;
                UserTelNumber = CurrentUser.TelNumber;
                UserPassword = CurrentUser.Password;
            }
            LogoutCommand = new NavigationCommand<LoginWindowViewModel>(navigator, () => new LoginWindowViewModel(navigator));
            AboutCommand = new RelayCommand(OnAboutCommandExecuted, CanAboutCommandExecute);
            //OrderTab
            CurrentStartDate = DateTime.Today;
            CurrentEndDate = CurrentStartDate.AddMonths(1);
            ReturnStartDate = CurrentStartDate.AddDays(1);
            ReturnEndDate = CurrentEndDate.AddMonths(1);
            PlaceOrderCommand = new RelayCommand(OnPlaceOrderExecuted, CanPlaceOrderExecute);
            RefreshOrderTabCommand = new RelayCommand(OnRefreshOrderTabExecuted, CanRefreshOrderTabExecute);
            SearchCarCommand = new RelayCommand(OnSearchCarExecuted, CanSearchCarExecute);
            ClearSearchFieldsCommand = new RelayCommand(OnClearSearchFieldsExecuted, CanClearSearchFieldsExecute);
            DisplayCars();

            //CabinetTab
            ChangePasswordCommand = new RelayCommand(OnChangePasswordExecuted, CanChangePasswordExecute);

            //OrdersBasket
            RefreshOrdersCommand = new RelayCommand(OnRefreshOrdersExecuted, CanRefreshOrdersExecute);
            CancelOrderCommand = new RelayCommand(OnCancelOrderExecuted, CanCancelOrderExecute);
            DisplayOrders();
        }

        #region PopupMenuCommands
       
        public ICommand LogoutCommand { get; }
        public ICommand AboutCommand { get; }
        private bool CanAboutCommandExecute(object o) => true;
        private void OnAboutCommandExecuted(object o)
        {
            try
            {

                var p = new Process();
                p.StartInfo = new ProcessStartInfo(@"D:\Учеба\2 семестр\OOP\Курсач\Пояснительная_записка_(Писарик).docx")
                {
                    UseShellExecute = true
                };
                p.Start();


            }
            catch (Exception ex)
            {
                var result = new CustomMessageBox(ex.Message,
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
            }
        }
        #endregion


        #region CurrentUser
        User CurrentUser = new User();
        private string _currentUserName;
        public string CurrentUserName
        {
            get => _currentUserName;
            set => Set(ref _currentUserName, value);
        }

        private string _currentUserSurname;
        public string CurrentUserSurname
        {
            get => _currentUserSurname;
            set => Set(ref _currentUserSurname, value);
        }
        #endregion

        #region OrderTab

        #region RefreshOrderTab
        public ICommand RefreshOrderTabCommand { get; }
        private bool CanRefreshOrderTabExecute(object o) => true;
        private void OnRefreshOrderTabExecuted(object o)
        {
            try
            {
                DisplayCars();
                ClearOrderFields();
            }
            catch(Exception ex)
            {
                var result = new CustomMessageBox(ex.Message,
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();

            }
           
        }
        #endregion

        #region CarProperties
        private string _carNumber;
        public string CarNumber
        {
            get => _carNumber;
            set => Set(ref _carNumber, value);

        }

        private string _carModel;
        public string CarModel
        {
            get => _carModel;
            set => Set(ref _carModel, value);
        }

        private string _carEngine;
        public string CarEngine
        {
            get => _carEngine;
            set => Set(ref _carEngine, value);

        }

        private string _carGearBox;
        public string CarGearBox
        {
            get => _carGearBox;
            set => Set(ref _carGearBox, value);
        }

        private string _carBody;
        public string CarBody
        {
            get => _carBody;
            set => Set(ref _carBody, value);
        }

        private string _carSeats;
        public string CarSeats
        {
            get => _carSeats;
            set => Set(ref _carSeats, value);
        }

        private string _carConsumption;
        public string CarConsumption
        {
            get => _carConsumption;
            set => Set(ref _carConsumption, value);
        }

        private string _carPrice;
        public string CarPrice
        {
            get => _carPrice;
            set
            {
                Set(ref _carPrice, value);
                CalculateOrderPrice();
            }
        }





        #endregion

        #region SelectedCar
        private Car _selectedCar;
        public Car SelectedCar
        {
            get => _selectedCar;
            set
            {

                Set(ref _selectedCar, value);
                SelectionCarChanged();
            }
        }
        private void SelectionCarChanged()
        {
            if (SelectedCar != null)
            {
                CarNumber = SelectedCar.CarId.ToString();
                CarBody = SelectedCar.BodyType;
                CarEngine = SelectedCar.EngineCapacity.ToString();
                CarGearBox = SelectedCar.GearBox;
                CarConsumption = SelectedCar.Consumption.ToString();
                CarModel = SelectedCar.Brand;
                CarSeats = SelectedCar.Seats.ToString();
                CarPrice = SelectedCar.Price.ToString();
            }

        }
        #endregion

        #region DisplayAllCars
        private List<Car> _carList;
        public List<Car> CarList
        {
            get => _carList;
            set => Set(ref _carList, value);
        }
        private void DisplayCars()
        {
            List<string> temp = new List<string>();
            CarList = (List<Car>)unitOfWork.CarRepository.FindAll();
            SearchBrandList = new List<string>();
           
            if(CarList.Count!=0)
            {
                foreach (var n in CarList)
                {
                    if (!temp.Contains(n.Brand))
                        temp.Add(n.Brand);
                }
            }
            SearchBrandList = temp;
        }
        #endregion

        #region OrderProperties

        public DateTime CurrentStartDate { get; set; }
        public DateTime CurrentEndDate { get; set; }

        private DateTime _returnEndDate;
        public DateTime ReturnEndDate
        {
            get => _returnEndDate;
            set => Set(ref _returnEndDate, value);
        }


        private DateTime _returnStartDate;
        public DateTime ReturnStartDate
        {
            get => _returnStartDate;
            set => Set(ref _returnStartDate, value);
        }


        private DateTime _rentDate = DateTime.Today;
        public DateTime RentDate
        {
            get => _rentDate;
            set
            {
                Set(ref _rentDate, value);
                ReturnStartDate = value.AddDays(1);
                ReturnEndDate = value.AddMonths(1);
                ReturnDate = value.AddDays(1);
                CalculateOrderPrice();
            }
        }

        private DateTime _returnDate = DateTime.Today.AddDays(1);
        public DateTime ReturnDate
        {
            get => _returnDate;
            set
            {
                Set(ref _returnDate, value);
                CalculateOrderPrice();
            }

        }

        private string _orderCity;
        public string OrderCity
        {
            get => _orderCity;
            set => Set(ref _orderCity, value);
        }

        private string _orderPrice;
        public string OrderPrice
        {
            get => _orderPrice;
            set => Set(ref _orderPrice, value);
        }


        #endregion

        #region CalculateOrderPrice
        public void CalculateOrderPrice()
        {
            if (CarPrice != null && RentDate != null && ReturnDate != null)
            {
                OrderPrice = (Convert.ToDouble(CarPrice) * Convert.ToDouble((ReturnDate - RentDate).Duration().Days)).ToString();
            }
        }
        #endregion

        #region ClearOrderFieldsMethod
        public void ClearOrderFields()
        {
            CarNumber = "";
            CarBody = "";
            CarModel = "";
            CarEngine = "";
            CarConsumption = "";
            CarGearBox = "";
            CarSeats = "";
            CarPrice = null;
            SelectedCar = null;
            RentDate = DateTime.Today;
            OrderCity = "";
            OrderPrice = "";

        }
        #endregion

        #region PlaceOrder
        public ICommand PlaceOrderCommand { get; }
        private bool CanPlaceOrderExecute(object o) => true;
        private void OnPlaceOrderExecuted(object o)
        {
            try
            {

                Order order = new Order();
                if (SelectedCar == null)
                {
                    throw new Exception("Выберите автомобиль");
                }
                if (OrderPrice == null)
                {
                    throw new Exception("Выберите автомобиль");
                }
                order.CarId = Convert.ToInt32(CarNumber);
                order.Email = CurrentUser.Email;
                order.City = OrderCity;
                order.RentDate = RentDate;
                order.ReturnDate = ReturnDate;
                order.Price = Convert.ToDouble(OrderPrice);
                order.Status = null;

                if (Validation.CheckValid(order))
                {
                    if(WaitingOrders.Where(x=>x.Email == CurrentUser.Email && x.Status == null).Any())
                    {
                        throw new Exception("Дождитесь пока администратор обработает ваш предыдущий заказ");
                    }
                    unitOfWork.OrderRepository.Create(order);
                    unitOfWork.Save();
                    RefreshOrderTabCommand.Execute(order);
                    RefreshOrdersCommand.Execute(order);
                    var result = new CustomMessageBox("Ваш заказ принят.\nПерейдите в корзину, чтобы следить за статусом заказа",
                                     MessageType.Success,
                                     MessageButtons.Ok).ShowDialog();

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

        #region SearchCar

        #region SearchCarCommand
        public ICommand SearchCarCommand { get; }
        private bool CanSearchCarExecute(object o) => true;
        private void OnSearchCarExecuted(object o)
        {
            try
            {
                //List<Car> _carList = new List<Car>();
                List<Car> _tempCarList = new List<Car>();
                List<Car> _tempList = new List<Car>();

                //_carList.AddRange(CarList);
                _tempCarList = (List<Car>)unitOfWork.CarRepository.FindAll();
                if (!String.IsNullOrEmpty(SearchBrand))
                {
                    _tempList.Clear();
                    foreach (var n in _tempCarList)
                    {
                        if (Regex.IsMatch(n.Brand, SearchBrand, RegexOptions.IgnoreCase))
                        {
                            _tempList.Add(n);
                        }
                    }
                    _tempCarList.Clear();
                    _tempCarList.AddRange(_tempList);
                }

                if (!String.IsNullOrEmpty(SearchBodyType))
                {
                    _tempList.Clear();
                    foreach (var n in _tempCarList)
                    {
                        if (Regex.IsMatch(n.BodyType, SearchBodyType, RegexOptions.IgnoreCase))
                        {
                            _tempList.Add(n);
                        }
                    }
                    _tempCarList.Clear();
                    _tempCarList.AddRange(_tempList);
                }

                if (!String.IsNullOrEmpty(SearchSeats))
                {
                    _tempList.Clear();
                    foreach (var n in _tempCarList)
                    {
                        if (Regex.IsMatch(n.Seats.ToString(), SearchSeats, RegexOptions.IgnoreCase))
                        {
                            _tempList.Add(n);
                        }
                    }
                    _tempCarList.Clear();
                    _tempCarList.AddRange(_tempList);
                }

                if (!String.IsNullOrEmpty(SearchGearBox))
                {
                    _tempList.Clear();
                    foreach (var n in _tempCarList)
                    {
                        if (Regex.IsMatch(n.GearBox, SearchGearBox, RegexOptions.IgnoreCase))
                        {
                            _tempList.Add(n);
                        }
                    }
                    _tempCarList.Clear();
                    _tempCarList.AddRange(_tempList);
                }

                if (!String.IsNullOrEmpty(SearchPriceFrom))
                {
                    _tempList.Clear();
                    foreach (var n in _tempCarList)
                    {
                        if ((n.Price >= Convert.ToDouble(SearchPriceFrom)))
                        {
                            _tempList.Add(n);
                        }
                    }
                    _tempCarList.Clear();
                    _tempCarList.AddRange(_tempList);
                }

                if (!String.IsNullOrEmpty(SearchPriceTo))
                {
                    _tempList.Clear();
                    foreach (var n in _tempCarList)
                    {
                        if ((n.Price <= Convert.ToDouble(SearchPriceTo)))
                        {
                            _tempList.Add(n);
                        }
                    }
                    _tempCarList.Clear();
                    _tempCarList.AddRange(_tempList);
                }
                _tempCarList.Clear();
                _tempCarList.AddRange(_tempList);

                if (_tempCarList.Count == 0)
                {
                    throw new Exception("Автомобилей с такими параметрами не найдено");
                }
                else
                {
                    CarList = _tempCarList;
                }

            }
            catch (Exception ex)
            {
                var result = new CustomMessageBox(ex.Message,
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
                DisplayCars();
            }
        }
        #endregion

        #region SearchProperties

        #region SearchBrandFields

        //public ICommand SearchBrandFocusChangedCommand { get; }
        //private bool CanSearchBrandFocusChangedExecute(object o) => true;
        //private void OnSearchBrandFocusChangedExecuted(object o)
        //{
        //    if(SearchBrandFocus == false)
        //        SearchBrandFocus = true;
        //    else
        //        SearchBrandFocus = false;
        //}

        //private bool _searchBrandFocus = false;
        //public bool SearchBrandFocus
        //{
        //    get => _searchBrandFocus;
        //    set => Set(ref _searchBrandFocus, value);
        //}



        #endregion


        private List<string> _searchBrandList;
        public List<string> SearchBrandList
        {
            get => _searchBrandList;
            set => Set(ref _searchBrandList, value);
        }

        private string _searchBrand;
        public string SearchBrand
        {
            get => _searchBrand;
            set => Set(ref _searchBrand, value);
        }

        private string _searchGearBox;
        public string SearchGearBox
        {
            get => _searchGearBox;
            set => Set(ref _searchGearBox, value);
        }

        private string _searchBodyType;
        public string SearchBodyType
        {
            get => _searchBodyType;
            set => Set(ref _searchBodyType, value);
        }

        private string _searchSeats;
        public string SearchSeats
        {
            get => _searchSeats;
            set => Set(ref _searchSeats, value);
        }

        private string _searchPriceFrom;
        public string SearchPriceFrom
        {
            get => _searchPriceFrom;
            set => Set(ref _searchPriceFrom, value);
        }

        private string _searchPriceTo;
        public string SearchPriceTo
        {
            get => _searchPriceTo;
            set => Set(ref _searchPriceTo, value);
        }

        #endregion

        #region ClearSearchFields
        public ICommand ClearSearchFieldsCommand { get; }
        private bool CanClearSearchFieldsExecute(object o) => true;
        private void OnClearSearchFieldsExecuted(object o)
        {
            SearchBrand = "";
            SearchGearBox = "";
            SearchSeats = "";
            SearchBodyType = "";
            SearchPriceFrom = "";
            SearchPriceTo = "";
            //SearchBrandList.Clear();
            DisplayCars();
        }
        #endregion

        #endregion

        #endregion

        #region CabinetTab

        #region CabinetProperties
        private string _userName;
        public string UserName
        {
            get => _userName;
            set => Set(ref _userName, value);
        }

        private string _userSurname;
        public string UserSurname
        {
            get => _userSurname;
            set => Set(ref _userSurname, value);
        }

        private string _userPassport;
        public string UserPassport
        {
            get => _userPassport;
            set => Set(ref _userPassport, value);
        }

        private string _userDriverLicense;
        public string UserDriverLicense
        {
            get => _userDriverLicense;
            set => Set(ref _userDriverLicense, value);
        }

        private string _userTelNumber;
        public string UserTelNumber
        {
            get => _userTelNumber;
            set => Set(ref _userTelNumber, value);
        }

        private string _userEmail;
        public string UserEmail
        {
            get => _userEmail;
            set => Set(ref _userEmail, value);
        }

        private string _userPassword;
        public string UserPassword
        {
            get => _userPassword;
            set => Set(ref _userPassword, value);
        }

        private string _userConfirmPassword;
        public string UserConfirmPassword
        {
            get => _userConfirmPassword;
            set => Set(ref _userConfirmPassword, value);
        }

        private string _userNewPassword;
        public string UserNewPassword
        {
            get => _userNewPassword;
            set => Set(ref _userNewPassword, value);
        }

        private string _userShownNewPassword;
        public string UserShownNewPassword
        {
            get => _userShownNewPassword;
            set => Set(ref _userShownNewPassword, value);
        }

        //private string _userRole;
        //public string UserRole
        //{
        //    get => _userRole;
        //    set => Set(ref _userRole, value);
        //}


        #endregion

        public ICommand ChangePasswordCommand { get; }

        private bool CanChangePasswordExecute(object o) => true;
        private void OnChangePasswordExecuted(object o)
        {
            try
            {
                User user = CurrentUser;
 
                if(Encryption.Dencrypt(UserPassword) != UserConfirmPassword)
                {
                    throw new Exception("Текущий пароль не верный");
                }

                user.Password = UserNewPassword;
                if(Validation.CheckValid(user))
                {
                    CurrentUser.Password = Encryption.Encrypt(UserNewPassword);
                    unitOfWork.UserRepository.Update(CurrentUser.Email, CurrentUser);
                    unitOfWork.Save();
                    //CurrentUser.Password = Encryption.Dencrypt(CurrentUser.Password); ;
                    UserPassword = CurrentUser.Password;
                    var result = new CustomMessageBox("Пароль успешно изменен",
                                     MessageType.Success,
                                     MessageButtons.Ok).ShowDialog();
                }
                
               
            }
            catch(Exception ex)
            {
                var result = new CustomMessageBox(ex.Message,
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
            }
        }




        #endregion

        #region OrderBasketTab

        #region Properties

        private List<Order> _allOrders;
        public List<Order> AllOrders
        {
            get => _allOrders;
            set => Set(ref _allOrders, value);
        }

        private List<Order> _waitingOrders;
        public List<Order> WaitingOrders
        {
            get => _waitingOrders;
            set => Set(ref _waitingOrders, value);
        }

        private List<Order> _confirmedOrders;
        public List<Order> ConfirmedOrders
        {
            get => _confirmedOrders;
            set => Set(ref _confirmedOrders, value);
        }

        private List<Order> _canceledOrders;
        public List<Order> CanceledOrders
        {
            get => _canceledOrders;
            set => Set(ref _canceledOrders, value);
        }

        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set => Set(ref _selectedOrder, value);
        }

        #endregion

        #region DisplayOrdersMethod
        private void DisplayOrders()
        {
            AllOrders = (List<Order>)unitOfWork.OrderRepository.FindAll();
            WaitingOrders = AllOrders.Where(x => x.Email == CurrentUser.Email && x.Status == null).ToList();
            ConfirmedOrders = AllOrders.Where(x => x.Email == CurrentUser.Email && x.Status == true).ToList();
            CanceledOrders = AllOrders.Where(x => x.Email == CurrentUser.Email && x.Status == false).ToList();

        }

        #endregion

        #region RefreshOrders
        public ICommand RefreshOrdersCommand { get; }

        private bool CanRefreshOrdersExecute(object o) => true;
        private void OnRefreshOrdersExecuted(object o)
        {
            DisplayOrders();
        }
        #endregion

        #region CancelOrder
        
        public ICommand CancelOrderCommand { get; }

        private bool CanCancelOrderExecute(object o) => true;
        private void OnCancelOrderExecuted(object o)
        {
            
            var result = new CustomMessageBox("Вы уверены, что хотите отменить свой заказ?",
                                     MessageType.Confirmation,
                                     MessageButtons.YesNo).ShowDialog();
            if(result == true)
            {
                Order order = SelectedOrder;
                order.Status = false;
                unitOfWork.OrderRepository.Update(order.OrderId, order);
                unitOfWork.Save();
                DisplayOrders();
                var res = new CustomMessageBox("Ваш заказ отменен и перемещен во вкладку \"Отмененные\" ",
                                     MessageType.Info,
                                     MessageButtons.Ok).ShowDialog();
            }
            
        }

        #endregion

        #endregion

    }

}
