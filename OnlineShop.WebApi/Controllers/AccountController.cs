using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data.Repositories;
using OnlineShop.Domain;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.RepositoriesInterfaces;
using OnlineShop.HttpModels.Request;

namespace OnlineShop.WebApi.Controllers;

[ApiController]
[Route("accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _repo;

    public AccountController(IAccountRepository repo)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<Account>> Register(RegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        Account? existedAccount = await _repo.FindByEmail(request.Email, cancellationToken);
        var emailRegistered = existedAccount is not null;
        if (emailRegistered)
        {
            return BadRequest(new { message = "Such email exists" });
        }

        var account = new Account(Guid.NewGuid(), request.Name, request.Email, request.Password);
        await _repo.Add(account, cancellationToken);
        return Ok(account);
    }

    [HttpPost("get_by_email/{email}")]
    public async Task<Account> FindAccount(string email, CancellationToken cancellationToken = default)
    {
        var account = await _repo.GetByEmail(email, cancellationToken);
        return account;
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