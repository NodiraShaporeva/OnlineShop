namespace OnlineShop.Domain.Entities;

public record Product : IEntity
{
    public Product()
    {
    }

    public Product(Guid id, string name, string description, decimal price, string image)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Image = image;
    }

    public Guid Id { get; init; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public string Image { get; set; } = "";
}