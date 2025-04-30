using AutoMapper;
using FribergHomeClient.Data.Dto;
using FribergHomeClient.Data.ViewModel;
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

        public async Task<RealEstateAgencyDTO> GetById(int id)
        {
            try
            {
                return  await client.GetFromJsonAsync<RealEstateAgencyDTO>($"/api/RealEstateAgencies/{id}");
            }
            catch (Exception)
            {

                return new RealEstateAgencyDTO();
            }
        }

        public async Task<List<ApplicationViewModel>> GetApplicationViewModels(RealEstateAgencyDTO agencyDTO)
        {
            List<ApplicationViewModel> applicationViewModels = new List<ApplicationViewModel>();
            
            foreach (var applicationDTO in agencyDTO.Applications)
            {
                var agent = agencyDTO.Agents.FirstOrDefault(a => a.Id == applicationDTO.AgentId);

                var applicationViewModel = new ApplicationViewModel()
                {
                    Id = applicationDTO.Id,
                    AgentId = applicationDTO.AgentId,
                    FirstName = agent.FirstName,
                    LastName = agent.LastName,
                    CreatedAt = applicationDTO.CreatedAt,
                    StatusType = applicationDTO.StatusType,
                    AgencyId = applicationDTO.AgencyId

                };
                applicationViewModels.Add(applicationViewModel);
            }
            return applicationViewModels;
        }
    }
}
