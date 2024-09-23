using Microsoft.AspNetCore.Mvc;
using UpBack.Application.Exceptions;

namespace UpBack.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Continua con el siguiente middleware en la cadena
                await _next(context);
            }
            catch (Exception ex)
            {
                // Manejar todas las excepciones
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Log detallado del error
            _logger.LogError(exception, "An error occurred: {Message}", exception.Message);

            var (statusCode, problemDetails) = GetExceptionDetails(exception);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }

        private static (int, ProblemDetails) GetExceptionDetails(Exception exception) => exception switch
        {
            ValidationException validationException => (
                StatusCodes.Status400BadRequest,
                new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = "https://httpstatuses.com/400",
                    Title = "Validation Failure",
                    Detail = "One or more validation errors have occurred.",
                    Extensions = { { "errors", validationException.Errors } }
                }),

            NotFoundException notFoundException => (
                StatusCodes.Status404NotFound,
                new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Type = "https://httpstatuses.com/404",
                    Title = "Resource Not Found",
                    Detail = notFoundException.Message,
                }),

            InvalidGuidException invalidGuidException => (
                StatusCodes.Status400BadRequest,
                new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = "https://httpstatuses.com/404",
                    Title = "Invalid GUID Format",
                    Detail = exception.Message,
                }
                ),

            _ => (
                StatusCodes.Status500InternalServerError,
                new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Type = "https://httpstatuses.com/500",
                    Title = "Server Error",
                    Detail = $"{exception.Message} - {exception.InnerException}"
                })
        };
    }
}
