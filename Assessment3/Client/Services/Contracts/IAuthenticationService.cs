using Assessment3.Shared.Models.Authentication;
using Flurl.Http;

namespace Assessment3.Client.Services.Contracts;

public interface IAuthenticationService
{
    Task<IFlurlResponse> Login(string email, string password);
    Task<IFlurlResponse> Register(RegisterRequest request);
}