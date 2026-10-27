
using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using ServiceAbstraction;
using ServiceAbstraction.ServiceAbstraction;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
        {
            return new GenericRepository<TEntity, TKey>(_context);
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
