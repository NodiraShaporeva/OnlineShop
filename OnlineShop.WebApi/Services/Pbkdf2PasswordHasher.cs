using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Services;

namespace OnlineShop.WebApi.Services;

public class Pbkdf2PasswordHasher: IPasswordHasherService
{
    private readonly PasswordHasher<Account> _hasher;
    private readonly Account _dummy = new(Guid.Empty, "", "fake@fake.com", "");

    public Pbkdf2PasswordHasher(IOptions<PasswordHasherOptions> optionAccessor)
    {
        _hasher = new PasswordHasher<Account>(optionAccessor);
    }
    public string HashPassword(string password)
    {
        string hashedPassword = _hasher.HashPassword(_dummy, password);
        return hashedPassword;
    }

    public bool VerifyPassword(string passwordHash, string providedPassword)
    {
        var result = _hasher.VerifyHashedPassword(_dummy, passwordHash, providedPassword);
        return result is not PasswordVerificationResult.Failed;
    }
}