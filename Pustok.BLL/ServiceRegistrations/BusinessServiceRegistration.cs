using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pustok.BLL.Services.Abstractions;
using Pustok.BLL.Services.Implementations;

namespace Pustok.BLL.ServiceRegistrations;

public static class BusinessServiceRegistration
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IEmailSender, EmailSenderService>();

        return services;
    }
}
