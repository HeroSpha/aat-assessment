using Assessment3.Client.Helpers;
using Assessment3.Client.Services.Contracts;
using Assessment3.Shared.Models.Authentication;
using Flurl;
using Flurl.Http;

namespace Assessment3.Client.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    public async Task<IFlurlResponse> Login(string email, string password)
    {
        return await Constants.AppUrl
            .AppendPathSegment("authentication/login")
            .PostJsonAsync(new {email, password});
    }

    public async Task<IFlurlResponse> Register(RegisterRequest request)
    {
        return await Constants.AppUrl
            .AppendPathSegment("authentication/register")
            .PostJsonAsync(request);
        
    }
}