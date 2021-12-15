using System;

namespace IntegrationAPI.DTO.Pharmacies
{
    public class PharmacyDTO
    {
        public Guid ApiKey { get; set; }
        public string BaseUrl { get; set; }
        public string PharmacyName { get; set; }
        public string CityName { get; set; }
        public int PostalCode { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string CountryName { get; set; }
        public bool GrpcSupported { get; set; }
    }
}
