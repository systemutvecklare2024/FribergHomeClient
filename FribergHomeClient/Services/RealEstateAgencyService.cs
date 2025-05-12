using AutoMapper;
using FribergHomeClient.Data.Dto;
using FribergHomeClient.Data.ViewModel;
using FribergHomeClient.Helpers;
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
        public async Task<ServiceResponse<List<RealEstateAgencyDTO>>> GetAll()
        {
			//try
			//{
   //             var response = await client.GetAsync($"/api/RealEstateAgencies");
   //             if (!response.IsSuccessStatusCode)
   //             {
   //                 var result = new ServiceResponse<List<RealEstateAgencyDTO>>
   //                 {
   //                     Success = false,
   //                     Message = $"Något gick fel: {response.ReasonPhrase}"
   //                 };
   //                 return result;
   //             }
   //             return new ServiceResponse<List<RealEstateAgencyDTO>>
   //             {
   //                 Success = true,
   //                 Data = await response.Content.ReadFromJsonAsync<List<RealEstateAgencyDTO>>(),
   //                 Message = response.ReasonPhrase ?? ""
   //             };

   //         }
			//catch (HttpRequestException ex)
			//{

   //             return new ServiceResponse<List<RealEstateAgencyDTO>>
   //             {
   //                 Success = false,
   //                 Message = $"Något gick fel: {ex.Message}",
   //             };
   //         }

            return await client.GetServiceResponseAsync<List<RealEstateAgencyDTO>>($"/api/RealEstateAgencies");
        }

        public async Task<ServiceResponse<RealEstateAgencyPageDTO>> GetById(int id)
        {
            //try
            //{
            //    var response = await client.GetAsync($"/api/RealEstateAgencies/{id}");
            //    if (!response.IsSuccessStatusCode)
            //    {
            //        var result = new ServiceResponse<RealEstateAgencyPageDTO>
            //        {
            //            Success = false,
            //            Message = $"Något gick fel: {response.ReasonPhrase}"
            //        };
            //        return result;
            //    }

            //    return new ServiceResponse<RealEstateAgencyPageDTO>
            //    {
            //        Success = true,
            //        Data = await response.Content.ReadFromJsonAsync<RealEstateAgencyPageDTO>(),
            //        Message = response.ReasonPhrase ?? ""
            //    };

            //}
            //catch (HttpRequestException ex)
            //{
            //    return new ServiceResponse<RealEstateAgencyPageDTO>
            //    {
            //        Success = false,
            //        Message = $"Något gick fel: {ex.Message}",
            //    };
            //}

            return await client.GetServiceResponseAsync<RealEstateAgencyPageDTO>($"/api/RealEstateAgencies/{id}");
        }

        public async Task<ServiceResponse<RealEstateAgencyPageDTO>> GetMyAgency()
        {
            //try
            //{
            //    var response = await client.GetAsync($"api/RealEstateAgencies/My");
            //    if (!response.IsSuccessStatusCode)
            //    {
            //        var result = new ServiceResponse<RealEstateAgencyPageDTO>
            //        {
            //            Success = false,
            //            Message = $"Något gick fel: {response.ReasonPhrase}"
            //        };
            //        return result;
            //    }

            //    return new ServiceResponse<RealEstateAgencyPageDTO>
            //    {
            //        Success = true,
            //        Data = await response.Content.ReadFromJsonAsync<RealEstateAgencyPageDTO>(),
            //        Message = response.ReasonPhrase ?? ""
            //    };

            //}
            //catch (HttpRequestException ex)
            //{
            //    return new ServiceResponse<RealEstateAgencyPageDTO>
            //    {
            //        Success = false,
            //        Message = $"Något gick fel: {ex.Message}",
            //    };
            //}
            return await client.GetServiceResponseAsync<RealEstateAgencyPageDTO>($"api/RealEstateAgencies/My");
        }

        public async Task<ServiceResponse> HandleApplication(ApplicationViewModel applicationVM)
        {
            //try
            //{
            //    var dto = mapper.Map<ApplicationDTO>(applicationVM);
            //    var response = await client.PostAsJsonAsync<ApplicationDTO>($"/api/RealEstateAgencies/{dto.AgencyId}/applications/{dto.Id}", dto);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var result = new ServiceResponse<bool>()
            //        {
            //            Success = true,
            //            Message = response.ReasonPhrase ?? ""
            //        };
            //        return result;
            //    }

            //    return new ServiceResponse<bool>()
            //    {
            //        Success = false,
            //        Message = $"Något gick fel: {response.ReasonPhrase}" ?? ""
            //    };

            //}
            //catch (HttpRequestException ex)
            //{

            //    return new ServiceResponse<bool>()
            //    {
            //        Success = false,
            //        Message = $"Något gick fel:{ex.Message}" ?? ""
            //    };
            //}

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
