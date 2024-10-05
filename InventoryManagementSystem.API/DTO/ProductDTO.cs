using InventoryManagementSystem.Data.Entities;

namespace InventoryManagementSystem.API.DTO
{
    public class ProductDTO 
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string? AdditionalInfo { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CategoryId { get; set; }
    }
}
