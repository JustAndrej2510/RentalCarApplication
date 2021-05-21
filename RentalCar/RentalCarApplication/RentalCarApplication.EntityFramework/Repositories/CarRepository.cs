using RentalCarApplication.Core.Repositories;
using RentalCarApplication.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RentalCarApplication.EntityFramework.Repositories
{
    internal sealed class CarRepository : ICarRepository
    {
        private readonly ApplicationContext _context;
        public CarRepository(ApplicationContext context) 
        {
            _context = context;
        }

        public Car Create(Car entity)
        {
            var car = _context.Set<Car>().Add(entity).Entity;
            return car;
        }

        public bool Delete(int id)
        {
            var entity = _context.Set<Car>().Find(id);
            if (entity != null)
            {
                _context.Set<Car>().Remove(entity);
                return true;
            }
            else
                return false;
        }

        public Car Find(int id)
        {
            var entity = _context.Set<Car>().Find(id);
            return entity;
        }

        public IEnumerable<Car> FindAll()
        {
            IEnumerable<Car> entities =  _context.Set<Car>().ToList();
            return entities;
        }

        public async Task<IEnumerable<Car>> FindAllAsync()
        {
            IEnumerable<Car> entities = await _context.Set<Car>().ToListAsync();
            return entities;
        }

        public async Task<Car> FindAsync(int id)
        {
            var entity = await _context.Set<Car>().FirstOrDefaultAsync((e) => e.CarId == id);
            return entity;
        }

        public Car Update(int id, Car entity)
        {
            var entry = _context.Set<Car>().First(e => e.CarId == entity.CarId);
            _context.Entry(entry).CurrentValues.SetValues(entity);
            
            return entry;
        }
    }
}
