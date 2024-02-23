namespace Presentation.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCors(this IServiceCollection services, IConfiguration configuration)
    {
        var withOrigins = configuration.GetSection("WithOrigins").Get<string>().Split(",");

        var allowSpecificOrigins = configuration.GetSection("CorsPolicyName").Get<string>();

        services.AddCors(
            options =>
            {
                options.AddPolicy(
                    name: allowSpecificOrigins,
                    configurePolicy: builder =>
                    {
                        builder
                            .WithOrigins(withOrigins)
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    }
                );
            }
        );
    }
}
