using FribergHomeClient.Data.Dto;

namespace FribergHomeClient.Services
{
    public interface IAccountService
    {
        Task<ResponseService<AgentCreatedDTO>> RegisterAccount(AccountDTO accountDTO);
    }
}
