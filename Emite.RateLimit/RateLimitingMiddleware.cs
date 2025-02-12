using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;

public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly ConcurrentDictionary<string, DateTime> _clientRequests = new ConcurrentDictionary<string, DateTime>();

    public RateLimitingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var clientIp = context.Connection.RemoteIpAddress?.ToString();

        if (clientIp != null && _clientRequests.TryGetValue(clientIp, out var lastRequestTime))
        {
            var timeSpan = DateTime.UtcNow - lastRequestTime;
            if (timeSpan.TotalSeconds < 1)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
                return;
            }
        }

        _clientRequests[clientIp] = DateTime.UtcNow;
        await _next(context);
    }
}
