using InventoryManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace InventoryManagementSystem.Data.Data
{
    public class ApplicationDataContext : DbContext
    {
        private readonly string _connectionString;

        protected readonly IConfiguration _configuration;

        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>()
            //    .HasOne(p => p.Categories)
            //    .WithMany(c => c.Products)
            //    .HasForeignKey(p => p.CategoryId);

            //modelBuilder.Entity<ProductLocation>()
            //    .HasOne(pl => pl.Location)
            //    .WithMany(p => p.ProductLocations)
            //    .HasForeignKey(pl => pl.LocationId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ProductLocation> ProductLocations { get; set; }
    }
}
