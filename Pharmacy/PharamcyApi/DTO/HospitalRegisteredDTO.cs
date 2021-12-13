using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmacyApi.DTO.Base;

namespace PharmacyApi.DTO
{
    public class HospitalRegisteredDTO : BaseCommunicationDTO
    {
        public string BaseUrl { get; set; }
        public string PharmacyName { get; set; }
        public string CityName { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string CountryName { get; set; }
        public string PostalCode { get; set; }
        public bool GrpcSupported { get; set; }
    }
}
