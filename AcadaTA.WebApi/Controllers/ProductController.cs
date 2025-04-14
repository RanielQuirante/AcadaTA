using AcadaTA.Models.Dtos.Request;
using AcadaTA.Models.Dtos.Response;
using AcadaTA.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AcadaTA.WebApi.Controllers
{
    /// <summary>
    /// Represents a controller for managing products in the system.
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints for adding, retrieving, and managing products.
    /// </remarks>
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
        /// Adds a new product to the system.
        /// </summary>
        /// <remarks>
        /// This endpoint requires a valid request product dto.
        /// If validation fails, it returns a 400 Bad Request with details in the response body.
        /// If successful, returns a 201 Created with a URI to the newly created product.
        /// </remarks>
        /// <param name="productDto">This should contain the product details.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ProductResponseDto>> AddProduct([FromBody] ProductRequestDto productDto)
        {
            // Asp net core doesn't need this, but adding this for unit test case.
            // So it wouldn't need to go to the Service layer anymore, and make this testable properly
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _productService.AddProductAsync(productDto);
            return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Retrieves all products in the system.
        /// </summary>
        /// <remarks>
        /// Returns a 204 No Content if there are no products found.
        /// Otherwise, returns a 200 OK with the list of products.
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<ProductResponseDto>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();

            if (products == null || products.Count == 0)
            {
                return NoContent();
            }

            return Ok(products);
        }

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <remarks>
        /// Returns 404 Not Found if the product doesn't exist. 
        /// Otherwise, returns a 200 OK with product.
        /// </remarks>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound("Not found: In order to receive a proper response, make sure to put a valid id.");
            }
            return Ok(product);
        }
    }
}
