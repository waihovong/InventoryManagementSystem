using InventoryManagementSystem.API.DTO;

namespace InventoryManagementSystem.API.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
        Task<CategoryResult> AddCategory(CreateCategoryDTO createCategory);
        Task<CategoryDTO> UpdateCategory(UpdateCategoryDTO updateCategory);
        Task<IEnumerable<CategoryDTO>> SearchCategories(string searchTerm);
    }
}
