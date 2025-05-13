using FribergHomeClient.Data.Dto;
using FribergHomeClient.Data.ViewModel;

namespace FribergHomeClient.Services
{
    //Author:Emelie
    public interface IRealEstateAgencyService
    {
        Task<ServiceResponse<List<RealEstateAgencyDTO>>> GetAll();
        Task<ServiceResponse<RealEstateAgencyPageDTO>> GetById(int id);
        Task<List<ApplicationViewModel>> GenerateApplicationViewModels(List<ApplicationDTO> applicationDTOs, List<RealEstateAgentDTO>agentDTOs);
        Task<ServiceResponse> HandleApplication(ApplicationViewModel applicationVM);
        Task<ServiceResponse<RealEstateAgencyPageDTO>> GetMyAgency();
    }
}
