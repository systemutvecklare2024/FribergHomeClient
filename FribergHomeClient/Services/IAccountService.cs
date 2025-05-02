using FribergHomeClient.Data.Dto;

namespace FribergHomeClient.Services
{
    //Author:Emelie
    public interface IAccountService
    {
        Task<ResponseService<AgentCreatedDTO>> RegisterAccount(AccountDTO accountDTO);
    }
}
