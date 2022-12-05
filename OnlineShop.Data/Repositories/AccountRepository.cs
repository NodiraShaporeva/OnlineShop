using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.RepositoriesInterfaces;

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

    public Task<Account?> FindByEmail(string email, CancellationToken cancellationToken = default)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
        return Entities.FirstOrDefaultAsync(it => it.Email == email, cancellationToken);
    }
}