using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Exceptions;
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
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<LogInResponse>> Register(RegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var (account, token) = await _service.Register(request.Name, request.Email, request.Password, cancellationToken);
            return new LogInResponse(token);
            
        }
        catch (EmailAlreadyExistsException)
        {
            return BadRequest(new { message = "Такой имейл уже зарегистрирован" });
        }
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<LogInResponse>> LogIn(LogInRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var (account, token) = await _service.LogIn(request.Email, request.Password, cancellationToken);
            return new LogInResponse(token);
        }
        catch (EmailNotFoundException)
        {
            return Unauthorized(new { message = "Такой имейл не зарегистрирован" });
        }
        catch (IncorrectPasswordException)
        {
            return Unauthorized(new { message = "Неверный пароль" });
        }
    }
    
    [Authorize]
    [HttpGet("get_account")]
    public async Task<ActionResult<Account>> GetCurrentAccount()
    {
        var strId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var guid = Guid.Parse(strId!);
        Account account = await _service.GetAccount(guid);
        return account;
    }

}