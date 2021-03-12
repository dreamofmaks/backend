using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using User.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using User.Data.Model;

namespace User.Data.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly UserContext context;
        private readonly DbSet<TEntity> dbEntities;

        public Repository(UserContext context)
        {
            this.context = context;
            dbEntities = this.context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(params object[] keys) => await dbEntities.FindAsync(keys);

        public TEntity Add(TEntity entity) => dbEntities.Add(entity).Entity;

        public async void AddRangeAsync(IEnumerable<TEntity> entities) => await dbEntities.AddRangeAsync(entities);


        public bool DeleteAsync(TEntity entity) =>  dbEntities.Remove(entity).Entity != null;

        public void DeleteRangeAsync(IEnumerable<TEntity> entities) => dbEntities.RemoveRange(entities);

        public TEntity Update(TEntity entity) =>  dbEntities.Update(entity).Entity;

        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
