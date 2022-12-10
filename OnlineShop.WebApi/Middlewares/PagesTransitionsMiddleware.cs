namespace OnlineShop.WebApi.Middlewares;

public class PagesTransitionsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<PagesTransitionsMiddleware> _logger;

    public PagesTransitionsMiddleware(RequestDelegate next,                                                                                                              
        ILogger<PagesTransitionsMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("Request Method: {Method}", context.Request.Method);
        await _next(context);
    }
}
