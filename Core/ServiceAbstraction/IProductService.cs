using DomainLayer.Entities;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        Task<IReadOnlyList<ProductDto>> GetAllProductsAsync();

        Task<ProductDto?> GetProductByIdAsync(int id);

        Task<IReadOnlyList<TypeDto>> GetAllTypesAsync();

        Task<IReadOnlyList<BrandDto>> GetAllBrandsAsync();
    }
}