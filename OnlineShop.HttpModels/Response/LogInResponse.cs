namespace OnlineShop.HttpModels.Response;

public class LogInResponse
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}