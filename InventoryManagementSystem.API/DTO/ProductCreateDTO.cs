﻿namespace InventoryManagementSystem.API.DTO
{
    public class ProductCreateDTO
    {
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}
