using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data.Repositories;
using OnlineShop.Models;

namespace OnlineShop.WebApi.Controllers;

[Route("accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _repo;

    public AccountController(IAccountRepository repo)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    [HttpGet]
    [Route("get_all")]
    public async Task<IReadOnlyList<Account>> GetAllAccounts(CancellationToken cancellationToken = default)
    {
        var accounts = await _repo.GetAll(cancellationToken);
        return accounts;
    }

    [HttpGet]
    [Route("get_by_id")]
    public async Task<Account> GetAccountById(Guid id, CancellationToken cancellationToken = default)
    {
        var account = await _repo.GetById(id, cancellationToken);
        return account;
    }

    [HttpGet]
    [Route("get_by_email/{email}")]
    public Task<Account> GetAccountByEmail(string email, CancellationToken cancellationToken = default)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
        return _repo.GetByEmail(email, cancellationToken);
    }

    [HttpPost]
    [Route("add")]
    [AllowAnonymous]
    public async Task AddAccount([FromBody] Account account, CancellationToken cancellationToken = default)
    {
        if (account == null) throw new ArgumentNullException(nameof(account));
        await _repo.Add(account, cancellationToken);
    }

    [HttpPut("update")]
    public async Task Edit([FromBody] Account account, CancellationToken cancellationToken = default)
    {
        if (account == null) throw new ArgumentNullException(nameof(account));
        await _repo.Update(account, cancellationToken);
    }

    [HttpPost("delete")]
    public async Task DeleteAccount([FromBody] Account account, CancellationToken cancellationToken = default)
    {
        if (account == null) throw new ArgumentNullException(nameof(account));
        await _repo.Delete(account, cancellationToken);
    }
}