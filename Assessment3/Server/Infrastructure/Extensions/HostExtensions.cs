using Assessment3.Server.Application.Data;
using Assessment3.Server.Infrastructure.Common.Persistence;

namespace Assessment3.Server.Infrastructure.Extensions;

public static class HostExtensions
{
    public static async Task SeedAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetService<IDataSeeder>();
        if (seeder is not null)
        {
            await seeder.SeedAsync(scope.ServiceProvider.GetService<AssessmentDbContext>());
        }
        else
        {
            throw new ApplicationException("Context is null");
        }
        
    }
}