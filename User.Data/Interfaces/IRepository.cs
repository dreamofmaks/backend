﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using User.Data.Model;

namespace User.Data.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity: class, IEntity
    {
        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> GetByIdWithoutTrackingAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task<bool> DeleteAsync(TEntity entity);

        Task DeleteRange(IEnumerable<TEntity> entities);

        Task<bool> DeleteByIdAsync(int id);

        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
