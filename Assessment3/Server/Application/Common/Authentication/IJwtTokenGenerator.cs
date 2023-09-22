
using Assessment3.Server.Domain.Common;
using Assessment3.Shared.Models;

namespace Assessment3.Server.Application.Common.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}