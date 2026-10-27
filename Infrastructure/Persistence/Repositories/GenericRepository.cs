
using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using ServiceAbstraction;
using ServiceAbstraction.ServiceAbstraction;

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

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _context.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(entity => entity.Id!.Equals(id));
        }
    }
}
