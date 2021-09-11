using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Centennial.Api.Data;
using Centennial.Api.Entities;
using Centennial.Api.Specifications;

namespace Centennial.Api.Interfaces
{
    public interface IAsyncRepository<T, D> where T : BaseEntity<D>
    {
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Fetched entity of type T based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single Entity having id</returns>
        Task<T> GetByIdAsync(D id);

        /// <summary>
        /// Fetches entity of type T based on specification
        /// </summary>
        /// <param name="specification"></param>
        /// <returns>Single Entity filtered by specification</returns>
        Task<T> GetByIdAsync(ISpecification<T> specification);

        /// <summary>
        /// Lists all active entities of type T
        /// </summary>
        /// <returns>List of active entities in database</returns>
        Task<List<T>> ListAllAsync();

        /// <summary>
        /// Lists all active entities of type T filtered by specification
        /// </summary>
        /// <param name="specification"></param>
        /// <returns>List of active entities filetered by specification</returns>
        Task<List<T>> ListAllAsync(ISpecification<T> specification);

        /// <summary>
        /// Creates new entity and saves it to database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Created entity</returns>
        Task<T> AddAsync(T entity, bool doSave = true);

        /// <summary>
        /// Updates a given entity of type T
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Updated entity</returns>
        Task<T> UpdateAsync(T entity, bool doSave = true);

        /// <summary>
        /// Deactivates given entity of type T
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity, bool doSave = true);

        /// <summary>
        /// Deactivates entity having id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(D id, bool doSave = true);

        /// <summary>
        /// Counts number of entities in database based on specification
        /// </summary>
        /// <param name="specification"></param>
        /// <returns>Count of entities</returns>
        Task<int> CountAsync(ISpecification<T> specification);

        /// <summary>
        /// Checks if entity of type T is present in database
        /// </summary>
        /// <param name="entity">entity to be checked</param>
        /// <param name="predicate">Predicate to be checked with</param>
        /// <returns>True if entity exists, otherwise False</returns>
        Task<bool> IsExists(T entity, Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Checks if entity of type T is present in database
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>True if entity exists, otherwise False</returns>
        //Task<bool> IsExists(D id);
    }
}
