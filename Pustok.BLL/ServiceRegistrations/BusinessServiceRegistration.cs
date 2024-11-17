using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pustok.BLL.Helpers.Abstractions;
using Pustok.BLL.Helpers.Implementations;
using Pustok.BLL.Services.Abstractions;
using Pustok.BLL.Services.Implementations;
using System.Reflection;

namespace Pustok.BLL.ServiceRegistrations;

public static class BusinessServiceRegistration
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddAutoMapper(Assembly.GetExecutingAssembly());

        //services.AddScoped<IAttendanceService, AttendanceService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ISettingService, SettingService>();
        //services.AddScoped<ISliderService, SliderService>();
        //services.AddScoped<ISubscribeService, SubscribeService>();
        //services.AddScoped<ITagService, TagService>();


        
        
        
        
        services.AddTransient<IEmailSender, EmailSenderService>();


        return services;
    }
}
