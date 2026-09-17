using DART_Claude.Data.ContextModels;
using DART_Claude.Data.Repositories;
using DART_Claude.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DART_Claude.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DartClaude")
            ?? throw new InvalidOperationException("Missing 'DartClaude' connection string.");
        // Retries transient connection failures (e.g. LocalDB auto-stopping after
        // inactivity and briefly restarting on the next connection attempt) instead of
        // failing the request outright.
        services.AddDbContext<DartClaudeContext>(options =>
            options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure()));
        services.AddScoped<IApplicationRepository, ApplicationRepository>();
        services.AddScoped<IPathRepository, PathRepository>();
        services.AddScoped<IDependencyRepository, DependencyRepository>();
        services.AddScoped<IFeedbackRepository, FeedbackRepository>();
        services.AddScoped<IFeedbackFileRepository, FeedbackFileRepository>();
        services.AddScoped<ITelemetryRepository, TelemetryRepository>();
        services.AddScoped<ILookupRepository, LookupRepository>();
        return services;
    }
}
