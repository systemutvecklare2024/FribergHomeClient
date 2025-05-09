using FribergHomeClient.Validation;
using System.ComponentModel.DataAnnotations;

namespace FribergHomeClient.Data.Dto
{
    public class AccountDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Lösenord krävs")]
        [DataType(DataType.Password)]
        [StrongPassword(MinimumLength = 8)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Upprepa lösenord")]
        [Compare(nameof(Password), ErrorMessage = "Lösenord matchar ej")]
        [DataType(DataType.Password)]
        [Display(Name = "Upprepa Lösenord")]
        public string ConfirmPassword { get; set; }

        [Required]
        public int AgencyId { get; set; }
    }
}
