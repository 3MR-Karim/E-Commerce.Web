using DomainLayer.Entities;

namespace DomainLayer.Specifications;

public class ProductCountSpecifications : Specification<Product>
{
    public ProductCountSpecifications(
        int? brandId,
        int? typeId,
        decimal? minPrice,
        decimal? maxPrice,
        string? search)
    {
        if (brandId.HasValue)
            AddCriteria(p => p.ProductBrandId == brandId.Value);

        if (typeId.HasValue)
            AddCriteria(p => p.ProductTypeId == typeId.Value);

        if (minPrice.HasValue)
            AddCriteria(p => p.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            AddCriteria(p => p.Price <= maxPrice.Value);

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();

            AddCriteria(p =>
                p.Name.Contains(search) ||
                p.Description.Contains(search));
        }
    }
}