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
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("products")]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), 200)]
        public async Task<ActionResult<IEnumerable<Data.Entities.Product>>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            if (products == null)
            {
                Console.WriteLine("No Products found");
            }

            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productDTOs);
        }

        [HttpPost]
        [Route("products")]
        public async Task<ActionResult<ProductDTO>> AddProduct([FromBody] ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return BadRequest();
            }

            var product = _mapper.Map<Data.Entities.Product>(productDTO);

            var response = await _productService.AddProduct(product);

            return Ok(response);
        }
    }
}
