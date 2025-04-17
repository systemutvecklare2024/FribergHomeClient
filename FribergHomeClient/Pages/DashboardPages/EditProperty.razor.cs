using FribergHomeClient.Data.ViewModel;
using FribergHomeClient.Services;
using Microsoft.AspNetCore.Components;

namespace FribergHomeClient.Pages.DashboardPages
{
    public partial class EditProperty
    {
        [Inject]
        public IPropertyService PropertyService { get; set; }

        public PropertyFormViewModel ViewModel;

        

        protected override Task OnInitializedAsync()
        {
            ViewModel = new PropertyFormViewModel();
            return base.OnInitializedAsync();
        }
    }
}
