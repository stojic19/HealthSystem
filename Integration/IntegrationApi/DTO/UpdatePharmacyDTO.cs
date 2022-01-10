using System;
using Microsoft.AspNetCore.Http;

namespace IntegrationAPI.DTO
{
    public class UpdatePharmacyDTO
    {
        public int Id { get; set; }
        public string PharmacyName { get; set; }
        public string CityName { get; set; }
        public int PostalCode { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string CountryName { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
    }
}