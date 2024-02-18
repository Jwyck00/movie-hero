using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Services;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.AddDbContext<ApplicationDbContext>();
        
        return services;
    }
}
