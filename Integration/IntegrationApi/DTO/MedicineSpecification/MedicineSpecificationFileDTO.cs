using System;

namespace IntegrationAPI.DTO.MedicineSpecification
{
    public class MedicineSpecificationFileDTO
    {
        public string FileName { get; set; }
        public Guid ApiKey { get; set; }
        public string Host { get; set; }
        public string MedicineName { get; set; }
        public DateTime Date { get; set; }
    }
}
