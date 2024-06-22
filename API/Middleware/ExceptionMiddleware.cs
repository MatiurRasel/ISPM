using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using API.Errors;
using API.Helpers;
using Microsoft.EntityFrameworkCore;

namespace API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next,
                               ILogger<ExceptionMiddleware> logger,
                               IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Log request details
            _logger.LogInformation($"Handling request: {context.Request.Method} {context.Request.Path}");

            await _next(context);

            // Log response details
            _logger.LogInformation($"Response status code: {context.Response.StatusCode}");
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        _logger.LogError(ex, ex.Message);

        string logsFolderPath = "Logs";
        string errorLogFilePath = Path.Combine(logsFolderPath, "ErrorLogs.txt");

        if (!Directory.Exists(logsFolderPath))
        {
            Directory.CreateDirectory(logsFolderPath);
        }

        string timestampFormat = "dd-MM-yyyy HH:mm:ss";
        using (StreamWriter writer = File.AppendText(errorLogFilePath))
        {
            writer.WriteLine("--------------------------------------------------");
            writer.WriteLine($"Timestamp (UTC): {DateTime.UtcNow.ToString(timestampFormat)}");
            writer.WriteLine($"Timestamp (Local): {DateTime.Now.ToString(timestampFormat)}");
            writer.WriteLine("--------------------------------------------------");
            writer.WriteLine($"Exception Type: {ex.GetType().Name}");
            writer.WriteLine($"Message: {ex.Message}");
            writer.WriteLine("Stack Trace:");
            writer.WriteLine(ex.StackTrace);
            if (ex.InnerException != null)
            {
                writer.WriteLine("--------------------------------------------------");
                writer.WriteLine("Inner Exception:");
                writer.WriteLine($"Exception Type: {ex.InnerException.GetType().Name}");
                writer.WriteLine($"Message: {ex.InnerException.Message}");
                writer.WriteLine("Stack Trace:");
                writer.WriteLine(ex.InnerException.StackTrace);
            }
            writer.WriteLine("--------------------------------------------------");
        }

        context.Response.ContentType = "application/json";

        var responseMessage = "An unexpected error occurred. Please try again later.";
        var responseDetails = _env.IsDevelopment() ? ex.Message : null;

        if (ex is ValidationException validationException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            responseMessage = validationException.Message;
            responseDetails = "Validation Error";
        }
        else if (ex is DbUpdateException dbUpdateException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            responseMessage = "Database update failed.";
            responseDetails = dbUpdateException.Message;
        }
        else if (ex is NotFoundException notFoundException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            responseMessage = notFoundException.Message;
            responseDetails = "Resource Not Found";
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        var response = _env.IsDevelopment()
                   ? new ApiException(context.Response.StatusCode, responseMessage, ex.StackTrace?.ToString())
                   : new ApiException(context.Response.StatusCode, responseMessage);

        var json = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
    }
}
