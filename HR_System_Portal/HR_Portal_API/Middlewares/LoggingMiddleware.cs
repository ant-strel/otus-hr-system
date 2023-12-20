namespace HR_Portal_API.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task Invoke(HttpContext httpContext)
    {
        var request = httpContext.Request;
        var response = httpContext.Response;

        _logger.LogInformation("{Method} {Path}", request.Method, request.Path);
        _logger.LogInformation("StatusCode: {StatusCode}", response.StatusCode);

        return _next(httpContext);
    }
}