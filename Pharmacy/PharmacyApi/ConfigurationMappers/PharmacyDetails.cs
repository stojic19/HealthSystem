using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApi.ConfigurationMappers
{
    public class PharmacyDetails
    {
        public string Name { get; set; }
        public string CityName { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string CountryName { get; set; }
        public string PostalCode { get; set; }
    }
}
