using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Services;
using Application.Services.Movies;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
}