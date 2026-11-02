using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Specifications;
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

        // Get All Products
        public async Task<IReadOnlyList<ProductDto>> GetAllProductsAsync()
        {
            var specification = new ProductSpecifications();

            var products = await _unitOfWork
                .GetRepository<Product, int>()
                .GetAllAsync(specification);

            return _mapper.Map<IReadOnlyList<ProductDto>>(products);
        }

        // Get Product By Id
        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var specification = new ProductSpecifications(id);

            var product = await _unitOfWork
                .GetRepository<Product, int>()
                .GetByIdAsync(specification);

            return _mapper.Map<ProductDto?>(product);
        }

        // Get All Types
        public async Task<IReadOnlyList<TypeDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork
                .GetRepository<ProductType, int>()
                .GetAllAsync();

            return _mapper.Map<IReadOnlyList<TypeDto>>(types);
        }

        // Get All Brands
        public async Task<IReadOnlyList<BrandDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork
                .GetRepository<ProductBrand, int>()
                .GetAllAsync();

            return _mapper.Map<IReadOnlyList<BrandDto>>(brands);
        }

        // Get Products With Filtering, Search, Sorting and Pagination
        public async Task<PaginatedResult<ProductDto>> GetProductsAsync(
            ProductQueryParameters parameters)
        {
            var pageIndex = Math.Max(1, parameters.PageIndex);
            var pageSize = Math.Clamp(parameters.PageSize, 1, 50);

            var specification = new ProductSpecifications(
                parameters.BrandId,
                parameters.TypeId,
                parameters.MinPrice,
                parameters.MaxPrice,
                parameters.Search,
                parameters.Sort,
                pageIndex,
                pageSize);

            var countSpecification = new ProductCountSpecifications(
                parameters.BrandId,
                parameters.TypeId,
                parameters.MinPrice,
                parameters.MaxPrice,
                parameters.Search);

            var repository = _unitOfWork
                .GetRepository<Product, int>();

            var products = await repository.GetAllAsync(specification);

            var count = await repository.CountAsync(countSpecification);

            var productDtos =
                _mapper.Map<IReadOnlyList<ProductDto>>(products);

            return new PaginatedResult<ProductDto>(
                pageIndex,
                pageSize,
                count,
                productDtos);
        }
    }
}