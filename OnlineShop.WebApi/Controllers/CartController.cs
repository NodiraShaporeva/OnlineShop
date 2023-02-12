using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Services;

namespace OnlineShop.WebApi.Controllers;

[ApiController]
[Route("carts")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly CartService _cartService;

    public CartController(CartService cartService)
    {
        _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
    }
    [HttpGet("get")]
    public Task<Cart> GetCart(Guid accountId)
    {
        return _cartService.GetCartForAccount(accountId);
    }
}