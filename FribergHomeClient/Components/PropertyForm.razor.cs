using AutoMapper;
using FribergHomeClient.Data.Dto;
using FribergHomeClient.Data.ViewModel;
using FribergHomeClient.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;


namespace FribergHomeClient.Components
{
    public partial class PropertyForm
    {
        private string? SuccesMessage {get; set; }

        [Inject]
        public IPropertyService PropertyService { get; set; }
        //Fredrik
        [Inject]
        public IMapper Mapper { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }


        [Parameter]
        public int? PropertyId { get; set; }
        public PropertyFormViewModel Property { get; set; }

        //Fredrik
        [Parameter]
        public EventCallback OnSave { get; set; }
        private bool IsEdit => PropertyId.HasValue;

        // TODO: Fix muncipality and images...

        protected override async Task OnInitializedAsync()
        {
            // This is Edit
            if (IsEdit)
            {
                //Fredrik
                var propertyDto = await PropertyService.GetProperty(PropertyId.Value);
                Property = Mapper.Map<PropertyFormViewModel>(propertyDto) ?? new PropertyFormViewModel();
                //Original
                //Property = await PropertyService.GetProperty(PropertyId.Value);
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
                if (IsEdit && Property != null)
                {
                    var propertyDto = Mapper.Map<PropertyDTO>(Property);
                    await PropertyService.SaveProperty(propertyDto);

                    SuccesMessage = "Ändringar sparade!";
                    StateHasChanged();

                    await Task.Delay(2000); // Wait for 2 seconds
                }
                else
                {
                    Console.WriteLine("Property is null, cannot save.");
                }
                await OnSave.InvokeAsync();
            }

            catch (Exception mapEx)
            {
                if (mapEx.InnerException != null)
                {
                    Console.WriteLine($"InnerException: {mapEx.InnerException.Message}");
                }
            }
            Navigation.NavigateTo("/dashboard/editproperty");
        }

    }
}
