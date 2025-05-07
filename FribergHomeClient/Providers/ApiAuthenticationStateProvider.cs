using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

// Author: Marcus
// CoAuthor: Team Charlie

namespace FribergHomeClient.Providers
{
	public class ApiAuthenticationStateProvider : AuthenticationStateProvider
	{
		private readonly ILocalStorageService localStorage;
        private readonly HttpClient client;
        private JwtSecurityTokenHandler jwtSecurityTokenHandler;

		public ApiAuthenticationStateProvider(ILocalStorageService localStorage, HttpClient client)
		{
			this.localStorage = localStorage;
            this.client = client;
            jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
		}
		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var user = new ClaimsPrincipal(new ClaimsIdentity());

			var savedToken = await localStorage.GetItemAsync<string>("accessToken");
			if (savedToken == null)
			{
				return new AuthenticationState(user);
			}

			var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
			if (tokenContent.ValidTo < DateTime.UtcNow)
			{
				await LoggedOut();
				return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
			}

			var claims = GetClaims(tokenContent);

			user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", savedToken);
			return new AuthenticationState(user);
		}

		private List<Claim> GetClaims(JwtSecurityToken token)
		{
			var claims = token.Claims.ToList();
			claims.Add(new Claim(ClaimTypes.Name, token.Subject));
			return claims;
		}

		public async Task LoggedIn()
		{
			var savedToken = await localStorage.GetItemAsync<string>("accessToken");
			var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
			var claims = GetClaims(tokenContent);
			var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", savedToken);

            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
		}

		public async Task LoggedOut()
		{
			await localStorage.RemoveItemAsync("accessToken");
            client.DefaultRequestHeaders.Clear();
            await localStorage.RemoveItemAsync("AgentId");

            var hellUserDestroyed = new ClaimsPrincipal(new ClaimsIdentity());
			var authState = Task.FromResult(new AuthenticationState(hellUserDestroyed));
			NotifyAuthenticationStateChanged(authState);
		}
	}
}
