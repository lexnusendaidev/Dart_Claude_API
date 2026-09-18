using DART_Claude.API.Handlers;

namespace DART_Claude.API.Extensions;

public static class ApiHandlerRegistration
{
    public static IServiceCollection AddApiHandlers(this IServiceCollection services)
    {
        services.AddScoped<GetApplicationsHandler>();
        services.AddScoped<GetApplicationByIdHandler>();
        services.AddScoped<CreateApplicationHandler>();
        services.AddScoped<GetApplicationPathsHandler>();
        services.AddScoped<CreatePathHandler>();
        services.AddScoped<GetApplicationDependenciesHandler>();
        services.AddScoped<CreateDependencyHandler>();
        services.AddScoped<DeleteDependencyHandler>();
        services.AddScoped<GetApplicationFeedbackHandler>();
        services.AddScoped<GetFeedbackFilesHandler>();
        services.AddScoped<GetApplicationTelemetryHandler>();
        services.AddScoped<GetLookupsHandler>();
        return services;
    }
}
