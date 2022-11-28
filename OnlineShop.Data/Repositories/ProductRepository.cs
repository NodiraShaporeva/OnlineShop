using OnlineShop.Models;

namespace OnlineShop.Data.Repositories;

public class ProductRepository : EfRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
    }
}

public interface IProductRepository : IRepository<Product>
{
}