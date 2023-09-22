using Assessment3.Client.Components;
using Assessment3.Client.Services.Contracts;
using Assessment3.Shared.Models.Events;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;

namespace Assessment3.Client.Pages.Events
{
    public partial class Home : BaseComponent
    {
        private string? inputValue;
        [Inject] public required IEventService EventService { get; set; }
        [CascadingParameter] public IModalService Modal { get; set; } = default!;
        private IQueryable<EventDto> events;

        protected override async Task OnInitializedAsync()
        {
            shouldRender = false;
            await GetEvents();
        }
        
        private async void HandleTextChanged(ChangeEventArgs e)
        {
            inputValue = e.Value?.ToString();
            // events = events.Where(x => x.Title
            //                                .ContainsIgnoreCase(inputValue!) 
            //                            || x.CategoryName.ContainsIgnoreCase(inputValue!));
        }

        private async Task GetEvents()
        {
            try
            {
                var items = await EventService.GetAll();
                events = new EnumerableQuery<EventDto>(items);
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
        private async Task AddEventModalAsync()
        {
            var options = new ModalOptions() 
            { 
                Size = ModalSize.Large 
            };
            var addEventModal = Modal.Show<AddEventModal>("Add Event", options);
            var result = await addEventModal.Result;
        }
    }
}
