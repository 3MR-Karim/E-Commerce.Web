using Shared.DataTransferObjects;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        Task<IReadOnlyList<ProductDto>> GetAllProductsAsync();

        Task<ProductDto?> GetProductByIdAsync(int id);

        Task<IReadOnlyList<TypeDto>> GetAllTypesAsync();

        Task<IReadOnlyList<BrandDto>> GetAllBrandsAsync();

        Task<PaginatedResult<ProductDto>> GetProductsAsync(
            ProductQueryParameters parameters);
    }
}