namespace OnlineShop.HttpModels.Response;

public class LogInResponse
{
    public LogInResponse(string token)
    {
        Token = token;
    }

    public string? Message { get; set; }
    public string? Email { get; set; }
    public string Token { get; set; }
}