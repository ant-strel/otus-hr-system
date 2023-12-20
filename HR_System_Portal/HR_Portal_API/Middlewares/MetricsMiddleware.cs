using System.Diagnostics;

namespace HR_Portal_API.Middlewares;

public class MetricsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<MetricsMiddleware> _logger;

    public MetricsMiddleware(RequestDelegate next, ILogger<MetricsMiddleware> logger)
    {
        _next = next;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Invoke(HttpContext context)
    {
        var sw = new Stopwatch();
        sw.Start();

        await _next(context);

        sw.Stop();
        var elapsed = sw.ElapsedMilliseconds;
        _logger.LogInformation("{Request} {Time} ms", context.Request.Path, elapsed);
    }
}