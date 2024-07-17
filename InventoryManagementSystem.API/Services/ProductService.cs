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
        private readonly ApplicationDataContext _context;
        public ProductService(ApplicationDataContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var product =  await _context.Products.ToListAsync();

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

            var product = await _context.Products.FindAsync(productId);

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
            try
            {
                var product = new Product
                {
                    AdditionalInfo = createProduct.AdditionalInfo,
                    Description = createProduct.Description,
                    ProductName = createProduct.ProductName,
                    Quantity = createProduct.Quantity
                };

                _context.Products.Add(product);

                await _context.SaveChangesAsync();

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
            catch (Exception ex)
            {
                Console.Write($"Failed to create product {ex.Message}");
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<ProductDTO> UpdateProduct(ProductUpdateDTO product)
        {         
            try
            {
                if (product == null)
                {
                    throw new ArgumentNullException(nameof(product));
                }

                if (product.ProductId <= 0)
                {
                    throw new ArgumentException("Invalid product ID");
                }


                var response = await _context.Products.FindAsync(product.ProductId);

                if (response == null)
                {
                    throw new ArgumentException("Invalid product ID");
                }

                response.ProductName = product.ProductName ?? response.ProductName;
                response.Description = product.Description ?? response.Description;
                response.Quantity = product.Quantity >= 0 ? product.Quantity : response.Quantity;
                response.AdditionalInfo = product.AdditionalInfo ?? response.AdditionalInfo;

                _context.Update(response);

                await _context.SaveChangesAsync();
                

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
            catch(ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch(Exception ex)
            {
                Console.Write($"Failed to update product {ex.Message}");
                throw new ApplicationException(ex.Message);
            }

        }
    }
}
