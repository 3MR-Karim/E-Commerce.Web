using DomainLayer.Entities;
using DomainLayer.Specifications;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Specifications;
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

        // Get All - Existing Method
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

        // Get By Id - Existing Method
        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _context.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(entity => entity.Id!.Equals(id));
        }

        // Get All Using Specification
        public async Task<IReadOnlyList<TEntity>> GetAllAsync(
            Specification<TEntity> specification)
        {
            var query = SpecificationEvaluator.GetQuery(
                _context.Set<TEntity>().AsNoTracking(),
                specification);

            return await query.ToListAsync();
        }

        // Get By Id Using Specification
        public async Task<TEntity?> GetByIdAsync(
            Specification<TEntity> specification)
        {
            var query = SpecificationEvaluator.GetQuery(
                _context.Set<TEntity>().AsNoTracking(),
                specification);

            return await query.FirstOrDefaultAsync();
        }

        // Count Using Specification
        public async Task<int> CountAsync(
            Specification<TEntity> specification)
        {
            var query = SpecificationEvaluator.GetQuery(
                _context.Set<TEntity>().AsNoTracking(),
                specification);

            return await query.CountAsync();
        }
    }
}