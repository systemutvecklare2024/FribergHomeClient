using FribergHomeClient.Data.Dto;

namespace FribergHomeClient.Services
{
    public interface IRealEstateAgentService
    {
        Task<RealEstateAgentDTO> GetById(int id);
        Task<List<RealEstateAgentDTO>> GetAll();
    }
}
