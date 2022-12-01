using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data.Repositories;
using OnlineShop.Models;

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
    [Route("add")]
    [AllowAnonymous]
    public ActionResult<Account> AddAccount(Account account, CancellationToken cancellationToken = default)
    {
        if (account == null) throw new ArgumentNullException(nameof(account));
        if (string.IsNullOrWhiteSpace(account.Name))
        {
            ModelState.AddModelError("Name", "Name is not valid");
            return BadRequest(ModelState);
        }

        if (string.IsNullOrWhiteSpace(account.Password) || account.Password.Length < 6)
        {
            ModelState.AddModelError("Password", "Password is not valid");
            return BadRequest(ModelState);
        }

        if (string.IsNullOrWhiteSpace(account.Email) ||
            account.Email.IndexOf('@') < 1 ||
             account.Email.IndexOf('@') >= account.Email.Length-1)
        {
            ModelState.AddModelError("Email", "Email is not valid");
            return BadRequest(ModelState);
        }
        
        var acnt = _repo.Add(account, cancellationToken);
        return new ObjectResult(acnt)
        {
            DeclaredType = typeof(Account),
            StatusCode = StatusCodes.Status200OK
        };
    }

    [HttpPost("get_by_email/{email}")]
    public ActionResult<Account> FindAccount(string email, CancellationToken cancellationToken = default)
    {
        var acnt = _repo.GetByEmail(email, cancellationToken);
        return new ObjectResult(acnt)
        {
            DeclaredType = typeof(Account),
            StatusCode = StatusCodes.Status200OK
        };
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