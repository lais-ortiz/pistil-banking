using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pistil.Banking.Services
{
    public interface IServiceBase<TEntity>
    {
        Task<List<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task<TEntity> GetById(long id);
        Task UpdateAsync(TEntity entity);
    }
}
