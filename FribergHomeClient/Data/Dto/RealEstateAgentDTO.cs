namespace FribergHomeClient.Data.Dto
{
    public class RealEstateAgentDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }

        // Navigation
        public RealEstateAgencyDTO? Agency { get; set; }
        public List<PropertyDTO>? Properties { get; set; }
    }
}
