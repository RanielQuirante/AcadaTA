using AcadaTA.Models.Dtos.Request;
using AcadaTA.Models.Dtos.Response;

namespace AcadaTA.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponseDto> AddProductAsync(ProductRequestDto productDto);
        Task<List<ProductResponseDto>> GetAllProductsAsync();
        Task<ProductResponseDto?> GetProductByIdAsync(int id);
    }
}