
using DomainLayer.Entities;
using System.Linq.Expressions;

namespace ServiceAbstraction
{
    public interface IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync(
            params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity?> GetByIdAsync(TKey id);
    }
}
