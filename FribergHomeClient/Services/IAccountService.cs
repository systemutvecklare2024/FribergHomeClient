using FribergHomeClient.Data.Dto;

namespace FribergHomeClient.Services
{
    public interface IAccountService
    {
        Task<ResponseService<AccountDTO>> RegisterAccount(AccountDTO accountDTO);
    }
}
