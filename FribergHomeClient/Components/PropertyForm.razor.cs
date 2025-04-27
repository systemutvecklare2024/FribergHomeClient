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
                Console.WriteLine("HandleSubmit triggered!");

                if (IsEdit && Property != null)
                {
                    //*
                    try
                    {
                        //*
                        Console.WriteLine($"PropertyID: {Property?.Id}, Type: {Property?.PropertyType}");

                    var propertyDto = Mapper.Map<PropertyDTO>(Property);
                        //*
                        Console.WriteLine($"Mapped DTO ID: {propertyDto?.Id}, Type: {propertyDto?.PropertyType}");

                    await PropertyService.SaveProperty(propertyDto);
                    Console.WriteLine("Ändringar sparade!");
                     Navigation.NavigateTo("/dashboard/editproperty");
                    }
                    catch (Exception mapEx)
                    {
                        Console.WriteLine($"Fel vid mappning: {mapEx.Message}");
                        Console.WriteLine($"StackTrace: {mapEx.StackTrace}");
                        if (mapEx.InnerException != null)
                        {
                            Console.WriteLine($"InnerException: {mapEx.InnerException.Message}");
                        }
                    }

                }
                else
                {
                    Console.WriteLine("Property is null, cannot save.");
                }
                    //else
                    //{
                    //    var propertyDto = Mapper.Map<PropertyDTO>(Property);
                    //    await PropertyService.CreateProperty(propertyDto);
                    //    Console.WriteLine("Ny egendom skapad!");
                    //}

                    await OnSave.InvokeAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid sparande: {ex.Message}");
            }
        }

        //Fredrik
        //private readonly PropertyDTO? property;
        //protected override async Task OnParametersSetAsync()
        //{
        //    Property = await Http.GetFromJsonAsync<PropertyDTO>($"api/Properties/{PropertyId}");
        //}

        //private async Task SaveChanges()
        //{
        //    await Http.PutAsJsonAsync($"api/Properties/{property.Id}", property);
        //    Console.WriteLine("Ändringar utförda");
        //    await OnSave.InvokeAsync();
        //}
    }
}
