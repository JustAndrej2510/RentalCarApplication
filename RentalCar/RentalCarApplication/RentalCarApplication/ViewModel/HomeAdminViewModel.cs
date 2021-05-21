using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using RentalCarApplication.View;
using RentalCarApplication.Base;
using RentalCarApplication.Infrastructure;
using RentalCarApplication.Core.Model;
using System.Windows.Input;
using RentalCarApplication.Commands;
using System.Diagnostics;
using RentalCarApplication.EntityFramework;
using Microsoft.Win32;
using System.IO;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;
using RentalCarApplication.View.CustomMessageBox;

namespace RentalCarApplication.ViewModel
{
    public class HomeAdminViewModel : ViewModelBase
    {
        UnitOfWork unitOfWork;
        public HomeAdminViewModel(Navigator navigator)
        {
            unitOfWork = new UnitOfWork();
            CurrentUser = LoginWindowViewModel.CurrentUser;
            if (CurrentUser != null)
            {
                CurrentUserName = CurrentUser.Name;
                CurrentUserSurname = CurrentUser.Surname;
            }
            //Cars
            LogoutCommand = new NavigationCommand<LoginWindowViewModel>(navigator, () => new LoginWindowViewModel(navigator));
            //NavigateToPersonalCabinetCommand = new NavigationCommand<AdminPersonalCabinetViewModel>(navigator, () => new AdminPersonalCabinetViewModel(navigator));
            AboutCommand = new RelayCommand(OnAboutCommandExecuted, CanAboutCommandExecute);
            AddCarPhotoCommand = new RelayCommand(OnAddCarPhotoCommandExecuted, CanAddCarPhotoCommandExecute);
            AddCarCommand = new RelayCommand(OnAddCarCommandExecuted, CanAddCarCommandExecute);
            ClearCarFieldsCommand = new RelayCommand(OnClearCarFieldsExecuted, CanClearCarFieldsExecute);
            UpdateCarCommand = new RelayCommand(OnUpdateCarExecuted, CanUpdateCarExecute);
            DeleteCarCommand = new RelayCommand(OnDeleteCarExecuted, CanDeleteCarExecute);
            FindCarByNumberCommand = new RelayCommand(OnFindCarByNumberExecuted, CanFindCarByNumberExecute);
            RefreshCarListCommand = new RelayCommand(OnRefreshCarListExecuted, CanRefreshCarListExecute);
            DisplayCars();

            //Orders
            ConfirmOrderCommand = new RelayCommand(OnConfirmOrderExecuted, CanConfirmOrderExecute);
            CancelOrderCommand = new RelayCommand(OnCancelOrderExecuted, CanCancelOrderExecute);
            RefreshOrdersCommand = new RelayCommand(OnRefreshOrdersExecuted, CanRefreshOrdersExecute);
            FindOrderByNumberCommand = new RelayCommand(OnFindOrderByNumberExecuted, CanFindOrderByNumberExecute);
            DisplayOrders();

            //Users
            FindUserByEmailCommand = new RelayCommand(OnFindUserByEmailExecuted, CanFindUserByEmailExecute);
            ChangeSelectedUserRoleCommand = new RelayCommand(OnChangeSelectedUserRoleExecuted, CanChangeSelectedUserRoleExecute);
            DeleteSelectedUserCommand = new RelayCommand(OnDeleteSelectedUserExecuted, CanDeleteSelectedUserExecute);
            DisplayUsers();
        }

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

        #region PopupMenuCommands
        public ICommand NavigateToPersonalCabinetCommand { get; }
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


        #region CarsTab
        #region CarBuilder
        public void CarBuilder(Car car)
        {
            int tempInt;
            double tempDouble;
            if(int.TryParse(CarNumber, out tempInt))
            {
                car.CarId = Convert.ToInt32(CarNumber);
            }
            else
            {
                throw new InvalidCastException("Неверный формат номера");
            }

            if(double.TryParse(CarEngine, out tempDouble))
            {
                car.EngineCapacity = Convert.ToDouble(CarEngine);
            }
            else
            {
                throw new InvalidCastException("Неверный формат объема двигателя");
            }

            if (double.TryParse(CarConsumption, out tempDouble))
            {
                car.Consumption = Convert.ToDouble(CarConsumption);
            }
            else
            {
                throw new InvalidCastException("Неверный формат расхода топливаa");
            }

            if (double.TryParse(CarPrice, out tempDouble))
            {
                car.Price = Convert.ToDouble(CarPrice);
            }
            else
            {
                throw new InvalidCastException("Неверный формат цены");
            }
            car.Seats = Convert.ToInt32(CarSeats);
            car.Brand = CarModel;
            car.BodyType = CarBody;
            car.GearBox = CarGearBox;
            car.PhotoPath = CarPhoto;
            Validation.CheckValid(car);
            
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
            set => Set(ref _carPrice, value);
        }

        private string _carPhoto;
        public string CarPhoto
        {
            get => _carPhoto;
            set => Set(ref _carPhoto, value);
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
            CarList = (List<Car>)unitOfWork.CarRepository.FindAll();
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
            if(SelectedCar!=null)
            {
                CarNumber = SelectedCar.CarId.ToString();
                CarBody = SelectedCar.BodyType;
                CarEngine = SelectedCar.EngineCapacity.ToString();
                CarGearBox = SelectedCar.GearBox;
                CarConsumption = SelectedCar.Consumption.ToString();
                CarModel = SelectedCar.Brand;
                CarSeats = SelectedCar.Seats.ToString();
                CarPhoto = SelectedCar.PhotoPath;
                CarPrice = SelectedCar.Price.ToString();
            }
            
        }
        #endregion

        #region ClearCarFields
        #region ClearCarFieldsMethod
        public void ClearCarFields()
        {
            CarNumber = "";
            CarBody = "";
            CarModel = "";
            CarEngine = "";
            CarConsumption = "";
            CarGearBox = "";
            CarPhoto = "";
            CarSeats = "";
            CarPrice = "";
            SelectedCar = null;
        }
        #endregion
        public ICommand ClearCarFieldsCommand { get; }

        private bool CanClearCarFieldsExecute(object o) => true;
        private void OnClearCarFieldsExecuted(object o)
        {
            ClearCarFields();
        }
        #endregion

        #region AddCar
        public ICommand AddCarCommand { get; }
        public ICommand AddCarPhotoCommand { get; }
        private bool CanAddCarCommandExecute(object o) => true;
        private void OnAddCarCommandExecuted(object o)
        {
            Car car = new Car();
            try
            {
                CarBuilder(car);
                if (unitOfWork.CarRepository.Find(car.CarId)!=null)
                {
                    throw new Exception("Машина с таким номером уже существует");
                }
                unitOfWork.CarRepository.Create(car);
                unitOfWork.Save();
                DisplayCars();
                ClearCarFieldsCommand.Execute(car);
            }
            catch(Exception ex)
            {
                var result = new CustomMessageBox(ex.Message,
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
            }
        }

        private bool CanAddCarPhotoCommandExecute(object o) => true;
        private void OnAddCarPhotoCommandExecuted(object o)
        {
           
            try
            {
                OpenFileDialog ofdPicture = new OpenFileDialog();
                ofdPicture.Filter =
                    "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif";
                ofdPicture.FilterIndex = 1;

                if (ofdPicture.ShowDialog() == true)
                {
                    CarPhoto = ofdPicture.FileName;

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

        #region UpdateCar

        public ICommand UpdateCarCommand { get; }
        private bool CanUpdateCarExecute(object o) => true;
        private void OnUpdateCarExecuted(object o)
        {
            Car car = new Car();
            try
            {
                CarBuilder(car);
                if (unitOfWork.CarRepository.Find(car.CarId) == null)
                {
                    throw new Exception("Машины с таким номером не существует в базе данных.");
                }
                unitOfWork.CarRepository.Update(car.CarId, car);
                unitOfWork.Save();
                DisplayCars();
                //ClearCarFieldsCommand.Execute(car);
            }
            catch (Exception ex)
            {
                var result = new CustomMessageBox(ex.Message,
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
            }
        }
        #endregion

        #region DeleteCar
        public ICommand DeleteCarCommand { get; }
        private bool CanDeleteCarExecute(object o) => true;
        private void OnDeleteCarExecuted(object o)
        {
            Car car = new Car();
            try
            {
                CarBuilder(car);
                if (unitOfWork.CarRepository.Find(car.CarId) == null)
                {
                    throw new Exception("Машины с таким номером не существует в базе данных.");
                }
                unitOfWork.CarRepository.Delete(car.CarId);
                unitOfWork.Save();
                DisplayCars();
                ClearCarFieldsCommand.Execute(car);
            }
            catch (Exception ex)
            {
                var result = new CustomMessageBox(ex.Message,
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
            }
        }
        #endregion

        #region FindByNumber
        private string _findingCarNumber;
        public string FindingCarNumber
        {
            get => _findingCarNumber;
            set => Set(ref _findingCarNumber, value);
        }


        public ICommand FindCarByNumberCommand { get; }
        private bool CanFindCarByNumberExecute(object o) => true;
        private void OnFindCarByNumberExecuted(object o)
        {
            try
            {
                
                if (!String.IsNullOrEmpty(FindingCarNumber))
                {
                    List<Car> searchTempList = new List<Car>();
                   searchTempList = (List<Car>)unitOfWork.CarRepository.FindAll();
                    var searchList = searchTempList.Where(n => Regex.IsMatch(n.CarId.ToString(), FindingCarNumber));
                    if (searchList == null)
                        throw new InvalidDataException("Элементов не найдено");
                    CarList = searchList.ToList();
                }
                else
                {
                    DisplayCars();
                }
            }
            catch(InvalidDataException ex)
            {
                var result = new CustomMessageBox(ex.Message,
                                    MessageType.Info,
                                    MessageButtons.Ok).ShowDialog();
            }
            catch(Exception ex)
            {
                var result = new CustomMessageBox(ex.Message,
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
            }
        }
        #endregion

        #region RefreshCarList

        public ICommand RefreshCarListCommand { get; }
        private bool CanRefreshCarListExecute(object o) => true;
        private void OnRefreshCarListExecuted(object o)
        {
            try
            {
                DisplayCars();
                ClearCarFields();
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

        #region OrdersTab

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
            WaitingOrders = AllOrders.Where(x => x.Status == null).ToList();
            ConfirmedOrders = AllOrders.Where(x => x.Status == true).ToList();
            CanceledOrders = AllOrders.Where(x => x.Status == false).ToList();
        }

        #endregion

        #region ConfirmOrder
        public ICommand ConfirmOrderCommand { get; }

        private bool CanConfirmOrderExecute(object o) => true;
        private void OnConfirmOrderExecuted(object o)
        {
            var result = new CustomMessageBox("Вы уверены, что хотите принять этот заказ?",
                                    MessageType.Confirmation,
                                    MessageButtons.YesNo).ShowDialog();
            try
            {
                if (result == true)
                {
                    Order order = SelectedOrder;
                    order.Status = true;
                    unitOfWork.OrderRepository.Update(order.OrderId, order);
                    unitOfWork.Save();
                    DisplayOrders();
                    var res = new CustomMessageBox("Заказ принят и перемещен во вкладку \"Подтвержденные\" ",
                                         MessageType.Info,
                                         MessageButtons.Ok).ShowDialog();
                }
            }
            catch(Exception ex)
            {
                var res = new CustomMessageBox(ex.Message,
                                         MessageType.Error,
                                         MessageButtons.Ok).ShowDialog();
            }
            
        }
        #endregion

        #region CancelOrder

        public ICommand CancelOrderCommand { get; }

        private bool CanCancelOrderExecute(object o) => true;
        private void OnCancelOrderExecuted(object o)
        {

            var result = new CustomMessageBox("Вы уверены, что хотите отменить этот заказ?",
                                    MessageType.Confirmation,
                                    MessageButtons.YesNo).ShowDialog();
            if (result == true)
            {
                Order order = SelectedOrder;
                order.Status = false;
                unitOfWork.OrderRepository.Update(order.OrderId, order);
                unitOfWork.Save();
                DisplayOrders();
                var res = new CustomMessageBox("Заказ отменен и перемещен во вкладку \"Отмененные\" ",
                                     MessageType.Info,
                                     MessageButtons.Ok).ShowDialog();
            }

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

        #region FindOrderByNumber
        private string _findingOrderNumber;
        public string FindingOrderNumber
        {
            get => _findingOrderNumber;
            set => Set(ref _findingOrderNumber, value);
        }


        public ICommand FindOrderByNumberCommand { get; }
        private bool CanFindOrderByNumberExecute(object o) => true;
        private void OnFindOrderByNumberExecuted(object o)
        {
            try
            {

                if (!String.IsNullOrEmpty(FindingOrderNumber))
                {
                    List<Order> searchTempList = new List<Order>();
                    searchTempList = (List<Order>)unitOfWork.OrderRepository.FindAll();
                    var searchList = searchTempList.Where(n => Regex.IsMatch(n.OrderId.ToString(), FindingOrderNumber) && n.Status==null);
                    if (searchList == null)
                        throw new InvalidDataException("Заказов не найдено");
                    WaitingOrders = searchList.ToList();
                }
                else
                {
                    DisplayOrders();
                }
            }
            catch (InvalidDataException ex)
            {
                var result = new CustomMessageBox(ex.Message,
                                    MessageType.Info,
                                    MessageButtons.Ok).ShowDialog();
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

        #region UsersTab

        #region DisplayUsers
        private List<User> _userList;
        public List<User> UserList
        {
            get => _userList;
            set => Set(ref _userList, value);
        }
        private void DisplayUsers()
        {
            UserList = (List<User>)unitOfWork.UserRepository.FindAll();
        }
        #endregion

        #region SelectedUser

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set => Set(ref _selectedUser, value);
        }

        #endregion

        #region FindUserByEmail
        private string _findingUserEmail;
        public string FindingUserEmail
        {
            get => _findingUserEmail;
            set => Set(ref _findingUserEmail, value);
        }


        public ICommand FindUserByEmailCommand { get; }
        private bool CanFindUserByEmailExecute(object o) => true;
        private void OnFindUserByEmailExecuted(object o)
        {
            try
            {

                if (!String.IsNullOrEmpty(FindingUserEmail))
                {
                    List<User> searchTempList = new List<User>();
                    searchTempList = (List<User>)unitOfWork.UserRepository.FindAll();
                    var searchList = searchTempList.Where(n => Regex.IsMatch(n.Email.ToString(), FindingUserEmail));
                    if (searchList == null)
                        throw new InvalidDataException("Элементов не найдено");
                    UserList = searchList.ToList();
                }
                else
                {
                    DisplayUsers();
                }
            }
            catch (InvalidDataException ex)
            {
                var result = new CustomMessageBox(ex.Message,
                                    MessageType.Info,
                                    MessageButtons.Ok).ShowDialog();
            }
            catch (Exception ex)
            {
                var result = new CustomMessageBox(ex.Message,
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
            }
        }
        #endregion

        #region ChangeRole

        public ICommand ChangeSelectedUserRoleCommand { get; }

        private bool CanChangeSelectedUserRoleExecute(object o) => true;
        private void OnChangeSelectedUserRoleExecuted(object o)
        {
            try
            {
                if(SelectedUser == null)
                {
                    throw new Exception("Выберите пользователя");
                }

                if(CurrentUser.Email == SelectedUser.Email)
                {
                    throw new Exception("Вы не можете изменить свою роль");
                }

                if (SelectedUser.IsAdmin == false)
                {
                    var result = new CustomMessageBox("Вы уверены, что хотите изменить роль выбранного пользователя?",
                                MessageType.Confirmation,
                                MessageButtons.YesNo).ShowDialog();
                    if(result == true)
                    {
                        SelectedUser.IsAdmin = true;
                        unitOfWork.UserRepository.Update(SelectedUser.Email, SelectedUser);
                        unitOfWork.Save();
                        var res = new CustomMessageBox($"{SelectedUser.Email} теперь администратор",
                                       MessageType.Info,
                                       MessageButtons.Ok).ShowDialog();
                    }
                    
                    
                }
                else
                if (SelectedUser.IsAdmin == true)
                {
                    var result = new CustomMessageBox("Вы уверены, что хотите изменить роль выбранного пользователя?",
                                MessageType.Confirmation,
                                MessageButtons.YesNo).ShowDialog();
                    if (result == true)
                    {
                        SelectedUser.IsAdmin = false;
                        unitOfWork.UserRepository.Update(SelectedUser.Email, SelectedUser);
                        unitOfWork.Save();
                        var res = new CustomMessageBox($"{SelectedUser.Email} теперь пользователь",
                                       MessageType.Info,
                                       MessageButtons.Ok).ShowDialog();
                    }


                }
                
                DisplayUsers();
            }
                
           
            catch(Exception ex)
            {
                var result = new CustomMessageBox(ex.Message,
                                   MessageType.Error,
                                   MessageButtons.Ok).ShowDialog();
            }
        }

        #endregion

        #region DeleteUser

        public ICommand DeleteSelectedUserCommand { get; }

        private bool CanDeleteSelectedUserExecute(object o) => true;
        private void OnDeleteSelectedUserExecuted(object o)
        {
            try
            {
                if (SelectedUser == null)
                {
                    throw new Exception("Выберите пользователя");
                }

                if (CurrentUser.Email == SelectedUser.Email)
                {
                    throw new Exception("Вы не можете удалить свою учетную запись");
                }

                var result = new CustomMessageBox("Вы уверены, что хотите удалить выбранного пользователя?",
                                MessageType.Confirmation,
                                MessageButtons.YesNo).ShowDialog();
                if (result == true)
                {
                    
                    List<Order> orders = AllOrders.Where(x => x.Email == SelectedUser.Email).ToList();
                    foreach(var n in orders)
                    {
                        unitOfWork.OrderRepository.Delete(n.OrderId);
                    }
                    unitOfWork.UserRepository.Delete(SelectedUser.Email);
                    unitOfWork.Save();
                    var res = new CustomMessageBox($"Пользователь {SelectedUser.Email} удален",
                                   MessageType.Info,
                                   MessageButtons.Ok).ShowDialog();
                }
                SelectedUser = null;
                DisplayOrders();
                DisplayUsers();
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

    }
}
