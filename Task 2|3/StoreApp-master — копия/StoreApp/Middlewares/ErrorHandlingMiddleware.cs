using Newtonsoft.Json;
using System.Net;

namespace StoreApp.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionEsync(context, ex);
            }
        }
            
        private static Task HandleExceptionEsync(HttpContext context, Exception exception)
        {
            var code =  HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
