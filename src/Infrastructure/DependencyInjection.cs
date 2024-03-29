using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Services;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Security;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped<IMoviesRepository, MoviesRepository>();
        services.AddScoped<IMovieActorsRepository, MovieActorsRepository>();
        services.AddScoped<IMovieStartRatingsRepository, MovieStartRatingsRepository>();
        services.AddScoped<IActorsRepository, ActorsRepository>();

        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Scoped);
        services.AddScoped<ApplicationDbContextInitialiser>();

        services.Configure<AuthorizationSettings>(
            configuration.GetSection(AuthorizationSettings.Section)
        );

        services
            .AddAuthentication("BasicAuthentication")
            .AddScheme<AuthenticationSchemeOptions, SimpleAuthenticationHandler>(
                authenticationScheme: "BasicAuthentication",
                configureOptions: null
            );

        return services;
    }
}
