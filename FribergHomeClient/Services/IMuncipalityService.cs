
using FribergHomeClient.Data.Dto;

namespace FribergHomeClient.Services
{
    public interface IMuncipalityService
    {
        Task<ServiceResponse<MuncipalityDTO>> GetById(int id);
        Task<ServiceResponse<List<MuncipalityDTO>>> GetAll();
    }
}
