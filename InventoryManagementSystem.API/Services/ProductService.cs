using InventoryManagementSystem.API.DTO;
using InventoryManagementSystem.API.Interfaces;
using InventoryManagementSystem.Data.Data;
using InventoryManagementSystem.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Runtime.CompilerServices;

namespace InventoryManagementSystem.API.Services
{
    public class ProductService : IProductService
    {
        //private readonly ApplicationDataContext _context;
        private IDbContextFactory<ApplicationDataContext> _contextFactory;

        public ProductService( IDbContextFactory<ApplicationDataContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        private async Task<T> UseDbContextAsync<T>(Func<ApplicationDataContext, Task<T>> action)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await action(context);
            }
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var product = await UseDbContextAsync(async context => await context.Products.ToListAsync());

            var productDto = product.Select(p => new ProductDTO
            {
                AdditionalInfo = p.AdditionalInfo,
                Description = p.Description,
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Quantity = p.Quantity
            }).ToList();

            return productDto;
        }

        [HttpGet]
        public async Task<ProductDTO> GetProductAsync(int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentException("Invalid product ID");
            }

            var product = await UseDbContextAsync(async context => 
                await context.Products.FindAsync(productId));

            if (product == null)
            {
                return null;
            }

            var getProductDto = new ProductDTO
            {
                AdditionalInfo = product.AdditionalInfo,
                ProductId = product.ProductId,
                Description = product.Description,
                Quantity = product.Quantity,
                ProductName = product.ProductName
            };

            return getProductDto;
        }

        [HttpPost]
        public async Task<ProductDTO> AddProduct(ProductCreateDTO createProduct)
        {
            var product = new Product
            {
                AdditionalInfo = createProduct.AdditionalInfo,
                Description = createProduct.Description,
                ProductName = createProduct.ProductName,
                Quantity = createProduct.Quantity
            };

            using (var context = _contextFactory.CreateDbContext())
            {
                context.Products.Add(product);
                await context.SaveChangesAsync();
            }

            var createdProductDto = new ProductDTO
            {
                AdditionalInfo = product.AdditionalInfo,
                ProductId = product.ProductId,
                Description = product.Description,
                Quantity = product.Quantity,
                ProductName = product.ProductName
            };

            return createdProductDto;
        }

        public async Task<ProductDTO> UpdateProduct(ProductUpdateDTO product)
        {

            if (product == null)
            {
                return null;
            }

            var response = await UseDbContextAsync(async context => 
                await context.Products.FindAsync(product.ProductId));

            if (response == null)
            {
                return null;
            }

            response.ProductName = product.ProductName ?? response.ProductName;
            response.Description = product.Description ?? response.Description;
            response.Quantity = product.Quantity >= 0 ? product.Quantity : response.Quantity;
            response.AdditionalInfo = product.AdditionalInfo ?? response.AdditionalInfo;

            using (var context = _contextFactory.CreateDbContext())
            {
                context.Update(response);
                await context.SaveChangesAsync();
            }


            var updatedProductDto = new ProductDTO
            {
                AdditionalInfo = response.AdditionalInfo,
                ProductId = response.ProductId,
                Description = response.Description,
                Quantity = response.Quantity,
                ProductName = response.ProductName
            };

            return updatedProductDto;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            if (productId <= 0)
            {
                return false;
            }

            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    var product = await context.Products.FindAsync(productId);

                    if (product != null)
                    {
                        context.Products.Remove(product);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        throw new Exception($"Product with ID {productId} not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
