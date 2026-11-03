using Microsoft.AspNetCore.Identity;
using System.Net;

namespace DomainLayer.Entities.Identity;

public class AppUser : IdentityUser
{
    public string DisplayName { get; set; } = null!;

    public Address? Address { get; set; }
}