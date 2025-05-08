using FribergHomeClient.Data.Dto;
using System.Net.Http.Json;

// Author: Christoffer, Emelie, Glate

namespace FribergHomeClient.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly HttpClient _client;

        public PropertyService(HttpClient client)
        {
            _client = client;
        }

        public async Task<PropertyDTO> GetPropertyDTO(int id)
        {
            try
            {
                var dto = await _client.GetFromJsonAsync<PropertyDTO>($"/api/Properties/{id}/details");
                var muncipality = await _client.GetFromJsonAsync<MuncipalityDTO>($"/api/muncipality/{dto.MuncipalityId}");
                dto.Muncipality = muncipality.Name;
                return dto;
                //Handle response here
            }
            catch (Exception)
            {
                return new PropertyDTO();
            }
        }

        public async Task<ServiceResponse<List<PropertyDTO>>> GetListAsync(string uri)
        {
            try
            {
                var response = await _client.GetAsync($"api/properties/{uri}");
                var muncipalities = await _client.GetFromJsonAsync<List<MuncipalityDTO>>("api/muncipality");

                if (response!.IsSuccessStatusCode)
                {
                    var result = new ServiceResponse<List<PropertyDTO>>
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

                return new ServiceResponse<List<PropertyDTO>>
                {
                    Success = false,
                    Message = $"Något gick fel: {response.ReasonPhrase}",
                };
            }
            catch (HttpRequestException ex)
            {
                return new ServiceResponse<List<PropertyDTO>>
                {
                    Success = false,
                    Message = $"Något gick fel: {ex.Message}",
                };
            }

        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            try
            {
                var response = await _client.DeleteAsync($"/api/properties/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    return new ServiceResponse { Success = false, Message = $"Något gick fel: {response.ReasonPhrase}" };
                }

                return new ServiceResponse { Success = true };

            }
            catch (Exception ex)
            {
                return new ServiceResponse { Success = false, Message = ex.Message };
            }
        }

    }
}
