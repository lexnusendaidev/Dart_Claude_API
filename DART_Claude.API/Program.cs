using DART_Claude.API.Extensions;
using DART_Claude.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(swagger => swagger.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
{
    Title = "DART_Claude",
    Version = "v1",
    Description = "Application tracking API for the DART_Claude project.",
}));

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApiHandlers();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
