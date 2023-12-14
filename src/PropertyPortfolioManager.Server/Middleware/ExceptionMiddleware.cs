using System.Net;
using System.Text.Json;

namespace PropertyPortfolioManager.Server.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IWebHostEnvironment hostingEnv;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, IWebHostEnvironment hostingEnv, RequestDelegate next)
        {
            this.logger = logger;
            this.hostingEnv = hostingEnv;
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);
            }
            catch (Exception ex)
            {
                await this.HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            this.logger.LogError(exception, exception.Message);

            var response = context.Response;
            var statusCode = (int)HttpStatusCode.InternalServerError;

            var message = this.hostingEnv.IsDevelopment() ? exception.Message : "Unexpected error";
            var description = this.hostingEnv.IsDevelopment() ? exception.StackTrace : "Unexpected error";

            response.ContentType = "application/json";
            response.StatusCode = statusCode;

            var errorObj = new
            {
                ErrorMessage = message,
                ErrorDescription = description
            };
            var jsonOutput = JsonSerializer.Serialize(errorObj);
            await response.WriteAsync(jsonOutput);
        }

    }
}
