
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Data.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? Description { get; set; } = string.Empty;
        public ICollection<Product_Category> ProductCategories { get; set; }
    }
}
