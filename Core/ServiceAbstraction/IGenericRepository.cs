using DomainLayer.Entities;
using DomainLayer.Specifications;
using System.Linq.Expressions;

namespace ServiceAbstraction
{
    public interface IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        // Existing Methods
        Task<IReadOnlyList<TEntity>> GetAllAsync(
            params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity?> GetByIdAsync(TKey id);

        // Specification Methods
        Task<IReadOnlyList<TEntity>> GetAllAsync(
            Specification<TEntity> specification);

        Task<TEntity?> GetByIdAsync(
            Specification<TEntity> specification);

        Task<int> CountAsync(
            Specification<TEntity> specification);
    }
}