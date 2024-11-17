using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pustok.DAL.DataContext.Configurations;
using Pustok.DAL.DataContext.Entities;

namespace Pustok.DAL.DataContext;

public class AppDBContext : IdentityDbContext<AppUser>
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AttendanceConfiguration).Assembly);
        base.OnModelCreating(builder);
    }

    public DbSet<Attendance> Attendances { get; set; } = null!;
    public DbSet<BasketItem> BasketItems { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductImage> ProductImages { get; set; } = null!;
    public DbSet<ProductTag> ProductTags { get; set; } = null!;
    public DbSet<Setting> Setting { get; set; } = null!;
    public DbSet<Slider> Slider { get; set; } = null!;
    public DbSet<Subscribe> Subscribe { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;

}
