using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Store.Core.Services.Contract;
using System.Net.Mime;
using System.Text;

namespace Store.Api.Attributes
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _expiretime;

        public CachedAttribute(int expiretime)
        {
            _expiretime = expiretime;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService= context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cacheKey=GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cacheResponse=await cacheService.GetCacheKeyAsync(cacheKey);
            if (!string.IsNullOrEmpty(cacheResponse))
            {
                var contextResult = new ContentResult()
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result= contextResult;
                return;
            }

            var executedContext= await next();
            if (executedContext.Result is OkObjectResult response)
            {
                await cacheService.SetCacheKeyAsync(cacheKey,response.Value,TimeSpan.FromMinutes(_expiretime));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var cacheKey = new StringBuilder();
            cacheKey.Append($"{request.Path}");

            foreach (var (Key,value) in request.Query.OrderBy(x=>x.Key))
            {
                cacheKey.Append($"|{Key}-{value}");
            }

            return cacheKey.ToString();
        }
    }
}
