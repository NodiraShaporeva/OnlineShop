using System.ComponentModel.DataAnnotations;

namespace OnlineShop.BlazorClient.Pages;

public class CheckoutForm
{
    [Required(ErrorMessage = "Требуется ввести имя.")]
    public string Name { get; set; } = "";
    [Required(ErrorMessage = "Нам нужен адрес для доставки товара.")]
    public string Address { get; set; } = "";
}