using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using User.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using User.Data.Models;

namespace User.Data.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly Context context;
        private readonly DbSet<TEntity> dbEntities;

        public Repository(Context context)
        {
            this.context = context;
            dbEntities = this.context.Set<TEntity>();
        }

        public ValueTask<TEntity> GetByIdAsync(params object[] keys) => dbEntities.FindAsync(keys);

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            return (await dbEntities.AddAsync(entity)).Entity;
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities) =>  dbEntities.AddRangeAsync(entities);


        public Task DeleteAsync(TEntity entity)
        {
            dbEntities.Remove(entity);
            return Task.CompletedTask;
        }

        public Task DeleteRange(IEnumerable<TEntity> entities)
        {
            dbEntities.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(TEntity entity)
        {
            var updatedEntity = dbEntities.Update(entity).Entity;
            return Task.CompletedTask;
        } 

        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes)
        {
            var dbSet = context.Set<TEntity>();
            IQueryable<TEntity> query = dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query ?? dbSet;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await dbEntities.ToListAsync();
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await dbEntities.FindAsync(id);
            return dbEntities.Remove(entity).Entity != null;
        }
    }
}
