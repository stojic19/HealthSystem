using System;

namespace IntegrationAPI.DTO.Complaints
{
    public class ComplaintDTO
    {
        public Guid ApiKey { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
