using Assessment3.Shared.Models.Events;
using Flurl.Http;

namespace Assessment3.Client.Services.Contracts;

public interface IEventService
{
    Task<IFlurlResponse> CreateAsync(EventCreateRequest input);
    Task<IFlurlResponse> UpdateAsync(UpdateEventRequest input);
    Task<IEnumerable<EventDto>> GetAll();
    Task<EventDto?> GetById(string id);
    Task<bool> DeleteAsync(string id);
}