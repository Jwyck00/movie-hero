using Application.Mapping;
using Application.Movies.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection))
        );
        services.AddMapping();
        return services;
    }
}
