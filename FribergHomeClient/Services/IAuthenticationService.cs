using FribergHomeClient.Data.Dto;

namespace FribergHomeClient.Services
{
	public interface IAuthenticationService
	{
		Task<ServiceResponse> AuthenticateAsync(LoginDTO loginDTO);

        Task Logout(); 
	}
}
