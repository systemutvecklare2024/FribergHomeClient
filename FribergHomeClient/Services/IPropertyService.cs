using FribergHomeClient.Data.Dto;
using FribergHomeClient.Data.ViewModel;

namespace FribergHomeClient.Services
{
    public interface IPropertyService
    {
        Task<PropertyFormViewModel> GetProperty(int id);
        Task SaveProperty(PropertyFormViewModel vm);
    }
}
