
using DomainLayer.Entities;

namespace ServiceAbstraction
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);

        Task<IReadOnlyList<ProductType>> GetAllProductTypesAsync();

        Task<IReadOnlyList<ProductBrand>> GetAllProductBrandsAsync();
    }
}
