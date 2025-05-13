using FribergHomeClient.Data.Dto;

namespace FribergHomeClient.Services
{
    public interface IPropertyService
    {
        Task<ServiceResponse<PropertyDTO>> GetAsync(int id);
        Task<ServiceResponse<List<PropertyDTO>>> GetListAsync(string uri);
        Task<ServiceResponse> DeleteAsync(int id);
        Task<ServiceResponse> CreateAsync(PropertyDTO property);
        Task<ServiceResponse> UpdateAsync(PropertyDTO property);
    }
}
