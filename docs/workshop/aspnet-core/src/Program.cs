using Microsoft.OpenApi.Models;
using ProductApi.Models;
using ProductApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Product Catalog API",
        Version = "v1.0",
        Description = "A simple product catalog API for Kubernetes workshop",
        Contact = new OpenApiContact
        {
            Name = "Workshop Team",
            Email = "workshop@example.com"
        }
    });
});

// Register ProductService as singleton (in-memory data store)
builder.Services.AddSingleton<ProductService>();

// Add CORS for testing
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Health check endpoint
app.MapGet("/health", () => 
{
    return Results.Ok(new 
    { 
        status = "healthy", 
        timestamp = DateTime.UtcNow,
        hostname = Environment.MachineName
    });
});

var productService = app.Services.GetRequiredService<ProductService>();
productService.SeedData();

app.Run();