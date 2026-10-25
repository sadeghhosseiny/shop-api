
using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Register controllers
builder.Services.AddControllers();

// Register database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Register services
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

// Configure HTTP pipeline
app.UseHttpsRedirection();

// Map controller routes
app.MapControllers();

app.Run();
