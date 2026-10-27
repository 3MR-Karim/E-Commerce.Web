
using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using ServiceAbstraction;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>
        : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        protected readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>()
                .AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _context.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(entity => entity.Id!.Equals(id));
        }
    }
}
