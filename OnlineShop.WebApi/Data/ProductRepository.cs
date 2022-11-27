using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.WebApi.Data;

public class ProductRepository : EfRepository<Account>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}

public interface IProductRepository
{
}