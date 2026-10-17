using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductService
    {


        Task<IReadOnlyList<Product>> GetAllProductsAsync();

        Task<Product?> GetProductByIdAsync(int id);





    }
}
