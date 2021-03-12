using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace User.Data.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity: class
    {
        Task<TEntity> GetByIdAsync(params object[] keys);

        TEntity Add(TEntity entity);

        void AddRangeAsync(IEnumerable<TEntity> entities);

        bool DeleteAsync(TEntity entity);

        void DeleteRangeAsync(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);

        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes);
    }
}
