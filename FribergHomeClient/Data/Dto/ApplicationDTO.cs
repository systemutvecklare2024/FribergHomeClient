using static FribergHomeClient.Data.StatusTypes;

namespace FribergHomeClient.Data.Dto
{
    public class ApplicationDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public StatusType StatusType { get; set; }
        public int AgencyId { get; set; }
        public int AgentId { get; set; }
    }
}
