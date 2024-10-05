using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.API.DTO
{
    public class ProductCreateDTO
    {
        [Required]
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string? AdditionalInfo { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
