
using AutoMapper;
using FribergHomeClient.Data.Dto;
using FribergHomeClient.Data.ViewModel;
using System.Net.Http.Json;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace FribergHomeClient.Services
{
	public class PropertyService : IPropertyService
	{
		private readonly HttpClient _client;
		private readonly IMapper _mapper;

		public PropertyService(HttpClient client, IMapper mapper)
		{
			_client = client;
			this._mapper = mapper;
		}
		public async Task<PropertyFormViewModel> GetProperty(int id)
		{
			try
			{
				var dto = await _client.GetFromJsonAsync<PropertyDTO>($"/api/Properties/{id}");
				var vm = _mapper.Map<PropertyFormViewModel>(dto);
				return vm;
			}
			catch (Exception)
			{
				return new PropertyFormViewModel();
			}
		}

		public async Task<PropertyDTO> GetPropertyDTO(int id)
		{
			try
			{
				var dto = await _client.GetFromJsonAsync<PropertyDTO>($"/api/Properties/{id}/details");
				var muncipality = await _client.GetFromJsonAsync<MuncipalityDTO>($"/api/muncipality/{dto.MuncipalityId}");
				dto.Muncipality = muncipality.Name;
				return dto;
			}
			catch (Exception)
			{
				return new PropertyDTO();
			}
		}

		public async Task SaveProperty(PropertyFormViewModel vm)
		{
			var url = vm.Id == null
				? "/api/Properties"
				: $"/api/Properties/{vm.Id}";

			try
			{
				var dto = _mapper.Map<PropertyDTO>(vm);

				if (dto.Id.HasValue)
				{
					var result = await _client.PutAsJsonAsync<PropertyDTO>(url, dto);
				}
				else
				{
					var result = await _client.PostAsJsonAsync<PropertyDTO>(url, dto);
				}
				// Check status code, throw exceptions
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<ResponseService<List<PropertyDTO>>> GetListAsync(string uri)
		{
			try
			{
				var response = await _client.GetAsync($"api/properties/{uri}");
				var muncipalities = await _client.GetFromJsonAsync<List<MuncipalityDTO>>("api/muncipality");

				if (response!.IsSuccessStatusCode)
				{
					var result = new ResponseService<List<PropertyDTO>>
					{
						Data = await response.Content.ReadFromJsonAsync<List<PropertyDTO>>(),
						Success = true,
						Message = response.ReasonPhrase ?? "",
					};
					foreach (var p in result.Data)
					{
						p.Muncipality = muncipalities.First(m => m.Id == p.MuncipalityId).Name;
					}
					return result;
				}

				return new ResponseService<List<PropertyDTO>>
				{
					Success = false,
					Message = $"Något gick fel: {response.ReasonPhrase}",
				};
			}
			catch (HttpRequestException ex)
			{
				return new ResponseService<List<PropertyDTO>>
				{
					Success = false,
					Message = $"Något gick fel: {ex.Message}",
				};
			}

		}
	}
}
