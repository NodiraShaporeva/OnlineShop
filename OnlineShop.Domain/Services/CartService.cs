using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Services;

public class CartService
{
    private readonly IUnitOfWork _uow;

    public CartService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Cart> GetCartForAccount(Guid accountId)
    {
        var cart = await _uow.CartRepository.GetByAccountId(accountId);
        return cart;
    }

    public async Task AddItem(Guid accountId, Product product, double quantity = 1d)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        if(quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        var cart = await _uow.CartRepository.GetByAccountId(accountId);
        await AddItem(cart, product, quantity);
    }

    public async Task AddItem(Cart cart, Product product, double quantity = 1d)
    {
        if (cart == null) throw new ArgumentNullException(nameof(cart));
        if (product == null) throw new ArgumentNullException(nameof(product));
        if(quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        cart.Add(product, quantity);
        
        await _uow.CartRepository.Update(cart);
        await _uow.SaveChangesAsync();
    }
}