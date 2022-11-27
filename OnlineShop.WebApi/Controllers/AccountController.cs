using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.WebApi.Data;

namespace OnlineShop.WebApi.Controllers;

[Route("accounts")]
public class AccountController : ControllerBase
{
    private readonly IRepository<Account> _accountRepository;
    private readonly IAccountRepository _repo;

    public AccountController(IRepository<Account> accountRepository, IAccountRepository repo)
    {
        _accountRepository = accountRepository;
        _repo = repo;
    }

    [HttpGet]
    [Route("get_all")]
    public async Task<IReadOnlyList<Account>> GetAllAccounts(CancellationToken cancellationToken)
    {
        var accounts = await _accountRepository.GetAll(cancellationToken);
        return accounts;
    }

    [HttpGet]
    [Route("get_by_id")]
    public async Task<Account> GetAccountById(Guid id, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetById(id, cancellationToken);
        return account;
    }
    [HttpGet]
    [Route("get_by_email/{email}")]
    public Task<Account> GetAccountByEmail(string email)
        => _repo.GetByEmail(email);
    
    [HttpPost]
    [Route("add")]
    [AllowAnonymous]
    public async Task AddAccount([FromBody] Account account, CancellationToken cancellationToken)
    {
        await _accountRepository.Add(account, cancellationToken);
    }

    [HttpPut("update")]
    public async Task Edit([FromBody] Account account, CancellationToken cancellationToken)
    {
        await _accountRepository.Update(account, cancellationToken);
    }

    [HttpPost("delete")]
    public async Task DeleteAccount([FromBody] Account account, CancellationToken cancellationToken)
    {
        await _accountRepository.Delete(account, cancellationToken);
    }
}