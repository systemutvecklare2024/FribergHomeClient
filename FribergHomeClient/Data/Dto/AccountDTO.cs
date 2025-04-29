namespace FribergHomeClient.Data.Dto
{
    public class AccountDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int? AgencyId { get; set; }
    }
}
