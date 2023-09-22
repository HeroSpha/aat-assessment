using Assessment3.Client.Helpers;
using Assessment3.Client.Services.Contracts;
using Flurl.Http;

namespace Assessment3.Client.Services.UserEvents;

public class UserEventService : BaseService, IUSerEventService
{
    public UserEventService(ITokenService tokenService) : base(tokenService)
    {
    }

    public async Task<IFlurlResponse> Register(string eventId)
    {
        return await Constants.AppUrl
            .WithOAuthBearerToken(TokenService.GetToken())
            .AppendPathSegment("user-events/register")
            .PostJsonAsync(new {UserId = TokenService.GetUserId(), eventId});
    }
}