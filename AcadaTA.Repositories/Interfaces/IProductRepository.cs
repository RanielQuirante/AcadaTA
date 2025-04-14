using AcadaTA.Models.Entities;

namespace AcadaTA.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductEntity> AddProductAsync(ProductEntity product);
        Task<List<ProductEntity>> GetAllProductsAsync();
        Task<ProductEntity?> GetProductByIdAsync(int id);
    }
}