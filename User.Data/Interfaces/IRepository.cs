using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using User.Data.Models;

namespace User.Data.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity: class
    {
        ValueTask<TEntity> GetByIdAsync(params object[] keys);

        Task<TEntity> AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task<bool> DeleteAsync(TEntity entity);

        Task<bool> DeleteById(int id);

        Task DeleteRange(IEnumerable<TEntity> entities);

        Task<TEntity> UpdateAsync(TEntity entity);

        IEnumerable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TEntity>> GetAll();
    }
}
