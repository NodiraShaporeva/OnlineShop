using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Services;
using OnlineShop.HttpModels.Request;
using OnlineShop.HttpModels.Response;

namespace OnlineShop.WebApi.Controllers;

[ApiController]
[Route("accounts")]
public class AccountController : ControllerBase
{
    private readonly AccountService _service;

    public AccountController(AccountService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<LogInResponse>> Register(RegisterRequest request,
        CancellationToken cancellationToken = default)
    {
            var (account, token) = await _service.Register(request.Name, request.Email, request.Password, cancellationToken); 
            return new LogInResponse(token);
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<LogInResponse>> LogIn(LogInRequest request,
        CancellationToken cancellationToken = default)
    {
            var (account, token) = await _service.LogIn(request.Email, request.Password, cancellationToken);
            return new LogInResponse(token);
    }
    
    [Authorize]
    [HttpGet("get_account")]
    public async Task<ActionResult<Account>> GetCurrentAccount(CancellationToken cancellationToken)
    {
        var strId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var guid = Guid.Parse(strId!);
        Account account = await _service.GetAccount(guid);
        return account;
    }
}