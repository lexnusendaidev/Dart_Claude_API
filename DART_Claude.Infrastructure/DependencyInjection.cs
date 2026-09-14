using DART_Claude.Business;
using DART_Claude.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DART_Claude.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddBusiness();
        services.AddData(configuration);
        return services;
    }
}
