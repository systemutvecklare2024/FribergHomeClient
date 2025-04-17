
using AutoMapper;
using FribergHomeClient.Data.Dto;
using FribergHomeClient.Data.ViewModel;
using System.Net.Http.Json;
using System.Text.Json;

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
                } else
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
    }
}
