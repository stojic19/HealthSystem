using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Model
{
    public class Hospital
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
        public bool isEqual(Hospital hospital)
        {
            return Name.Equals(hospital.Name) && StreetName.Equals(hospital.StreetName) && StreetNumber.Equals(hospital.StreetNumber)
                   && City.isEqual(hospital.City);
        }
    }
}
