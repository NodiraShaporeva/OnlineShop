using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618

namespace OnlineShop.HttpModels.Request;

public class RegisterRequest
{
    [Required]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [MinLength(6)]
    public string Password { get; set; }
        
    [Required]
    [Display(Name ="Confirm Password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    [DataType(DataType.Password)]
    public string PasswordConfirm { get; set; }
}