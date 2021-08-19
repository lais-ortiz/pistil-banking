using Pistil.Banking.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pistil.Banking.Services
{
    public abstract class ServiceBase<TEntity> : IServiceBase<TEntity> 
        where TEntity : class 
    {
        private readonly IRepositoryBase<TEntity> _repositoryBase;

        protected ServiceBase(
            IRepositoryBase<TEntity> repositoryBase
        )
        {
            _repositoryBase = repositoryBase;
        }

        public async Task<List<TEntity>> GetAllAsync() => await _repositoryBase.GetAllAsync();
        public async Task<TEntity> GetById(long id) => await _repositoryBase.GetByIdAsync(id);
        public async Task AddAsync(TEntity entity) => await _repositoryBase.AddAsync(entity);
        public async Task UpdateAsync(TEntity entity) => await _repositoryBase.UpdateAsync(entity);
    }
}
