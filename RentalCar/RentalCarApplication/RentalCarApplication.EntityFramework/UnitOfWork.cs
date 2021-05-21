using RentalCarApplication.Core.Repositories;
using RentalCarApplication.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarApplication.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _db = new ApplicationContext();

        private IUserRepository _userRepository;
        private IOrderRepository _orderRepository;
        private ICarRepository _carRepository;

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_db);
        public ICarRepository CarRepository => _carRepository ??= new CarRepository(_db);
        public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_db);
        public void Save()
        {
            _db.SaveChanges();
        }
        private bool _isDisposed;

        public void Dispose()
        {
            if (_db == null)
            {
                return;
            }

            if (!_isDisposed)
            {
                _db.Dispose();
            }

            _isDisposed = true;
        }

    }
}
