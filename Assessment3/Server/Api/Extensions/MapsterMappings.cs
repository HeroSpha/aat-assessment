using System.Reflection;
using Mapster;
using MapsterMapper;

namespace Assessment3.Server.Api.Extensions;

public static class MapsterMappings
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}