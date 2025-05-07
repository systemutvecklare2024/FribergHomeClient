using FribergHomeClient.Data.Dto;

namespace FribergHomeClient.Services
{
    //Author:Emelie
    public interface IAccountService
    {
        Task<ResponseService<AccountDTO>> RegisterAccount(AccountDTO accountDTO);
    }
}
