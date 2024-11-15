using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Pustok.DAL.DataContext.Repositories.Implementation;

namespace Pustok.DAL.DataContext.ServiceRegistrations;

public static class DataAccessServiceRegistrations
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDBContext>(
        options =>
             options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
         builder =>
             builder.MigrationsAssembly("Pustok.DAL")
         ));

        services.AddIdentity<AppUser, IdentityRole>(
            options =>
            {
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            }).AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IBasketItemRepository, BasketItemManager>();
        services.AddScoped<ICategoryRepository, CategoryManager>();
        services.AddScoped<IProductRepository, ProductManager>();
        services.AddScoped<IProductImageRepository, ProductImageManager>();
        services.AddScoped<IAttendanceRepository, AttendanceManager>();
        services.AddScoped<ISettingRepository, SettingManager>();
        services.AddScoped<ISliderRepository, SliderManager>();
        services.AddScoped<ISubscribeRepository, SubscribeManager>();
        services.AddScoped<ITagRepository, TagManager>();


        services.AddScoped<DataInitializer>();

        return services;
    }
}
