using DomainLayer.Entities;

namespace DomainLayer.Specifications;

public class ProductSpecifications : Specification<Product>
{
    public ProductSpecifications()
    {
        AddInclude(p => p.ProductBrand);
        AddInclude(p => p.ProductType);
    }

    public ProductSpecifications(int id) : this()
    {
        AddCriteria(p => p.Id == id);
    }

    public ProductSpecifications(
        int? brandId,
        int? typeId,
        decimal? minPrice,
        decimal? maxPrice,
        string? search,
        string? sort,
        int pageIndex,
        int pageSize) : this()
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

        switch (sort?.ToLower())
        {
            case "name":
                AddOrderBy(p => p.Name);
                break;

            case "name-desc":
                AddOrderByDescending(p => p.Name);
                break;

            case "price":
                AddOrderBy(p => p.Price);
                break;

            case "price-desc":
                AddOrderByDescending(p => p.Price);
                break;
        }

        ApplyPaging(
            (pageIndex - 1) * pageSize,
            pageSize);
    }
}