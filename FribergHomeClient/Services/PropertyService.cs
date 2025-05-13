using FribergHomeClient.Data.Dto;
using System.Net.Http.Json;

// Author: Christoffer, Emelie, Glate
namespace FribergHomeClient.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly HttpClient Http;

        public PropertyService(HttpClient client)
        {
            Http = client;
        }

        public async Task<ServiceResponse<PropertyDTO>> GetAsync(int id)
        {
            try
            {
                var response = await Http.GetAsync($"/api/Properties/{id}/details");
                if (!response.IsSuccessStatusCode)
                {
                    var failResult = new ServiceResponse<PropertyDTO>
                    {
                        Success = false,
                        Message = $"Något gick fel: {response.ReasonPhrase}"
                    };

                    return failResult;
                }


                var result = new ServiceResponse<PropertyDTO>
                {
                    Data = await response.Content.ReadFromJsonAsync<PropertyDTO>(),
                    Success = true,
                    Message = response.ReasonPhrase ?? ""
                };

                if (result.Data != null)
                {
                    var muncipality = await Http.GetFromJsonAsync<MuncipalityDTO>($"/api/muncipality/{result.Data.MuncipalityId}");
                    result.Data.Muncipality = muncipality.Name;
                }

                return result;
            }
            catch (HttpRequestException ex)
            {
                return new ServiceResponse<PropertyDTO>
                {
                    Success = false,
                    Message = $"Något gick fel: {ex.Message}",
                };
            }
        }

        public async Task<ServiceResponse<List<PropertyDTO>>> GetListAsync(string uri)
        {
            try
            {
                var response = await Http.GetAsync($"api/properties/{uri}");
                var muncipalities = await Http.GetFromJsonAsync<List<MuncipalityDTO>>("api/muncipality");

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
                var response = await Http.DeleteAsync($"/api/properties/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    return new ServiceResponse { 
                        Success = false, 
                        Message = $"Något gick fel vid borttagning av fastigheten: {response.ReasonPhrase}",
                    };
                }

                return new ServiceResponse { Success = true };

            }
            catch (Exception ex)
            {
                return new ServiceResponse { Success = false, Message = ex.Message };
            }
        }

        public async Task<ServiceResponse> CreateAsync(PropertyDTO property)
        {
            try
            {
                var response = await Http.PostAsJsonAsync("api/properties", property);
                if (!response.IsSuccessStatusCode)
                {
                    return new ServiceResponse { Success = false, Message = $"Misslyckades att lägga till bostaden: {response.ReasonPhrase}" };
                }

                return new ServiceResponse { Success = true, Message = "Bostad tillagd" };
            }
            catch (Exception ex)
            {
                return new ServiceResponse { Success = false, Message = ex.Message };
            }

        }

        public async Task<ServiceResponse> UpdateAsync(PropertyDTO property)
        {
            try
            {
                var response = await Http.PutAsJsonAsync($"api/properties/{property.Id}", property);
                if (!response.IsSuccessStatusCode)
                {
                    return new ServiceResponse { Success = false, Message = $"Något gick fel: {response.ReasonPhrase}" };
                }

                return new ServiceResponse { Success = true, Message = "Bostaden har uppdaterats." };
            }
            catch (Exception ex)
            {
                return new ServiceResponse { Success = false, Message = ex.Message };
            }
        }
    }
}
