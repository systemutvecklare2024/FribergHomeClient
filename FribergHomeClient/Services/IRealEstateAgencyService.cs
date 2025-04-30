using FribergHomeClient.Data.Dto;
using FribergHomeClient.Data.ViewModel;

namespace FribergHomeClient.Services
{
    public interface IRealEstateAgencyService
    {
        Task<List<RealEstateAgencyDTO>> GetAll();
        Task<RealEstateAgencyDTO> GetById(int id);
        Task<List<ApplicationViewModel>> GetApplicationViewModels(RealEstateAgencyDTO agencyDTO);
    }
}
