using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        readonly RequestDelegate _next;
        readonly ILogger<ExceptionMiddleware> _logger;
        readonly IHostEnvironment _env;

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
                // // Your middleware logic for login
                // string username = context.Request.Form["userName"];
                // string password = context.Request.Form["password"];

                // // Simulate an invalid login for testing purposes
                // if (username != "validUsername" || password != "validPassword")
                // {
                //     throw new Exception("Invalid login credentials.");
                // }

                // Continue with the next middleware or request processing

                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the error to the console and a log file
                _logger.LogError(ex, ex.Message);

                // Specify the path for the error log file within the "Logs" folder
                string logsFolderPath = "Logs";
                string errorLogFilePath = Path.Combine(logsFolderPath, "ErrorLogs.txt");

                // Check if the "Logs" folder exists, and create it if not
                if (!Directory.Exists(logsFolderPath))
                {
                    Directory.CreateDirectory(logsFolderPath);
                }

                // Check if the error log file exists, and create it if not
                if (!File.Exists(errorLogFilePath))
                {
                    using (StreamWriter createFileWriter = File.CreateText(errorLogFilePath))
                    {
                        // Add any initial information to the file if needed
                        createFileWriter.WriteLine("Error Log File Created");
                        createFileWriter.WriteLine();
                    }
                }

                // Write the error details to the error log file

                using (StreamWriter writer = File.AppendText(errorLogFilePath))
                {
                    writer.WriteLine($"Timestamp: {DateTime.UtcNow}");
                    writer.WriteLine($"Message: {ex.Message}");
                    writer.WriteLine($"StackTrace: {ex.StackTrace}");
                    writer.WriteLine();
                }


                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}