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
        public async Task<ResponseService<AccountDTO>> RegisterAccount(AccountDTO accountDTO)
        {
            try
            {
                var result = await _client.PostAsJsonAsync<AccountDTO>("/api/Accounts/register", accountDTO);

                if (result.IsSuccessStatusCode)
                {
                    var account = await result.Content.ReadFromJsonAsync<AccountDTO>();
                    return new ResponseService<AccountDTO> { Data = account };
                }

                return new ResponseService<AccountDTO>
                {
                    Success = false,
                    Message = $"Något gick fel vid registrering: {result.ReasonPhrase}"
                };
            }
            // Check status code, throw exceptions
            catch (Exception ex)
            {

                return new ResponseService<AccountDTO>
                {
                    Success = false,
                    Message = $"Undantag: {ex.Message}"
                };
            }
        }
    }
}
