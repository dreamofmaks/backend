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
        Task<TEntity> GetById(params object[] keys);

        TEntity Add(TEntity entity);

        void AddRangeAsync(IEnumerable<TEntity> entities);

        bool Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);

        public IEnumerable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes);

        public Task<IEnumerable<TEntity>> GetAll();
    }
}
