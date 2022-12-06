using OnlineShop.Domain.Entities;
using OnlineShop.Domain.RepositoriesInterfaces;

namespace OnlineShop.Domain.Services;

public class AccountService
{
    private readonly IAccountRepository _repo;

    public AccountService(IAccountRepository repo)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    public virtual async Task<Account> Register(string name, string email, string password,
        CancellationToken cancellation = default)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));
        if (email == null) throw new ArgumentNullException(nameof(email));
        if (password == null) throw new ArgumentNullException(nameof(password));

        if (await _repo.FindByEmail(email, cancellation) != null)
        {
            throw new EmailAlreadyExistsException("Такой имейл уже зарегистрирован", email);
        }

        var account = new Account(Guid.NewGuid(), name, email, password);
        await _repo.Add(account, cancellation);
        return account;
    }
}

[Serializable]
public class EmailAlreadyExistsException : Exception
{
    public string Email { get; }
    public EmailAlreadyExistsException(string message, string email)
        : base(message)
    {
        Email = email;
    }
}