using RentalCarApplication.Core.Repositories;
using RentalCarApplication.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RentalCarApplication.EntityFramework.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context) 
        {
            _context = context;
        }

        public User Create(User entity)
        {
            var user = _context.Set<User>().Add(entity).Entity;
            return user;
        }
        public User Update(string id, User entity)
        {
            var entry = _context.Set<User>().First(e => e.Email == entity.Email);
            _context.Entry(entry).CurrentValues.SetValues(entity);
           
            return entry;
        }

        public bool Delete(string id)
        {
             var entity = _context.Set<User>().Find(id);
            if (entity != null)
            {
                _context.Set<User>().Remove(entity);
                return true;
            }
            else
                return false;
        }
        public User Find(string id)
        {
            var entity = _context.Set<User>().Find(id);
            return entity;
        }
        public IEnumerable<User> FindAll()
        {
            IEnumerable<User> entities = _context.Set<User>().ToList();
            return entities;
        }

        public bool CheckPassportAndLicense(string passport, string license)
        {
            if (_context.Set<User>().FirstOrDefault(e => e.Passport == passport) != null)
                return false;
            if (_context.Set<User>().FirstOrDefault(e => e.DriverLicense == license) != null)
                return false;
            return true;
        }

        public async Task<IEnumerable<User>> FindAllAsync()
        {
            IEnumerable<User> entities = await _context.Set<User>().ToListAsync();
            return entities;
        }

        public async Task<User> FindAsync(string id)
        {
            var entity = await _context.Set<User>().FirstOrDefaultAsync((e) => e.Email == id);
            return entity;
        }
       


    }
}
