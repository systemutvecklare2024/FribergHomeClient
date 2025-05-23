﻿using FribergHomeClient.Data.Dto;
using System.Net.Http.Json;
using FribergHomeClient.Validation;

// Author: Christoffer, Glate

namespace FribergHomeClient.Services
{
	public class AccountService : IAccountService
	{
		private readonly HttpClient _client;

		public AccountService(HttpClient client)
		{
			_client = client;
		}

		public async Task<ServiceResponse<AccountDTO>> RegisterAccount(AccountDTO accountDTO)
		{
			try
			{
				var result = await _client.PostAsJsonAsync<AccountDTO>("/api/Accounts/register", accountDTO);

                if (result.IsSuccessStatusCode)
                {
                    var account = await result.Content.ReadFromJsonAsync<AccountDTO>();
                    return new ServiceResponse<AccountDTO> { Data = account };
                }

                //If not successful status code:
                List<ValidationProblemDetails> problemDetails = await BengelService.GetValidationProblemsAsync(result);

                return new ServiceResponse<AccountDTO>
                {
                    Success = false,
                    Message = $"Något gick fel vid registrering: {result.ReasonPhrase}",
                    ProblemDetails = problemDetails
                };
            }
            // Check status code, throw exceptions
            catch (Exception ex)
            {
                return new ServiceResponse<AccountDTO>
                {
                    Success = false,
                    Message = $"Undantag: {ex.Message}"
                };
            }
        }
    }
}
