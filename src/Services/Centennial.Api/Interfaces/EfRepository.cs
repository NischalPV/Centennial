using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Centennial.Api.Data;
using Centennial.Api.Entities;
using Centennial.Api.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Centennial.Api.Interfaces
{
    public class EfRepository<T, D> : IAsyncRepository<T, D> where T : BaseEntity<D>
    {
        protected readonly CentennialDbContext _dbContext;
        private readonly ILogger<EfRepository<T, D>> _logger;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _dbContext;
            }
        }

        public EfRepository(CentennialDbContext dbContext, ILogger<EfRepository<T, D>> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); ;
            _logger = logger;
        }

        public virtual async Task<T> AddAsync(T entity, bool doSave = true)
        {

            if (doSave)
            {
                try
                {
                    _dbContext.Set<T>().Add(entity);

                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occured while adding entity");
                }
            }
            else
            {
                if (entity.IsTransient())
                {
                    return _dbContext.Set<T>().Add(entity).Entity;
                }
                else
                {
                    return entity;
                }
            }

            return entity;
        }

        public virtual async Task<int> CountAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).CountAsync();
        }

        public virtual async Task DeleteAsync(T entity, bool doSave = true)
        {
            entity.IsActive = false;
            await UpdateAsync(entity, doSave);
        }

        public virtual async Task DeleteByIdAsync(D id, bool doSave = true)
        {
            var entity = await GetByIdAsync(id);
            await DeleteAsync(entity, doSave);
        }

        public virtual async Task<T> GetByIdAsync(D id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetByIdAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public virtual async Task<bool> IsExists(T entity, Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AnyAsync(predicate);
        }

        public virtual async Task<List<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().Where(e => e.IsActive).ToListAsync();
        }

        public virtual async Task<List<T>> ListAllAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public virtual async Task<T> UpdateAsync(T entity, bool doSave)
        {

            if (doSave)
            {
                try
                {
                    _dbContext.Entry(entity).State = EntityState.Modified;

                    await _dbContext.SaveChangesAsync();

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occured while saving entity");
                }
            }
            else
            {
                return _dbContext.Set<T>().Update(entity).Entity;
            }
            
            return entity;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T, D>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }
    }
}
