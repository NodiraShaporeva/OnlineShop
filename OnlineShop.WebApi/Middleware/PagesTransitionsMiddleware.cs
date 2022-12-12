using System.Collections.Concurrent;

namespace OnlineShop.WebApi.Middleware;

public class PagesTransitionsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ConcurrentDictionary<string, int> _transitions = new();

    public PagesTransitionsMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        var requestPath = context.Request.Path.ToString();

        if (requestPath == "/metrics")
        {
            if (!context.Response.HasStarted)
            {
                await context.Response.WriteAsJsonAsync(_transitions);
            }
        }
        else
        {
            _transitions.AddOrUpdate(context.Request.Path,
                _ => 1,
                (_, count) => count + 1
            );
            await _next(context);
        }
    }
}