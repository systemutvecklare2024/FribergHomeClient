using FribergHomeClient.Data.Dto;

namespace FribergHomeClient.Services
{
    public interface IRealEstateAgentService
    {
        Task<ServiceResponse<RealEstateAgentDTO>> GetById(int id);
        Task<ServiceResponse<List<RealEstateAgentDTO>>> GetAll();
        Task<int?> GetMyAgentId();
        Task<ServiceResponse<UpdateAgentDTO>> GetForEditWithMyId();
    }
}
