namespace InventoryManagementSystem.API.DTO
{
    public class CategoryResult
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}
