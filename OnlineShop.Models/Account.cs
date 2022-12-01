using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models;

public record Account : IEntity
{
    public Account(Guid id, string name, string email, string password, string passwordConfirm)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        PasswordConfirm = passwordConfirm;
    }
    public Guid Id { get; init; }
    
    [Required]
    public string Name { get; set; } = "";
    
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = "";
    
    [Required]
    [DataType(DataType.Password)]
    [MinLength(6)]
    public string Password { get; set; }
         
    [Required]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    public string PasswordConfirm { get; set; }
}
