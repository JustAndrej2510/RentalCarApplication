using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarApplication.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICarRepository CarRepository { get; }
        IUserRepository UserRepository { get; }
        IOrderRepository OrderRepository { get; }

        void Save();
    }
}
