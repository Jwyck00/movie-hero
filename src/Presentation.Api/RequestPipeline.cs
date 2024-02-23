using Presentation.Api.Middleware;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication;

namespace Presentation.Api;

public static class RequestPipeline
{
    public static IApplicationBuilder UsePresentation(
        this IApplicationBuilder app,
        IConfiguration configuration
    )
    {
        app.UseCors(configuration.GetSection("CorsPolicyName").Get<string>());
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
}
