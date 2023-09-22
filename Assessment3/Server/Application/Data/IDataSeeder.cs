using Assessment3.Server.Infrastructure.Common.Persistence;

namespace Assessment3.Server.Application.Data;

public interface IDataSeeder
{
    Task SeedAsync(AssessmentDbContext? context);
}