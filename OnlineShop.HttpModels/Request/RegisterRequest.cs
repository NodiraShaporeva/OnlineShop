using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618

namespace OnlineShop.HttpModels.Request;

public class RegisterRequest
{
    private const string PasswordPattern =
        @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}[]:;<>,.?/~_+-=|\]).{8,32}$";

    [Required] public string Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    //[RegularExpression(PasswordPattern)]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    [Display(Name = "Подтверждение пароля")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string PasswordConfirm { get; set; }
}