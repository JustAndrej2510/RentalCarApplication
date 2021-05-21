using RentalCarApplication.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarApplication.Core.Repositories
{
    public interface IOrderRepository : IRepository<Order,int>
    {
        bool CheckUserOrders(string email);
    }
}
