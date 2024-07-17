using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Data.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; } = string.Empty;
        //public string? Code { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public string? AdditionalInfo { get; set; }
        //public int CategoryId { get; set; }
        //public Category Categories { get; set; }
        //public ICollection<ProductLocation> ProductLocations { get; set; }
    }
}
