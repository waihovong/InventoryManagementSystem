using InventoryManagementSystem.Web.Components;
using InventoryManagementSystem.API.Interfaces;
using InventoryManagementSystem.API.Services;
using InventoryManagementSystem.Data.Data;
using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem.API.Controllers.Product;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("InventoryManagementSystem.Data")));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ProductController>();
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
