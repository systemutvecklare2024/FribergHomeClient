namespace FribergHomeClient.Data.Dto
{
    public class RealEstateAgencyPageDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Presentation { get; set; }
        public string LogoUrl { get; set; }
        public List<ApplicationDTO> Applications { get; set; }

        //Navigation
        public List<RealEstateAgentsFromAgencyDTO?> Agents { get; set; }
    }
}
