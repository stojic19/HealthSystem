using System.Collections.Generic;
using Hospital.Model;
using Hospital.Model.Enumerations;

namespace HospitalApi.DTOs
{
    public class NewMedicalRecordDTO
    {
        
        public double Height { get; set; }
        public double Weight { get; set; }
        public BloodType BloodType { get; set; }
        public JobStatus JobStatus { get; set; }
        public IEnumerable<NewAllergyDTO> Allergies { get; set; }
        public int Id { get; set; }

        //public DoctorDTO Doctor { get; set; }

    }
}