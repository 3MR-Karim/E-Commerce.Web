namespace Shared.DataTransferObjects;

public class ProductQueryParameters
{
    public int? BrandId { get; set; }

    public int? TypeId { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public string? Sort { get; set; }

    public string? Search { get; set; }

    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}