namespace OnlineShop.HttpModels.Response;

public record ErrorResponse(string Message)
{
    public override string ToString()
    {
        return $"{{Message = {Message}}}";
    }
}