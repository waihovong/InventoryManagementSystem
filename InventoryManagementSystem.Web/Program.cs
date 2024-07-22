using InventoryManagementSystem.Web.Components;
using InventoryManagementSystem.API.Interfaces;
using InventoryManagementSystem.API.Services;
using InventoryManagementSystem.Data.Data;
using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem.API.Controllers.Product;
using InventoryManagementSystem.API.Controllers.Category;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<ApplicationDataContext>((DbContextOptionsBuilder options) => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ProductController>();
builder.Services.AddScoped<CategoryController>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
