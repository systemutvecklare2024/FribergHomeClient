using FribergHomeClient.Data.Dto;
using FribergHomeClient.Data.ViewModel;

namespace FribergHomeClient.Services
{
    //Author:Emelie
    public interface IRealEstateAgencyService
    {
        Task<List<RealEstateAgencyDTO>> GetAll();
        Task<RealEstateAgencyPageDTO> GetById(int id);
        Task<List<ApplicationViewModel>> GenerateApplicationViewModels(RealEstateAgencyPageDTO agencyDTO);
        Task HandleApplication(ApplicationViewModel applicationVM);
    }
}
