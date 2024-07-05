namespace InventoryManagementSystem.API.DTO
{
    public class ProductDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
