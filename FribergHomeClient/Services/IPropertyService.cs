using FribergHomeClient.Data.Dto;

namespace FribergHomeClient.Services
{
    public interface IPropertyService
    {
        Task<PropertyDTO> GetPropertyDTO(int id);
        Task<ServiceResponse<List<PropertyDTO>>> GetListAsync(string uri);
        Task<ServiceResponse> DeleteAsync(int id);
    }
}
