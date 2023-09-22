using Assessment3.Server.Application.Common.Persistence;
using Assessment3.Server.Domain.Common;
using Assessment3.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Assessment3.Server.Infrastructure.Common.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AssessmentDbContext context) : base(context)
    {
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await  _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        return user;
    }
}