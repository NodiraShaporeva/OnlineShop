using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.RepositoriesInterfaces;

namespace OnlineShop.Data.Repositories;

public class CartRepository: EfRepository<Cart>, ICartRepository
{
    public CartRepository(AppDbContext dbContext) : base(dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
    }

    public Task<Cart> GetByAccountId(Guid accountId, CancellationToken cancellationToken = default)
    {
        // return GetById(accountId, cancellationToken);
        return Entities.SingleAsync(it => it.AccountId == accountId, cancellationToken);
    }
}