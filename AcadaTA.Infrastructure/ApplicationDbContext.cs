using AcadaTA.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcadaTA.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ProductEntity> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEntity>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductEntity>().HasData(
                new ProductEntity { Id = 1, Name = "Notebook", Description = "College-ruled, 100 pages", Price = 25.00m },
                new ProductEntity { Id = 2, Name = "Ballpen", Description = "Blue ink, smooth writing", Price = 12.50m },
                new ProductEntity { Id = 3, Name = "Eraser", Description = "Rubber eraser, soft type", Price = 5.00m }
            );
        }
    }
}
