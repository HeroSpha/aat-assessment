

using Assessment3.Server.Domain.Common;
using Assessment3.Shared.Models;

namespace Assessment3.Server.Application.Common.Persistence;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email);
}