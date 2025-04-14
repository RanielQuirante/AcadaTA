using AcadaTA.Models.Dtos.Request;
using AcadaTA.Models.Dtos.Response;
using AcadaTA.Models.Entities;
using AcadaTA.Repositories.Interfaces;
using AcadaTA.Services.Implementations;
using AutoMapper;
using Moq;

namespace AcadaTA.Services.UnitTests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _productRepoMock = new Mock<IProductRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new ProductService(_productRepoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnMappedList()
        {
            // Arrange
            var products = new List<ProductEntity>
            {
                new ProductEntity { Id = 1, Name = "Test 1", Description = "Desc", Price = 100 },
                new ProductEntity { Id = 2, Name = "Test 2", Description = "Desc 2", Price = 200 }
            };

            var expectedDtoList = new List<ProductResponseDto>
            {
                new ProductResponseDto { Id = 1, Name = "Test 1", Description = "Desc", Price = 100 },
                new ProductResponseDto { Id = 2, Name = "Test 2", Description = "Desc 2", Price = 200 }
            };

            _productRepoMock.Setup(r => r.GetAllProductsAsync()).ReturnsAsync(products);
            _mapperMock.Setup(m => m.Map<List<ProductResponseDto>>(products)).Returns(expectedDtoList);

            // Act
            var result = await _service.GetAllProductsAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnMappedDto_WhenProductExists()
        {
            // Arrange
            var product = new ProductEntity { Id = 1, Name = "Test", Description = "Desc", Price = 100 };
            var expectedDto = new ProductResponseDto { Id = 1, Name = "Test", Description = "Desc", Price = 100 };

            _productRepoMock.Setup(r => r.GetProductByIdAsync(1)).ReturnsAsync(product);
            _mapperMock.Setup(m => m.Map<ProductResponseDto>(product)).Returns(expectedDto);

            // Act
            var result = await _service.GetProductByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result?.Id);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            _productRepoMock.Setup(r => r.GetProductByIdAsync(1)).ReturnsAsync((ProductEntity?)null);

            // Act
            var result = await _service.GetProductByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddProductAsync_ShouldReturnMappedResult()
        {
            // Arrange
            var requestDto = new ProductRequestDto { Name = "New", Description = "New Desc", Price = 99 };
            var productEntity = new ProductEntity { Name = "New", Description = "New Desc", Price = 99 };
            var createdProduct = new ProductEntity { Id = 1, Name = "New", Description = "New Desc", Price = 99 };
            var expectedResponse = new ProductResponseDto { Id = 1, Name = "New", Description = "New Desc", Price = 99 };

            _mapperMock.Setup(m => m.Map<ProductEntity>(requestDto)).Returns(productEntity);
            _productRepoMock.Setup(r => r.AddProductAsync(productEntity)).ReturnsAsync(createdProduct);
            _mapperMock.Setup(m => m.Map<ProductResponseDto>(createdProduct)).Returns(expectedResponse);

            // Act
            var result = await _service.AddProductAsync(requestDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }
    }
}