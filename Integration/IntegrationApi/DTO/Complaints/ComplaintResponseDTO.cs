using System;

namespace IntegrationAPI.DTO.Complaints
{
    public class ComplaintResponseDTO
    {
        public DateTime ComplaintCreatedDate { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ApiKey { get; set; }
    }
}
