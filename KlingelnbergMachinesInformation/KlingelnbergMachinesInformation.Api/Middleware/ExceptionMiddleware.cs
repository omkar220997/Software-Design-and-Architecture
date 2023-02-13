using Newtonsoft.Json;
using System.Net;

namespace KlingelnbergMachinesInformation.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            object errorMsg = ex.Message;

            switch (ex)
            {
                case FileNotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
                case InvalidOperationException:
                    code = HttpStatusCode.BadRequest;
                    break;
                default:
                    errorMsg = "Internal server error";
                    break;
            }

            var result = JsonConvert.SerializeObject(new { error = errorMsg });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
