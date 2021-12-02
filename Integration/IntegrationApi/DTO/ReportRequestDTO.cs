using Integration.Shared.Model;

namespace IntegrationAPI.DTO
{
    public class ReportRequestDTO
    {
        public TimeRange TimeRange { get; set; }
        public int PharmacyId { get; set; }
    }
}
