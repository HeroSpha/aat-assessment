using System.Net;
using Assessment3.Client.Components;
using Assessment3.Client.Services.Contracts;
using Assessment3.Shared.Models.Events;
using Blazored.Modal;
using Flurl.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Assessment3.Client.Pages.Events;

public partial class AddEventModal : BaseComponent
{
    [Inject] public required IEventService EventService { get; set; }
    [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;
    private EventCreateRequest EventModel = new();
    private string image = "";
    protected override bool ShouldRender()
    {
        return shouldRender;
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        shouldRender = true;
    }

    private async Task HandleSubmit()
    {
        try
        {
            shouldRender = false;
            var response = await EventService.CreateAsync(EventModel);
            if (response.StatusCode == (int) HttpStatusCode.OK)
            {
                var newModel = await response.GetJsonAsync<EventDto>();
                if (newModel is not null)
                {
                    ToastService.ShowSuccess("Event successfully created.");
                    EventModel = new();
                }
            }
        }
        catch (FlurlHttpException ex)
        {
            await GetErrors(ex);
        }
        finally
        {
            shouldRender = true;
            StateHasChanged();
        }
    }
    private async Task Close() => await BlazoredModal.CloseAsync();
    
    private async void LoadFiles(InputFileChangeEventArgs e)
    {
        long maxFileSize = 5 * 1024 * 1024;
        var file = e.GetMultipleFiles(1).FirstOrDefault();
        if (file is null && file.Size > maxFileSize) return;
        var fileContent = new StreamContent(file.OpenReadStream(maxFileSize));
        var byteImage = await fileContent.ReadAsByteArrayAsync();
        EventModel.Image = Convert.ToBase64String(byteImage);
        image = $"data:image/jpeg;base64,{EventModel.Image}";
        StateHasChanged();
    }
}