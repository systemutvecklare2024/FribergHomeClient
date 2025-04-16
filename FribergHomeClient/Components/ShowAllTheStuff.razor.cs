
using System.Net.Http.Json;
using FribergHomeClient.Data.Dto;
using Microsoft.AspNetCore.Components;

namespace FribergHomeClient.Components
{
	public partial class ShowAllTheStuff
	{
		public IEnumerable<PropertyDTO> _properties { get; set; } = [];
		[Inject] public HttpClient HttpClient { get; set; }
		protected override async Task OnInitializedAsync()
		{

			await base.OnInitializedAsync();
			_properties = await HttpClient.GetFromJsonAsync<IEnumerable<PropertyDTO>>("api/Properties");
		}
	}
}
