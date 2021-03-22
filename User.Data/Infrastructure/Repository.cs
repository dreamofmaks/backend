using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using User.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using User.Data.Models;

namespace User.Data.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly Context _context;
        protected virtual IQueryable<TEntity> IncludedEntities => _context.Set<TEntity>();

        public Repository(Context context)
        {
            _context = context;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await IncludedEntities.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await IncludedEntities.ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            return (await _context.Set<TEntity>().AddAsync(entity)).Entity;
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            return _context.Set<TEntity>().AddRangeAsync(entities);
        }

        public Task<bool> DeleteAsync(TEntity entity)
        {
            return Task.FromResult(_context.Set<TEntity>().Remove(entity).Entity != null);
        }

        public Task DeleteRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            return _context.Set<TEntity>().Remove(entity).Entity != null;
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            var updatedEntity = _context.Set<TEntity>().Update(entity).Entity;
            _context.Entry(updatedEntity).State = EntityState.Modified;
            return Task.FromResult(updatedEntity);
        }
    }
}
