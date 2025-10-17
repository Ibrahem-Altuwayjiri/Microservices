using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;


namespace Services.Email.Infrastructure.Configuration.ExceptionHandlers
{
    public class MiddlewareExceptionHandler
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareExceptionHandler> _logger;

        public MiddlewareExceptionHandler(
            RequestDelegate next,
            ILogger<MiddlewareExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try { await _next(context); }
            catch (RestfulException exception) { await HandleExceptionAsync(context, exception, _logger); }
            catch (Exception exception) { await HandleExceptionAsync(context, exception, _logger); }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception,
            ILogger<MiddlewareExceptionHandler> logger)
        {

            // Object holds the error details that will be logged in the server
            dynamic loggingErrorObject = new { };

            // To help troubleshoot the issue, trace id will be shown to the user
            string traceId = context.TraceIdentifier.ToString();
            int statusCode = exception is RestfulException ? ((RestfulException)exception).StatusCode : RestfulStatusCodes.InternalServerError;

            // Set response status code in the acual response
            context.Response.StatusCode = statusCode;

            if (exception is RestfulException)
            {
                loggingErrorObject = new
                {
                    StatusCode = statusCode,
                    Message = ((RestfulException)exception).ErrorMessage,
                    LineNumber = ((RestfulException)exception).LineNumber,
                    FunctionName = ((RestfulException)exception).FunctionName,
                    ClassName = ((RestfulException)exception).ClassName,
                    Cause = "Exception was thrown by the developer's error handling error",
                    TraceId = traceId
                };
            }
            else
            {
                loggingErrorObject = new
                {
                    StatusCode = statusCode,
                    Message = exception.Message?.ToString(),
                    StackTrace = exception.StackTrace?.ToString(),
                    InnerException = exception.InnerException?.ToString(),
                    Cause = "Unhandled exception",
                    TraceId = traceId
                };
            }

            // Convert logging error object to string and log it
            string serializedErrorObject = JsonSerializer.Serialize(loggingErrorObject);
            logger.LogError(serializedErrorObject);

            // Show custom error thrown by the developer or Internal Error Message if unhandled error
            string? exceptionMessage = exception is RestfulException ? ((RestfulException)exception).ErrorMessage : "Internal Server Error";

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                StatusCode = statusCode,
                ErrorMessage = exceptionMessage,
                TraceId = traceId
            }));
        }
    }
}
