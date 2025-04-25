using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Blazored.LocalStorage;
using FribergHomeClient.Data.Dto;
using FribergHomeClient.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Net.WebRequestMethods;

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
		public async Task<bool> AuthenticateAsync(LoginDTO loginDTO)
		{
			var json = JsonConvert.SerializeObject(loginDTO);

			var response = await client.PostAsJsonAsync<LoginDTO>("/api/accounts/login", loginDTO);

			if(!response.IsSuccessStatusCode)
			{
				return false;
			}

			var content = await response.Content.ReadFromJsonAsync<AuthResponse>();
			var token = content.Token;
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

			await localStorage.SetItemAsync("accessToken", token);

			await ((ApiAuthenticationStateProvider)authProvider).LoggedIn();

			return true;
		}

		public async Task Logout()
		{
			client.DefaultRequestHeaders.Clear();
			await ((ApiAuthenticationStateProvider)authProvider).LoggedOut();
		}
	}
}
