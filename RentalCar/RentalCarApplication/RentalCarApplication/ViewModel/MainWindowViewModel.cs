using System;
using System.Windows;
using System.Windows.Input;
using RentalCarApplication.Base;
using RentalCarApplication.Infrastructure;

namespace RentalCarApplication.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly Navigator _navigator;
        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;

        public MainWindowViewModel() { }
        public MainWindowViewModel(Navigator navigator)
        {
            _navigator = navigator;
            _navigator.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }
        
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        
        
       
        
    }
}
