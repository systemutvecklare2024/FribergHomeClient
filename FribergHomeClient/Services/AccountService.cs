using FribergHomeClient.Data.Dto;
using System.Net.Http.Json;

namespace FribergHomeClient.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _client;

        public AccountService(HttpClient client)
        {
            _client = client;
        }
        public async Task<ResponseService<AgentCreatedDTO>> RegisterAccount(AccountDTO accountDTO)
        {
            try
            {
                var result = await _client.PostAsJsonAsync<AccountDTO>("/api/Accounts/register", accountDTO);

                Console.WriteLine(result.StatusCode.ToString());

                if (result.IsSuccessStatusCode)
                {
                    var agent = await result.Content.ReadFromJsonAsync<AgentCreatedDTO>(); //Här kommer en Agent???
                    return new ResponseService<AgentCreatedDTO> { Data = agent };
                }

                return new ResponseService<AgentCreatedDTO>
                {
                    Success = false,
                    Message = $"Något gick fel vid registrering: {result.ReasonPhrase}"
                };
            }
            // Check status code, throw exceptions
            catch (Exception ex)
            {

                return new ResponseService<AgentCreatedDTO>
                {
                    Success = false,
                    Message = $"Undantag: {ex.Message}"
                };
            }
        }
    }
}
