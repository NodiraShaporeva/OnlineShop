using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.WebApi.Data;

public class AccountRepository : EfRepository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public Task<Account> GetByEmail(string email)
        => Entities.SingleAsync(it => it.Email == email);
}

public interface IAccountRepository
{
    public Task<Account> GetByEmail(string email);
}