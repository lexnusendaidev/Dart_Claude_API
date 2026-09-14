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
        services.AddDbContext<DartClaudeContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IApplicationRepository, ApplicationRepository>();
        return services;
    }
}
