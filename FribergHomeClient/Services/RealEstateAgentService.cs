using Blazored.LocalStorage;
using FribergHomeClient.Data.Dto;
using FribergHomeClient.Helpers;

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
            return await client.GetServiceResponseAsync<List<RealEstateAgentDTO>>("api/RealEstateAgents");
        }
        public async Task<ServiceResponse<RealEstateAgentDTO>> GetById(int id)
        {
            return await client.GetServiceResponseAsync<RealEstateAgentDTO>($"api/RealEstateAgents/{id}");
        }

        public async Task<ServiceResponse<UpdateAgentDTO>> GetForEditWithMyId()
        {
            int? id = await GetMyAgentId();
            return await client.GetServiceResponseAsync<UpdateAgentDTO>($"api/RealEstateAgents/{id.Value}");
        }

        public async Task<ServiceResponse> UpdateAgentProfile(UpdateAgentDTO agentDTO)
        {
            return await client.PutServiceResponseAsync("api/RealEstateAgents/My", agentDTO);
        }

        public async Task<int?> GetMyAgentId()
        {
            return await localStorage.GetItemAsync<int>("AgentId");
        }
    }
}
