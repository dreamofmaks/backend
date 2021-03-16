﻿using System;
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

        public ValueTask<TEntity> GetByIdAsync(params object[] keys) => dbEntities.FindAsync(keys);

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            return (await dbEntities.AddAsync(entity)).Entity;
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities) =>  dbEntities.AddRangeAsync(entities);


        public async Task<bool> DeleteAsync(TEntity entity) =>
            (await Task.Run(() => dbEntities.Remove(entity))).Entity != null;

        public Task DeleteRangeAsync(IEnumerable<TEntity> entities) =>
            Task.Run(() => dbEntities.RemoveRange(entities));

        public async Task<TEntity> UpdateAsync(TEntity entity) => await Task.Run(() => dbEntities.Update(entity).Entity);

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
    }
}
