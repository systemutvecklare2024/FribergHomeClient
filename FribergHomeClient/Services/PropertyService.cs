
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
            _mapper = mapper;
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

        //public async Task SaveProperty(PropertyFormViewModel vm)
        //{
        //    var url = vm.Id == null
        //        ? "/api/Properties"
        //        : $"/api/Properties/{vm.Id}";

        //    try
        //    {
        //        var dto = _mapper.Map<PropertyDTO>(vm);

        //        //if (dto.Id.HasValue)
        //        //Fredrik
        //        if (dto.Id > 0)
        //        {
        //            var result = await _client.PutAsJsonAsync<PropertyDTO>(url, dto);
        //        } else
        //        {
        //            var result = await _client.PostAsJsonAsync<PropertyDTO>(url, dto);
        //        }
        //        // Check status code, throw exceptions
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //Test method Fredrik
        public async Task SaveProperty(PropertyDTO dto)
        {
            var url = dto.Id > 0
                ? $"/api/Properties/{dto.Id}" // Update existing property
                : "/api/Properties"; // Create new property

            try
            {
                var response = dto.Id > 0
                    ? await _client.PutAsJsonAsync(url, dto) // Update
                    : await _client.PostAsJsonAsync(url, dto); // Create

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API request failed: {response.StatusCode}");
                }

                Console.WriteLine("Ändringar sparade!");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid sparande: {ex.Message}");
                throw;
            }
        }


        //*Ta fram lista med bostäder*@
        public async Task<List<PropertyDTO>> GetProperties(PropertyDTO propertiesDto)
        {
            try
            {
                var result = await _client.GetFromJsonAsync<List<PropertyDTO>>("/api/Properties");
                return result;
            }
            catch (Exception)
            {
                return new List<PropertyDTO>();
            }
        }
    }
}
