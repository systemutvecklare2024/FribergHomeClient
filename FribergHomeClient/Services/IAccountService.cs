using FribergHomeClient.Data.Dto;

namespace FribergHomeClient.Services
{
    //Author:Emelie
    public interface IAccountService
    {
        Task<ServiceResponse<AccountDTO>> RegisterAccount(AccountDTO accountDTO);
    }
}
