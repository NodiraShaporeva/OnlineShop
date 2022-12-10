using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.RepositoriesInterfaces;

namespace OnlineShop.Domain.Services;

public class AccountService
{
    private readonly IAccountRepository _repo;
    private readonly IPasswordHasherService _service;
    public AccountService(IAccountRepository repo, IPasswordHasherService service)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        _service = service;
    }

    public virtual async Task<Account> Register(string name, string email, string passwordHash,
        CancellationToken cancellation = default)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));
        if (email == null) throw new ArgumentNullException(nameof(email));
        if (passwordHash == null) throw new ArgumentNullException(nameof(passwordHash));

        if (await _repo.FindByEmail(email, cancellation) != null)
        {
            throw new EmailAlreadyExistsException("Такой имейл уже зарегистрирован", email);
        }

        string hashedPassword = _service.HashPassword(passwordHash);
        var account = new Account(Guid.NewGuid(), name, email, hashedPassword);
        await _repo.Add(account, cancellation);
        return account;
    }

    public async Task<Account> LogIn(string email, string password, CancellationToken cancellationToken = default)
    {
        var account = await _repo.FindByEmail(email, cancellationToken);
        if (account is null) throw new EmailNotFoundException(email);

        var result = _service.VerifyPassword(account.PasswordHash, password);
        if (!result)
        {
            throw new IncorrectPasswordException();
        }
        return account;
    }
}