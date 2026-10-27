using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{

    using DomainLayer.Entities;

    namespace ServiceAbstraction
    {
        public interface IGenericRepository<TEntity, TKey>
            where TEntity : BaseEntity<TKey>
        {
            Task<IReadOnlyList<TEntity>> GetAllAsync();

            Task<TEntity?> GetByIdAsync(TKey id);
        }
    }

}
