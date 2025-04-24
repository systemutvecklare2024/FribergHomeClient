using FribergHomeClient.Data.ViewModel;
using FribergHomeClient.DTOs;

namespace FribergHomeClient.Services
{
    public interface IAccountService
    {
        Task<ResponseService<AccountDTO>> RegisterAccount(AccountDTO accountDTO);
    }
}
