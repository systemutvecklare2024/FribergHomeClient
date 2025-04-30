using static FribergHomeClient.Data.StatusTypes;

namespace FribergHomeClient.Data.ViewModel
{
    public class ApplicationViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public StatusType StatusType { get; set; }
        public int AgencyId { get; set; }
        public int AgentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
