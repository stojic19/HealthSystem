using System;
using System.Collections.Generic;
using Hospital.SharedModel.Model.Enumerations;

namespace HospitalApi.DTOs
{
    public class NewMedicalRecordDTO
    {
        public int DoctorId { get; set; }
        //public DoctorDTO Doctor { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public BloodType BloodType { get; set; }
        public JobStatus JobStatus { get; set; }
        public IEnumerable<NewAllergyDTO> Allergies { get; set; }
  
    }
}