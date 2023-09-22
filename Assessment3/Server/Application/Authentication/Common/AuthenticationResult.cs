using Assessment3.Server.Domain.Common;
using Assessment3.Shared.Models;

namespace Assessment3.Server.Application.Authentication.Common;

public record AuthenticationResult(User User, string Token);