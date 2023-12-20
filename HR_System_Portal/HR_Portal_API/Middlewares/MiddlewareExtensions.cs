namespace HR_Portal_API.Middlewares;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<LoggingMiddleware>();
        builder.UseMiddleware<MetricsMiddleware>();
        return builder;
    }
}