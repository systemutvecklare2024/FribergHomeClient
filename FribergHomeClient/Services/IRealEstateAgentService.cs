using FribergHomeClient.Data.Dto;

namespace FribergHomeClient.Services
{
    public interface IRealEstateAgentService
    {
        Task<RealEstateAgentDTO> GetById(int id);
        Task<ServiceResponse<List<RealEstateAgentDTO>>> GetAll();
    }
}
