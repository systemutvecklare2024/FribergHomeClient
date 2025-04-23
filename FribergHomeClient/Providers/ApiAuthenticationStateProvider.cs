using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using FribergHomeClient.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace FribergHomeClient.Providers
{
	public class ApiAuthenticationStateProvider : AuthenticationStateProvider
	{
		private readonly ILocalStorageService localStorage;
		private JwtSecurityTokenHandler jwtSecurityTokenHandler;

		public ApiAuthenticationStateProvider(ILocalStorageService localStorage)
		{
			this.localStorage = localStorage;
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
			if (tokenContent.ValidTo < DateTime.Now)
			{
				return new AuthenticationState(user);
			}

			var claims = GetClaims(tokenContent);

			user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
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

			var authState = Task.FromResult(new AuthenticationState(user));
			NotifyAuthenticationStateChanged(authState);
		}

		public async Task LoggedOut()
		{
			await localStorage.RemoveItemAsync("accessToken");


			var hellUserDestroyed = new ClaimsPrincipal(new ClaimsIdentity());
			var authState = Task.FromResult(new AuthenticationState(hellUserDestroyed));
			NotifyAuthenticationStateChanged(authState);
		}
	}
}
