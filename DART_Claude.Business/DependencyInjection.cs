using DART_Claude.Models.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DART_Claude.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddScoped<IApplicationService, Services.ApplicationService>();
        return services;
    }
}
