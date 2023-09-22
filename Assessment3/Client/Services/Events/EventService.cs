using Assessment3.Client.Helpers;
using Assessment3.Client.Services.Contracts;
using Assessment3.Shared.Models.Events;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Components;

namespace Assessment3.Client.Services.Events;

public class EventService : BaseService, IEventService
{
    public EventService(ITokenService tokenService) : base(tokenService)
    {
    }
    public async Task<IFlurlResponse> CreateAsync(EventCreateRequest input)
    {
        return await Constants.AppUrl
            .WithOAuthBearerToken(TokenService.GetToken())
            .AppendPathSegment("events/create")
            .PostJsonAsync(input);
    }

    public async Task<IFlurlResponse> UpdateAsync(UpdateEventRequest input)
    {
        return await Constants.AppUrl
            .WithOAuthBearerToken(TokenService.GetToken())
            .AppendPathSegment("events/update")
            .PutJsonAsync(input);
    }

    public async Task<IEnumerable<EventDto>> GetAll()
    {
        return await Constants.AppUrl
            .WithOAuthBearerToken(TokenService.GetToken())
            .AppendPathSegment("events/getlist")
            .GetJsonAsync<IEnumerable<EventDto>>();
    }

    public async Task<EventDto?> GetById(string Id)
    {
        return await Constants.AppUrl
            .WithOAuthBearerToken(TokenService.GetToken())
            .AppendPathSegment($"events/getbyid/{Id}")
            .GetJsonAsync<EventDto>();
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await Constants.AppUrl
            .WithOAuthBearerToken(TokenService.GetToken())
            .AppendPathSegment($"events/delete/{id}")
            .DeleteAsync()
            .ReceiveJson<bool>();
    }
}