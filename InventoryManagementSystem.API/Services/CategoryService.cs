using InventoryManagementSystem.API.DTO;
using InventoryManagementSystem.API.Interfaces;
using InventoryManagementSystem.Data.Data;
using InventoryManagementSystem.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;


namespace InventoryManagementSystem.API.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly ApplicationDataContext _context;

        public CategoryService(ApplicationDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
        {
            var category = await _context.Categories.ToListAsync();

            var categoryDto = category.Select(c => new CategoryDTO
            {
                CategoryName = c.CategoryName,
                Description = c.Description,
                CategoryId = c.CategoryId
            }).ToList();

            return categoryDto;
        }

        [HttpPost]
        public async Task<CategoryResult> AddCategory(CreateCategoryDTO createCategory)
        {
            try
            {
                var category = new Data.Entities.Category
                {
                    CategoryName = createCategory.CategoryName,
                    Description = createCategory.Description
                };

                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                var createdCategory = new CategoryResult
                {
                    CategoryName = category.CategoryName,
                    Description = category.Description,
                };

                return createdCategory;
            }
            catch (DbUpdateException ex)
            {
                var pgException = ex.InnerException as PostgresException;

                if (pgException != null && pgException.SqlState == "23505")
                {
                    return new CategoryResult
                    {
                        ErrorMessage = $"A category with the name {createCategory.CategoryName} already exists.",
                        IsSuccess = false,
                    };
                }

                return new CategoryResult
                {
                    IsSuccess = false,
                    ErrorMessage = "An error occurred while adding the category."
                };
            }

        }

        [HttpPatch]
        public async Task<CategoryDTO> UpdateCategory(UpdateCategoryDTO updateCategory)
        {
            var category = await _context.Categories.FindAsync(updateCategory.CategoryId);

            if (category == null)
            {
                return null;
            }

            category.CategoryName = updateCategory.CategoryName ?? category.CategoryName;
            category.Description = updateCategory.Description ?? category.Description;

            _context.Update(category);
            await _context.SaveChangesAsync();

            var updatedCategory = new CategoryDTO
            {
                CategoryName = category.CategoryName,
                Description = category.Description
            };

            return updatedCategory;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> SearchCategories(string searchTerm)
        {
            var category = await _context.Categories
                .Where(c => EF.Functions.ILike(c.CategoryName, $"%{searchTerm}%"))
                .ToListAsync();

            var orderedCategory = category.OrderBy(d => d.CategoryName);

            return orderedCategory.Select(r => new CategoryDTO
            {
                CategoryId = r.CategoryId,
                CategoryName = r.CategoryName,
                Description = r.Description
            });
        }

    }
}
