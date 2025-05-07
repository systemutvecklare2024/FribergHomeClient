using FribergHomeClient.Data.Dto;
using FribergHomeClient.Data.ViewModel;

namespace FribergHomeClient.Services
{
    //Author:Emelie
    public interface IRealEstateAgencyService
    {
        Task<List<RealEstateAgencyDTO>> GetAll();
        Task<RealEstateAgencyPageDTO> GetById(int id);
        Task<List<ApplicationViewModel>> GenerateApplicationViewModels(List<ApplicationDTO> applicationDTOs, List<RealEstateAgentDTO>agentDTOs);
        Task<ResponseService<bool>> HandleApplication(ApplicationViewModel applicationVM);
    }
}
