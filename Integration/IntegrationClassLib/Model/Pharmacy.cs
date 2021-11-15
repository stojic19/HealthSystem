using System;
using System.Collections.Generic;

namespace Integration.Model
{
    public class Pharmacy
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string StreetNumber { get; set; }
        public string StreetName { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public Guid ApiKey { get; set; }

        public IEnumerable<Complaint> Complaints { get; set; }

        public string BaseUrl { get; set; }

        public bool isEqual(Pharmacy pharmacy)
        {
            return Name.Equals(pharmacy.Name) && StreetName.Equals(pharmacy.StreetName) && StreetNumber.Equals(pharmacy.StreetNumber)
                && City.isEqual(pharmacy.City);
        }
    }
}
