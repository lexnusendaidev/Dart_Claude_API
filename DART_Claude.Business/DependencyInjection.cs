using DART_Claude.Business.Services;
using DART_Claude.Models.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DART_Claude.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddScoped<IApplicationService, ApplicationService>();
        services.AddScoped<IPathService, PathService>();
        services.AddScoped<IDependencyService, DependencyService>();
        services.AddScoped<IFeedbackService, FeedbackService>();
        services.AddScoped<IFeedbackFileService, FeedbackFileService>();
        services.AddScoped<ITelemetryService, TelemetryService>();
        services.AddScoped<ILookupService, LookupService>();
        return services;
    }
}
