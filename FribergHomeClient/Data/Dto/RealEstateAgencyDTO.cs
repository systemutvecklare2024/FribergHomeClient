using static System.Net.Mime.MediaTypeNames;

namespace FribergHomeClient.Data.Dto
{
    public class RealEstateAgencyDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Presentation { get; set; }
        public string LogoUrl { get; set; }
        public List<RealEstateAgentDTO>? Agents { get; set; }
        public List<ApplicationDTO>? Applications { get; set; }
    }
}
