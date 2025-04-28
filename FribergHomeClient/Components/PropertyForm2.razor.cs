using FribergHomeClient.Data.Dto;
using FribergHomeClient.Data.ViewModel;
using FribergHomeClient.Services;
using Microsoft.AspNetCore.Components;

namespace FribergHomeClient.Components
{
    public class PropertyForm1
    {
        [Inject]
        public IPropertyService PropertyService { get; set; }

        [Parameter]
        public int? PropertyId { get; set; }
        public PropertyFormViewModel Property { get; set; }

        private bool IsEdit => PropertyId.HasValue;

        // TODO: Fix muncipality and images...

        protected async Task OnInitializedAsync()
        {
            // This is Edit
            if (IsEdit)
            {

                Property = await PropertyService.GetProperty(PropertyId.Value);
                
            }
            
            else
            {
                Property = new PropertyFormViewModel();
            }
        }

        private async Task HandleSubmit()
        {
            try
            {
                await PropertyService.SaveProperty(Property);

            } catch(Exception)
            {
                Console.WriteLine("Everything is fine...");
            }
        }
    }
}
