
using AutoMapper;
using DomainLayer.Entities;
using ServiceAbstraction;
using Shared.DataTransferObjects;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ProductDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork
                .GetRepository<Product, int>()
                .GetAllAsync(
                    p => p.ProductBrand,
                    p => p.ProductType
                );

            return _mapper.Map<IReadOnlyList<ProductDto>>(products);
        }

        public Task<ProductDto?> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<TypeDto>> GetAllTypesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<BrandDto>> GetAllBrandsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
