using AcadaTA.Models.Dtos.Request;
using AcadaTA.Models.Dtos.Response;
using AcadaTA.Services.Interfaces;
using AcadaTA.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace AcadaTA.Controller.UnitTests
{
    public class ProductControllerUnitTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductController _controller;

        public ProductControllerUnitTests()
        {
            _mockProductService = new Mock<IProductService>();
            _controller = new ProductController(_mockProductService.Object);
        }

        [Fact]
        public async Task AddProduct_ValidProduct_ReturnsCreatedAtAction()
        {
            // Arrange
            var requestDto = new ProductRequestDto
            {
                Name = "ValidName",
                Description = "ValidDescription",
                Price = 99.99m
            };

            var responseDto = new ProductResponseDto
            {
                Id = 1,
                Name = requestDto.Name,
                Description = requestDto.Description,
                Price = requestDto.Price
            };

            // Mock the service to return a valid ProductResponseDto
            _mockProductService
                .Setup(s => s.AddProductAsync(requestDto))
                .ReturnsAsync(responseDto);

            // Act
            var result = await _controller.AddProduct(requestDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProductResponseDto>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var createdDto = Assert.IsType<ProductResponseDto>(createdAtActionResult.Value);

            Assert.Equal(nameof(ProductController.GetProductById), createdAtActionResult.ActionName);
            Assert.Equal(responseDto.Id, createdDto.Id);
            Assert.Equal(requestDto.Name, createdDto.Name);
        }

       
        [Fact]
        public async Task GetAllProducts_EmptyList_ReturnsNoContent()
        {
            // Arrange
            _mockProductService
                .Setup(s => s.GetAllProductsAsync())
                .ReturnsAsync(new List<ProductResponseDto>());

            // Act
            var result = await _controller.GetAllProducts();

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<ProductResponseDto>>>(result);
            Assert.IsType<NoContentResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetAllProducts_NonEmptyList_ReturnsOk()
        {
            // Arrange
            var products = new List<ProductResponseDto>
            {
                new ProductResponseDto { Id = 1, Name = "Product1", Price = 50.0m },
                new ProductResponseDto { Id = 2, Name = "Product2", Price = 75.0m }
            };

            _mockProductService
                .Setup(s => s.GetAllProductsAsync())
                .ReturnsAsync(products);

            // Act
            var result = await _controller.GetAllProducts();

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<ProductResponseDto>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedProducts = Assert.IsType<List<ProductResponseDto>>(okResult.Value);

            Assert.Equal(2, returnedProducts.Count);
        }

        [Fact]
        public async Task GetProductById_ProductNotFound_ReturnsNotFound()
        {
            // Arrange
            var nonExistentId = 999;

            // Mock the service to return null for that ID
            _mockProductService
                .Setup(s => s.GetProductByIdAsync(nonExistentId))
                .ReturnsAsync((ProductResponseDto)null);

            // Act
            var result = await _controller.GetProductById(nonExistentId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProductResponseDto>>(result);
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal("Not found: In order to receive a proper response, make sure to put a valid id.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetProductById_ProductFound_ReturnsOk()
        {
            // Arrange
            var existingId = 1;
            var productResponse = new ProductResponseDto
            {
                Id = existingId,
                Name = "ExistingProduct",
                Description = "Some Description",
                Price = 99.99m
            };

            _mockProductService
                .Setup(s => s.GetProductByIdAsync(existingId))
                .ReturnsAsync(productResponse);

            // Act
            var result = await _controller.GetProductById(existingId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProductResponseDto>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedProduct = Assert.IsType<ProductResponseDto>(okResult.Value);

            Assert.Equal(existingId, returnedProduct.Id);
            Assert.Equal("ExistingProduct", returnedProduct.Name);
        }

        [Fact]
        public void AddProduct_InvalidProductRequestDto_ShouldFailValidation()
        {
            // Arrange
            var dto = new ProductRequestDto
            {
                Name = null,
                Description = "Some desc",
                Price = 0
            };

            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(dto);

            // Act
            var isValid = Validator.TryValidateObject(dto, context, validationResults, validateAllProperties: true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults,
                r => r.ErrorMessage == "Name is required.");
            Assert.Contains(validationResults,
                r => r.ErrorMessage == "Price must be greater than 1.");
        }

        [Fact]
        public async Task AddProduct_InvalidDto_ReturnsBadRequest()
        {
            // Arrange
            var productDto = new ProductRequestDto
            {
                Name = "Item",
                Description = "Desc",
                Price = 0
            };

            _controller.ModelState.AddModelError("Price", "Invalid price");

            // Act
            var result = await _controller.AddProduct(productDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}