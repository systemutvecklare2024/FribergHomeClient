using FribergHomeClient.Data.Dto;

namespace FribergHomeClient.Services
{
    public interface IAccountService
    {
        Task<ServiceResponse<AccountDTO>> RegisterAccount(AccountDTO accountDTO);
    }
}
