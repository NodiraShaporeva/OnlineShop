namespace OnlineShop.WebApi.Middlewares;

public class PagesTransitionsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<PagesTransitionsMiddleware> _logger;
    private readonly Dictionary<string, int> _transitions = new Dictionary<string, int>();

    public PagesTransitionsMiddleware(RequestDelegate next,                                                                                                              
        ILogger<PagesTransitionsMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("Request Method: {Method}", context.Request.Method);
        if (!_transitions.TryGetValue(context.Request.Path, out int value)) value = 0;
        _transitions[context.Request.Path] = ++value;
        _logger.LogInformation("Request Path: {Path}, transitions: {value}", context.Request.Path, value);

        await _next(context);
    }
}
