using System;
using AutoMapper;
using InventoryManagementSystem.API.DTO;
using InventoryManagementSystem.API.Interfaces;
using InventoryManagementSystem.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace InventoryManagementSystem.API.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
         
        /// <summary>
        /// Gets all products that have been created
        /// TODO: Add pagination in future
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("products")]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), 200)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        /// <summary>
        /// Gets a product by Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("product/{productId}")]
        [ProducesResponseType(typeof(ProductDTO), 200)]
        public async Task<ActionResult> GetProduct(int productId)
        {
            try
            {
                var product = await _productService.GetProductAsync(productId);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Creates a new product to be added into the inventory list
        /// </summary>
        /// <param name="productDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("product")]
        public async Task<ActionResult<ProductDTO>> AddProduct([FromBody] ProductCreateDTO productDTO)
        {
            try
            {
                var response = await _productService.AddProduct(productDTO);
                return Ok(response);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create product, please try again later");
            }
        }

        /// <summary>
        /// TODO: Update product to the dto and same with service layer
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("product")]
        public async Task<ActionResult<ProductDTO>> UpdateProduct(ProductUpdateDTO product)
        {
            try
            {
                var response = await _productService.UpdateProduct(product);

                if (response == null)
                {
                    return NotFound();
                }

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }
    }
}
