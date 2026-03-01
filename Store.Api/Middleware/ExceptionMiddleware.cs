using Store.Api.Errors;
using System.Text.Json;

namespace Store.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger , IHostEnvironment environment)
        {
            _next=next;
            _logger=logger;
            _env=environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await _next.Invoke(context);
            }
            catch (Exception e)
            { 
                _logger.LogError(e,e.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode=StatusCodes.Status500InternalServerError;

                var response = _env.IsDevelopment() ?
                    new ApiExceptionResponse(StatusCodes.Status500InternalServerError,e.Message , e?.StackTrace?.ToString())
                   :new ApiExceptionResponse(StatusCodes.Status500InternalServerError);
                var Option=new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var JSON=JsonSerializer.Serialize(response, Option);
                await context.Response.WriteAsync(JSON);


            }
        }
    }
}
