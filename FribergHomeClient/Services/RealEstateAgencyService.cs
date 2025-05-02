using AutoMapper;
using FribergHomeClient.Data.Dto;
using FribergHomeClient.Data.ViewModel;
using System.Net.Http.Json;

namespace FribergHomeClient.Services
{
    //Author: Emelie
    public class RealEstateAgencyService : IRealEstateAgencyService
    {
        private readonly HttpClient client;
        private readonly IMapper mapper;

        public RealEstateAgencyService(HttpClient client, IMapper mapper)
		{
            this.client = client;
            this.mapper = mapper;
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

        public async Task<RealEstateAgencyPageDTO> GetById(int id)
        {
            try
            {
                return  await client.GetFromJsonAsync<RealEstateAgencyPageDTO>($"/api/RealEstateAgencies/{id}");
            }
            catch (Exception)
            {

                return new RealEstateAgencyPageDTO();
            }
        }

        public async Task HandleApplication(ApplicationViewModel applicationVM)
        {
            try
            {
                var dto = mapper.Map<ApplicationDTO>(applicationVM);
                client.PostAsJsonAsync<ApplicationDTO>($"/api/RealEstateAgencies/{dto.AgencyId}/applications/{dto.Id}", dto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<ApplicationViewModel>> GenerateApplicationViewModels(RealEstateAgencyPageDTO agencyDTO) //Should this be async
        {
            List<ApplicationViewModel> applicationViewModels = new List<ApplicationViewModel>();
            
            foreach (var applicationDTO in agencyDTO.Applications)
            {
                var agent = agencyDTO.Agents.FirstOrDefault(a => a.Id == applicationDTO.AgentId);
                //var agent1 = GetById(applicationDTO.AgencyId);

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
