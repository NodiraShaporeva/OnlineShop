namespace OnlineShop.Domain.Entities;

public record Account : IEntity
{
    public Account(Guid id, string name, string email, string password)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PasswordHash = password ?? throw new ArgumentNullException(nameof(password));
    }

    private Account()
    {
        
    }
    public Guid Id { get; init; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
}
