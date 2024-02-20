using System.Reflection;
using Mapster;
using Mapster.Utils;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Mapping;

public static class DependencyInjection
{
    public static IServiceCollection AddMapping(this IServiceCollection services, Assembly assembly)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        // config.Default.PreserveReference(true);

        config.ScanInheritedTypes(assembly);
        config.Scan(assembly);
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
