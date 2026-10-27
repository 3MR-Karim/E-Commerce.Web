
namespace Persistence.DataSeeding;

public class ProductSeedDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string PictureUrl { get; set; } = string.Empty;

    public string ProductTypeName { get; set; } = string.Empty;
    public string ProductBrandName { get; set; } = string.Empty;
}
