WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(swagger => swagger.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
{
    Title = "AgenticApiTemplate",
    Version = "v1",
    Description = "A template for building agentic APIs with .NET 10.0",
}));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
