using OnlineShop.Domain.RepositoriesInterfaces;

namespace OnlineShop.Domain;

public interface IUnitOfWork: IDisposable
{
    IAccountRepository AccountRepository { get; }
    ICartRepository CartRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}