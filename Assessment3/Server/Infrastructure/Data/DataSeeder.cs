using Assessment3.Server.Application.Data;
using Assessment3.Server.Application.Helpers;
using Assessment3.Server.Domain.Common;
using Assessment3.Server.Infrastructure.Common.Persistence;
using Assessment3.Shared.Models;

namespace Assessment3.Server.Infrastructure.Data;

public class DataSeeder : IDataSeeder
{
    public async Task SeedAsync(AssessmentDbContext? context)
    {
        try
        {
            var existingUsers =  context?.Users;
            if (existingUsers is not null && !existingUsers.Any())
            {
                var (salt, password) = PasswordHelper.HashPassword("1q2w3e");
                var users = new List<User>
                {
                    User.Create("Admin", "Admin", "admin@assessment.co.za", password ,salt, "Admin" ),
                    User.Create("User", "User", "customer@assessment.co.za", password ,salt, "User" ),
                    
                };
                context?.Users.AddRange(users);
                await context?.SaveChangesAsync()!;
            }
        }
        catch (Exception e)
        {
            throw new ApplicationException("Context is null");
        }
    }
}