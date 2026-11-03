
using DomainLayer.Entities.Identity;
using DomainLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.DataSeeding;
using Persistence.Repositories;
using Presentation.Controllers;
using ServiceAbstraction;
using Services;
using Services.MappingProfiles;
using ServicesAbstraction;
using StackExchange.Redis;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register Controllers
            builder.Services
                .AddControllers()
                .AddApplicationPart(typeof(ProductsController).Assembly);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register AutoMapper and discover ProductProfile
            builder.Services.AddAutoMapper(
      cfg => { },
      typeof(ProductProfile).Assembly
  );

            // Register DbContext
            builder.Services.AddDbContext<StoreDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                )
            );
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("IdentityConnection")
    )
); builder.Services
    .AddIdentityCore<AppUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>();

            // Register Redis Connection
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var connectionString =
                    builder.Configuration.GetConnectionString("Redis")
                    ?? throw new InvalidOperationException("Redis connection string is missing");

                return ConnectionMultiplexer.Connect(connectionString);
            });

            // Register Basket Repository
            // Register Basket Repository
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();


            // Register Unit of Work and Service Manager
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();

            var app = builder.Build();

            // Seed database
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                // Seed Store Database
                var context = services.GetRequiredService<StoreDbContext>();
                await StoreContextSeed.SeedAsync(context);

                // Seed Identity Users
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(userManager);
            }

            // Configure HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Serve static files from wwwroot
            app.UseStaticFiles();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
