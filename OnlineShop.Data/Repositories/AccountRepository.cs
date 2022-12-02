using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain;
using OnlineShop.Models;

namespace OnlineShop.Data.Repositories;

public class AccountRepository : EfRepository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext dbContext) : base(dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
    }

    public Task<Account> GetByEmail(string email, CancellationToken cancellationToken = default)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
        return Entities.SingleAsync(it => it.Email == email, cancellationToken);
    }
}

public interface IAccountRepository : IRepository<Account>
{
    public Task<Account> GetByEmail(string email, CancellationToken cancellationToken = default);
}