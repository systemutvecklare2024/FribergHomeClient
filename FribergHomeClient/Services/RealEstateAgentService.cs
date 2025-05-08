using FribergHomeClient.Data.Dto;
using System.Net.Http.Json;

namespace FribergHomeClient.Services
{
    //Author: Emelie
    public class RealEstateAgentService : IRealEstateAgentService
    {
        private readonly HttpClient client;

        public RealEstateAgentService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<ServiceResponse<List<RealEstateAgentDTO>>> GetAll()
        {
            try
            {
                var response = await client.GetAsync("api/RealEstateAgents");
                if (!response.IsSuccessStatusCode)
                {
                    var result = new ServiceResponse<List<RealEstateAgentDTO>>
                    {
                        Success = false,
                        Message = $"Något gick fel: {response.ReasonPhrase}"
                    };

                    return result;
                }

                return new ServiceResponse<List<RealEstateAgentDTO>>
                {
                    Data = await response.Content.ReadFromJsonAsync<List<RealEstateAgentDTO>>(),
                    Success = true,
                    Message = response.ReasonPhrase ?? ""
                };
            }
            catch (HttpRequestException ex)
            {

                return new ServiceResponse<List<RealEstateAgentDTO>>
                {
                    Success = false,
                    Message = $"Något gick fel: {ex.Message}",
                };
            }
        }
        public async Task<ServiceResponse<RealEstateAgentDTO>> GetById(int id)
        {
			try
			{
                var response = await client.GetAsync($"api/RealEstateAgents/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    var result = new ServiceResponse<RealEstateAgentDTO>
                    {
                        Success= false,
                        Message = $"Något gick fel: {response.ReasonPhrase}"
                    };
                    return result;
                }
                return new ServiceResponse<RealEstateAgentDTO>
                {
                    Success = true,
                    Message = response.ReasonPhrase ?? "",
                    Data = await response.Content.ReadFromJsonAsync<RealEstateAgentDTO>()
                };
                //Handle response
			}
			catch (HttpRequestException ex)
			{

                return new ServiceResponse<RealEstateAgentDTO>
                {
                    Success = false,
                    Message = $"Något gick fel: {ex.Message}",
                };
            }
        }
    }
}
