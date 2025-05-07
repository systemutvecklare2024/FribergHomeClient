using AutoMapper;
using FribergHomeClient.Data.Dto;
using FribergHomeClient.Data.ViewModel;
using Radzen;
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
                Console.WriteLine("Hamnar i try");
                return  await client.GetFromJsonAsync<RealEstateAgencyPageDTO>($"/api/RealEstateAgencies/{id}");
                //Handle response here
            }
            catch (Exception)
            {
                Console.WriteLine("Hamnar i catch");
                return new RealEstateAgencyPageDTO();
            }
        }

        public async Task<ResponseService<bool>> HandleApplication(ApplicationViewModel applicationVM)
        {
            try
            {
                var dto = mapper.Map<ApplicationDTO>(applicationVM);
                var response = await client.PostAsJsonAsync<ApplicationDTO>($"/api/RealEstateAgencies/{dto.AgencyId}/applications/{dto.Id}", dto);
                if (response.IsSuccessStatusCode)
                {
                    var result = new ResponseService<bool>()
                    {
                        Success = true,
                        Message = response.ReasonPhrase ?? ""
                    };
                    return result;
                }

                return new ResponseService<bool>()
                {
                    Success = false,
                    Message = response.ReasonPhrase ?? ""
                };

            }
            catch (HttpRequestException ex)
            {

                return new ResponseService<bool>()
                {
                    Success = false,
                    Message = $"Något gick fel:{ex.Message}" ?? ""
                };
            }
        }

        public async Task<List<ApplicationViewModel>> GenerateApplicationViewModels(List<ApplicationDTO> applicationDTOs, List<RealEstateAgentDTO> agentDTOs) //Should this be async
        {
            List<ApplicationViewModel> applicationViewModels = new List<ApplicationViewModel>();

            foreach (var applicationDTO in applicationDTOs)
            {
                var agent = agentDTOs.FirstOrDefault(a => a.Id == applicationDTO.AgentId);

                if(agent == null)
                {
                    return new List<ApplicationViewModel>(); //What to do here?
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
