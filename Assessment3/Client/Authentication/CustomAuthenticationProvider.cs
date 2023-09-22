using System.Security.Claims;
using Assessment3.Client.Configuration;
using Assessment3.Client.Services.Contracts;
using Assessment3.Shared.Models.Authentication;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Assessment3.Client.Authentication;

public class CustomAuthenticationProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    public CustomAuthenticationProvider(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var state = new AuthenticationState(new ClaimsPrincipal());
        var loggedUser = await _localStorageService.GetItemAsync<AuthenticationResponse>(UserConfig.User);
        if (loggedUser is not null)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, loggedUser.FirstName),
                new Claim(ClaimTypes.NameIdentifier, loggedUser.Id.ToString()),
                new Claim(ClaimTypes.Email, loggedUser.Email),
                new Claim(ClaimTypes.Role, loggedUser.Role)
            }, "Bearer");
            state = new AuthenticationState(new ClaimsPrincipal(identity));
        }
        NotifyAuthenticationStateChanged(Task.FromResult(state));
        return state;
    }
}