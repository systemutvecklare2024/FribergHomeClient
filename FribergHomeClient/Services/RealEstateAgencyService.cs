using AutoMapper;
using FribergHomeClient.Data.Dto;
using System.Net.Http.Json;

namespace FribergHomeClient.Services
{
    public class RealEstateAgencyService : IRealEstateAgencyService
    {
        private readonly HttpClient client;

        public RealEstateAgencyService(HttpClient client)
		{
            this.client = client;
        }
        public async Task<List<RealEstateAgencyDTO>> GetAll()
        {
			try
			{
                var dtos = await client.GetFromJsonAsync<List<RealEstateAgencyDTO>>($"/api/RealEstateAgencies");
                return dtos;

            }
			catch (Exception)
			{

				return new List<RealEstateAgencyDTO>();
			}
        }
    }
}
