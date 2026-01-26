using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace OHS_program_api.API.Attributes
{
    /// <summary>
    /// Response caching attribute - belirli süreli GET endpoint'leri cache'ler
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheResponseAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _durationSeconds;

        public CacheResponseAttribute(int durationSeconds = 300) // Default 5 minutes
        {
            _durationSeconds = durationSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // GET istekleri için cache kontrolü
            if (!HttpMethods.IsGet(context.HttpContext.Request.Method))
            {
                await next();
                return;
            }

            var cacheKey = GenerateCacheKey(context);
            var cache = context.HttpContext.RequestServices.GetRequiredService<IMemoryCache>();

            if (cache.TryGetValue(cacheKey, out var cachedResponse))
            {
                context.Result = new Microsoft.AspNetCore.Mvc.OkObjectResult(cachedResponse);
                return;
            }

            var executedContext = await next();

            if (executedContext.Result is Microsoft.AspNetCore.Mvc.OkObjectResult okResult && okResult.Value != null)
            {
                cache.Set(cacheKey, okResult.Value, TimeSpan.FromSeconds(_durationSeconds));
            }
        }

        private string GenerateCacheKey(ActionExecutingContext context)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{context.HttpContext.Request.Path}");

            // Query parameters'ı cache key'e ekle
            foreach (var queryParam in context.HttpContext.Request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"_{queryParam.Key}={queryParam.Value}");
            }

            return keyBuilder.ToString();
        }
    }
}
