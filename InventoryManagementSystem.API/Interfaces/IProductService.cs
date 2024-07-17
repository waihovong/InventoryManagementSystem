using InventoryManagementSystem.API.DTO;
using InventoryManagementSystem.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.API.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProductsAsync();
        Task<ProductDTO> AddProduct(ProductCreateDTO product);
        Task<ProductDTO> UpdateProduct(ProductUpdateDTO product);
        Task<ProductDTO> GetProductAsync(int productId);
    }
}
