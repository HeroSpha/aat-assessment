using Assessment3.Shared.Models.UserEvents;
using Flurl.Http;

namespace Assessment3.Client.Services.Contracts;

public interface IUSerEventService
{
    Task<IFlurlResponse> Register(string eventId);
}