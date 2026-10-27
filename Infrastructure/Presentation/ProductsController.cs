

using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DataTransferObjects;
using System.Runtime.InteropServices;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager)
        : ControllerBase
    {
        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>>
            GetAllProducts()
        {
            var products =
                await serviceManager.ProductService.GetAllProductsAsync();

            return Ok(products);
        }

        // GET: api/Products/10
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product =
                await serviceManager.ProductService.GetProductByIdAsync(id);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        // GET: api/Products/types
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetTypes()
        {
            var types =
                await serviceManager.ProductService.GetAllTypesAsync();

            return Ok(types);
        }

        // GET: api/Products/brands
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetBrands()
        {
            var brands =
                await serviceManager.ProductService.GetAllBrandsAsync();

            return Ok(brands);
        }
    }
}
