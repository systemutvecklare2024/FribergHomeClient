using FribergHomeClient.Data.Dto;

namespace FribergHomeClient.Services
{
    public interface IPropertyService
    {
        Task<PropertyDTO> GetPropertyDTO(int id);
        Task<ResponseService<List<PropertyDTO>>> GetListAsync(string uri);

        Task<ResponseService> DeleteAsync(int id);
    }
}
