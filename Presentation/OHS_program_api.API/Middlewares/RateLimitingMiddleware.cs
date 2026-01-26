using System.Collections.Concurrent;

namespace OHS_program_api.API.Middlewares
{
    /// <summary>
    /// Rate Limiting Middleware - IP adresi başına belirli sayıda request izni verir
    /// </summary>
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly int _maxRequests;
        private readonly int _windowSeconds;
        private static readonly ConcurrentDictionary<string, List<DateTime>> _requestsCache = new();

        public RateLimitingMiddleware(RequestDelegate next, int maxRequests = 100, int windowSeconds = 60)
        {
            _next = next;
            _maxRequests = maxRequests;
            _windowSeconds = windowSeconds;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var now = DateTime.UtcNow;
            var windowStart = now.AddSeconds(-_windowSeconds);

            if (!_requestsCache.TryGetValue(clientIp, out var requests))
            {
                requests = new List<DateTime>();
                _requestsCache.TryAdd(clientIp, requests);
            }

            lock (requests)
            {
                // Pencere dışındaki istekleri temizle
                requests.RemoveAll(r => r < windowStart);

                if (requests.Count >= _maxRequests)
                {
                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    context.Response.WriteAsJsonAsync(new
                    {
                        message = "Rate limit exceeded",
                        retryAfter = _windowSeconds
                    }).Wait();
                    return;
                }

                requests.Add(now);
            }

            await _next(context);
        }
    }
}
