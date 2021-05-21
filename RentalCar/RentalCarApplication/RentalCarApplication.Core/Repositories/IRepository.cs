using RentalCarApplication.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarApplication.Core.Repositories
{
    public interface IRepository<TEntity, in TKey> where TEntity : Entity<TKey>
    {
        TEntity Find(TKey id);
        Task<TEntity> FindAsync(TKey id);
        Task<IEnumerable<TEntity>> FindAllAsync();
        IEnumerable<TEntity> FindAll();
        TEntity Create(TEntity entity);
        TEntity Update(TKey id, TEntity entity);
        bool Delete(TKey id);

    }
}
