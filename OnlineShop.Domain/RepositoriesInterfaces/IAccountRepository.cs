using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.RepositoriesInterfaces;

public interface IAccountRepository : IRepository<Account>
{
    public Task<Account> GetByEmail(string email, CancellationToken cancellationToken = default);
    public Task<Account?> FindByEmail(string email, CancellationToken cancellationToken = default);
}