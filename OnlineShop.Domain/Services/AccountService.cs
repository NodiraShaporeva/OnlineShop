using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.RepositoriesInterfaces;

namespace OnlineShop.Domain.Services;

public class AccountService
{
    private readonly IAccountRepository _repo;
    private readonly IPasswordHasherService _service;
    private readonly ITokenService _jwtTokenService;
    private readonly IUnitOfWork _uow;

    public AccountService(IAccountRepository repo, IPasswordHasherService service, IUnitOfWork uow, ITokenService jwtTokenService)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        _service = service;
        _uow = uow;
        _jwtTokenService = jwtTokenService;
    }

    public virtual async Task<(Account, string token)> Register(string name, string email, string passwordHash,
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
        Cart cart = new() { Id = Guid.NewGuid(), AccountId = account.Id };
        
        await _uow.AccountRepository.Add(account, cancellation);
        await _uow.CartRepository.Add(cart, cancellation);
        await _uow.SaveChangesAsync(cancellation);

        var token = _jwtTokenService.GenerateToken(account);
        return (account, token);
    }

    public async Task<(Account, string token)> LogIn(string email, string password, CancellationToken cancellationToken = default)
    {
        var account = await _repo.FindByEmail(email, cancellationToken);
        if (account is null) throw new EmailNotFoundException(email);

        var result = _service.VerifyPassword(account.PasswordHash, password);
        if (!result)
        {
            throw new IncorrectPasswordException();
        }
        return (account, _jwtTokenService.GenerateToken(account));
    }

    public  Task<Account> GetAccount(Guid guid)
    {
        return _repo.GetById(guid);
    }
}