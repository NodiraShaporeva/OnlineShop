namespace OnlineShop.HttpModels.Response;

public class LogInResponse
{
    public LogInResponse(string? message = null)
    {
        Message = message;
    }

    public string? Message { get; set; }
    public string? Email { get; set; }
    public string? Token { get; set; }
}