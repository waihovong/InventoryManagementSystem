using InventoryManagementSystem.API.DTO;
using InventoryManagementSystem.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryManagementSystem.API.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategory()
        {
            var category = await _categoryService.GetCategoriesAsync();

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> AddCategory([FromBody] CreateCategoryDTO category)
        {
            try
            {
                var response = await _categoryService.AddCategory(category);

                if (response.IsSuccess == false)
                {
                    return Conflict(response.ErrorMessage);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        public async Task<ActionResult<CategoryDTO>> UpdateCategory(UpdateCategoryDTO updateCategory)
        {
            var response = await _categoryService.UpdateCategory(updateCategory);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
