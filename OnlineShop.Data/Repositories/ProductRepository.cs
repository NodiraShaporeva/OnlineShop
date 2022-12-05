using OnlineShop.Domain.Entities;
using OnlineShop.Domain.RepositoriesInterfaces;

namespace OnlineShop.Data.Repositories;

public class ProductRepository : EfRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
    }
}