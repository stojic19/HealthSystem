using Integration.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO
{
    public class HospitalDTO
    {
        public string Name { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string CityName { get; set; }
        public string BaseUrl { get; set; }
    }
}
