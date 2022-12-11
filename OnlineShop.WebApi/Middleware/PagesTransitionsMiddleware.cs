namespace OnlineShop.WebApi.Middleware;

public class PagesTransitionsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<PagesTransitionsMiddleware> _logger;
    public readonly Dictionary<string, int> Transitions = new();

    public PagesTransitionsMiddleware(RequestDelegate next,
        ILogger<PagesTransitionsMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        if (!Transitions.TryGetValue(context.Request.Path, out int value)) value = 0;
        Transitions[context.Request.Path] = ++value;
        _logger.LogInformation("Request Path: {Path}, transitions: {value}", context.Request.Path, value);

        await _next(context);
    }
}