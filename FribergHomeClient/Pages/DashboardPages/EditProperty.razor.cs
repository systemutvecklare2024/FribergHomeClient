using FribergHomeClient.Data.Dto;
using FribergHomeClient.Data.ViewModel;
using FribergHomeClient.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace FribergHomeClient.Pages.DashboardPages
{
    public partial class EditProperty
    {
        [Inject]
        public IPropertyService PropertyService { get; set; }
        [Inject]
        public HttpClient HttpClient { get; set; }

        private List<PropertyDTO> properties = new List<PropertyDTO>();
        private int? selectedPropertyId;

        protected override async Task OnInitializedAsync()
        {
            await LoadProperties();
        }

        private async Task LoadProperties()
        {
            properties = await Http.GetFromJsonAsync<List<PropertyDTO>>("/api/Properties");
        }

        private void SelectedProperty(int propertyId)
        {
            selectedPropertyId = propertyId;
        }

        private void DeselectedProperty()
        {
            selectedPropertyId = null;
        }

        private async Task RefreshList()
        {
            await LoadProperties();
            selectedPropertyId = null;
        }
    }
}
