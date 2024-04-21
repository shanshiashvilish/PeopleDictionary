using PeopleDictionary.Core.Base;
using System.Net;

namespace PeopleDictionary.Api.Middlewares
{

    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorLoggingMiddleware(RequestDelegate next)
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
                Console.WriteLine(ex);
                await HandleExceptionAsync(context);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(text: new BaseModel<object>()
            {
                StatusCode = Core.Enums.StatusCodeEnums.UnknownError,
                Message = "unexpected error",
                IsSuccess = false,
                Data = null
            }.ToString());
        }
    }
}
