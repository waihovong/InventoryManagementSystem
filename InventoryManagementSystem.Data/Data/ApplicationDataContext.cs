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
        public DbSet<Product> Products { get; set; }
    }
}
