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

        public TEntity GetById(params object[] keys) => dbEntities.Find(keys);

        public async Task<TEntity> Add(TEntity entity) => dbEntities.Add(entity).Entity;

        public async void AddRangeAsync(IEnumerable<TEntity> entities) => await dbEntities.AddRangeAsync(entities);


        public bool Delete(TEntity entity) =>  dbEntities.Remove(entity).Entity != null;

        public void DeleteRange(IEnumerable<TEntity> entities) => dbEntities.RemoveRange(entities);

        public TEntity Update(TEntity entity) =>  dbEntities.Update(entity).Entity;

        public IEnumerable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes)
        {
            var dbSet = context.Set<TEntity>();
            IQueryable<TEntity> query = dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.ToList() ?? dbSet.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await dbEntities.ToListAsync();
        }
    }
}
