using DomainLayer.Entities;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{

    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
            return _productRepository.GetAllAsync();
        }

        public Task<Product?> GetProductByIdAsync(int id)
        {
            return _productRepository.GetByIdAsync(id);
        }

    }
}