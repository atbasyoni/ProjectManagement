using ProjectsManagement.Data;
using ProjectsManagement.ViewModels;
using System.Net;
using System.Text.Json;

namespace ProjectsManagement.Middleware
{
    public class GlobalErrorHandlerMiddleware
    {
        RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public GlobalErrorHandlerMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlerMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


                var respnse = _env.IsDevelopment() ?
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                  : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);


                var option = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };


                var json = JsonSerializer.Serialize(respnse, option);

                await httpContext.Response.WriteAsync(json);

            }

        }
    }
}
