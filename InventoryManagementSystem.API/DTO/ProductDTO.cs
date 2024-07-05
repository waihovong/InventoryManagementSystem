using InventoryManagementSystem.Data.Entities;

namespace InventoryManagementSystem.API.DTO
{
    public class ProductDTO
    {
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string? CategoryName { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}
