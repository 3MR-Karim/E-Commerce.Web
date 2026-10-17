
using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.configuration
{
    internal class productConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductBrand)
                   .WithMany(b => b.Products)
                   .HasForeignKey(p => p.ProductBrandId);

            builder.HasOne(p => p.ProductType)
                   .WithMany(t => t.Products)
                   .HasForeignKey(p => p.ProductTypeId);

            builder.Property(p => p.Price)
                   .HasPrecision(18, 2);
        }
    }
}
