using Presentation.Api.Middleware;

namespace Presentation.Api;

public static class RequestPipeline
{
    public static IApplicationBuilder UsePresentation(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseAuthentication();
        return app;
    }
}
