using InventoryManagementSystem.API.DTO;
using InventoryManagementSystem.API.Interfaces;
using InventoryManagementSystem.Data.Data;
using InventoryManagementSystem.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace InventoryManagementSystem.API.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDataContext _context;
        public ProductService(ApplicationDataContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var response =  await _context.Products.ToListAsync();
            return response;
        }

        [HttpPost]
        public async Task<bool> AddProduct(Product product)
        {
            try
            {
                var response = await _context.Products.AddAsync(product);

                var result = await _context.SaveChangesAsync();

                return result > 0;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error adding proudct: {ex.Message}");
                return false;
            }
        }
    }
}
