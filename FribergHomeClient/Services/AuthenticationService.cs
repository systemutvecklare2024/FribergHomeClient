using System.Net.Http.Json;
using Blazored.LocalStorage;
using FribergHomeClient.Data.Dto;
using FribergHomeClient.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;

// Author: Marcus
// CoAuthor: Team Charlie

namespace FribergHomeClient.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly HttpClient client;
		private readonly ILocalStorageService localStorage;
		private readonly AuthenticationStateProvider authProvider;

		public AuthenticationService(HttpClient client, ILocalStorageService localStorage, AuthenticationStateProvider authProvider)
		{
			this.client = client;
			this.localStorage = localStorage;
			this.authProvider = authProvider;
		}

		public async Task<ServiceResponse> AuthenticateAsync(LoginDTO loginDTO)
		{
			var json = JsonConvert.SerializeObject(loginDTO);

			var response = await client.PostAsJsonAsync("/api/accounts/login", loginDTO);

            var content = await response.Content.ReadFromJsonAsync<AuthResponse>();
            
			if (content == null)
            {
                return new ServiceResponse
                {
                    Success = false,
					Message = $"Något gick fel med anslutningen till servern. Försök igen."
                };
            }

            if (response.IsSuccessStatusCode)
            {
                await SetLocalStorage(content);
                await ((ApiAuthenticationStateProvider)authProvider).LoggedIn();
                return new ServiceResponse { Success = true };
            }

            return new ServiceResponse
            {
                Success = false,
                Message = $"Något gick fel vid inloggningen",
                ProblemDetails = await BengelService.GetValidationProblemsAsync(response)
            };
		}

        private async Task SetLocalStorage(AuthResponse content)
        {
            var token = content.Token;
            var agentId = content.AgentId;
            await localStorage.SetItemAsync<int>("AgentId", agentId);

            await localStorage.SetItemAsync("accessToken", token);
        }

        public async Task Logout()
		{
            await ((ApiAuthenticationStateProvider)authProvider).LoggedOut();
		}
	}
}
