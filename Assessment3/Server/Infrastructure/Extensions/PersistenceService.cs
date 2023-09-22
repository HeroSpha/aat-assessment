using Assessment3.Server.Application.Common.Persistence;
using Assessment3.Server.Infrastructure.Common;
using Assessment3.Server.Infrastructure.Common.Persistence;
using Assessment3.Server.Infrastructure.Common.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Assessment3.Server.Infrastructure.Extensions;

public static class PersistenceService
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<AssessmentDbContext>(opt => opt.UseSqlite(configuration.GetConnectionString("Default")));
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}