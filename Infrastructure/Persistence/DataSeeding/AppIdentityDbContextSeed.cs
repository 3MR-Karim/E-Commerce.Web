
using DomainLayer.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Persistence.DataSeeding;

public static class AppIdentityDbContextSeed
{
    public static async Task SeedUserAsync(
        UserManager<AppUser> userManager)
    {
        // Create Amru
        if (await userManager.FindByEmailAsync("amru@example.com") is null)
        {
            var amru = new AppUser
            {
                DisplayName = "Amru",
                Email = "amrkarim000@example.com",
                UserName = "Amru",
                PhoneNumber = "01000000001"
            };

            await userManager.CreateAsync(amru, "Pa$$w0rd");
        }

        // Create Rawaa
        if (await userManager.FindByEmailAsync("rawaa@example.com") is null)
        {
            var rawaa = new AppUser
            {
                DisplayName = "Rawaa",
                Email = "rawaa@example.com",
                UserName = "Rawaa",
                PhoneNumber = "01000000002"
            };

            await userManager.CreateAsync(rawaa, "Pa$$w0rd");
        }
    }
}
