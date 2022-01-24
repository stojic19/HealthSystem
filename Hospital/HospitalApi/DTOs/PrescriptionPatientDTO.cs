using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class PrescriptionPatientDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PhoneNumber { get; set; }
    }
}
