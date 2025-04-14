using AcadaTA.Models.Dtos.Request;
using AcadaTA.Models.Dtos.Response;
using AcadaTA.Models.Entities;
using AcadaTA.Repositories.Interfaces;
using AcadaTA.Services.Interfaces;
using AutoMapper;

namespace AcadaTA.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync().ConfigureAwait(false);

            if (products == null || products.Count == 0)
            {
                return new List<ProductResponseDto>();
            }

            var mapResult = _mapper.Map<List<ProductResponseDto>>(products);
            return mapResult;
        }

        public async Task<ProductResponseDto?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id).ConfigureAwait(false);

            if (product == null)
            {
                return null;
            }

            var mapResult = _mapper.Map<ProductResponseDto>(product);
            return mapResult;
        }

        public async Task<ProductResponseDto> AddProductAsync(ProductRequestDto productDto)
        {
            var mapRequestProduct = _mapper.Map<ProductEntity>(productDto);
            var result = await _productRepository.AddProductAsync(mapRequestProduct).ConfigureAwait(false);
            var mapResult = _mapper.Map<ProductResponseDto>(result);
            return mapResult;
        }
    }
}
