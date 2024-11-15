using Microsoft.AspNetCore.Identity;
using Pustok.DAL.DataContext.AppSettingModels;
using Pustok.DAL.DataContext.Enums;

namespace Pustok.DAL.DataContext;

public class DataInitializer
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AppDBContext _dbContext;
    private readonly IConfiguration _configuration;

    public DataInitializer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDBContext dbContext, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task SeedRoleData()
    {
        await _dbContext.Database.MigrateAsync();

        await CreateRolesAsync();

        await CreateAdminAsync();
    }

    public async Task CreateRolesAsync()
    {
        var roles = Enum.GetNames(typeof(Roles));
            
        foreach(var role in roles)
        {
            if(await _roleManager.FindByNameAsync(role) != null)
            {
                continue;
            }

            await _roleManager.CreateAsync(new IdentityRole
            {
                Name = role
            });
        }
    }

    public async Task CreateAdminAsync()
    {
        var admin = _configuration.GetSection("Admin").Get<Admin>();

        if(admin == null)
        {
            return;
        }

        var existsAdmin = await _userManager.FindByNameAsync(admin.Username);
        if(existsAdmin != null)
        {
            return;
        }

        var adminUser = new AppUser()
        {
            FullName = admin.FullName,
            Email = admin.Email,
            UserName = admin.Username,
        };

        var result = await _userManager.CreateAsync(adminUser, admin.Password);
        if (!result.Succeeded)
        {
            throw new Exception("User has not been created.");
        }
        result = await _userManager.AddToRoleAsync(adminUser, Roles.Admin.ToString());
        result = await _userManager.AddToRoleAsync(adminUser, Roles.Moderator.ToString());

        if (!result.Succeeded)
        {
            throw new Exception("User can not be assigned.");
        }
    }
}
