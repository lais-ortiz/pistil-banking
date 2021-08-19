using Microsoft.EntityFrameworkCore;
using Pistil.Banking.Data.ContextFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pistil.Banking.Data.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> 
        where TEntity : class
    {
        protected DbContext _context;

        protected RepositoryBase(PistilDbContext context)
        {
            _context = context;
        }

        #region read operations
        public async Task<List<TEntity>> GetAllAsync() => await _context.Set<TEntity>().ToListAsync();
        public async Task<TEntity> GetByIdAsync(long id) => await _context.Set<TEntity>().FindAsync(id);
        #endregion

        #region write operations
        public async Task AddAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException($"Please, provide a value");

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException($"Please, provide a value");

            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
