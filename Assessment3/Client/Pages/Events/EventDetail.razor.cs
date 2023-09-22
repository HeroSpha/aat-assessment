using System.Net;
using Assessment3.Client.Components;
using Assessment3.Client.Services.Contracts;
using Assessment3.Client.Shared;
using Assessment3.Shared.Models.Events;
using Blazored.Modal;
using Blazored.Modal.Services;
using Flurl.Http;
using Microsoft.AspNetCore.Components;

namespace Assessment3.Client.Pages.Events;

public partial class EventDetail : BaseComponent
{
    [Inject] public required IModalService ModalService { get; set; }
    [Inject] public required IEventService EventService { get; set; }
    [Inject] public required IUSerEventService UserEventService { get; set; }
    [Parameter] public  required string Id { get; set; }

    private EventDto? eventDto;
    protected override async Task OnInitializedAsync()
    {
        shouldRender = false;
        await GetEvent();
    }

    private async Task GetEvent()
    {
        try
        {
            if (string.IsNullOrEmpty(Id))
            {
                return;
            }
            eventDto = await EventService.GetById(Id);
        }
        catch (Exception e)
        {
            shouldRender = true;
        }
        finally
        {
            shouldRender = true;
        }
    }

    private async Task Register()
    {
        try
        {
            
            var parameters = new ModalParameters()
                .Add(nameof(ConfirmModal.Message), "Register with selected event?");

            var confirmModal = ModalService.Show<ConfirmModal>("Register for the event?", parameters);
            var result = await confirmModal.Result;
            if (result.Confirmed)
            {
                shouldRender = false;
                var response = await UserEventService.Register(Id);
                if (response.StatusCode == (int) HttpStatusCode.OK)
                {
                    ToastService.ShowSuccess("Successfully registered for the event.");
                }
            }
        }
        catch (FlurlHttpException e)
        {
            await GetErrors(e);
        }
        finally
        {
            shouldRender = true;
            StateHasChanged();
        }
    }
    private async Task Delete()
    {
        var parameters = new ModalParameters()
            .Add(nameof(ConfirmModal.Message), "Delete selected event?");
           
        var confirmModal =   ModalService.Show<ConfirmModal>("Delete Event", parameters); 
        var result =  await confirmModal.Result;
        if (result.Confirmed)
        {
            var response = await DeleteAsync(Id);
            if (response)
            {
                ToastService.ShowSuccess("Event successfully deleted.");
                NavigationManager.NavigateTo("/");
            }
            else
            {
                ToastService.ShowError("Failed to delete event.");
            }
        }
       
    }

    private async Task<bool> DeleteAsync(string id)
    {
        return await EventService.DeleteAsync(id);
    }
    private async Task Update()
    {
        if (eventDto is null)
        {
            return;
        }
        var options = new ModalOptions() 
        { 
            Size = ModalSize.Large 
        };
        var parameters = new ModalParameters()
            .Add(nameof(UpdateEventModal.EventModel), new UpdateEventRequest
            {
                Id = eventDto.Id,
                Title = eventDto.Title,
                Description = eventDto.Description,
                Date = eventDto.Date,
                Image = eventDto.Image,
                Venue = eventDto.Venue,
                Seats = eventDto.Seats
            })
            .Add(nameof(UpdateEventModal.Image), $"data:image/jpeg;base64,{eventDto.Image}");
           
        var updateModal =   ModalService.Show<UpdateEventModal>("Edit Event", parameters, options); 
        var result =  await updateModal.Result;
        if (result.Confirmed)
        {
            var response = await DeleteAsync(Id);
            if (response)
            {
                ToastService.ShowSuccess("Event successfully deleted.");
                await GetEvent();
            }
            else
            {
                ToastService.ShowError("Failed to update event.");
            }
        }
    }
}