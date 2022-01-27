using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class PrescriptionDoctorDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string Specialization { get; set; }
        public string PhoneNumber { get; set; }
    }
}
