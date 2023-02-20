namespace OnlineShop.Domain.Entities;

public record Cart: IEntity
{
    public int ItemCount => Items.Count;
    public Guid AccountId { get; set; }

    public List<CartItem> Items { get; set; } = new();
    public Guid Id { get; init; }
   
    public decimal GetTotalPrice()
    {
        return Items.Sum(it => it.Price);
    }
    
    public void Add(Product product, double quantity = 1d)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        if(quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        
        var cartItem = Items.SingleOrDefault(it => it.ProductId == product.Id);
        if (cartItem is not null)
        {
            var newQty = cartItem.Quantity + quantity;
            if (newQty > 1000)
            {
                throw new InvalidOperationException("Quantity cannot be greater than 1000");
            }

            cartItem.Quantity = newQty;
        }
        else
        {
            cartItem = new CartItem()
            {
                Id = Guid.Empty,
                Price = product.Price,
                ProductId = product.Id,
                Quantity = quantity
            };
            Items.Add(cartItem);
        }
    }
}
public class CartItem
{
    public Guid Id { get; init; }
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public double Quantity { get; set; }
}