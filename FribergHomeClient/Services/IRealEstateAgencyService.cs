using FribergHomeClient.Data.Dto;

namespace FribergHomeClient.Services
{
    public interface IRealEstateAgencyService
    {
        Task<List<RealEstateAgencyDTO>> GetAll();

    }
}
