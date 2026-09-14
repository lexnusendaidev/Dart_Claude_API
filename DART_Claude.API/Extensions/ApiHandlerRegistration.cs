using DART_Claude.API.Handlers;

namespace DART_Claude.API.Extensions;

public static class ApiHandlerRegistration
{
    public static IServiceCollection AddApiHandlers(this IServiceCollection services)
    {
        services.AddScoped<GetApplicationsHandler>();
        return services;
    }
}
