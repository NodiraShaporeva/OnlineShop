namespace OnlineShop.HttpModels.Response;

public class RegisterResponse
{
    public RegisterResponse(string token)
    {
        Token = token;
    }

    public string? Message { get; set; }
    public string? Email { get; set; }
    public string Token { get; }
    public Guid AccountId { get; set; }
}