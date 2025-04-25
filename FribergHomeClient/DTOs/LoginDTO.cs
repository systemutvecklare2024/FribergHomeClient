using System.ComponentModel.DataAnnotations;

namespace FribergHomeClient.DTOs
{
	public class LoginDTO
	{
		[Required]
        [EmailAddress(ErrorMessage = "Ogiltig e-postadress")]
        public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
