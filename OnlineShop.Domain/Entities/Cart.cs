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
}
public class CartItem
{
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public double Quantity { get; set; }
}