using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.SharedModel.Model;

namespace HospitalApi.DTOs
{
    public class SpecializedDoctorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
    }
}
