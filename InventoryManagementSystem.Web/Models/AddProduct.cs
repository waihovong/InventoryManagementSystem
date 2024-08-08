using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Web.Models
{
    public class AddProduct
    {
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}
