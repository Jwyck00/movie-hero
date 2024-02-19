using Application.Behaviors;
using Application.Mapping;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(
            configuration =>
            {
                configuration.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));

                configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            }
        );

        services.AddMapping();
        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));
        return services;
    }
}
