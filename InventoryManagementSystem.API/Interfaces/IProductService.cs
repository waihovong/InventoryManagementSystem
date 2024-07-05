using InventoryManagementSystem.API.DTO;
using InventoryManagementSystem.Data.Entities;

namespace InventoryManagementSystem.API.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<bool> AddProduct(Product product);
    }
}
