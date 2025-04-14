using AcadaTA.Infrastructure;
using AcadaTA.Models.Entities;
using AcadaTA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AcadaTA.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductEntity>> GetAllProductsAsync()
        {
            return await _context.Products.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<ProductEntity?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            return product;
        }

        public async Task<ProductEntity> AddProductAsync(ProductEntity product)
        {
            var entityEntry = await _context.Products.AddAsync(product).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entityEntry.Entity;
        }
    }
}
