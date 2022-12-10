using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Services;
using OnlineShop.HttpModels.Request;

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
    public async Task<ActionResult<Account>> Register(RegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var account = await _service.Register(request.Name, request.Email, request.Password, cancellationToken);
            return Ok(account);
        }
        catch (EmailAlreadyExistsException)
        {
            return BadRequest(new { message = "Такой имейл уже зарегистрирован" });
        }
    }

    [HttpPost]
    [Route("check")]
    public async Task<ActionResult<Account>> LogIn(RegisterRequest request, string email, string password,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var account = await _service.LogIn(request.Email, request.Password, cancellationToken);
            return Ok(account);
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
}