using AutoMapper;
using FribergHomeClient.Data.Dto;
using FribergHomeClient.Data.ViewModel;
using FribergHomeClient.Helpers;
using Radzen;

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

        public async Task<ServiceResponse<List<RealEstateAgencyDTO>>> GetAll()
        {
            return await client.GetServiceResponseAsync<List<RealEstateAgencyDTO>>($"/api/RealEstateAgencies");
        }

        public async Task<ServiceResponse<RealEstateAgencyPageDTO>> GetById(int id)
        {
            return await client.GetServiceResponseAsync<RealEstateAgencyPageDTO>($"/api/RealEstateAgencies/{id}");
        }

        public async Task<ServiceResponse<RealEstateAgencyPageDTO>> GetMyAgency()
        {
            return await client.GetServiceResponseAsync<RealEstateAgencyPageDTO>($"api/RealEstateAgencies/My");
        }

        public async Task<ServiceResponse> HandleApplication(ApplicationViewModel applicationVM)
        {
            var dto = mapper.Map<ApplicationDTO>(applicationVM);
            return await client.PostServiceResponseAsync($"/api/RealEstateAgencies/{dto.AgencyId}/applications/{dto.Id}", dto);
        }

        public async Task<List<ApplicationViewModel>> GenerateApplicationViewModels(List<ApplicationDTO> applicationDTOs, List<RealEstateAgentDTO> agentDTOs)
        {
            List<ApplicationViewModel> applicationViewModels = new List<ApplicationViewModel>();

            foreach (var applicationDTO in applicationDTOs)
            {
                var agent = agentDTOs.FirstOrDefault(a => a.Id == applicationDTO.AgentId);

                if(agent == null)
                {
                    return new List<ApplicationViewModel>();
                }

                var applicationViewModel = mapper.Map<ApplicationViewModel>(applicationDTO);

                applicationViewModel.FirstName = agent.FirstName; 
                applicationViewModel.LastName = agent.LastName; 

                applicationViewModels.Add(applicationViewModel);
            }

            return applicationViewModels;
        }
    }
}
