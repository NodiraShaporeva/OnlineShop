namespace OnlineShop.Models;

public record Account : IEntity
{
    public Account(Guid id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public Guid Id { get; init; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
}