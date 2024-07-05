using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Data.Entities
{
    public class ProductLocation
    {
        public int ProductLocationId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public int QuantityInLocation { get; set; }

    }
}
