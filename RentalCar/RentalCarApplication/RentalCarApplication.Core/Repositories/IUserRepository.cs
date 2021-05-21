using RentalCarApplication.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarApplication.Core.Repositories
{
    public interface IUserRepository : IRepository<User,string>
    {
        bool CheckPassportAndLicense(string passport, string license);
        
    }
}
