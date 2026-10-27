
using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<ProductBrand> ProductBrands { get; set; } = null!;

        public DbSet<ProductType> ProductTypes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(StoreDbContext).Assembly);

            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<ProductBrand>().ToTable("ProductBrand");
            modelBuilder.Entity<ProductType>().ToTable("ProductType");
        }
    }
}
