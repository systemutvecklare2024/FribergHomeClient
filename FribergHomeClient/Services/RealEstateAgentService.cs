using Blazored.LocalStorage;
using FribergHomeClient.Data.Dto;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace FribergHomeClient.Services
{
    //Author: Emelie
    public class RealEstateAgentService : IRealEstateAgentService
    {
        private readonly HttpClient client;
        private readonly ILocalStorageService localStorage;

        public RealEstateAgentService(HttpClient client, ILocalStorageService localStorage)
        {
            this.client = client;
            this.localStorage = localStorage;
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
                        Success = false,
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

        public async Task<ServiceResponse<UpdateAgentDTO>> GetForEditWithMyId()
        {
            try
            {
                int? id = await GetMyAgentId();
                var response = await client.GetAsync($"api/RealEstateAgents/{id.Value}");
                if (!response.IsSuccessStatusCode)
                {
                    var result = new ServiceResponse<UpdateAgentDTO>
                    {
                        Success = false,
                        Message = $"Något gick fel: {response.ReasonPhrase}"
                    };
                    return result;
                }
                return new ServiceResponse<UpdateAgentDTO>
                {
                    Success = true,
                    Message = response.ReasonPhrase ?? "",
                    Data = await response.Content.ReadFromJsonAsync<UpdateAgentDTO>()
                };
            }
            catch (HttpRequestException ex)
            {
                return new ServiceResponse<UpdateAgentDTO>
                {
                    Success = false,
                    Message = $"Något gick fel: {ex.Message}",
                };
            }
        }

        public async Task<ServiceResponse> UpdateAgentProfile(UpdateAgentDTO agentDTO)
        {
            try
            {
                var response = await client.PutAsJsonAsync<UpdateAgentDTO>($"api/RealEstateAgents/My", agentDTO);
                if (!response.IsSuccessStatusCode)
                {
                    var result = new ServiceResponse
                    {
                        Success = false,
                        Message = $"Något gick fel: {response.ReasonPhrase}"
                    };
                    return result;
                }
                return new ServiceResponse
                {
                    Success = true,
                    Message = response.ReasonPhrase ?? "",
                };
            }
            catch (HttpRequestException ex)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Något gick fel: {ex.Message}",
                };
            }
        }

        public async Task<int?> GetMyAgentId()
        {
            return await localStorage.GetItemAsync<int>("AgentId");
        }
    }
}
