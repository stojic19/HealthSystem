using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO
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
    }
}
