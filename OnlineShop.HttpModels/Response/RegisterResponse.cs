namespace OnlineShop.HttpModels.Response;

public class RegisterResponse
{
    public RegisterResponse(string? message = null)
    {
        Message = message;
    }

    public string? Message { get; set; }
    public string? Email { get; set; }
    public string? Token { get; set; }
}