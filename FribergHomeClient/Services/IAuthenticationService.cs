using FribergHomeClient.DTOs;

namespace FribergHomeClient.Services
{
	public interface IAuthenticationService
	{
		Task<bool> AuthenticateAsync(LoginDTO loginDTO);
		Task Logout(); 
	}
}
