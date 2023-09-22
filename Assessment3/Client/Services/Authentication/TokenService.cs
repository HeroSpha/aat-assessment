using Assessment3.Client.Configuration;
using Assessment3.Client.Services.Contracts;
using Assessment3.Shared.Models.Authentication;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace Assessment3.Client.Services.Authentication;

public class TokenService : ITokenService
{
    private readonly ISyncLocalStorageService SyncLocalStorage;

    public TokenService(ISyncLocalStorageService syncLocalStorage)
    {
        SyncLocalStorage = syncLocalStorage;
    }

    public string GetToken()
    {
        var userJson = SyncLocalStorage.GetItemAsString(UserConfig.User);
        return string.IsNullOrEmpty(userJson) ? "" : JsonConvert.DeserializeObject<AuthenticationResponse>(userJson).Token;
    }

    public string GetUserId()
    {
        var userJson = SyncLocalStorage.GetItemAsString(UserConfig.User);
        return string.IsNullOrEmpty(userJson) ? "" : JsonConvert.DeserializeObject<AuthenticationResponse>(userJson).Id.ToString();
    }
}