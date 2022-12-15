using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.RepositoriesInterfaces;

public interface ITokenService
{
    string GenerateToken(Account account);
}