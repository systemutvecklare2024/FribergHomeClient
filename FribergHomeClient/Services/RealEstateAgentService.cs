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

        public async Task<List<RealEstateAgentDTO>> GetAll()
        {
            try
            {
                return await client.GetFromJsonAsync<List<RealEstateAgentDTO>>("api/RealEstateAgents");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<RealEstateAgentDTO> GetById(int id)
        {
			try
			{
                return await client.GetFromJsonAsync<RealEstateAgentDTO>($"api/RealEstateAgents/{id}");
                //Handle response
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
