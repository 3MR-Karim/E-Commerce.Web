
using System.Text.Json;
using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.DataSeeding;

public static class StoreContextSeed
{
    private static readonly JsonSerializerOptions JsonOptions =
        new() { PropertyNameCaseInsensitive = true };

    public static async Task SeedAsync(StoreDbContext context)
    {
        if (!await context.ProductBrands.AnyAsync())
        {
            var path = Path.Combine(
                AppContext.BaseDirectory, "DataSeeding", "brands.json");

            var json = await File.ReadAllTextAsync(path);
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(
                json, JsonOptions) ?? new();

            await context.ProductBrands.AddRangeAsync(brands);
            await context.SaveChangesAsync();
        }

        if (!await context.ProductTypes.AnyAsync())
        {
            var path = Path.Combine(
                AppContext.BaseDirectory, "DataSeeding", "types.json");

            var json = await File.ReadAllTextAsync(path);
            var types = JsonSerializer.Deserialize<List<ProductType>>(
                json, JsonOptions) ?? new();

            await context.ProductTypes.AddRangeAsync(types);
            await context.SaveChangesAsync();
        }

        if (!await context.Products.AnyAsync())
        {
            var path = Path.Combine(
                AppContext.BaseDirectory, "DataSeeding", "products.json");

            var json = await File.ReadAllTextAsync(path);
            var items = JsonSerializer.Deserialize<List<ProductSeedDto>>(
                json, JsonOptions) ?? new();

            var brands = await context.ProductBrands
                .ToDictionaryAsync(x => x.Name);

            var types = await context.ProductTypes
                .ToDictionaryAsync(x => x.Name);

            var products = new List<Product>();

            foreach (var item in items)
            {
                if (!brands.TryGetValue(item.ProductBrandName, out var brand))
                    throw new InvalidOperationException(
                        $"Brand not found: {item.ProductBrandName}");

                if (!types.TryGetValue(item.ProductTypeName, out var type))
                    throw new InvalidOperationException(
                        $"Product type not found: {item.ProductTypeName}");

                products.Add(new Product
                {
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    PictureUrl = item.PictureUrl,
                    ProductBrandId = brand.Id,
                    ProductTypeId = type.Id
                });
            }

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}
