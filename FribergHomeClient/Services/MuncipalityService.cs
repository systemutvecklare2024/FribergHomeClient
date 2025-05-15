using FribergHomeClient.Data.Dto;
using FribergHomeClient.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FribergHomeClient.Services
{
    public class MuncipalityService : IMuncipalityService
    {
        private readonly HttpClient client;

        public MuncipalityService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<ServiceResponse<List<MuncipalityDTO>>> GetAll()
        {
            return await client.GetServiceResponseAsync<List<MuncipalityDTO>>("/api/Muncipality");
        }

        public async Task<ServiceResponse<MuncipalityDTO>> GetById(int id)
        {
            return await client.GetServiceResponseAsync<MuncipalityDTO>($"/api/Muncipality/{id}");
        }
    }
}
