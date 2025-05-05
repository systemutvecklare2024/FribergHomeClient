using FribergHomeClient.Data.Dto;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
using System.Text.Json;
using Newtonsoft.Json;

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

				//If not successful status code:
				var errorContent = await result.Content.ReadAsStringAsync();

				Dictionary<string, List<string>> errorDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(errorContent);

				List<ValidationProblemDetails> problemDetails = new List<ValidationProblemDetails>();

				foreach (var p in errorDictionary)
				{
					ValidationProblemDetails validationProblemDetails = new ValidationProblemDetails()
					{
						Key = p.Key,
						Value = p.Value[0]
					};
					problemDetails.Add(validationProblemDetails);
				}

				return new ResponseService<AccountDTO>
				{
					Success = false,
					Message = $"Något gick fel vid registrering: {result.ReasonPhrase}",
					ProblemDetails = problemDetails
				};

				//var response = new ResponseService<AccountDTO>();
				//response.Success = false;
				//response.Message = $"Något gick fel vid registrering: {result.ReasonPhrase}";
				//response.ProblemDetails = problemDetails;
				//return response;

				//return new ResponseService<AccountDTO>
				//{
				//	Success = false,
				//	Message = $"Något gick fel vid registrering: {result.Content.ReadAsStringAsync()}",
				//	ValidationProblemDetails = problemDetails
				//};
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
