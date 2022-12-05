using OnlineShop.Domain.Entities;
using OnlineShop.HttpApiClient;

namespace OnlineShop.BlazorClient.Services;

public class CartService
{
    private readonly IShopClient _shopClient;
    
    public CartService(IShopClient shopClient)
    {
        _shopClient = shopClient;
    }
    public List<Product?> SelectedItems { get; set; } = new();
    
    public async Task AddProductToCart(Guid productId)
    {
        var product = await _shopClient.GetProduct(productId);
        
        if (SelectedItems.Contains(product) is false)
        {
            SelectedItems.Add(product);
        }
    }

    public async Task DeleteProductFromCart(Guid productId)
    {
        DeleteProductFromCart(await _shopClient.GetProduct(productId));
    }
    
    public void DeleteProductFromCart(Product? product)
    {
        if (SelectedItems.Contains(product))
        {
            SelectedItems.Remove(product);
        }
    }
}