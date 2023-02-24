using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Test;

public class CartTests
{
    public CartTests()
    {
        
    }
    [Fact]
    public void New_cart_is_empty()
    {
        var cart = new Cart()
        {
            Id=Guid.Empty,
            AccountId = Guid.Empty,
            Items=new List<CartItem>()
        };   
        Assert.Empty(cart.Items);
    }
    
    [Fact]
    public void New_item_is_added_to_cart()
    {
        var cart = new Cart()
        {
            Id=Guid.Empty,
            AccountId = Guid.Empty,
            Items=new List<CartItem>()
        };
        var product = new Product(Guid.NewGuid(), "Excellent code", "", 500, "");
        var quantity = 1d;
        
        cart.Add(product);

        Assert.Single(cart.Items);
        CartItem cartItem = cart.Items.First();
        Assert.NotNull(cartItem);
        Assert.Equal(product.Id, cartItem.ProductId);
        Assert.Equal(quantity, cartItem.Quantity);
        Assert.Equal(product.Price, cartItem.Price);
    }
    
    [Fact]
    private void Adding_existed_product_to_cart_changes_item_quantity()
    {
        var cart = new Cart()
        {
            Id=Guid.Empty,
            AccountId = Guid.Empty,
            Items=new List<CartItem>()
        };
        var product = new Product(Guid.NewGuid(), "Excellent code", "", 500, "");
        var quantity = 2d;
        
        cart.Add(product);

        Assert.Single(cart.Items);
        
        cart.Add(product);
        
        CartItem cartItem = cart.Items.First();
        Assert.NotNull(cartItem);
        Assert.Equal(product.Id, cartItem.ProductId);
        Assert.Equal(quantity, cartItem.Quantity);
        Assert.Equal(product.Price, cartItem.Price);
    }
    
    [Fact]
    private void Adding_empty_product_is_impossible()
    {
        var cart = new Cart()
        {
            Id=Guid.Empty,
            AccountId = Guid.Empty,
            Items=new List<CartItem>()
        };
        
        Assert.Throws<ArgumentNullException>(() => cart.Add(null));
    }

    [Fact]
    private void Quantity_must_be_positive()
    {
        var cart = new Cart()
        {
            Id=Guid.Empty,
            AccountId = Guid.Empty,
            Items=new List<CartItem>()
        };
        var product = new Product(Guid.NewGuid(), "Excellent code", "", 500, "");
        
        Assert.Throws<ArgumentOutOfRangeException>(() => cart.Add(product, 0));
    }
    
    [Fact]
    private void Adding_more_than_1000_items_is_impossible()
    {
        var cart = new Cart()
        {
            Id=Guid.Empty,
            AccountId = Guid.Empty,
            Items=new List<CartItem>()
        };
        var product = new Product(Guid.NewGuid(), "Excellent code", "", 500, "");

        for (int i = 0; i < 1000; i++)
        {
            cart.Add(product);
        }
        Assert.Throws<InvalidOperationException>(() => cart.Add(product));
    }
}