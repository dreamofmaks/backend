using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using User.Data.Model;

namespace User.Data.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity: class
    {
        ValueTask<TEntity> GetByIdAsync(params object[] keys);

        public Task<TEntity> AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task<bool> DeleteAsync(TEntity entity);

        Task DeleteRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> UpdateAsync(TEntity entity);

        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes);

        public Task<IEnumerable<TEntity>> GetAll();
    }
}
