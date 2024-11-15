using Microsoft.AspNetCore.Identity;

namespace Pustok.DAL.DataContext.Entities;

public class AppUser : IdentityUser
{
    public string? FullName { get; set; }
    public List<BasketItem> BasketItems { get; set; } = [];
}
