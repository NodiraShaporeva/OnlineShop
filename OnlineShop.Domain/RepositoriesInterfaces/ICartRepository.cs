using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.RepositoriesInterfaces;

public interface ICartRepository: IRepository<Cart>
{
    Task<Cart> GetByAccountId(Guid accountId, CancellationToken cancellationToken = default);
}